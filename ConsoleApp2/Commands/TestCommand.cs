using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Commands
{
    public class TestCommand : ICommand
    {

        public string Signature { get; } = "command:test";

        public void handle()
        {
            Console.WriteLine("Ik ben een test commando");
        }

        public override string ToString()
        {
            return "Ik ben een test command!";
        }
    }
}
