using System;
using System.Collections.Generic;
using System.Text;
using MainGame.Applicazione;

namespace MainGame.Console_Framework
{
    class ComandiEnum 
    {

		public static ValueTuple<Boolean, Boolean> keyIsBound(char _key,int _function, Boolean _printError = false)
		{
			ValueTuple<Boolean,Boolean> isBound;
			Boolean functionIsBound = false;
			Boolean keyIsBound = false;
			Dictionary<char, int>.Enumerator keybindEnum;
			int relatedFunction=-1;

			keybindEnum = ParametriUtente.keybind.GetEnumerator();

			while (keybindEnum.MoveNext())
			{
				KeyValuePair<char, int> current = keybindEnum.Current;
				if(current.Value == _function)
				{
					if (_printError)
					{
						UI.GameUIHandler.Write("ERROR: Il comando è già assegnato al tasto " + current.Key);
					}

					if(current.Key != _key)
					{
						functionIsBound = true;
					}
					
					break;
				}

			}

			if(ParametriUtente.keybind.TryGetValue(_key,out relatedFunction))
			{

				keyIsBound = true;
			}

			isBound.Item1 = keyIsBound;
			isBound.Item2 = functionIsBound;

			return isBound;
		}

		public static void addOrSubstituteCommand(char _key, int _function)
		{
			int sourceFunctionId= -1;
			int targetFunctionId= -2;

			ValueTuple<Boolean, Boolean> isBound = ComandiEnum.keyIsBound(_key, _function); 
			//<keyisBound,functionisBound>

			if(isBound.Item1 && isBound.Item2)
			{
				ParametriUtente.keybind.TryGetValue(_key,out sourceFunctionId);
				swapKeybinds(sourceFunctionId, targetFunctionId);
			}
			else
			{
				if(isBound.Item1 && !isBound.Item2)
				{
					//Key is bound, func is not
					unbindKey(_key);
				}
				else if(isBound.Item2 && !isBound.Item1)
				{
					//Func is bound, key is not
					char otherKey = findKeyByFunction(_function);
					unbindKey(otherKey);
				}

				addKeyBind(_key, _function);
			}
		}


		public static char findKeyByFunction(int _functionId)
		{

			char key = '-';
			Dictionary<char, int>.Enumerator keybindEnum;

			keybindEnum = ParametriUtente.keybind.GetEnumerator();

			while (keybindEnum.MoveNext())
			{

				KeyValuePair<char, int> current = keybindEnum.Current;
				if(current.Value == _functionId)
				{

					key = current.Key;
					break;
				}
			}


			
			return key;
		}

		public static void printKeybinds()
		{
			 Dictionary<char,int>.Enumerator keybindEnum;

			keybindEnum = ParametriUtente.keybind.GetEnumerator();

			while(keybindEnum.MoveNext())
			{
				KeyValuePair<char,int> current = keybindEnum.Current;
				Command function = new Applicazione.Commands.CommandNotFound();
				ParametriUtente.relatedFunction.TryGetValue(current.Value,out function);
				UI.GameUIHandler.Write("Tasto: " + current.Key + " con funzione:(n°:"+current.Value+")"+ function.getName());
			
			}
		}

		public static Command getCommandByKey(char _key)
		{
			int functionId = 0;
			Command command = new Applicazione.Commands.CommandNotFound();
			ParametriUtente.keybind.TryGetValue(_key,out functionId);
			ParametriUtente.relatedFunction.TryGetValue(functionId, out command);

			return command;

		}


		private static void swapKeybinds(int _sourceFunctionId, int _targetFunctionId)
		{

			Command sourceFunc;
			Command targetFunc;
			
			ParametriUtente.relatedFunction.TryGetValue(_sourceFunctionId, out sourceFunc);
			ParametriUtente.relatedFunction.TryGetValue(_targetFunctionId, out targetFunc);

			ParametriUtente.relatedFunction.Remove(_sourceFunctionId);
			ParametriUtente.relatedFunction.Remove(_targetFunctionId);
			ParametriUtente.relatedFunction.Add(_sourceFunctionId, targetFunc);
			ParametriUtente.relatedFunction.Add(_targetFunctionId, sourceFunc);
		}

		/** deprecated
		 * private static void substituteValue(int _key, int _value)
		{
			ParametriUtente.commandCode.Remove(_key);
			ParametriUtente.commandCode.Add(_key, _value);
		}

		private static void substituteValue(char _key, int _value)
		{
			ParametriUtente.keybind.Remove(_key);
			ParametriUtente.keybind.Add(_key, _value);
		}

		private static void substituteValue(int _key, Command _value)
		{
			ParametriUtente.relatedFunction.Remove(_key);
			ParametriUtente.relatedFunction.Add(_key, _value);
		}
		**/
		public static void unbindKey(char _key)
		{
			
			ParametriUtente.keybind.Remove(_key);
			
		}

		public static void addKeyBind(char _key, int _functionId )
		{

			ParametriUtente.keybind.Add(_key, _functionId);
		}

	}
}
