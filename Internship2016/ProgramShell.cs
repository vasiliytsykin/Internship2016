namespace Internship2016
{
	public class ProgramShell
	{
		private GameData gameData;
		private GameLogic gameLogic;
		private InputOutput IO;
		private bool exit;

		public ProgramShell ()
		{
			gameLogic = new GameLogic ();
			IO = new InputOutput ();
			IO.StartNewGame += StartGame;
			IO.MakeNextMove += MakeNextMove;
			IO.Exit += Exit;
		}

		void StartGame (Card[] deck)
		{
			gameData = new GameData ();
			gameLogic.DealCards (deck, 5, gameData);
		}

		void FinishGame(int turn, int score, int withRisk)
		{
			IO.PrintGameResult (turn, score, withRisk);
		}

		void MakeNextMove (Move move)
		{
			if (!gameLogic.TryMakeMove (move, gameData)) 
				FinishGame (gameData.CurrentTurn, gameData.CurrentScore, gameData.WithRiskMoves);
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