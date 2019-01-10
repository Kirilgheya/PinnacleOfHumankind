
using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
	

    public class Partita : Sessione 
    {
		

		//AC This must initialize every low level settings for the game
		public static Partita createPartita()
		{
			Boolean gameIsOn = true;
			Partita game = new Partita();
			game.initPartita();
			

			while(gameIsOn)
			{

				game.showMainMenu();
				game.getUserCommand();
				game.elaborateUserCommand(out gameIsOn);
			}
			return game;
		}

		private void elaborateUserCommand(out Boolean _gameisOn)
		{
			
			Applicazione.Command commandToExecute = Console_Framework.ComandiEnum.getCommandByKey(this.command);
			if( commandToExecute is Applicazione.Commands.ExitCommand)
			{
				_gameisOn = false;
			}
			else
			{
				commandToExecute.ExecuteCommand();
				_gameisOn = true;
			}
			
		}

		private void getUserCommand()
		{
			this.command = UI.GameUIHandler.readUserInput();
		}

		private void showMainMenu()
		{
			

			//debug purposes: Console_Framework.ComandiEnum.printKeybinds();
			UI.GameUIHandler.showMainMenu();


		}

		private void initPartita()
		{
			Applicazione.ParametriUtente.resetToGlobal();
			Console_Framework.ComandiEnum.addOrSubstituteCommand('n', 2);
			Console_Framework.ComandiEnum.addOrSubstituteCommand('o', 3);
			Console_Framework.ComandiEnum.addOrSubstituteCommand('l', 4);
			this.setExitChar('q');
		}
    }
}
