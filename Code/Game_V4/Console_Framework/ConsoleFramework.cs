using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.Console_Framework
{
    class ConsoleFramework
    {
		public static void writeConsole(string _output, Boolean _addline = true)
		{

			if (_addline)
			{
				Console.WriteLine(_output);
			}
			else
			{
				Console.Write(_output);
			}
		}

		public static string readConsole()
		{
			

			return Console.ReadLine();
		}

    }
}
