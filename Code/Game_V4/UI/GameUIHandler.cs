using System;
using System.Collections.Generic;
using System.Text;
using MainGame.Console_Framework;

namespace MainGame.UI
{
    class GameUIHandler : Sessione
    {
		private static char lineChar_x = '-';
		private static char lineChar_y = '|';
		private static string mainMenuTitle = "The Amazin' Main Menu";

		public static void showMainMenu()
		{

			printLine();
			ConsoleFramework.writeConsole(mainMenuTitle);
			printLine();
		}

		public static void Write(string _message)
		{
			printLine();
			ConsoleFramework.writeConsole(_message);
			printLine();
		}

		private static void printLine()
		{

			ConsoleFramework.writeConsole(SysUtils.repeat(lineChar_x,10));
		}

		public static char readUserInput()
		{
			ConsoleFramework.writeConsole("Waiting for User Command");
			
			string input = ConsoleFramework.readConsole();
			while(input.Length > 1 )
			{
				ConsoleFramework.writeConsole("Comando nel formato errato, usare un solo carattere (Es:q)");
				input = ConsoleFramework.readConsole();
			}
			return Convert.ToChar(input);
		}
    }
}
