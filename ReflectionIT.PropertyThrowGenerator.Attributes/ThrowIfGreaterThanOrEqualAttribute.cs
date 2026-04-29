namespace ReflectionIT.PropertyThrowGenerator.Attributes;

/// <summary>
/// Indicates that the generated property setter should throw when the assigned value is greater than or equal to the specified value.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ThrowIfGreaterThanOrEqualAttribute : Attribute {

    /// <summary>
    /// Gets the value used by the generated validation check.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThrowIfGreaterThanOrEqualAttribute"/> class with a string value.
    /// </summary>
    /// <param name="value">The upper bound that causes the generated setter to throw when reached or exceeded.</param>
    public ThrowIfGreaterThanOrEqualAttribute(string value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = $"""
            "{value}"
            """;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThrowIfGreaterThanOrEqualAttribute"/> class with an object value.
    /// </summary>
    /// <param name="value">The upper bound that causes the generated setter to throw when reached or exceeded.</param>
    public ThrowIfGreaterThanOrEqualAttribute(object value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = value.ToString() ?? string.Empty;
    }

}
