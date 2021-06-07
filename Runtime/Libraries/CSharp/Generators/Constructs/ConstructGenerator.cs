﻿using System.Collections.Generic;

namespace Bolt.Addons.Libraries.CSharp
{
    public abstract class ConstructGenerator : ICodeGenerator
    {
        public abstract string Generate(int indent);

        public string GenerateClean(int indent)
        {
            return GenerateWithoutStyles(indent);
        }

        public string GenerateWithoutStyles(int indent)
        {
            var output = this.Generate(indent);
            return output.RemoveHighlights().RemoveMarkdown();
        }

        public abstract List<string> Usings();
    }
}