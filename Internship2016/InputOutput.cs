using System;
using System.IO;

namespace Internship2016
{
	public class InputOutput
	{

		public event Action<Card[]> StartNewGame;
		public event Action<Move> MakeNextMove;
		public event Action Exit;

		string nextCommand;
		InputConverter converter;

		StreamReader reader;

		public InputOutput ()
		{
			reader = File.OpenText("2-big.in");
			converter = new InputConverter ();
		}


		public void Read()
		{
			//nextCommand = Console.ReadLine ();
			nextCommand = reader.ReadLine();
			if (nextCommand == null)
				Exit ();
			else if (nextCommand.StartsWith ("S"))
				StartNewGame (converter.ConvertToDeck (nextCommand));
			else
				MakeNextMove (converter.ConvertToMove (nextCommand));
		}

		public void Write(int turn, int score, int withRisk)
		{
			Console.WriteLine ("Turn: {0}, cards: {1}, with risk: {2}", turn, score, withRisk);
		}
	}
}