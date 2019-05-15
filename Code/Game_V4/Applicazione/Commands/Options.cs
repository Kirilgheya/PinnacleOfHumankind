using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.Applicazione.Commands
{
    class Options : Command
    {
		protected override void Exec()
		{
			Console_Framework.ConsoleFramework.writeConsole("Dovrei aprire le opzioni. TODO");
			
		}


		protected override void newChild()
		{
			this.commandName = "Opzioni";
		}

		protected override void toString()
		{
			UI.GameUIHandler.Write(this.commandName);
		}
	}
}
