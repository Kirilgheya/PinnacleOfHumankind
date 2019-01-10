using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Applicazione.Commands
{
    class ExitCommand : Command
    {
		protected override void Exec()
		{
			UI.GameUIHandler.Write("Game is Shutting down.....");
		

		}

		protected override void newChild()
		{
			this.commandName = "Esci dal gioco";
		}

		protected override void toString()
		{
			UI.GameUIHandler.Write(this.commandName);
		}
	}
}
