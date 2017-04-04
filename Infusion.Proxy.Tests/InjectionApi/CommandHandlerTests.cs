﻿using System;
using System.Linq;
using System.Text;
using System.Threading;
using FluentAssertions;
using Infusion.Proxy.LegacyApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infusion.Proxy.Tests.LegacyApi
{
    [TestClass]
    public class CommandHandlerTests
    {
        private CommandHandler commandHandler;

        [TestInitialize]
        public void Initialize()
        {
            commandHandler = new CommandHandler();
        }

        [TestMethod]
        public void Can_create_two_scripts_with_different_names()
        {
            commandHandler.RegisterCommand("testName1", () => { });
            commandHandler.RegisterCommand("testName2", () => { });
        }

        [TestMethod]
        public void Can_invoke_script_parameterless_script()
        {
            var ev = new AutoResetEvent(false);
            commandHandler.RegisterCommand("testName", () => ev.Set());

            commandHandler.Invoke(",testName");
            ev.WaitOne(1000).Should().BeTrue();
        }

        [TestMethod]
        public void Can_create_parametrized_script()
        {
            commandHandler.RegisterCommand("testName", parameters => { });
        }

        [TestMethod]
        public void Can_invoke_parametrized_script()
        {
            var actualParameters = string.Empty;
            var ev = new AutoResetEvent(false);

            commandHandler.RegisterCommand("testName", parameters =>
            {
                actualParameters = parameters;
                ev.Set();
            });

            commandHandler.Invoke(",testName parameter1 parameter2");
            ev.WaitOne(1000).Should().BeTrue();
            actualParameters.Should().Be("parameter1 parameter2");
        }

        [TestMethod]
        public void Can_return_registered_script_names()
        {
            commandHandler.RegisterCommand("testName", () => { });
            commandHandler.RegisterCommand("testName2", parameters => { });

            commandHandler.CommandNames.Should().Contain("testName").And.Contain("testName2");
        }

        [TestMethod]
        public void Can_recognize_script_invocation_syntax()
        {
            commandHandler.IsInvocationSyntax(",testName").Should().BeTrue();
            commandHandler.IsInvocationSyntax("testName").Should().BeFalse();
        }

        [TestMethod]
        public void Can_unregister_parameterless_script()
        {
            commandHandler.RegisterCommand("testname1", param => { });
            commandHandler.RegisterCommand("testname2", () => { });

            commandHandler.Unregister("testname2");

            commandHandler.CommandNames.Should().BeEquivalentTo("testname1");
        }

        [TestMethod]
        public void Can_unregister_parametrized_script()
        {
            commandHandler.RegisterCommand("testname1", param => { });
            commandHandler.RegisterCommand("testname2", () => { });

            commandHandler.Unregister("testname1");

            commandHandler.CommandNames.Should().BeEquivalentTo("testname2");
        }

        [TestMethod]
        public void Can_list_running_commands()
        {
            var command1 = new TestCommand("cmd1");
            var command2 = new TestCommand("cmd2");
            commandHandler.RegisterCommand(command1.Command);
            commandHandler.RegisterCommand(command2.Command);
            commandHandler.Invoke(",cmd1");
            commandHandler.Invoke(",cmd2");

            command1.WaitForInitialization();
            command2.WaitForInitialization();

            commandHandler.RunningCommands.Select(c => c.Name).Should().Contain("cmd1", "cmd2");

            command1.Finish();
            command2.Finish();
        }

        [TestMethod]
        public void Can_remove_finished_command_from_list()
        {
            var finishedCommand = new TestCommand("finished_cmd");
            commandHandler.RegisterCommand(finishedCommand.Command);
            commandHandler.Invoke(",finished_cmd");
            finishedCommand.WaitForInitialization();
            finishedCommand.Finish();
            finishedCommand.WaitForFinished();

            var runningCommand = new TestCommand("running_cmd");
            commandHandler.RegisterCommand(runningCommand.Command);
            commandHandler.Invoke(",running_cmd");
            runningCommand.WaitForInitialization();

            commandHandler.RunningCommands.Select(c => c.Name).Should().NotContain("finished_cmd");
            commandHandler.RunningCommands.Select(c => c.Name).Should().Contain("running_cmd");
        }

        [TestMethod]
        public void Can_terminate_specific_running_command_multipletimes()
        {
            int executionCount = 0;
            EventWaitHandle counterDone = new EventWaitHandle(false, EventResetMode.ManualReset);

            var command = new TestCommand(commandHandler, "cmd", () =>
            {
                Legacy.Wait(TimeSpan.FromMilliseconds(1));
                executionCount++;
                counterDone.Set();
                Legacy.Wait(TimeSpan.FromHours(1));
            });
            commandHandler.RegisterCommand(command.Command);

            for (var i = 0; i < 10; i++)
            {
                int oldExecutionCount = executionCount;
                counterDone.Reset();
                command.Reset();
                commandHandler.Invoke(",cmd");
                counterDone.WaitOne(TimeSpan.FromMilliseconds(100));

                commandHandler.Terminate("cmd");
                command.Finish();

                command.WaitForFinished().Should().BeTrue();
                commandHandler.RunningCommands.Should().BeEmpty();
                executionCount.Should().Be(oldExecutionCount + 1);
            }

            executionCount.Should().Be(10);
        }

        [TestMethod]
        public void Can_terminate_only_specific_running_command()
        {
            var command = new TestCommand(commandHandler, "cmd", () => Legacy.Wait(TimeSpan.FromHours(1)));
            commandHandler.RegisterCommand(command.Command);
            commandHandler.Invoke(",cmd");
            command.WaitForInitialization();

            commandHandler.Terminate("non_existing_command");
            command.Finish();

            command.WaitForFinished(TimeSpan.FromMilliseconds(50)).Should().BeFalse();
        }

        [TestMethod]
        public void Can_terminate_all_running_commands_multiple_times()
        {
            var command1 = new TestCommand(commandHandler, "cmd1", () => Legacy.Wait(TimeSpan.FromHours(1)));
            commandHandler.RegisterCommand(command1.Command);

            var command2 = new TestCommand(commandHandler, "cmd2", () => Legacy.Wait(TimeSpan.FromHours(1)));
            commandHandler.RegisterCommand(command2.Command);

            for (var i = 0; i < 50; i++)
            {
                command1.Reset();
                command2.Reset();
                commandHandler.Invoke(",cmd1");
                command1.WaitForInitialization();
                commandHandler.Invoke(",cmd2");
                command2.WaitForInitialization();

                commandHandler.Terminate();

                command1.Finish();
                command1.WaitForFinished().Should().BeTrue();
                command2.Finish();
                command2.WaitForFinished().Should().BeTrue();

                commandHandler.RunningCommands.Should().BeEmpty();
            }
        }

        [TestMethod]
        public void Can_execute_nested_command_When_Normal()
        {
            var nestedCommandExecuted = false;
            var nestedCommand = new TestCommand(commandHandler, "nested", () => nestedCommandExecuted = true);
            commandHandler.RegisterCommand(nestedCommand.Command);
            var command = new TestCommand(commandHandler, "cmd1", () => commandHandler.Invoke(",nested"));
            commandHandler.RegisterCommand(command.Command);

            commandHandler.Invoke(",cmd1");
            command.WaitForInitialization();
            nestedCommand.WaitForAdditionalAction();

            nestedCommandExecuted.Should().BeTrue();
            commandHandler.RunningCommands.Select(c => c.Name).Should().Contain("cmd1");
            commandHandler.RunningCommands.Select(c => c.Name).Should().NotContain("nested");

            nestedCommand.Finish();
            command.Finish();
        }

        [TestMethod]
        public void Can_execute_nested_command_When_AlwaysParallel()
        {
            var nestedCommandExecuted = false;
            var nestedCommand = new TestCommand(commandHandler, "nested", CommandExecutionMode.AlwaysParallel, () => nestedCommandExecuted = true);
            commandHandler.RegisterCommand(nestedCommand.Command);
            var command = new TestCommand(commandHandler, "cmd1", () => commandHandler.Invoke(",nested"));
            commandHandler.RegisterCommand(command.Command);

            commandHandler.Invoke(",cmd1");
            command.WaitForInitialization();
            nestedCommand.WaitForAdditionalAction();

            nestedCommandExecuted.Should().BeTrue();
            commandHandler.RunningCommands.Select(c => c.Name).Should().Contain("cmd1");
            commandHandler.RunningCommands.Select(c => c.Name).Should().Contain("nested");

            nestedCommand.Finish();
            command.Finish();
        }

        [TestMethod]
        public void Cannot_execute_same_commands_in_parallel()
        {
            int executionCount = 0;

            var command = new TestCommand(commandHandler, "cmd1", () => executionCount++);
            commandHandler.RegisterCommand(command.Command);
            commandHandler.Invoke(",cmd1");
            commandHandler.Invoke(",cmd1");

            command.Finish();
            command.WaitForFinished();

            executionCount.Should().Be(1);
        }

        [TestMethod]
        public void Can_execute_Direct_command_on_caller_thread_without_listing_in_RunningCommands()
        {
            int commandThread = -1;
            var command = new TestCommand(commandHandler, "cmd1", CommandExecutionMode.Direct, () => commandThread = Thread.CurrentThread.ManagedThreadId);
            commandHandler.RegisterCommand(command.Command);
            command.Finish();
            commandHandler.Invoke(",cmd1");
            command.WaitForAdditionalAction();

            commandHandler.RunningCommands.Length.Should().Be(0);

            commandThread.Should().Be(Thread.CurrentThread.ManagedThreadId);
        }

        private sealed class TestCommand
        {
            private readonly Action additionalAction;

            private readonly EventWaitHandle additionalActionFinished = new EventWaitHandle(false,
                EventResetMode.ManualReset);

            private readonly EventWaitHandle finishedEvent = new EventWaitHandle(false, EventResetMode.ManualReset);
            private readonly EventWaitHandle finishEvent = new EventWaitHandle(false, EventResetMode.ManualReset);

            private readonly EventWaitHandle initializeEvent = new EventWaitHandle(false, EventResetMode.ManualReset);
            private readonly StringBuilder trace = new StringBuilder(1024);

            public TestCommand(CommandHandler handler, string name, Action additionalAction)
                : this(handler, name, CommandExecutionMode.Normal, additionalAction)
            {
            }

            public TestCommand(CommandHandler handler, string name, CommandExecutionMode executionMode,
                Action additionalAction)
            {
                Command = new Command(name, CommandAction, executionMode);

                this.additionalAction = additionalAction;

                if (handler != null)
                    handler.CommandStopped += HandlerOnCommandStopped;
                else
                    Command.Stopped += CommandOnStopped;
            }

            public TestCommand(string name) : this(null, name, CommandExecutionMode.Normal, () => { })
            {
            }

            public Command Command { get; }

            private void CommandOnStopped(object sender, EventArgs eventArgs)
            {
                trace.AppendLine("CommandOnStopped: OnEntry");
                finishedEvent.Set();
                trace.AppendLine("CommandOnStopped: OnExit");
            }

            private void HandlerOnCommandStopped(object sender, Command command)
            {
                if (command.Name.Equals(Command.Name, StringComparison.Ordinal))
                {
                    trace.AppendLine("HandlerOnCommandStopped: OnEntry");
                    finishedEvent.Set();
                    trace.AppendLine("HandlerOnCommandStopped: OnExit");
                }
            }

            public void Finish()
            {
                trace.AppendLine("Finish: OnStart");
                finishEvent.Set();
                trace.AppendLine("Finish: OnExit");
            }

            public void WaitForInitialization()
            {
                trace.AppendLine("WaitForInitialiation: OnEntry");
                initializeEvent.WaitOne(TimeSpan.FromSeconds(10));
                trace.AppendLine("WaitForInitialiation: OnExit");
            }

            public bool WaitForFinished() => WaitForFinished(TimeSpan.FromSeconds(1));

            public bool WaitForFinished(TimeSpan timeout)
            {
                trace.AppendLine("WaitForFinished: OnEntry");
                var result = finishedEvent.WaitOne(timeout);
                trace.AppendLine("WaitForFinished: OnExit");

                return result;
            }

            private void CommandAction()
            {
                trace.AppendLine("CommandAction: OnEntry");

                initializeEvent.Set();

                additionalAction?.Invoke();
                additionalActionFinished.Set();

                finishEvent.WaitOne(TimeSpan.FromSeconds(10));

                trace.AppendLine("CommandAction: OnExit");
            }

            public void Reset()
            {
                trace.Clear();
                initializeEvent.Reset();
                finishEvent.Reset();
                finishedEvent.Reset();
                additionalActionFinished.Reset();
            }

            public override string ToString() => trace.ToString();

            public void WaitForAdditionalAction()
            {
                additionalActionFinished.WaitOne(TimeSpan.FromSeconds(10));
            }
        }
    }
}