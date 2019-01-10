using System;
using System.Collections.Generic;
using System.Text;
using Game.Console_Framework;
using Game.UI;
using Game.Applicazione;
namespace Game
{
	
     public abstract class Sessione
    {
		//Every time this char is pressed on a prompt the game should be closed
		protected char exitChar;
		protected char command;

		protected char getExitChar()
		{

			return this.exitChar;
		}

		public void setExitChar(char _exitChar)
		{

			this.exitChar = _exitChar;
		}
	}

	 
}
