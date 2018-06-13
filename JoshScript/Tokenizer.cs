using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace JoshScript {
	public enum Token { JOSH, josh, Josh, josH, JOsh, JoSH, JOsH, jOsh, JOSh, JosH, joSH, joSh, JoSh, jOsH };

	class Tokenizer {
		private static readonly char[] josh = { 'j', 'o', 's', 'h' };

		public static ArrayList Tokenize(byte[] data) {
			ArrayList tokens = new ArrayList();

			for (int i = 0; i < data.Length; i++) {
				char c = (char)data[i];
				if (ignoreChar(c)) continue;

				if (c == '(') { // skip over comment
					while (++i < data.Length && (char)data[i] != ')');
				} else {
					int joshCtr = 0;
					string joshToken = "";

					do { // look for a josh
						c = (char)data[i];

						if (ignoreChar(c)) continue;
						if (Char.ToLower(c) == josh[joshCtr]) {
							joshCtr++;
							joshToken += c;
						}
					} while (++i < data.Length && joshCtr < josh.Length);
					i--;

					if (joshCtr == josh.Length) { // found a josh
						if (Enum.IsDefined(typeof(Token), joshToken)) {
							tokens.Add((Token)System.Enum.Parse(typeof(Token), joshToken));
						} else {
							throw new Exception("JoshScript Error: Attempt to use undefined josh command (" + joshToken + ")");
						}
					}
				}
			}

			return tokens;
		}

		private static Boolean ignoreChar(char c) {
			return c == '\n' || c == '\r' || c == '\t' || c == ';' || c == ' ';
		}

	}
}
