namespace Internship2016
{
	
	public interface IGame
	{
		void InitNewGame (Card[] deck);
		bool TryMakeMove (Move move);
		GameResult Result { get; }
	}

	public class Game: IGame
	{

		GameData gameData;
		GameLogic gameLogic;

		public Game ()
		{
			gameLogic = new GameLogic ();
		}

		public void InitNewGame (Card[] deck)
		{
			gameData = new GameData ();
			gameLogic.DealCards (deck, 5, gameData);
		}

		public bool TryMakeMove (Move move)
		{
			return gameLogic.TryMakeMove (move, gameData);
		}

		public GameResult Result 
		{
			get { return new GameResult (gameData.CurrentTurn, gameData.CurrentScore, gameData.WithRiskMoves); }
		}
	}
}