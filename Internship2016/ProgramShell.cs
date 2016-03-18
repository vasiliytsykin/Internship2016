using System;

namespace Internship2016
{
	public class ProgramShell
	{
		GameWorld currentGame;
		InputOutput IO;
		bool exit;

		public ProgramShell ()
		{
			IO = new InputOutput ();
			IO.StartNewGame += StartGame;
			IO.MakeNextMove += MakeNextMove;
			IO.Exit += Exit;
		}

		void StartGame (Card[] deck)
		{
			currentGame = new GameWorld (deck, 5);
			currentGame.GameOver += FinishGame;
		}

		void FinishGame(int turn, int score, int withRisk)
		{
			IO.Write (turn, score, withRisk);
			currentGame = null;
		}

		void MakeNextMove (Move move)
		{
			if (currentGame != null)
				currentGame.TryMakeMove (move);
		}

		void Exit()
		{
			exit = true;
		}

		public void Start()
		{
			while (!exit) 
			{
				IO.Read ();
			}
		}			
	}
}