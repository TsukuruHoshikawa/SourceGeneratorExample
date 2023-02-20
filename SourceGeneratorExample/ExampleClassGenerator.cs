using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using Tsukuru;

namespace SourceGeneratorExample
{
    [Generator]
    public class ExampleGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            var cb = new CodeBuilder();
            cb.AddCode("//Auto Generated Class");
            cb.NewLine("using UnityEngine;");

            using (new BlockScope(cb, "internal class ExampleGeneratedClass"))
            {
                using (new BlockScope(cb, "internal static void ExampleGeneratedFunction()"))
                {
                    cb.NewLine("Debug.Log(\"I am auto generated class!!\");");
                }
            }

            context.RegisterForPostInitialization(_ =>
            {
                _.AddSource("ExampleGeneratedClass_g.cs", SourceText.From(cb.ToString(), Encoding.UTF8));
            });
        }
    }
}
