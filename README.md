# ReflectionIT.PropertyThrowGenerator

`ReflectionIT.PropertyThrowGenerator` is a source generator that adds validation to partial properties annotated with `[ThrowIf...]` attributes.

The generated setters call the appropriate `Argument...Exception.ThrowIf...` helper methods before assigning the value.

## NuGet package

| Package | Version |
| ------ | ------ |
| ReflectionIT.PropertyThrowGenerator | [![NuGet](https://img.shields.io/nuget/v/ReflectionIT.PropertyThrowGenerator)](https://www.nuget.org/packages/ReflectionIT.PropertyThrowGenerator/) |         

## Requirements

- `LangVersion` must be **C# 14.0** or higher because the generated code uses the `field` keyword.
- `TargetFramework` must be **.NET 8.0** or higher because the generated code uses `ArgumentOutOfRangeException` throw helper methods.

## How it works

1. Add the NuGet package to your project.
2. Annotate a `partial` property with one or more `[ThrowIf...]` attributes.
3. The generator emits the implementation part of the property and inserts the required validation logic into the `set` or `init` accessor.

If a `ThrowIf...` attribute is used on a non-`partial` property, the analyzer reports a diagnostic.

## Example

After installing the package, write code like this:

```cs
using ReflectionIT.PropertyThrowGenerator.Attributes;

partial class Employee {

    [ThrowIfNull]
    public required partial string Name { get; set; }

    [ThrowIfNull]
    [ThrowIfGreaterThan("London")]
    public partial string City { get; set; } = string.Empty;

    [ThrowIfGreaterThanOrEqual(16)]
    public partial int Age { get; set; }

    // Decimal attribute arguments are not supported directly, so use a string literal instead.
    [ThrowIfGreaterThan("1234.56M")]
    [ThrowIfNegative]
    public partial decimal Salary { get; set; }

}
```

This generates a partial class with four validated properties:

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

    public partial int Age {
        get;
        set {
            global::System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, 16);
            field = value;
        }
    }

    public partial decimal Salary {
        get;
        set {
            global::System.ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 1234.56M);
            global::System.ArgumentOutOfRangeException.ThrowIfNegative(value);
            field = value;
        }
    }
}
```

## Attributes

- `ThrowIfEqualAttribute`
- `ThrowIfGreaterThanAttribute`
- `ThrowIfGreaterThanOrEqualAttribute`
- `ThrowIfLessThanAttribute`
- `ThrowIfLessThanOrEqualAttribute`
- `ThrowIfNegativeAttribute`
- `ThrowIfNegativeOrZeroAttribute`
- `ThrowIfNotEqualAttribute`
- `ThrowIfNullAttribute`
- `ThrowIfNullOrEmptyAttribute`
- `ThrowIfNullOrWhiteSpaceAttribute`
- `ThrowIfZeroAttribute`

## Analyzer rules

The package also includes analyzers that help detect unsupported usage at compile time.

| Rule ID | Severity | Description |
| ------ | ------ | ------ |
| `PTG001` | Error | Reported when the project uses a C# language version lower than **14.0**. |
| `PTG002` | Error | Reported when a `ThrowIf...` attribute is applied to a property that is not declared `partial`. |

### PTG001: C# language version is not supported

This rule is reported when the consuming project does not use **C# 14.0** or later.

The generator requires C# 14 because the generated code uses the `field` keyword.

### PTG002: ThrowIf attributes require partial properties

This rule is reported when one of the `ThrowIf...` attributes is used on a property that is not declared `partial`.

Only `partial` properties are supported because the generator emits the implementation part of the property.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.txt) file for details.