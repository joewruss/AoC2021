using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AoC2021
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("What day?");
            var inputDay = Console.ReadLine();
            int day = 1;
            while (!int.TryParse(inputDay, out day) && (day < 1 || day > 25))
            {
                Console.WriteLine("What day?");
                inputDay = Console.ReadLine();
            }

            Console.WriteLine("what part?");
            var inputPart = Console.ReadLine();
            int part = 1;
            while (!int.TryParse(inputPart, out part) && (part < 1 ||  part > 2))
            {
                Console.WriteLine("What part?");
                inputPart = Console.ReadLine();
            }

            try
            {
                var interfaceType = typeof(ILogic);
                var concreteType = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => interfaceType.IsAssignableFrom(t))
                    .SingleOrDefault(t => t.Namespace.Equals($"AoC2021.Day{day}"));

                if (concreteType != null)
                {
                    var logic = Activator.CreateInstance(concreteType) as ILogic;
                    var result = "";
                    if (part == 1) result = await logic.RunPart1(@$"C:\Users\jwr\source\repos\AoC2021\AoC2021\Day{day}\Input.txt");
                    if (part == 2) result = await logic.RunPart2(@$"C:\Users\jwr\source\repos\AoC2021\AoC2021\Day{day}\Input.txt");
                    Console.WriteLine($"The solution to day {day} is [{result}]");
                }
                else
                {
                    Console.WriteLine($"Day {day} has not been implemented yet!");
                }                
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Press enter to close");
                Console.ReadLine();
            }
        }
    }
}