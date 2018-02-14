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
        private static Processor processor;

        static void Main(string[] args)
        {
            Issuer issuer = new Issuer();
            issuer.CommandIssued += issuer_onCommandIssued;

            //Listen(issuer);

            issuer.Issue(new Command { DateIssued = DateTime.Now, Name = "First command" });
            issuer.Issue(new Command { DateIssued = DateTime.Now, Name = "Second command" });
            issuer.Issue(new Command { DateIssued = DateTime.Now, Name = "Third command" });
            issuer.Issue(new Command { DateIssued = DateTime.Now, Name = "Fourth command" });
            issuer.Issue(new Command { DateIssued = DateTime.Now, Name = "Fifth command" });
            issuer.Issue(new Command { DateIssued = DateTime.Now, Name = "Sixth command" });
            issuer.Issue(new Command { DateIssued = DateTime.Now, Name = "Seventh command" });

            Console.ReadKey();
        }

        public static void issuer_onCommandIssued(object sender, CommandIssuedEventArgs eventArgs)
        {
            processor = new Processor();
            Processor.CommandReceived += _processor_OnCommandReceived;

            Task.Run(async () => { await processor.Process(eventArgs.Command); });

            Console.WriteLine($" {eventArgs.Issuer.GetType()} --issued command \"{eventArgs.Command.Name}\"; \n--issued by {sender}");
            Console.WriteLine();
        }

        private static void _processor_OnCommandReceived(object sender, CommandReceivedEventArgs eventArgs)
        {
            Console.WriteLine(
                $@"*** Received command {eventArgs.Command.Name} on {eventArgs.DateReceived}, issued on {eventArgs.Command.DateIssued} ms: {eventArgs.Command.DateIssued.Millisecond}");
        }

        private static void Listen(Issuer issuer)
        {
            Console.WriteLine("====================================");

            Console.WriteLine("/help to list available operators");

            Console.WriteLine("====================================");

            while (true)
            {
                Console.WriteLine("Listening for commands: ");
                string command = Console.ReadLine();
                
                switch (command)
                {
                    case "/help":
                        Operators[command].Invoke(issuer);
                        break;
                    case "/issue":
                        Operators[command].Invoke(issuer);
                        break;
                    default: break;
                }
            }
        }

        private static Dictionary<string, Action<Issuer>> Operators = new Dictionary<string, Action<Issuer>>()
        {
            { "/help", (issuer) =>
                {
                    Console.WriteLine("---------------");
                    Console.WriteLine("/help");
                    Console.WriteLine("/issue");
                    Console.WriteLine("---------------");
                }
            },
            { "/issue", (issuer) => 
                {
                    Console.Write("Command name: ");
                    string commandName = Console.ReadLine();

                    Console.WriteLine("Type /confirm to issue, or anything else to abort");
                    string decision = Console.ReadLine();

                    bool doIssue = decision == "/confirm" ? true : decision == "/abort" ? false : false;

                    if (doIssue)
                    {
                        issuer.Issue(new Command{ Name = commandName, DateIssued = DateTime.Now  });
                    }
                }
            }
        };
    }
}
