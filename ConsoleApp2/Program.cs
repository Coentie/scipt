using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp2.Commands;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
            ICommand myCommand = getCommandInstance("command:teste");

                Console.WriteLine(myCommand);
                Console.ReadKey();
            } catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Command could not be found");
                Console.ReadKey();
                return;
            }
            
        }

        /** Retrieves all the claseses from the commands directory **/
        private static ICommand getCommandInstance(string commandSignature)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Type[] commands = GetTypesInNamespace(myAssembly, getMyNamespace() +  ".Commands");

            foreach(Type Command in commands)
            {
                if(Command.IsInterface)
                {
                    continue;
                }

                ICommand instance = getObjectInstance(Command);

                if(instance.Signature == commandSignature)
                {
                    return instance;
                }
            }

            throw new Exception("Class does not exist");
        }
        
        /** Retrieves all the types from a namespace= **/
        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

        /** Returns the namespace of the program class **/
        private static string getMyNamespace()
        {
            Type type = typeof(Program);

            return (string) type.Namespace;
        }

        /** Generates an instance of the command **/
        private static ICommand getObjectInstance(Type Command)
        {
            return (ICommand) Activator.CreateInstance(Command);
        }

    }
}
