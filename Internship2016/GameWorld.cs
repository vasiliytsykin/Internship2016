using System;
using System.Collections.Generic;
using System.Linq;


namespace Internship2016
{

	public class GameWorld
	{
		Queue<Card> deck;
		List<Card> currentPlayer;
		List<Card> nextPlayer;
		Dictionary<Color, byte> table;
		Dictionary<MoveType, Func<Move, bool>> actions;
		bool TableIsFull { get { return table.Values.All (v => v == 5); } }
		int turn = 0;
		int score = 0;
		int withRisk = 0;
		HashSet<Card> knownColor = new HashSet<Card>();
		HashSet<Card> knownRank = new HashSet<Card>();

		public event Action<int, int, int> GameOver;

		public GameWorld (Card[] newDeck, int handSize)
		{
			deck = new Queue<Card> ();
			currentPlayer = new List<Card> ();
			nextPlayer = new List<Card> ();
			table = new Dictionary<Color, byte>() {
				{Color.Red, 0},
				{Color.Green, 0},
				{Color.Blue, 0},
				{Color.White, 0},
				{Color.Yellow, 0}
			};
			DealCards (newDeck, handSize);
			SetMoves ();
		}

		void DealCards(Card[] newDeck, int handSize)
		{

			for (int i = 0; i < handSize; i++) 
			{
				currentPlayer.Add (newDeck [i]);
				nextPlayer.Add (newDeck [i + handSize]);
			}

			for (int i = handSize * 2; i < newDeck.Length; i++)
				deck.Enqueue (newDeck [i]);
		}

		public void TryMakeMove(Move move)
		{
			turn++;
			bool successfulMove = MakeMove (move);
			bool over = !successfulMove || deck.Count == 0 || TableIsFull;
			if (over && GameOver != null)
				GameOver (turn, score, withRisk);
			else
				SwitchPlayer ();
		}

		bool MakeMove(Move move)
		{
			var action = actions [move.KindOfMove];
			return action (move);
		}

		bool PlayCard(Move move)
		{
			var cardIndex = move.AppliedFor [0];
			var cardToPlay = currentPlayer [cardIndex];
			if (table [cardToPlay.Color] == cardToPlay.Rank - 1) 
			{
				table [cardToPlay.Color]++;
				DropAndTakeCard (cardIndex);
				score++;
				if (!knownColor.Contains (cardToPlay) || !knownRank.Contains (cardToPlay))
					withRisk++;
				return true;
			}
			return false;
		}

		bool DropCard(Move move)
		{
			var cardIndex = move.AppliedFor [0];
			if (currentPlayer.Count >= cardIndex && deck.Count != 0) 
			{
				DropAndTakeCard (cardIndex);
				return true;
			}
			return false;
		}

		bool Tell(Move move)
		{
			if (move.KindOfMove == MoveType.TellColor) 
			{
				var cardColors = nextPlayer.Select (c => c.Color).ToArray ();
				if (CheckParameter (cardColors, move.Color.Value, move.AppliedFor)) 
				{
					MarkCards (move.AppliedFor, knownColor);
					return true;
				}
			} 
			else 
			{
				var cardRanks = nextPlayer.Select (c => c.Rank).ToArray ();
				if (CheckParameter (cardRanks, move.Rank.Value, move.AppliedFor))
				{
					MarkCards (move.AppliedFor, knownRank);
					return true;
				}
			}
			return false;
		}


		void MarkCards(byte[] indexes, HashSet<Card> markType)
		{
			foreach (var index in indexes)
				markType.Add (nextPlayer [index]);
		}

		bool CheckParameter<T>(T[] cardValues, T moveValue, byte[] appliedFor)
		{
			List<byte> indexes = new List<byte> ();
			for (byte i = 0; i < cardValues.Length; i++)
				if (cardValues [i].Equals(moveValue))
					indexes.Add (i);
			if (indexes.SequenceEqual (appliedFor))
				return true;
			return false;

		}
			
		void SwitchPlayer()
		{
			var temp = currentPlayer;
			currentPlayer = nextPlayer;
			nextPlayer = temp;
		}

		void DropAndTakeCard(byte index)
		{
			currentPlayer.RemoveAt (index);
			var nextCard = deck.Dequeue ();
			currentPlayer.Add (nextCard);
		}

		void SetMoves()
		{
			actions = new Dictionary<MoveType, Func<Move, bool>> ();
			actions [MoveType.Play] = PlayCard;
			actions [MoveType.Drop] = DropCard;
			actions [MoveType.TellColor] = Tell;
			actions [MoveType.TellRank] = Tell;
		}
	}
}