using System;
using System.Collections;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace MapEditor
{
    public class Compiler
    {
        public string errors;

        public CompilerResults CompileCDir(string path, string dllName)
        {

           // ArrayList refs = new ArrayList();
            //refs.Add("System");
            //refs.Add("System.Collections.Generic");
            //refs.Add("System.Text");

            string Output = dllName;
            CSharpCodeProvider provider = new CSharpCodeProvider();

             string[] files = GetScripts(path, "*.cs");
            // CodeDomProvider dom = CodeDomProvider.CreateProvider("CSharp");
           // string[] refers = (string[])refs.ToArray(typeof(string));
            CompilerParameters param = new CompilerParameters(null,Output,false);

            //System.CodeDom.Compiler.CompilerParameters param = new CompilerParameters();
            param.GenerateExecutable = false;
            //param.OutputAssembly = Output;
            
            //CompilerResults results = dom.CompileAssemblyFromSource(param, sources);
            CompilerResults results = provider.CompileAssemblyFromFile(param, files);

            if (results.Errors.Count > 0)
            {
                foreach (CompilerError Comperr in results.Errors)
                {
                    errors = errors + "Line " + Comperr.Line + ": " + Comperr.ErrorText + "\n";
                }
                return results;
            }
            return results;

        } 
        public bool CompileC(string[] sources, string dllName)
        {
            string Output = dllName;
            CSharpCodeProvider provider = new CSharpCodeProvider();
            
           // CodeDomProvider dom = CodeDomProvider.CreateProvider("CSharp");

            
            System.CodeDom.Compiler.CompilerParameters param = new CompilerParameters();
            param.GenerateExecutable = false;
            param.OutputAssembly = Output;
            //CompilerResults results = dom.CompileAssemblyFromSource(param, sources);
            CompilerResults results = provider.CompileAssemblyFromFile(param, sources);

            if (results.Errors.Count > 0)
            {
                foreach(CompilerError Comperr in results.Errors)
                {
                    errors = errors + "Line " + Comperr.Line + ": " + Comperr.ErrorText + "\n";
                }
                    return false;
            }
            return true;

        }

        bool CompileVB(string[] sources, string dllName)
        {
            string Output = dllName;
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CodeDomProvider dom = CodeDomProvider.CreateProvider("VBasic");


            System.CodeDom.Compiler.CompilerParameters param = new CompilerParameters();
            param.GenerateExecutable = false;
            param.OutputAssembly = Output;
            CompilerResults results = dom.CompileAssemblyFromSource(param, sources);
            
            if (results.Errors.Count > 0)
            {
                return false;
            }
            return true;

        }
        private static string[] GetScripts(string path,string type)
        {
            ArrayList list = new ArrayList();
            //list.AddRange(Directory.GetFiles(path, type));
            GetScripts(list, path, type);

            return (string[])list.ToArray(typeof(string));
        }

        private static void GetScripts(ArrayList list, string path, string type)
        {
            foreach (string dir in Directory.GetDirectories(path))
                GetScripts(list, dir, type);

            list.AddRange(Directory.GetFiles(path, type));
        }
    }
}
