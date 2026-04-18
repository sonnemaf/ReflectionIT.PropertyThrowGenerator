using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;

namespace ReflectionIT.PropertyThrowGenerator.Tests;

public class TestThrowGenerators {

    [Fact]
    public async Task GenerateThrowIfEqualForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {
                    partial class Employee {
                
                        [ThrowIfEqual("abc")]
                        public required partial string Name { get; init; }

                        [ThrowIfEqual("10.5M")]
                        protected partial decimal Salary { private set; get; }
                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
                namespace X;
                partial class Employee
                {
                    public required partial string Name {
                        get;
                        init {
                            global::System.ArgumentOutOfRangeException.ThrowIfEqual(value, "abc");
                            field = value;
                        }
                    }
                    protected partial decimal Salary {
                        get;
                        private set {
                            global::System.ArgumentOutOfRangeException.ThrowIfEqual(value, 10.5M);
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
    public async Task GenerateThrowIfNotEqualForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {
                    partial class Employee {
                
                        [ThrowIfNotEqual("abc")]
                        public required partial string Name { get; init; }

                        [ThrowIfNotEqual("10.5M")]
                        protected partial decimal Salary { private set; get; }
                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
                namespace X;
                partial class Employee
                {
                    public required partial string Name {
                        get;
                        init {
                            global::System.ArgumentOutOfRangeException.ThrowIfNotEqual(value, "abc");
                            field = value;
                        }
                    }
                    protected partial decimal Salary {
                        get;
                        private set {
                            global::System.ArgumentOutOfRangeException.ThrowIfNotEqual(value, 10.5M);
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
    public async Task GenerateThrowIfNullForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
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
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
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
    public async Task GenerateThrowIfNullOrEmptyForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {
                    partial class Employee {
                
                        [ThrowIfNullOrEmpty]
                        public required partial string Name { get; init; }

                        [ThrowIfNullOrEmpty]
                        protected partial string City { private set; get; }
                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
                namespace X;
                partial class Employee
                {
                    public required partial string Name {
                        get;
                        init {
                            global::System.ArgumentException.ThrowIfNullOrEmpty(value);
                            field = value;
                        }
                    }
                    protected partial string City {
                        get;
                        private set {
                            global::System.ArgumentException.ThrowIfNullOrEmpty(value);
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
    public async Task GenerateThrowIfNullOrWhiteSpaceForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {
                    partial class Employee {
                
                        [ThrowIfNullOrWhiteSpace]
                        public required partial string Name { get; init; }

                        [ThrowIfNullOrWhiteSpace]
                        protected partial string City { private set; get; }
                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
                namespace X;
                partial class Employee
                {
                    public required partial string Name {
                        get;
                        init {
                            global::System.ArgumentException.ThrowIfNullOrWhiteSpace(value);
                            field = value;
                        }
                    }
                    protected partial string City {
                        get;
                        private set {
                            global::System.ArgumentException.ThrowIfNullOrWhiteSpace(value);
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
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {

                    partial record struct Employee {
                
                        [ThrowIfNegative]
                        public required partial decimal Salary { get; init; }

                        [ThrowIfNegative]
                        public partial int Age { get; set; }
                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
                namespace X;
                partial record struct Employee
                {
                    public required partial decimal Salary {
                        get;
                        init {
                            global::System.ArgumentOutOfRangeException.ThrowIfNegative(value);
                            field = value;
                        }
                    }
                    public partial int Age {
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
    public async Task GenerateThrowIfNegativeOrZeroForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {

                    partial class Employee {
                
                        [ThrowIfNegativeOrZero]
                        public required partial decimal Salary { get; init; }

                        [ThrowIfNegativeOrZero]
                        protected partial int Age { get; set; }
                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
                namespace X;
                partial class Employee
                {
                    public required partial decimal Salary {
                        get;
                        init {
                            global::System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
                            field = value;
                        }
                    }
                    protected partial int Age {
                        get;
                        set {
                            global::System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
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
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
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
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
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
    public async Task GenerateThrowIfGreaterThanOrEqualForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {

                    partial class Employee {
                
                        [ThrowIfGreaterThanOrEqual(1000)]
                        public partial decimal Salary { get; set; }

                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
                namespace X;
                partial class Employee
                {
                    public partial decimal Salary {
                        get;
                        set {
                            global::System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, 1000);
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
    public async Task GenerateThrowIfLessThanForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {

                    partial class Employee {
                
                        [ThrowIfLessThan(1000)]
                        public partial decimal Salary { get; set; }

                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
                namespace X;
                partial class Employee
                {
                    public partial decimal Salary {
                        get;
                        set {
                            global::System.ArgumentOutOfRangeException.ThrowIfLessThan(value, 1000);
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
    public async Task GenerateThrowIfLessThanOrEqualForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {

                    partial class Employee {
                
                        [ThrowIfLessThanOrEqual(1000)]
                        public partial decimal Salary { get; set; }

                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
                namespace X;
                partial class Employee
                {
                    public partial decimal Salary {
                        get;
                        set {
                            global::System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 1000);
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
    public async Task GenerateThrowIfZeroForPartialProperties() {
        var context = new CSharpSourceGeneratorTest<ThrowGenerators, DefaultVerifier> {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
            TestCode = $$"""
                {{ATTRIBUTE_CODE_IN_TEST}}

                namespace X {

                    partial class Employee {
                
                        [ThrowIfZero]
                        public required partial decimal Salary { get; init; }

                        [ThrowIfZero]
                        protected partial int Age { get; set; }
                    }
                }
                """,
        };

        // List of expected generated sources
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
                namespace X;
                partial class Employee
                {
                    public required partial decimal Salary {
                        get;
                        init {
                            global::System.ArgumentOutOfRangeException.ThrowIfZero(value);
                            field = value;
                        }
                    }
                    protected partial int Age {
                        get;
                        set {
                            global::System.ArgumentOutOfRangeException.ThrowIfZero(value);
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
            ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
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
        context.TestState.GeneratedSources.Add((typeof(ThrowGenerators), "X.Employee.g.cs",
            $$"""
                {{HEADER_CODE}}
                namespace X;
                partial class Employee
                {
                    public partial decimal Salary {
                        get;
                        set {
                            global::System.ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 1000);
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

    public const string HEADER_CODE = """
        //------------------------------------------------------------------------------
        // <auto-generated>
        //     This code was generated by the ReflectionIT.PropertyThrowGenerator source generator
        //     Changes to this file may cause incorrect behavior and will be lost if
        //     the code is regenerated.
        // </auto-generated>
        //------------------------------------------------------------------------------
        #pragma warning disable
        #nullable enable annotations

        """;


    public const string ATTRIBUTE_CODE = """
            namespace ReflectionIT.PropertyThrowGenerator.Attributes {
            
                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfEqualAttribute : Attribute { 
                    public ThrowIfEqualAttribute(string value) {}
                    public ThrowIfEqualAttribute(object value) {}
                }

                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfGreaterThanAttribute : Attribute { 
                    public ThrowIfGreaterThanAttribute(string value) {}
                    public ThrowIfGreaterThanAttribute(object value) {}
                }

                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfGreaterThanOrEqualAttribute : Attribute { 
                    public ThrowIfGreaterThanOrEqualAttribute(string value) {}
                    public ThrowIfGreaterThanOrEqualAttribute(object value) {}
                }

                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfLessThanAttribute : Attribute { 
                    public ThrowIfLessThanAttribute(string value) {}
                    public ThrowIfLessThanAttribute(object value) {}
                }
            
                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfLessThanOrEqualAttribute : Attribute { 
                    public ThrowIfLessThanOrEqualAttribute(string value) {}
                    public ThrowIfLessThanOrEqualAttribute(object value) {}
                }
            
                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfNegativeAttribute : Attribute { }
            
                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfNegativeOrZeroAttribute : Attribute { }
            
                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfNotEqualAttribute : Attribute { 
                    public ThrowIfNotEqualAttribute(string value) {}
                    public ThrowIfNotEqualAttribute(object value) {}
                }

                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfNullAttribute : Attribute { }
            
                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfZeroAttribute : Attribute { }

                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfNullOrEmptyAttribute : Attribute { }

                [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
                public class ThrowIfNullOrWhiteSpaceAttribute : Attribute { }
            }
            """;

    public const string ATTRIBUTE_CODE_IN_TEST = $$"""
            using System;
            using ReflectionIT.PropertyThrowGenerator.Attributes;
            
            {{ATTRIBUTE_CODE}}
            
            """;

}
