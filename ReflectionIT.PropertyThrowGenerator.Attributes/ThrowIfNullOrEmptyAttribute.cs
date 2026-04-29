namespace ReflectionIT.PropertyThrowGenerator.Attributes;

/// <summary>
/// Indicates that the generated property setter should throw when the assigned string value is <see langword="null"/> or empty.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ThrowIfNullOrEmptyAttribute : Attribute {
}
