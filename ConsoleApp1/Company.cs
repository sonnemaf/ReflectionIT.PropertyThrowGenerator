using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp1.Demo;


internal partial class Company {

    [ThrowIfNull]
    public required partial string Name { get; set; }

}