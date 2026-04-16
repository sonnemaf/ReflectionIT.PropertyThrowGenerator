//using ConsoleApp1.Demo;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp1.Test;

partial class Outer {

    internal partial class Employee {

        public Employee() {
        }

        [SetsRequiredMembers]
        public Employee(string name, string city, decimal salary, global::ConsoleApp1.Demo.Company employer) {
            Name = name;
            City = city;
            Salary = salary;
            Employer = employer;
        }

        [ThrowIfNull]
        public required partial string Name { get; set; }

        [ThrowIfNull]
        public required partial Demo.Company Employer { get; set; }

        //public required global::ConsoleApp1.Demo.Company Employer {
        //    get;
        //    set {
        //        global::System.ArgumentNullException.ThrowIfNull(value);
        //        field = value;
        //    }
        //}

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
}