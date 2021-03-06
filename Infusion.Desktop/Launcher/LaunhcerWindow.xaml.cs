﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using Infusion.Desktop.Profiles;
using Infusion.LegacyApi.Console;
using Infusion.Proxy;
using Newtonsoft.Json;

namespace Infusion.Desktop.Launcher
{
    internal partial class LauncherWindow : Window
    {
        private readonly Action<LaunchProfile> launchCallback;
        private readonly LauncherViewModel launcherViewModel;
        private readonly IConsole console;
        private readonly InfusionProxy proxy;

        private void ShowError(string errorMessage)
        {
            console.Error(errorMessage);
        }

        internal LauncherWindow(Action<LaunchProfile> launchCallback, IConsole console, InfusionProxy proxy)
        {
            this.console = console;
            this.proxy = proxy;
            this.launchCallback = launchCallback;
            InitializeComponent();
            launcherViewModel = new LauncherViewModel(pwd => passwordBox.Password = pwd);

            var profileInstaller = new ProfilesInstaller(console, ProfileRepository.ProfilesPath);
            profileInstaller.Install();
            var profiles = ProfileRepository.LoadProfiles();
            if (profiles != null)
                launcherViewModel.Profiles = new ObservableCollection<LaunchProfile>(profiles);

            string selectedProfileId = ProfileRepository.LoadSelectedProfileId();
            if (profiles != null && !string.IsNullOrEmpty(selectedProfileId))
            {
                launcherViewModel.SelectedProfile = launcherViewModel.Profiles.FirstOrDefault(p => p.Id == selectedProfileId) ?? launcherViewModel.Profiles.FirstOrDefault();
            }
            DataContext = launcherViewModel;
        }

        private async void OnLaunchButtonClicked(object sender, RoutedEventArgs e)
        {
            (sender as System.Windows.Controls.Button).Focus();

            ProfileRepository.SaveProfiles(launcherViewModel.Profiles);
            ProfileRepository.SelectedProfile = launcherViewModel.SelectedProfile;
            ProfileRepository.SaveSelectedProfileId(launcherViewModel.SelectedProfile.Id);

            proxy.ConfigRepository = new ProfileConfigRepository(launcherViewModel.SelectedProfile, console);

            var launcherOptions = launcherViewModel.SelectedProfile.LauncherOptions;
            if (!launcherOptions.Validate(out var validationMessage))
            {
                ShowError(validationMessage);
                return;
            }

            IsEnabled = false;
            string originalTitle = Title;

            var launcher = new Launcher(console);

            try
            {
                await launcher.Launch(launcherOptions, proxy);
            }
            catch (AggregateException ex)
            {
                HandleException(ex, originalTitle);
                return;
            }
            catch (Exception ex)
            {
                HandleException(ex, originalTitle);
                return;
            }

            launchCallback(launcherViewModel.SelectedProfile);

            Close();
        }

        private void HandleException(Exception exception, string title)
        {
            IsEnabled = true;
            Title = title;

            string message;
            if (exception is AggregateException aggregateException)
            {
                message =
                    aggregateException.InnerExceptions.Select(x => x.ToString())
                        .Aggregate((l, r) => l + Environment.NewLine + r);
            }
            else
            {
                message = exception.ToString();
            }

            ShowError(message);
        }

        private void OnNewProfileButtonClick(object sender, RoutedEventArgs e)
        {
            launcherViewModel.NewProfile();
        }

        private void OnDeleteProfileButtonClick(object sender, RoutedEventArgs e)
        {
            launcherViewModel.DeleteSelectedProfile();
        }

        private void OnSelectOrionPath(object sender, RoutedEventArgs e)
        {
            launcherViewModel.SelectedProfile.LauncherOptions.Orion.ClientExePath
                = PathPickerHelper.SelectPath(launcherViewModel.SelectedProfile.LauncherOptions.Orion.ClientExePath, "Orion|OrionUO.exe|*.exe|*.exe");
        }

        private void OnSelectCrossPath(object sender, RoutedEventArgs e)
        {
            launcherViewModel.SelectedProfile.LauncherOptions.Cross.ClientExePath
                = PathPickerHelper.SelectPath(launcherViewModel.SelectedProfile.LauncherOptions.Orion.ClientExePath, "CrossUO|crossuo.exe|*.exe|*.exe");
        }

        private void OnSelectClassicPath(object sender, RoutedEventArgs e)
        {
            launcherViewModel.SelectedProfile.LauncherOptions.Official.ClientExePath
                = PathPickerHelper.SelectPath(launcherViewModel.SelectedProfile.LauncherOptions.Official.ClientExePath, "*.exe|*.exe");

        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            launcherViewModel.SelectedProfile.LauncherOptions.Password = passwordBox.Password;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            launcherViewModel.ShowPassword = true;
            launcherViewModel.SelectedProfile.LauncherOptions.Password = passwordBox.Password;
            passwordTextBox.Text = passwordBox.Password;
            passwordBox.Visibility = Visibility.Collapsed;
            passwordTextBox.Visibility = Visibility.Visible;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            launcherViewModel.ShowPassword = false;
            passwordTextBox.Text = string.Empty;
            passwordBox.Visibility = Visibility.Visible;
            passwordTextBox.Visibility = Visibility.Collapsed;
        }
    }
}