namespace Internship2016
{
	
	public class ProgramShell
	{
		private IGame game;
		private IInputOutput IO;
		private bool exit;

		public ProgramShell (IInputOutput IO, IGame game)
		{
			this.game = game;
			this.IO = IO;
			IO.StartNewGame += StartGame;
			IO.MakeNextMove += MakeNextMove;
			IO.Exit += Exit;
		}

		void StartGame (Card[] deck)
		{
			game.InitNewGame (deck);
		}

		void FinishGame(int turn, int score, int withRisk)
		{
			IO.PrintGameResult (turn, score, withRisk);
		}

		void MakeNextMove (Move move)
		{
			if (!game.TryMakeMove (move)) 
				FinishGame (game.Result.Turn, game.Result.Score, game.Result.WithRisk);
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