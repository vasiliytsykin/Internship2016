using System;
using System.IO;

namespace Internship2016
{
	public interface IInputOutput
	{
		void Read();
		void PrintGameResult(int turn, int score, int withRisk);
		event Action<Card[]> StartNewGame;
		event Action<Move> MakeNextMove;
		event Action Exit;
	}

	public class InputOutput: IInputOutput
	{

		public event Action<Card[]> StartNewGame;
		public event Action<Move> MakeNextMove;
		public event Action Exit;

		IConverter converter;

		public InputOutput (IConverter converter)
		{
			this.converter = converter;
		}


		public void Read()
		{
			var nextCommand = Console.ReadLine ();		
			if (String.IsNullOrEmpty(nextCommand))
				Exit ();
			else if (nextCommand.StartsWith ("S"))
				StartNewGame (converter.ConvertToDeck (nextCommand));
			else
				MakeNextMove (converter.ConvertToMove (nextCommand));
		}

		public void PrintGameResult(int turn, int score, int withRisk)
		{
			Console.WriteLine ("Turn: {0}, cards: {1}, with risk: {2}", turn, score, withRisk);
		}
	}
}