using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.TypeSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace compilerTools
{
    class Csharp_decompiler : @base
    {
        public Csharp_decompiler()
        {
            id = "ICSharpCode.Decompiler";
            sid = "namespaces";
        }
        public override void writeFile()
        {
            base.writeFile();
            File.WriteAllText(string.Format("{0}.decompiled{1}.cs", Path.GetFileNameWithoutExtension(filename), Path.GetExtension(filename)), module);
        }
        public override void runPayload()
        {
            base.runPayload();
            var decompiler = new CSharpDecompiler(filename, new DecompilerSettings());

            module = decompiler.DecompileWholeModuleAsString();
            INamespace icsdns = decompiler.TypeSystem.RootNamespace;
            foreach (var ns in icsdns.ChildNamespaces) namespacess.Add(ns.FullName);
        }
    }
}
