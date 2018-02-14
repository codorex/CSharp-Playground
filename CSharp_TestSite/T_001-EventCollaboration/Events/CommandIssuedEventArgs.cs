using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_001_EventCollaboration.Agents;
using T_001_EventCollaboration.Commands.Implementations;

namespace T_001_EventCollaboration.Events
{
    public delegate void CommandIssuedEventHandler(object sender, CommandIssuedEventArgs commandIssuedEventArgs);

    public class CommandIssuedEventArgs : EventArgs
    {
        public Command Command { get; set; }
        public DateTime DateIssued { get; set; }
        public Issuer Issuer { get; set; }
    }
}
