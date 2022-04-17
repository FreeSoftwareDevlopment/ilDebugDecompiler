using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace compilerTools
{
    class @base
    {
        public string id = "";
        public virtual void writeFile() { }
        protected string filename = "";
        public string moduleType = "", module = "", sid = "";
        public List<string> namespacess = new List<string>();
        public void setWorkOn(string w) { filename = w; }
        public virtual void runPayload() { clean(); }
        public virtual void clean() { namespacess.Clear(); moduleType = ""; module = ""; }
    }
}
