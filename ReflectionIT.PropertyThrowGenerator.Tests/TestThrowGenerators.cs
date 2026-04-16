using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;

namespace ReflectionIT.PropertyThrowGenerator.Tests;

public class TestThrowGenerators {

    [Fact]
    public async Task GenerateThrowIfNullForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net90,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {
                    partial class Employee {
                
                        [ThrowIfNull]
                        public required partial string Name { get; init; }

                        [ThrowIfNull]
                        protected partial string City { private set; get; }
                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.cs",
            """
                namespace X;
                partial class Employee
                {
                    public required partial string Name {
                        get;
                        init {
                            global::System.ArgumentNullException.ThrowIfNull(value);
                            field = value;
                        }
                    }
                    protected partial string City {
                        get;
                        private set {
                            global::System.ArgumentNullException.ThrowIfNull(value);
                            field = value;
                        }
                    }
                }

                """));

        context.SolutionTransforms.Add((solution, projectId) => {
            var project = solution.GetProject(projectId)!;
            var parse = (CSharpParseOptions)project.ParseOptions!;
            return solution.WithProjectParseOptions(projectId, parse.WithLanguageVersion(LanguageVersion.CSharp14));
        });

        await context.RunAsync();
    }

    [Fact]
    public async Task GenerateThrowIfNegativeForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net90,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {

                    partial class Employee {
                
                        [ThrowIfNegative]
                        public required partial decimal Salary { get; init; }

                        [ThrowIfNegative]
                        protected partial int Age { get; set; }
                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.cs",
            """
                namespace X;
                partial class Employee
                {
                    public required partial decimal Salary {
                        get;
                        init {
                            global::System.ArgumentOutOfRangeException.ThrowIfNegative(value);
                            field = value;
                        }
                    }
                    protected partial int Age {
                        get;
                        set {
                            global::System.ArgumentOutOfRangeException.ThrowIfNegative(value);
                            field = value;
                        }
                    }
                }

                """));

        context.SolutionTransforms.Add((solution, projectId) => {
            var project = solution.GetProject(projectId)!;
            var parse = (CSharpParseOptions)project.ParseOptions!;
            return solution.WithProjectParseOptions(projectId, parse.WithLanguageVersion(LanguageVersion.CSharp14));
        });

        await context.RunAsync();
    }

    [Fact]
    public async Task GenerateThrowIfGreaterThanForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net90,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {

                    partial class Employee {
                
                        [ThrowIfGreaterThan(1000)]
                        public partial decimal Salary { get; set; }

                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.cs",
            """
                namespace X;
                partial class Employee
                {
                    public partial decimal Salary {
                        get;
                        set {
                            global::System.ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 1000);
                            field = value;
                        }
                    }
                }

                """));

        context.SolutionTransforms.Add((solution, projectId) => {
            var project = solution.GetProject(projectId)!;
            var parse = (CSharpParseOptions)project.ParseOptions!;
            return solution.WithProjectParseOptions(projectId, parse.WithLanguageVersion(LanguageVersion.CSharp14));
        });

        await context.RunAsync();
    }

    [Fact]
    public async Task GenerateMultiple() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net90,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {

                    partial class Employee {
                
                        [ThrowIfGreaterThan(1000)]
                        [ThrowIfNegative]
                        public partial decimal Salary { get; set; }

                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.cs",
            """
                namespace X;
                partial class Employee
                {
                    public partial decimal Salary {
                        get;
                        set {
                            global::System.ArgumentOutOfRangeException.ThrowIfNegative(value);
                            global::System.ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 1000);
                            field = value;
                        }
                    }
                }

                """));

        context.SolutionTransforms.Add((solution, projectId) => {
            var project = solution.GetProject(projectId)!;
            var parse = (CSharpParseOptions)project.ParseOptions!;
            return solution.WithProjectParseOptions(projectId, parse.WithLanguageVersion(LanguageVersion.CSharp14));
        });

        await context.RunAsync();
    }


    public const string ATTRIBUTE_CODE = """
            namespace ReflectionIT.PropertyThrowGenerator.Attributes {
            
                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfNullAttribute : Attribute { }

                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfNegativeAttribute : Attribute { }

                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfGreaterThanAttribute : Attribute { 
                    public ThrowIfGreaterThanAttribute(string value) {}
                    public ThrowIfGreaterThanAttribute(object value) {}
                }
            }
            """;

    public const string ATTRIBUTE_CODE_CONDITIONAL = $$"""
            //------------------------------------------------------------------------------
            // <auto-generated>
            //     This code was generated by the ReflectionIT.ComparisonOperatorsGenerator source generator
            //
            //     Changes to this file may cause incorrect behavior and will be lost if
            //     the code is regenerated.
            // </auto-generated>
            //------------------------------------------------------------------------------

            #nullable enable
            #if COMPARISON_OPERATORS_GENERATOR_EMBED_ATTRIBUTES
            {{ATTRIBUTE_CODE}}
            #endif
            """;

    public const string ATTRIBUTE_CODE_IN_TEST = $$"""
            using System;
            using ReflectionIT.PropertyThrowGenerator.Attributes;
            
            {{ATTRIBUTE_CODE}}
            
            """;

}
