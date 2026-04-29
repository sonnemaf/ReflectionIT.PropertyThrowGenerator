namespace ReflectionIT.PropertyThrowGenerator.Attributes;

/// <summary>
/// Indicates that the generated property setter should throw when the assigned numeric value is negative or zero.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ThrowIfNegativeOrZeroAttribute : Attribute {
}
