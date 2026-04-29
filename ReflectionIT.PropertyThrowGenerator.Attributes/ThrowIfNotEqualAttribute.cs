namespace ReflectionIT.PropertyThrowGenerator.Attributes;

/// <summary>
/// Indicates that the generated property setter should throw when the assigned value is not equal to the specified value.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ThrowIfNotEqualAttribute : Attribute {

    /// <summary>
    /// Gets the value used by the generated validation check.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThrowIfNotEqualAttribute"/> class with a string value.
    /// </summary>
    /// <param name="value">The required value for the generated setter.</param>
    public ThrowIfNotEqualAttribute(string value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = $"""
            "{value}"
            """;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThrowIfNotEqualAttribute"/> class with an object value.
    /// </summary>
    /// <param name="value">The required value for the generated setter.</param>
    public ThrowIfNotEqualAttribute(object value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = value.ToString() ?? string.Empty;
    }

}