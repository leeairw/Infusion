﻿using Infusion.Desktop.Launcher.ClassicUO;
using Infusion.Desktop.Launcher.CrossUO;
using Infusion.Desktop.Launcher.Generic;
using Infusion.Desktop.Launcher.Official;
using Infusion.Desktop.Launcher.Orion;
using Infusion.Desktop.Profiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Infusion.Desktop.Launcher
{
    internal sealed class LauncherViewModel : INotifyPropertyChanged
    {
        private LaunchProfile selectedProfile;
        private ObservableCollection<LaunchProfile> profiles = new ObservableCollection<LaunchProfile>
        {
            new LaunchProfile {Name = "new profile"}
        };
        private readonly Action<string> passwordSetter;
        private readonly Dictionary<string, ClassicUOViewModel> classicUOViewModels
            = new Dictionary<string, ClassicUOViewModel>();
        private readonly Dictionary<string, OfficialViewModel> officialViewModels
            = new Dictionary<string, OfficialViewModel>();
        private readonly Dictionary<string, CrossViewModel> crossViewModels
            = new Dictionary<string, CrossViewModel>();
        private readonly Dictionary<string, OrionViewModel> orionViewModels
            = new Dictionary<string, OrionViewModel>();
        private readonly Dictionary<string, GenericViewModel> genericViewModels
            = new Dictionary<string, GenericViewModel>();

        public ClassicUOViewModel SelectedClassicUOViewModel
            => GetViewModel(classicUOViewModels, () => new ClassicUOViewModel(SelectedProfile.LauncherOptions.ClassicUO));
        public OfficialViewModel SelectedOfficialViewModel
            => GetViewModel(officialViewModels, () => new OfficialViewModel(SelectedProfile.LauncherOptions.Official));
        public CrossViewModel SelectedCrossViewModel
            => GetViewModel(crossViewModels, () => new CrossViewModel(SelectedProfile.LauncherOptions.Cross));
        public OrionViewModel SelectedOrionViewModel
            => GetViewModel(orionViewModels, () => new OrionViewModel(SelectedProfile.LauncherOptions.Orion));
        public GenericViewModel SelectedGenericViewModel
            => GetViewModel(genericViewModels, () => new GenericViewModel(SelectedProfile.LauncherOptions.Generic));

        public bool OfficialClientOptionsVisible => SelectedProfile.LauncherOptions.ClientType == UltimaClientType.Classic;
        public bool OrionOptionsVisible => SelectedProfile.LauncherOptions.ClientType == UltimaClientType.Orion;
        public bool CrossOptionsVisible => SelectedProfile.LauncherOptions.ClientType == UltimaClientType.CrossUO;
        public bool ClassicUOOptionsVisible => SelectedProfile.LauncherOptions.ClientType == UltimaClientType.ClassicUO;
        public bool GenericOptionsVisible => SelectedProfile.LauncherOptions.ClientType == UltimaClientType.Generic;

        public bool CanDeleteSelectedProfile => Profiles.Count > 1;

        private T GetViewModel<T>(Dictionary<string, T> models, Func<T> newViewModel)
        {
            if (!models.TryGetValue(SelectedProfile.Id, out var model))
            {
                var newModel = newViewModel();
                models.Add(SelectedProfile.Id, newModel);
                return newModel;
            }
            else
                return model;

        }

        private bool showPassword;
        public bool ShowPassword
        {
            get => showPassword;
            set
            {
                showPassword = value;
                OnPropertyChanged();
                OnPropertyChanged("HidePassword");
            }
        }
        public bool HidePassword => !showPassword;

        public ProtocolVersion[] ProtocolVersions { get; } = new[]
        {
            new ProtocolVersion() { Version = new Version(3, 0, 0), Label = ">= 3.0.0" },
            new ProtocolVersion() { Version = new Version(7, 0, 0, 0), Label = ">= 7.0.0.0" },
            new ProtocolVersion() { Version = new Version(7, 0, 9, 0), Label = ">= 7.0.9.0" },
            new ProtocolVersion() { Version = new Version(7, 0, 16, 0), Label = ">= 7.0.16.0" },
            new ProtocolVersion() { Version = new Version(7, 0, 18, 0), Label = ">= 7.0.18.0" },
            new ProtocolVersion() { Version = new Version(7, 0, 33, 0), Label = ">= 7.0.33.0" },
        };

        public ObservableCollection<LaunchProfile> Profiles
        {
            get => profiles;
            set
            {
                profiles = value;
                if (profiles.Any())
                {
                    SelectedProfile = profiles.First();
                }
            }
        }

        public LauncherViewModel(Action<string> passwordSetter)
        {
            this.passwordSetter = passwordSetter;
            SelectedProfile = Profiles.First();
        }

        public LaunchProfile SelectedProfile
        {
            get => selectedProfile;
            set
            {
                selectedProfile = value;
                OnPropertyChanged();
                OnSelectedClientTypeChanged();
                passwordSetter(selectedProfile.LauncherOptions.Password);
            }
        }

        public string SelectedProfileName
        {
            get => SelectedProfile.Name;
            set => SelectedProfile.Name = value;
        }

        public void NewProfile()
        {
            var profile = new LaunchProfile { Name = "new profile" };
            Profiles.Add(profile);
            SelectedProfile = profile;

            OnPropertyChanged("CanDeleteSelectedProfile");
        }

        public ProtocolVersion SelectedProtocolVersion
        {
            get => ProtocolVersions.FirstOrDefault(x => x.Version == SelectedProfile.LauncherOptions.ProtocolVersion)
                ?? ProtocolVersions.First();
            set
            {
                SelectedProfile.LauncherOptions.ProtocolVersion = value.Version;
                OnPropertyChanged();
            }
        }

        public UltimaClientType SelectedClientType
        {
            get => SelectedProfile.LauncherOptions.ClientType;
            set
            {
                SelectedProfile.LauncherOptions.ClientType = value;
                OnSelectedClientTypeChanged();
            }
        }

        private void OnSelectedClientTypeChanged()
        {
            OnPropertyChanged("SelectedClientType");
            OnPropertyChanged("SupportsCredentials");

            OnPropertyChanged("OfficialClientOptionsVisible");
            OnPropertyChanged("SelectedOfficialViewModel");

            OnPropertyChanged("OrionOptionsVisible");
            OnPropertyChanged("SelectedOrionViewModel");

            OnPropertyChanged("CrossOptionsVisible");
            OnPropertyChanged("SelectedCrossViewModel");

            OnPropertyChanged("ClassicUOOptionsVisible");
            OnPropertyChanged("SelectedClassicUOViewModel");

            OnPropertyChanged("GenericOptionsVisible");
            OnPropertyChanged("SelectedGenericViewModel");

            OnPropertyChanged("SelectedProtocolVersion");
        }

        public void DeleteSelectedProfile()
        {
            if (Profiles.Count > 1)
            {
                var profileToRemove = SelectedProfile;
                SelectedProfile = Profiles.First(x => x != profileToRemove);
                Profiles.Remove(profileToRemove);
                ProfileRepository.DeleteProfile(profileToRemove);

            }
            OnPropertyChanged("CanDeleteSelectedProfile");
        }

        public bool SupportsCredentials => SelectedClientType != UltimaClientType.Generic;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}