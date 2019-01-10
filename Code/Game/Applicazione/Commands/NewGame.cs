using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Applicazione.Commands
{
	class NewGame : Command
	{
		protected override void Exec()
		{
			Console_Framework.ConsoleFramework.writeConsole("Dovrei caricare una nuova partita. TODO");
		}

		protected override void newChild()
		{
			this.commandName = "Nuova Partita";
		}

		protected override void toString()
		{
			UI.GameUIHandler.Write(this.commandName);
		}
	}
}
