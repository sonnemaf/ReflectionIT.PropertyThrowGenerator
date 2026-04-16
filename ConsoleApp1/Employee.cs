using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp1;

internal partial class Employee {

    public Employee() {
    }

    [SetsRequiredMembers]
    public Employee(string name, string city, decimal salary) {
        Name = name;
        City = city;
        Salary = salary;
    }

    [ThrowIfNull]
    public required partial string Name { get; set; }

    [ThrowIfNull]
    [ThrowIfGreaterThan("Eindhoven")]
    public partial string City { get; set; } = string.Empty;

    [ThrowIfNegative]
    [ThrowIfGreaterThan("5000.5M")]
    public partial decimal Salary { get; set; }

    public void RaiseSalary(decimal percentage) {
        Salary += Salary * (percentage / 100);
    }

    public virtual decimal YearSalary => Salary * 12;

    public override string ToString() => $"Employee Name = {Name}, Salary = {Salary}";
}
