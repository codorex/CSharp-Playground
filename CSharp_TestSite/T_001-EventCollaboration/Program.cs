using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using T_001_EventCollaboration.Agents;
using T_001_EventCollaboration.Commands.Implementations;
using T_001_EventCollaboration.Events;
using T_001_EventCollaboration.Processors;

namespace T_001_EventCollaboration
{
    public class Program
    {
        static void Main(string[] args)
        {
            Issuer issuer = new Issuer();
            issuer.CommandIssued += issuer_onCommandIssued;

            Task.WaitAll(new Task[]
            {
                issuer.Issue(new Command { DateIssued = DateTime.Now, Name = "First command" }),
                issuer.Issue(new Command { DateIssued = DateTime.Now, Name = "Second command" }),
                issuer.Issue(new Command { DateIssued = DateTime.Now, Name = "Third command" })
            });

            Console.ReadKey();
        }

        public static void issuer_onCommandIssued(object sender, CommandIssuedEventArgs eventArgs)
        {
            Console.WriteLine(eventArgs.Issuer.GetType());
            Console.WriteLine($"--issued command \"{eventArgs.Command.Name}\"; \n--issued by {sender}");
            Console.WriteLine();
        }
    }
}
