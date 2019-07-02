using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.Applicazione
{
    abstract class Command
    {
		protected Boolean errorHashappened;
		protected String stackTrace;
		protected String commandName;

		public Command(String name = "")
		{
			if(name.Length > 0)
			{

				commandName = name;
			}
			else
			{
				this.newChild();
			}
		}

		public String getName()
		{
			return this.commandName;
		}

		public void ExecuteCommand()
		{
			try
			{
				this.Exec();
			}
			catch(Exception e)
			{
				this.errorHashappened = true;
				this.stackTrace = e.StackTrace;
			}
			
		}
		protected abstract void Exec();
		protected abstract void newChild();
		protected abstract void toString();
		
    }
}
