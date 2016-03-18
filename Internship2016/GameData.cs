using System.Collections.Generic;

namespace Internship2016
{
	public class GameData
	{

		public Queue<Card> Deck { get; set; }
		public List<Card> CurrentPlayer { get; set; }
		public List<Card> NextPlayer { get; set; }
		public Dictionary<Color, byte> Table { get; set; }
		public int CurrentTurn { get; set; }
		public int CurrentScore { get; set; }
		public int WithRiskMoves { get; set; }
		public HashSet<Card> KnownColor { get; set; }
		public HashSet<Card> KnownRank { get; set; }

		public GameData()
		{
			Deck = new Queue<Card> ();
			CurrentPlayer = new List<Card> ();
			NextPlayer = new List<Card> ();
			KnownColor = new HashSet<Card>();
			KnownRank = new HashSet<Card>();
			Table = new Dictionary<Color, byte>() {
				{Color.Red, 0},
				{Color.Green, 0},
				{Color.Blue, 0},
				{Color.White, 0},
				{Color.Yellow, 0}
			};
		}
	}
}