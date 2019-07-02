using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.Applicazione.Commands
{
    class Help : Command
    {
		protected override void Exec()
		{
			Console_Framework.ComandiEnum.printKeybinds();
		}

		protected override void newChild()
		{
			this.commandName = "Mostra comandi";
		}

		protected override void toString()
		{
			UI.GameUIHandler.Write(this.commandName);
		}
	}
}
