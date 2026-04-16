global using ReflectionIT.PropertyThrowGenerator.Attributes;
using ConsoleApp1.Test;

namespace ConsoleApp1; 

internal partial class Program {

    private static void Main() {

        var fons = new Outer.Employee("Fons", "Asten", 2000);

        fons.RaiseSalary(10);

        Console.WriteLine(fons);

        var jim = new Outer.Employee { Name = "Jim", Salary = 4000 };

        Console.WriteLine(jim);

        ArgumentOutOfRangeException.ThrowIfGreaterThan(1, 2);
        ArgumentOutOfRangeException.ThrowIfNegative(1);

        try {
            //fons.Name = null!;

            var _ = new Outer.Employee(null!, "Asten", 2000);


        } catch (Exception ex) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ToString());
        }

    }
}