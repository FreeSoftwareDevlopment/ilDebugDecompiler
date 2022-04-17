using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Mono.Cecil.Cil;
using Mono.Cecil;

namespace compilerTools
{
    internal class il_decoMod : @base
    {
        public il_decoMod()
        {
            id = "Mono.Cecil";
            sid = "methods";
        }
        ModuleDefinition module;
        public override void runPayload()
        {
            base.runPayload();
            module = ModuleDefinition.ReadModule(filename);
            if (module != null)
            {
                moduleType = module.Kind == ModuleKind.Dll ? "DLL" : (module.Kind == ModuleKind.Console ? "Console" : (module.Kind == ModuleKind.Windows ? "Windows" : ""));
                if (module.Kind == ModuleKind.Windows)
                    module.Kind = ModuleKind.Console;
                MethodReference clog = module.ImportReference(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(object) })),
                    amp = module.ImportReference(typeof(System.Reflection.Assembly).GetMethod("GetExecutingAssembly", Type.EmptyTypes)),
                    concat = module.ImportReference(typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(string) }));
                Instruction loginst = Instruction.Create(OpCodes.Call, clog);
                foreach (var type in module.Types)
                {
                    foreach (var method in type.Methods)
                    {
                        if (!method.HasBody) continue;
                        var ilProc = method.Body.GetILProcessor();
                        namespacess.Add($"${method.FullName}");
                        var fi = method.Body.Instructions.First();
                        var li = method.Body.Instructions.Last();
                        ilProc.InsertBefore(fi, Instruction.Create(OpCodes.Ldstr,
                            String.Concat(
                                "-- Modified by SharkDebug\n",
                                module.IsMain ? "-- Running as: \"{0}\"\n" : "",
                                $"-- Entering \"{method.Name}\" of \"{type.Namespace}::{type.Name}\""
                            )));
                        if (module.IsMain)
                        {
                            ilProc.InsertBefore(fi, Instruction.Create(OpCodes.Call, amp));//                                                                                                           //
                            ilProc.InsertBefore(fi, Instruction.Create(OpCodes.Callvirt, module.ImportReference(typeof(System.Reflection.Assembly).GetMethod("get_Location", Type.EmptyTypes))));       // System.Reflection.Assembly.GetExecutingAssembly().Location

                            ilProc.InsertBefore(fi, Instruction.Create(OpCodes.Call, concat));
                        }

                        ilProc.InsertBefore(fi, loginst);
                        ilProc.InsertBefore(li, Instruction.Create(OpCodes.Ldstr, $"-- Exiting {method.Name} of {type.Name}"));
                        ilProc.InsertBefore(li, loginst);
                    }
                }
            }
        }
        public override void writeFile()
        {
            string saveas = string.Format("{0}.dbg{1}", Path.GetFileNameWithoutExtension(filename), Path.GetExtension(filename));
            Console.WriteLine($"\t\tSave File: {saveas}");
            module.Write(saveas);
        }
    }
}
