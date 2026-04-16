namespace ReflectionIT.PropertyThrowGenerator.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ThrowIfEqualAttribute : Attribute {

    public string Value { get; }

    public ThrowIfEqualAttribute(string value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = $"""
            "{value}"
            """;
    }

    public ThrowIfEqualAttribute(object value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = value.ToString() ?? string.Empty;
    }

}
