namespace Internship2016
{
	public class GameResult
	{
		public int Turn { get; private set;}
		public int Score { get; private set;}
		public int WithRisk { get; private set;}

		public GameResult (int turn, int score, int withRisk)
		{
			Turn = turn;
			Score = score;
			WithRisk = withRisk;
		}
	}
}