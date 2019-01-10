using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Applicazione.Commands
{
    class CommandNotFound : Command
    {

		protected override void Exec()
		{
			int x = 0;
		}


		protected override void newChild()
		{
			this.commandName = "Comando non trovato";
		}

		protected override void toString()
		{
			UI.GameUIHandler.Write(this.commandName);
		}
	}
}
