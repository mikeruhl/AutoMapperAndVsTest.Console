using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Manually Test converting a DomainObject to a DomainDto");
            Console.Write("Enter FirstName Value: ");
            var firstName = Console.ReadLine();

            Console.Write("Enter LastName Value: ");
            var lastName = Console.ReadLine();

            Console.Write("Enter Age Value: ");
            var age = Console.ReadLine();

            var domain = new DomainObject(){FirstName = firstName, LastName = lastName, Age = int.Parse(age)};
            var extruder = new Extruder<DomainObject, DomainDto>(domain);
            var output = extruder.Generate();

            Console.WriteLine($"FirstName: {output.FirstName}");
            Console.WriteLine($"LastName: {output.LastName}");
            Console.WriteLine($"Age: {output.Age}");
            Console.WriteLine("Press Any Key to Exit");
            Console.ReadKey();
        }
    }
}
