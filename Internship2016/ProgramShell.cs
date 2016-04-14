using System;

namespace Internship2016
{
	
	public class ProgramShell
	{
		private IGame game;
		private IInputOutput IO;
		private bool currentGameFinished;
		private bool exit;

		public ProgramShell (IInputOutput IO, IGame game)
		{
			this.game = game;
			this.IO = IO;
		}

		private void StartNewGame (Card[] deck)
		{
			currentGameFinished = false;
			game.StartNewGame (deck);
		}

		private void FinishGame(int turn, int score, int withRisk)
		{
			currentGameFinished = true;
			IO.PrintGameResult (turn, score, withRisk);
		}

		private void MakeNextMove (MoveInfo move)
		{
			if (!currentGameFinished) 
			{
				try 
				{
					game.MakeMove (move);
				} 
				catch (GameOverException) 
				{
					FinishGame (game.Result.Turn, game.Result.Score, game.Result.WithRisk);
				}
			}
		}

		private void Exit()
		{
			exit = true;
		}

		public void Start()
		{
			while (!exit) 
			{
				var nextCommand = IO.ReadNextLine ();

				if (String.IsNullOrEmpty(nextCommand))
					Exit ();
				else if (nextCommand.StartsWith ("Start new game"))
					StartNewGame (DeckParser.Parse (nextCommand));
				else
					MakeNextMove (MoveParser.Parse (nextCommand));
			}
		}			
	}
}