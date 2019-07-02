using System;
using System.Collections.Generic;
using System.Text;
using MainGame.Console_Framework;
using MainGame.UI;
using MainGame.Applicazione;
namespace MainGame
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
