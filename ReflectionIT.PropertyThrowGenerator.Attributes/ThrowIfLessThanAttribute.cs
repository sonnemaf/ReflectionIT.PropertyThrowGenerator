namespace ReflectionIT.PropertyThrowGenerator.Attributes;

/// <summary>
/// Indicates that the generated property setter should throw when the assigned value is less than the specified value.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ThrowIfLessThanAttribute : Attribute {

    /// <summary>
    /// Gets the value used by the generated validation check.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThrowIfLessThanAttribute"/> class with a string value.
    /// </summary>
    /// <param name="value">The lower bound that causes the generated setter to throw when not met.</param>
    public ThrowIfLessThanAttribute(string value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = $"""
            "{value}"
            """;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThrowIfLessThanAttribute"/> class with an object value.
    /// </summary>
    /// <param name="value">The lower bound that causes the generated setter to throw when not met.</param>
    public ThrowIfLessThanAttribute(object value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = value.ToString() ?? string.Empty;
    }

}
