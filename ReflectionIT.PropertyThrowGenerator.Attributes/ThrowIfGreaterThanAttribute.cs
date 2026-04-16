namespace ReflectionIT.PropertyThrowGenerator.Attributes;


[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ThrowIfGreaterThanAttribute : Attribute {

    public string Value { get; }

    public ThrowIfGreaterThanAttribute(string value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = $"""
            "{value}"
            """;
    }

    public ThrowIfGreaterThanAttribute(object value) {
        if (value is null) {
            throw new ArgumentNullException(nameof(value));
        }
        Value = value.ToString() ?? string.Empty;
    }

}
