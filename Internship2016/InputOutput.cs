using System;
using System.IO;

namespace Internship2016
{
	public interface IInputOutput
	{
		string ReadNextLine();
		void PrintGameResult(int turn, int score, int withRisk);
	}

	public class InputOutput: IInputOutput
	{

		StreamReader reader;

		public InputOutput ()
		{
			reader = File.OpenText ("2-big.in");
		}


		public string ReadNextLine()
		{
			//var nextCommand = Console.ReadLine ();
			var nextCommand = reader.ReadLine ();
			return nextCommand;
		}

		public void PrintGameResult(int turn, int score, int withRisk)
		{
			Console.WriteLine ("Turn: {0}, cards: {1}, with risk: {2}", turn, score, withRisk);
		}
	}
}