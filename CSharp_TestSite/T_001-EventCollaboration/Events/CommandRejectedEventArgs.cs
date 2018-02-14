using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_001_EventCollaboration.Commands.Implementations;

namespace T_001_EventCollaboration.Events
{
    public class CommandRejectedEventArgs : EventArgs
    {
        public Command Command { get; set; }
        public DateTime DateRejected { get; set; }
        public string Reason { get; set; }
    }
}
