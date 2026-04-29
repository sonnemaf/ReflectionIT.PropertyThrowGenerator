namespace ReflectionIT.PropertyThrowGenerator.Attributes;

/// <summary>
/// Indicates that the generated property setter should throw when the assigned numeric value is negative.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ThrowIfNegativeAttribute : Attribute {
}
