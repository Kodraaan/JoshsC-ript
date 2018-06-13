using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshScript {
	class Program {
		static void Main(string[] args) {
			try {
				byte[] data = System.IO.File.ReadAllBytes(args[0]);
				ArrayList tokens = Tokenizer.Tokenize(data);
				Interpreter.Execute(tokens);

			} catch (Exception e) {
				Console.WriteLine(e.ToString());
			}
		}
	}
}
