# ReflectionIT.PropertyThrowGenerator

A source generator package that creates partial properties annotated with `[ThrowIf...]` attributes.
The generated property setters throw `Argument...` exceptions when an invalid value is assigned.

# NuGet package

| Package | Version |
| ------ | ------ |
| ReflectionIT.PropertyThrowGenerator | [![NuGet](https://img.shields.io/nuget/v/ReflectionIT.PropertyThrowGenerator)](https://www.nuget.org/packages/ReflectionIT.PropertyThrowGenerator/) |         

##              
Your project must use `LangVersion` **C# 14.0** or higher because the generated code uses the new `field` keyword.

It must also target **.NET 8.0** or higher because the generated code uses the `ArgumentOutOfRangeException` throw helper methods.

## Example

Install the NuGet package and write the following code:

```cs
using ReflectionIT.PropertyThrowGenerator.Attributes;

partial class Employee {

    public Employee() {
    }

    [ThrowIfNull]
    public required partial string Name { get; set; }

    [ThrowIfNull]
    [ThrowIfGreaterThan("London")]
    public partial string City { get; set; } = string.Empty;

    [ThrowIfNegative]
    [ThrowIfGreaterThan(5000)]
    public partial decimal Salary { get; set; }
}
```

This generates the following partial class with three partial properties that validate the assigned value in their setters.

```cs
partial class Employee
{
    public required partial string Name {
        get;
        set {
            global::System.ArgumentNullException.ThrowIfNull(value);
            field = value;
        }
    }
    public partial string City {
        get;
        set {
            global::System.ArgumentNullException.ThrowIfNull(value);
            global::System.ArgumentOutOfRangeException.ThrowIfGreaterThan(value, "London");
            field = value;
        }
    }
    public partial decimal Salary {
        get;
        set {
            global::System.ArgumentOutOfRangeException.ThrowIfNegative(value);
            global::System.ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 5000);
            field = value;
        }
    }
}
```

