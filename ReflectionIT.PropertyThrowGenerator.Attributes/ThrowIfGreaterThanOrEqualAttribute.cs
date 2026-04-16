namespace ReflectionIT.PropertyThrowGenerator.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ThrowIfGreaterThanOrEqualAttribute : Attribute {

    public string Value { get; }

    public ThrowIfGreaterThanOrEqualAttribute(string value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = $"""
            "{value}"
            """;
    }

    public ThrowIfGreaterThanOrEqualAttribute(object value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = value.ToString() ?? string.Empty;
    }

}
