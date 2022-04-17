using compilerTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


class Prog
{
    public static void Main(String[] args)
    {
        List<@base> r = new List<@base>();
        r.Add(new Csharp_decompiler());
        r.Add(new il_decoMod());

        if (args.Length > 0)
        {
            foreach (var e in args)
            {
                if (File.Exists(e))
                {
                    Console.WriteLine(string.Concat("Working on: ", e));
                    foreach (var t in r)
                    {
                        Console.WriteLine(string.Concat("\tRun: ", t.id));
                        try
                        {
                            t.setWorkOn(e);
                            t.runPayload();
                            t.writeFile();
                            if (t.namespacess.Count > 0)
                            {
                                Console.WriteLine($"\t\t{t.sid}:");
                                foreach (var n in t.namespacess)
                                {
                                    Console.WriteLine(string.Concat("\t\t- ", n));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(string.Concat("\t\tERROR:", ex.Message));
                        }
                        Console.WriteLine(string.Concat("\tCompleted: ", t.id));
                    }
                    Console.WriteLine(string.Concat("Completed on: ", e, "\n"));
                }
            }
        }
        else
            Console.WriteLine("YOU SHOULD OPEN A FILE WITH THIS APPLICATION!");
    }
}
