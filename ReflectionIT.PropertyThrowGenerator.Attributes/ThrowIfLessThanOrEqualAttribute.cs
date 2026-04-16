namespace ReflectionIT.PropertyThrowGenerator.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ThrowIfLessThanOrEqualAttribute : Attribute {

    public string Value { get; }

    public ThrowIfLessThanOrEqualAttribute(string value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = $"""
            "{value}"
            """;
    }

    public ThrowIfLessThanOrEqualAttribute(object value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = value.ToString() ?? string.Empty;
    }

}