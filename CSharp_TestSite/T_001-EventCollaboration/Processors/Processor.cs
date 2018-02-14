using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using T_001_EventCollaboration.Commands.Implementations;
using T_001_EventCollaboration.Events;

namespace T_001_EventCollaboration.Processors
{
    public delegate void CommandReceivedEventHandler(object sender, CommandReceivedEventArgs eventArgs);
    public delegate void CommandProcessedEventHandler(object sender, CommandProcessedEventArgs eventArgs);

    public class Processor
    {
        private static CommandReceivedEventHandler commandReceivedEventHandler;

        public static event CommandProcessedEventHandler CommandProcessed;
        public static event CommandReceivedEventHandler CommandReceived
        {
            add
            {
                if (commandReceivedEventHandler == null || !commandReceivedEventHandler.GetInvocationList().Contains(value))
                {
                    commandReceivedEventHandler += value;
                }
            }
            remove
            {
                commandReceivedEventHandler -= value;
            }
        }

        public async Task Process(Command command)
        {
            OnCommandReceived(this, new CommandReceivedEventArgs { Command = command, DateReceived = DateTime.Now });

            if (Processor.IsValidCommand(command) == false)
            {
                command_onCommandFailed(this, new CommandProcessedEventArgs { Command = command, DateProcessed = DateTime.Now });
                throw new ArgumentOutOfRangeException("Issued command is not valid.");
            }

            await Task.Run(() =>
            {
                command = SimulatePotentialFailure(command);
            });

            OnCommandProcessed(this, new CommandProcessedEventArgs { Command = command });
        }

        public static bool IsValidCommand(Command command)
        {
            if (ReferenceEquals(command, null) || command.DateIssued == DateTime.MinValue || String.IsNullOrEmpty(command.Name))
            {
                return false;
            }

            return true;
        }

        private static Command SimulatePotentialFailure(Command command)
        {
            Random rand = new Random();
            int modifier = rand.Next(0, 100);

            //simulate doing processing work with roughly 70% chance of failure
            Thread.Sleep(2000);

            if (modifier < 70)
            {
                command.State.MarkProcessed();
            }

            if (command.State.IsProcessed)
            {
                CommandProcessed = command_onCommandProcessed;
            }
            else
            {
                CommandProcessed = command_onCommandFailed;
            }

            return command;
        }

        protected virtual void OnCommandReceived(object sender, CommandReceivedEventArgs eventArgs)
        {
            commandReceivedEventHandler?.Invoke(sender, eventArgs);
        }

        protected virtual void OnCommandProcessed(object sender, CommandProcessedEventArgs eventArgs)
        {
            CommandProcessed?.Invoke(sender, eventArgs);
        }

        private static void command_onCommandProcessed(object sender, CommandProcessedEventArgs eventArgs)
        {
            Console.WriteLine();
            Console.WriteLine($"+++ Successfully processed command: {eventArgs.Command.Name} on {eventArgs.DateProcessed} ms: {eventArgs.DateProcessed.Millisecond}");
        }

        private static void command_onCommandFailed(object sender, CommandProcessedEventArgs eventArgs)
        {
            Console.WriteLine();
            Console.WriteLine($"!!! Failed to process command: {eventArgs.Command.Name}, finished processing attempt on {eventArgs.DateProcessed} ms: {eventArgs.DateProcessed.Millisecond}");
        }
    }
}
