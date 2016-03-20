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
			IO.StartNewGame += StartGame;
			IO.MakeNextMove += MakeNextMove;
			IO.Exit += Exit;
		}

		private void StartGame (Card[] deck)
		{
			currentGameFinished = false;
			game.InitNewGame (deck);
		}

		private void FinishGame(int turn, int score, int withRisk)
		{
			currentGameFinished = true;
			IO.PrintGameResult (turn, score, withRisk);
		}

		private void MakeNextMove (Move move)
		{
			if (!currentGameFinished && !game.TryMakeMove (move)) 
				FinishGame (game.Result.Turn, game.Result.Score, game.Result.WithRisk);
		}

		private void Exit()
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