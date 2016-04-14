namespace Internship2016
{
	public class GameResult
	{
		public readonly int Turn;
		public readonly int Score;
		public readonly int WithRisk;

		public GameResult (int turn, int score, int withRisk)
		{
			Turn = turn;
			Score = score;
			WithRisk = withRisk;
		}
	}
}