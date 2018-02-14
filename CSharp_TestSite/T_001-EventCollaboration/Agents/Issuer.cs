using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using T_001_EventCollaboration.Commands.Implementations;
using T_001_EventCollaboration.Events;
using T_001_EventCollaboration.Processors;

namespace T_001_EventCollaboration.Agents
{
    public class Issuer
    {
        private Command _command;

        public Command Command
        {
            get => _command;
            set => _command = value;
        }

        public event CommandIssuedEventHandler CommandIssued;

        public async Task Issue(Command command)
        {
            var _processor = new Processor();

            var eventArgs = new CommandIssuedEventArgs { Command = command, DateIssued = DateTime.Now, Issuer = this };
            OnCommandIssued(eventArgs);

            Console.WriteLine(_processor);
            Processor.CommandReceived += null;
            Processor.CommandReceived += _processor_OnCommandReceived;

            await _processor.Process(command);
        }

        protected virtual void OnCommandIssued(CommandIssuedEventArgs eventArgs)
        {
            if (CommandIssued != null)
            {
                CommandIssued(this, eventArgs);
            }
        }

        private static void _processor_OnCommandReceived(object sender, CommandReceivedEventArgs eventArgs)
        {
            Console.WriteLine($"--Received command {eventArgs.Command.Name} on {eventArgs.DateReceived}, issued on {eventArgs.Command.DateIssued}");
            Console.WriteLine($"Command state: \n--Processed: {eventArgs.Command.State.IsProcessed} \n--Rejected: {eventArgs.Command.State.IsRejected}");
        }
    }
}
