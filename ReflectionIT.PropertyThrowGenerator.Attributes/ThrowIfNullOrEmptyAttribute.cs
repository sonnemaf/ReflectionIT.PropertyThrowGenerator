namespace ReflectionIT.PropertyThrowGenerator.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ThrowIfNullOrEmptyAttribute : Attribute {
}
