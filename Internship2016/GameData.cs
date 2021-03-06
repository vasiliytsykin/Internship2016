﻿using System.Collections.Generic;

namespace Internship2016
{
	public class GameData
	{

		public Queue<Card> Deck { get; set; }
		public List<Card> CurrentPlayer { get; set; }
		public List<Card> NextPlayer { get; set; }
		public Dictionary<Color, byte> Table { get; set; }
		public int Turn { get; set; }
		public int Score { get; set; }
		public int WithRisk { get; set; }

		public Dictionary<Card, List<Color>> PossibleColor { get; set; }
		public Dictionary<Card, List<byte>> PossibleRank { get; set; }

		public GameData()
		{
			Deck = new Queue<Card> ();
			CurrentPlayer = new List<Card> ();
			NextPlayer = new List<Card> ();
			Table = new Dictionary<Color, byte>() {
				{Color.Red, 0},
				{Color.Green, 0},
				{Color.Blue, 0},
				{Color.White, 0},
				{Color.Yellow, 0}
			};
			PossibleColor = new Dictionary<Card, List<Color>> ();
			PossibleRank = new Dictionary<Card, List<byte>> ();
		}
	}
}