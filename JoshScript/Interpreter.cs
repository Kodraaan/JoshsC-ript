using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshScript {
	class Interpreter {
		public static void Execute(ArrayList tokens) {
			Stack<int> loops = new Stack<int>();
			LinkedList<int> tape = new LinkedList<int>();
			LinkedListNode<int> curValue;

			tape.AddFirst(0);
			curValue = tape.First;

			for (int i = 0; i < tokens.Count; i++) {
				Token token = (Token) tokens[i];

				switch (token) {
					case Token.JOSH:
						curValue.Value++;
						break;
					case Token.josh:
						curValue.Value--;
						break;
					case Token.Josh:
						curValue.Value *= 2;
						break;
					case Token.josH:
						curValue.Value /= 2;
						break;
					case Token.JOsh:
						Console.Write((char)curValue.Value);
						break;
					case Token.JoSH:

						// TODO: only grab one number, not the whole line
						string inp = Console.ReadLine();
						int num;
						while (!Int32.TryParse(inp, out num)) {
							inp = Console.ReadLine();
						}
						
						curValue.Value = num;
						break;
					case Token.JOsH:
						int c = Console.Read();
						while (c == '\n' || c == '\r') {
							c = Console.Read();
						}

						curValue.Value = c;
						break;
					case Token.jOsh:
						curValue.Value = 0;
						break;
					case Token.JOSh:
						curValue.Value *= curValue.Value;
						break;
					case Token.JosH:
						Console.Write(curValue.Value);
						break;
					case Token.joSH:
						if (curValue.Next == null) {
							tape.AddLast(0);
						}

						curValue = curValue.Next;
						break;
					case Token.joSh:
						if (curValue.Previous == null) {
							tape.AddBefore(curValue, 0);
						}

						curValue = curValue.Previous;
						break;
					case Token.JoSh:
						if (curValue.Value == 0) {
							while (i < tokens.Count && (Token) tokens[i++] != Token.jOsH);
						} else {
							loops.Push(i);
						}

						break;
					case Token.jOsH:
						if (loops.Count == 0) {
							throw new Exception("JoshScript Error: Attempt to end non-existent loop");
						}

						if (curValue.Value == 0) {
							loops.Pop();
						} else {
							i = loops.Peek();
						}

						break;
					default:
						throw new Exception("JoshScript Error: Attempt to use undefined josh command (" + token.ToString() + ")");
				}
			}
		}
	}
}
