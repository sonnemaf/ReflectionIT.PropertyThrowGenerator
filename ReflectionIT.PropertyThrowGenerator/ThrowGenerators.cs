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

    internal const string MINIMUM_LANGUAGE_VERSION_DIAGNOSTIC_ID = "PTG001";

    internal const string PARTIAL_PROPERTY_DIAGNOSTIC_ID = "PTG002";

    internal static DiagnosticDescriptor PartialPropertyRule { get; } = new(
        id: PARTIAL_PROPERTY_DIAGNOSTIC_ID,
        title: "ThrowIf attributes require partial properties",
        messageFormat: "The property {0} must be partial to use the {1}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static DiagnosticDescriptor MinimumLanguageVersionDiagnostic { get; } = new(
        id: MINIMUM_LANGUAGE_VERSION_DIAGNOSTIC_ID,
        title: "C# language version is not supported",
        messageFormat: "ReflectionIT.PropertyThrowGenerator requires C# language version 14.0 or higher",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public void Initialize(IncrementalGeneratorInitializationContext context) {

        IncrementalValuesProvider<PropInfo> propertyThrowIfEqualSymbols = GetSymbolsForType(context, typeof(ThrowIfEqualAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfGreaterThanSymbols = GetSymbolsForType(context, typeof(ThrowIfGreaterThanAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfGreaterThanOrEqualSymbols = GetSymbolsForType(context, typeof(ThrowIfGreaterThanOrEqualAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfLessThanSymbols = GetSymbolsForType(context, typeof(ThrowIfLessThanAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfLessThanOrEqualSymbols = GetSymbolsForType(context, typeof(ThrowIfLessThanOrEqualAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfNegativeSymbols = GetSymbolsForType(context, typeof(ThrowIfNegativeAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfNegativeOrZeroSymbols = GetSymbolsForType(context, typeof(ThrowIfNegativeOrZeroAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfNotEqualSymbols = GetSymbolsForType(context, typeof(ThrowIfNotEqualAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfNullSymbols = GetSymbolsForType(context, typeof(ThrowIfNullAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfZeroSymbols = GetSymbolsForType(context, typeof(ThrowIfZeroAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfNullOrEmptySymbols = GetSymbolsForType(context, typeof(ThrowIfNullOrEmptyAttribute));
        IncrementalValuesProvider<PropInfo> propertyThrowIfNullOrWhiteSpaceSymbols = GetSymbolsForType(context, typeof(ThrowIfNullOrWhiteSpaceAttribute));


        var c0 = propertyThrowIfEqualSymbols.Collect();
        var c1 = propertyThrowIfGreaterThanOrEqualSymbols.Collect();
        var c2 = propertyThrowIfGreaterThanSymbols.Collect();
        var c3 = propertyThrowIfLessThanSymbols.Collect();
        var c4 = propertyThrowIfLessThanOrEqualSymbols.Collect();
        var c5 = propertyThrowIfNegativeSymbols.Collect();
        var c6 = propertyThrowIfNegativeOrZeroSymbols.Collect();
        var c7 = propertyThrowIfNotEqualSymbols.Collect();
        var c8 = propertyThrowIfNullSymbols.Collect();
        var c9 = propertyThrowIfZeroSymbols.Collect();
        var c10 = propertyThrowIfNullOrEmptySymbols.Collect();
        var c11 = propertyThrowIfNullOrWhiteSpaceSymbols.Collect();

        var all = c0.Combine(c1).Combine(c2).Combine(c3).Combine(c4).Combine(c5).Combine(c6).Combine(c7).Combine(c8).Combine(c9).Combine(c10).Combine(c11);
        var compilationAndProperties = context.CompilationProvider.Combine(all);

        context.RegisterSourceOutput(compilationAndProperties, GenerateSource);

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

    private void GenerateSource(SourceProductionContext context, (Compilation Left, (((((((((((ImmutableArray<PropInfo> Left, ImmutableArray<PropInfo> Right) Left, ImmutableArray<PropInfo> Right) Left, ImmutableArray<PropInfo> Right) Left, ImmutableArray<PropInfo> Right) Left, ImmutableArray<PropInfo> Right) Left, ImmutableArray<PropInfo> Right) Left, ImmutableArray<PropInfo> Right) Left, ImmutableArray<PropInfo> Right) Left, ImmutableArray<PropInfo> Right) Left, ImmutableArray<PropInfo> Right) Left, ImmutableArray<PropInfo> Right) Right) tuple) {
        if (tuple.Left is CSharpCompilation csharpCompilation && csharpCompilation.LanguageVersion < LanguageVersion.CSharp14) {
            context.ReportDiagnostic(Diagnostic.Create(MinimumLanguageVersionDiagnostic, Location.None));
            return;
        }

        var properties = tuple.Right.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.AddRange(
                         tuple.Right.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right).AddRange(
                         tuple.Right.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right).AddRange(
                         tuple.Right.Left.Left.Left.Left.Left.Left.Left.Left.Right).AddRange(
                         tuple.Right.Left.Left.Left.Left.Left.Left.Left.Right).AddRange(
                         tuple.Right.Left.Left.Left.Left.Left.Left.Right).AddRange(
                         tuple.Right.Left.Left.Left.Left.Left.Right).AddRange(
                         tuple.Right.Left.Left.Left.Left.Right).AddRange(
                         tuple.Right.Left.Left.Left.Right).AddRange(
                         tuple.Right.Left.Left.Right).AddRange(
                         tuple.Right.Left.Right).AddRange(tuple.Right.Right);

        if (properties.IsDefaultOrEmpty) {
            return;
        }


        foreach (var typeGroup in properties.GroupBy(static p => p.PropertySymbol.ContainingType, SymbolEqualityComparer.Default)) {

            if (typeGroup.Key is ITypeSymbol ct) {

                var builder = new CsFileBuilder();

                builder.AddAutoGeneratedHeader("ReflectionIT.PropertyThrowGenerator")
                     .AddPreprocessorDirectives()
                     .AddEmptyLine();

                builder.AddFileScopedNamespace(ct.ContainingNamespace);

                builder.AddPartialType(ct);
                builder.AddStatementAndStartBlock(string.Empty);

                foreach (var pi in typeGroup.GroupBy(static p => p.PropertySymbol, SymbolEqualityComparer.Default)) {

                    if (pi.Key is IPropertySymbol pis) {


                        var prop = pi.First();

                        if (!prop.IsPartial) {
                            context.ReportDiagnostic(Diagnostic.Create(PartialPropertyRule, prop.PropertyDeclarationSyntax.GetLocation(), pis.Name, prop.AttributeType.Name));
                            continue;
                        }

                        string typeName = pis.Type.ToDisplayString();
                        if (typeName.Contains('.')) {
                            typeName = $"global::{typeName}";
                        }

                        builder.AddStatements(
                            $"{prop.Modifiers} {typeName} {pis.Name} {{",
                            $"    {prop.Get};",
                            $"    {prop.SetOrInit} {{");

                        foreach (var item in pi) {

                            

                            switch (item.AttributeType.Name) {
                                case nameof(ThrowIfEqualAttribute):
                                    builder.AddStatements($"        global::System.ArgumentOutOfRangeException.ThrowIfEqual(value, {item.Value});");
                                    break;
                                case nameof(ThrowIfNotEqualAttribute):
                                    builder.AddStatements($"        global::System.ArgumentOutOfRangeException.ThrowIfNotEqual(value, {item.Value});");
                                    break;
                                case nameof(ThrowIfZeroAttribute):
                                    builder.AddStatements("        global::System.ArgumentOutOfRangeException.ThrowIfZero(value);");
                                    break;
                                case nameof(ThrowIfNullAttribute):
                                    builder.AddStatements("        global::System.ArgumentNullException.ThrowIfNull(value);");
                                    break;
                                case nameof(ThrowIfNegativeAttribute):
                                    builder.AddStatements("        global::System.ArgumentOutOfRangeException.ThrowIfNegative(value);");
                                    break;
                                case nameof(ThrowIfNegativeOrZeroAttribute):
                                    builder.AddStatements("        global::System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);");
                                    break;
                                case nameof(ThrowIfGreaterThanAttribute):
                                    builder.AddStatements($"        global::System.ArgumentOutOfRangeException.ThrowIfGreaterThan(value, {item.Value});");
                                    break;
                                case nameof(ThrowIfGreaterThanOrEqualAttribute):
                                    builder.AddStatements($"        global::System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, {item.Value});");
                                    break;
                                case nameof(ThrowIfLessThanAttribute):
                                    builder.AddStatements($"        global::System.ArgumentOutOfRangeException.ThrowIfLessThan(value, {item.Value});");
                                    break;
                                case nameof(ThrowIfLessThanOrEqualAttribute):
                                    builder.AddStatements($"        global::System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, {item.Value});");
                                    break;
                                case nameof(ThrowIfNullOrEmptyAttribute):
                                    builder.AddStatements($"        global::System.ArgumentException.ThrowIfNullOrEmpty(value);");
                                    break;
                                case nameof(ThrowIfNullOrWhiteSpaceAttribute):
                                    builder.AddStatements($"        global::System.ArgumentException.ThrowIfNullOrWhiteSpace(value);");
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

                var src = builder.Build();

                var filename = ct.ToDisplayString()
                              .Replace('<', '{')
                              .Replace('>', '}')
                              .Replace(" ", string.Empty);

                context.AddSource($"{filename}.g.cs", SourceText.From(src, Encoding.UTF8));
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
        if (node is not PropertyDeclarationSyntax { AccessorList.Accessors: { Count: 2 } accessors, AttributeLists.Count: > 0 }) {
            return false;
        }

        // The accessors must be a get and a set (with any accessibility)
        return (accessors[0].Kind() is SyntaxKind.GetAccessorDeclaration or SyntaxKind.SetAccessorDeclaration or SyntaxKind.InitAccessorDeclaration) &&
               (accessors[1].Kind() is SyntaxKind.GetAccessorDeclaration or SyntaxKind.SetAccessorDeclaration or SyntaxKind.InitAccessorDeclaration);
    }
}
