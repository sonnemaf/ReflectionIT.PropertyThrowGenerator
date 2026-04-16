global using ReflectionIT.PropertyThrowGenerator.Attributes;

namespace ConsoleApp1; 

internal partial class Program {

    private static void Main() {

        Employee fons = new Employee("Fons", "Asten", 2000);

        fons.RaiseSalary(10);

        Console.WriteLine(fons);

        Employee jim = new Employee { Name = "Jim", Salary = 4000 };

        Console.WriteLine(jim);

        ArgumentOutOfRangeException.ThrowIfGreaterThan(1, 2);
        ArgumentOutOfRangeException.ThrowIfNegative(1);

        try {
            //fons.Name = null!;

            var _ = new Employee(null!, "Asten", 2000);


        } catch (Exception ex) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ToString());
        }

    }
}