using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.Applicazione.Commands
{
    class LoadGame : Command
    {
		protected override void Exec()
		{
			Console_Framework.ConsoleFramework.writeConsole("Dovrei caricare la partita. TODO");
		}

		protected override void newChild()
		{
			this.commandName = "Carica Partita";
		}

		protected override void toString()
		{
			UI.GameUIHandler.Write(this.commandName);
		}
	}

}
