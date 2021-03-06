﻿using System;
using System.IO;
using System.Text;

namespace VaultBreaker.Helpers
{
	public class DebugFunctions
	{
		public static void PrintByteArray(byte[] thebizz, string filename)
		{
			string path = filename + "_output.txt";
			string strBytes = "";
			Console.WriteLine("Debugging output");
			//for (int i = 0; i < thebizz.Length; ++i)
			//{
			//		if (!File.Exists(path))
			//		{
			//			using (StreamWriter sw = File.CreateText(path))
			//			{
			//				sw.WriteLine("Output");
			//			}
			//		}
			//		using (StreamWriter sw = File.AppendText(path))
			//		{
			//			//sw.Write("{0:X2}", thebizz[i]);
			//			File.WriteAllBytes(path, thebizz);
			//		}
			//	}
			File.WriteAllBytes(path, thebizz);
			Console.WriteLine("\n*******************");
		}

		public static byte[] ReplaceBytes(byte[] src, byte[] search, byte[] repl)
		{
			byte[] dst = null;
			int index = FindBytes(src, search);
			if (index >= 0)
			{
				dst = new byte[src.Length - search.Length + repl.Length];
				// before found array
				Buffer.BlockCopy(src, 0, dst, 0, index);
				// repl copy
				Buffer.BlockCopy(repl, 0, dst, index, repl.Length);
				// rest of src array
				Buffer.BlockCopy(
					src,
					index + search.Length,
					dst,
					index + repl.Length,
					src.Length - (index + search.Length));
			}
			return dst;
		}
		public static void writeDebug(string message, bool debug)
		{
            if (debug) 
			    Console.WriteLine("[DEBUG] {0}",message);
		}

		public static string ReturnCleanASCII(string s)
		{
			StringBuilder sb = new StringBuilder(s.Length);
			foreach (char c in s)
			{
				if ((int)c > 127) // you probably don't want 127 either
					continue;
				if ((int)c < 32)  // I bet you don't want control characters 
					continue;
				if (c == ',')
					continue;
				if (c == '"')
					continue;
				sb.Append(c);
			}
			return sb.ToString();
		}

		public static int FindBytes(byte[] src, byte[] find)
		{
			int index = -1;
			int matchIndex = 0;
			// handle the complete source array
			for (int i = 0; i < src.Length; i++)
			{
				if (src[i] == find[matchIndex])
				{
					if (matchIndex == (find.Length - 1))
					{
						index = i - matchIndex;
						break;
					}
					matchIndex++;
				}
				else if (src[i] == find[0])
				{
					matchIndex = 1;
				}
				else
				{
					matchIndex = 0;
				}

			}
			return index;
		}
	}
}
