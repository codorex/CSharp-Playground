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
            this.State.StateChangedEventHandler += command_OnCommandStateChanged;
        }

        public void command_OnCommandStateChanged(object sender, CommandStateChangedEventArgs eventArgs)
        {
            Console.WriteLine();
            Console.WriteLine($"\"{this.Name}\" state changed!\n--Processed: {eventArgs.PreviousState.IsProcessed} -> {eventArgs.CurrentState.IsProcessed} \n--Rejected: {eventArgs.PreviousState.IsRejected} -> {eventArgs.CurrentState.IsRejected}");
        }
    }

    public class CommandState
    {
        public CommandStateChangedEventHandler StateChangedEventHandler;

        public bool IsProcessed { get; private set; }
        public bool IsRejected { get; private set; }

        public void MarkProcessed()
        {
            OnCommandStateChanged(this, new CommandStateChangedEventArgs
            {
                PreviousState = this,
                CurrentState = new CommandState { IsProcessed = true, IsRejected = this.IsRejected }
            });

            this.IsProcessed = true;
        }

        public void MarkRejected()
        {
            OnCommandStateChanged(this, new CommandStateChangedEventArgs
            {
                PreviousState = this,
                CurrentState = new CommandState { IsProcessed = this.IsProcessed, IsRejected = true }
            });

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
                if (StateChangedEventHandler == null || !StateChangedEventHandler.GetInvocationList().Contains(value))
                {
                    StateChangedEventHandler += value;
                }
            }
            remove
            {
                StateChangedEventHandler -= value;
            }
        }

        protected virtual void OnCommandStateChanged(object sender, CommandStateChangedEventArgs eventArgs)
        {
            StateChangedEventHandler?.Invoke(sender, eventArgs);
        }
    }
}
