using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using ReflectionIT.PropertyThrowGenerator.Attributes;
using System.Collections.Immutable;
using System.Text;

namespace ReflectionIT.PropertyThrowGenerator;

[Generator]
public sealed class ThrowGenerators : IIncrementalGenerator {

    public void Initialize(IncrementalGeneratorInitializationContext context) {

        IncrementalValuesProvider<PropInfo> propertyThrowIfNullSymbols = GetSymbolsForType(context, typeof(ThrowIfNullAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfNegativeSymbols = GetSymbolsForType(context, typeof(ThrowIfNegativeAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfGreaterSymbols = GetSymbolsForType(context, typeof(ThrowIfGreaterThanAttribute));

        var c1 = propertyThrowIfNullSymbols.Collect();
        var c2 = propertyThrowIfNegativeSymbols.Collect();
        var c3 = propertyThrowIfGreaterSymbols.Collect();

        var all = c1.Combine(c2).Combine(c3);

        context.RegisterSourceOutput(all, GenerateSource);

        static IncrementalValuesProvider<PropInfo> GetSymbolsForType(IncrementalGeneratorInitializationContext context, Type t) {
            return context.SyntaxProvider.ForAttributeWithMetadataName(
                t.FullName,
                predicate: static (node, cancel) => IsValidCandidateProperty(node),
                transform: (context, cancel) =>
                    new PropInfo(
                        (IPropertySymbol)context.SemanticModel.GetDeclaredSymbol(context.TargetNode, cancel)!,
                        (PropertyDeclarationSyntax)context.TargetNode!, t)
            );
        }
    }

    private void GenerateSource(SourceProductionContext context, ((ImmutableArray<PropInfo> Left, ImmutableArray<PropInfo> Right) Left, ImmutableArray<PropInfo> Right) tuple) {
        var properties = tuple.Left.Left.AddRange(tuple.Left.Right).AddRange(tuple.Right);

        if (properties.IsDefaultOrEmpty) {
            return;
        }


        foreach (var typeGroup in properties.GroupBy(static p => p.PropertySymbol.ContainingType, SymbolEqualityComparer.Default)) {

            if (typeGroup.Key is ITypeSymbol ct) {

                ICsFileBuilder builder = new CsFileBuilder();
                builder.AddFileScopedNamespace(ct.ContainingNamespace);

                builder.AddPartialType(ct);
                builder.AddStatementAndStartBlock(string.Empty);

                foreach (var pi in typeGroup.GroupBy(static p => p.PropertySymbol, SymbolEqualityComparer.Default)) {

                    if (pi.Key is IPropertySymbol pis) {

                        var prop = pi.First();

                        builder.AddStatements(
                            $"{prop.Modifiers} {pis.Type} {pis.Name} {{",
                            $"    {prop.Get};",
                            $"    {prop.SetOrInit} {{");


                        foreach (var item in pi) {
                            switch (item.AttributeType.Name) {
                                case nameof(ThrowIfNullAttribute):
                                    builder.AddStatements("        global::System.ArgumentNullException.ThrowIfNull(value);");
                                    break;
                                case nameof(ThrowIfNegativeAttribute):
                                    builder.AddStatements("        global::System.ArgumentOutOfRangeException.ThrowIfNegative(value);");
                                    break;
                                case nameof(ThrowIfGreaterThanAttribute):
                                    builder.AddStatements($"        global::System.ArgumentOutOfRangeException.ThrowIfGreaterThan(value, {item.Value});");
                                    break;
                                default:
                                    break;
                            }
                        }

                        builder.AddStatements(
                            "        field = value;",
                            "    }",
                            "}");
                    }
                }

                builder.EndPartialType(ct);

                context.AddSource($"{typeGroup.Key}.cs", SourceText.From(builder.Build(), Encoding.UTF8));
            }
        }
    }

    /// <summary>
    /// Checks whether a given property declaration has valid syntax.
    /// </summary>
    /// <param name="node">The input node to validate.</param>
    /// <param name="containingTypeNode">The resulting node for the containing type of the property, if valid.</param>
    /// <returns>Whether <paramref name="node"/> is a valid property.</returns>
    internal static bool IsValidCandidateProperty(SyntaxNode node) {
        // The node must be a property declaration with two accessors
        if (node is not PropertyDeclarationSyntax { AccessorList.Accessors: { Count: 2 } accessors, AttributeLists.Count: > 0 } property) {
            return false;
        }

        // The property must be partial (we'll check that it's a declaration from its symbol)
        if (!property.Modifiers.Any(SyntaxKind.PartialKeyword)) {
            return false;
        }

        // The accessors must be a get and a set (with any accessibility)
        return (accessors[0].Kind() is SyntaxKind.GetAccessorDeclaration or SyntaxKind.SetAccessorDeclaration or SyntaxKind.InitAccessorDeclaration) &&
               (accessors[1].Kind() is SyntaxKind.GetAccessorDeclaration or SyntaxKind.SetAccessorDeclaration or SyntaxKind.InitAccessorDeclaration);
    }
}
