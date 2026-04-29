using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ReflectionIT.PropertyThrowGenerator;

internal class PropInfo {

    public IPropertySymbol PropertySymbol { get; }
    public string Modifiers { get; }
    public bool IsPartial { get; }
    public string Get { get; }
    public string SetOrInit { get; }

    public Type AttributeType { get; }
    public string Value { get; }

    public PropInfo(IPropertySymbol propertySymbol, PropertyDeclarationSyntax propertyDeclarationSyntax, Type attributeType) {

        PropertySymbol = propertySymbol;
        Modifiers = propertyDeclarationSyntax.Modifiers.ToString();
        IsPartial = propertyDeclarationSyntax.Modifiers.Any(SyntaxKind.PartialKeyword);

        var (g, s) = propertyDeclarationSyntax.AccessorList?.Accessors[0].Kind() is SyntaxKind.GetAccessorDeclaration ? (0, 1) : (1, 0);

        Get = $"{AppendSpace(propertyDeclarationSyntax.AccessorList?.Accessors[g].Modifiers.ToString())}get";
        SetOrInit = (propertyDeclarationSyntax.AccessorList?.Accessors[s].Kind() is SyntaxKind.InitAccessorDeclaration) ? "init" : "set";
        SetOrInit = $"{AppendSpace(propertyDeclarationSyntax.AccessorList?.Accessors[s].Modifiers.ToString())}{SetOrInit}";

        static string? AppendSpace(string? text) => string.IsNullOrEmpty(text) ? text : $"{text} ";

        AttributeType = attributeType;

        var attribute = propertySymbol.GetAttributes()
             .First(a => a.AttributeClass?.ToDisplayString() == attributeType.FullName);

        Value = attribute.ConstructorArguments.FirstOrDefault().ToCSharpString();
        if (PropertySymbol.Type.ToString() != "string" && Value.StartsWith("\"") && Value.EndsWith("\"")) {
            Value = Value.Substring(1, Value.Length - 2);
        }
    }
}