using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_001_EventCollaboration.Commands.Implementations;

namespace T_001_EventCollaboration.Events
{
    public class CommandStateChangedEventArgs : EventArgs
    {
        public string CommandName { get; set; }
        public CommandState PreviousState { get; set; }
        public CommandState CurrentState { get; set; }
    }
}
