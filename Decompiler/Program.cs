using System;
using System.IO;
using Mono.Cecil;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Ast;
using System.Reflection;

namespace Decompiler
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length == 0) {
				help ();
				return;
			}

			string pathToAssembly = args [0];

			Console.WriteLine ("Decompiling ... " + pathToAssembly);

			AssemblyDefinition assemblyDefinition = Mono.Cecil.AssemblyDefinition.ReadAssembly(pathToAssembly);
			AstBuilder decompiler = new AstBuilder(new DecompilerContext(assemblyDefinition.MainModule));
			decompiler.AddAssembly (assemblyDefinition);
			StringWriter output = new StringWriter();
			decompiler.GenerateCode (new PlainTextOutput (output));
			Console.WriteLine (output.ToString ());
		}

		public static void help() {
			Console.Write ("Usage: Decompiler.exe exe|dll\n\n");
		}
	}
}
