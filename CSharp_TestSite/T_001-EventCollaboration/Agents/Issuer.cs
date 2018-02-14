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

        public void Issue(Command command)
        {
            var eventArgs = new CommandIssuedEventArgs { Command = command, DateIssued = DateTime.Now, Issuer = this };
            OnCommandIssued(eventArgs);
        }

        protected virtual void OnCommandIssued(CommandIssuedEventArgs eventArgs)
        {
            if (CommandIssued != null)
            {
                CommandIssued(this, eventArgs);
            }
        }
    }
}
