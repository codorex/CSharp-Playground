using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_001_EventCollaboration.Events;

namespace T_001_EventCollaboration.Commands.Implementations
{
    public delegate void CommandStateChangedEventHandler(object sender, CommandStateChangedEventArgs eventArgs);

    public class Command
    {
        public string Name { get; set; }
        public DateTime DateIssued { get; set; }
        public CommandState State { get; private set; }

        public Command()
        {
            this.State = new CommandState();
        }
    }

    public class CommandState
    {
        private CommandStateChangedEventHandler stateChangedEventHandler;

        public bool IsProcessed { get; private set; }
        public bool IsRejected { get; private set; }

        public void MarkProcessed()
        {
            this.IsProcessed = true;

            this.CommandStateChanged += command_OnCommandStateChanged;
            OnCommandStateChanged(this, new CommandStateChangedEventArgs { PreviousState = new CommandState { IsProcessed = false, IsRejected = false }, CurrentState = this });
        }

        public void MarkRejected()
        {
            this.IsRejected = true;
        }

        public void UnmarkRejected()
        {
            this.IsRejected = false;
        }

        public event CommandStateChangedEventHandler CommandStateChanged
        {
            add
            {
                if (stateChangedEventHandler == null || !stateChangedEventHandler.GetInvocationList().Contains(value))
                {
                    stateChangedEventHandler += value;
                }
            }
            remove
            {
                stateChangedEventHandler -= value;
            }
        }

        protected virtual void OnCommandStateChanged(object sender, CommandStateChangedEventArgs eventArgs)
        {
            stateChangedEventHandler?.Invoke(sender, eventArgs);
        }

        private void command_OnCommandStateChanged(object sender, CommandStateChangedEventArgs eventArgs)
        {
            Console.WriteLine($"Command state: \n--Processed: {eventArgs.CurrentState.IsProcessed} \n--Rejected: {eventArgs.CurrentState.IsRejected} \nPrevious state: \n--Processed: {eventArgs.PreviousState.IsProcessed} \n--Rejected: {eventArgs.PreviousState.IsRejected}");
        }
    }
}
