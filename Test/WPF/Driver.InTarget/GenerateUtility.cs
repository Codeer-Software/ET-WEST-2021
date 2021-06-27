using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Driver.InTarget
{
    public static class GenerateUtility
    {
        public static void RemoveDuplicationFunction(CaptureCodeGeneratorBase generator, List<Sentence> list, string function)
        {
            bool findChangeText = false;
            for (int i = list.Count - 1; 0 <= i; i--)
            {
                if (IsDuplicationFunction(generator, list[i], function))
                {
                    if (findChangeText)
                    {
                        list.RemoveAt(i);
                    }
                    findChangeText = true;
                }
                else
                {
                    findChangeText = false;
                }
            }
        }

        static bool IsDuplicationFunction(CaptureCodeGeneratorBase generator, Sentence sentence, string function)
        {
            if (!ReferenceEquals(generator, sentence.Owner))
            {
                return false;
            }
            if (sentence.Tokens.Length <= 2)
            {
                return false;
            }
            if (!(sentence.Tokens[0] is TokenName) ||
                (sentence.Tokens[1] == null))
            {
                return false;
            }
            return sentence.Tokens[1].ToString().IndexOf("." + function) == 0;
        }

        public static string ToLiteral(string text)
        {
            using (var writer = new StringWriter())
            using (var provider = CodeDomProvider.CreateProvider("CSharp"))
            {
                var expression = new CodePrimitiveExpression(text);
                provider.GenerateCodeFromExpression(expression, writer, options: null);
                return writer.ToString();
            }
        }
    }
}
