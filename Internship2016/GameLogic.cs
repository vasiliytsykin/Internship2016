using System;
using System.Linq;
using System.Collections.Generic;

namespace Internship2016
{
	
	public class GameLogic
	{

		Dictionary<MoveType, Func<Move, GameData, bool>> actions;

		public GameLogic ()
		{
			actions = new Dictionary<MoveType, Func<Move, GameData, bool>> ();
			SetMoves ();
		}
			
		void SetMoves()
		{
			actions [MoveType.Play] = PlayCard;
			actions [MoveType.Drop] = DropCard;
			actions [MoveType.TellColor] = Tell;
			actions [MoveType.TellRank] = Tell;
		}

		public void DealCards(Card[] newDeck, int handSize, GameData gameData)
		{

			for (int i = 0; i < handSize; i++) 
			{
				gameData.CurrentPlayer.Add (newDeck [i]);
				gameData.NextPlayer.Add (newDeck [i + handSize]);
			}

			for (int i = handSize * 2; i < newDeck.Length; i++)
				gameData.Deck.Enqueue (newDeck [i]);
		}

		public bool TryMakeMove(Move move, GameData gameData)
		{
			gameData.CurrentTurn++;
			bool successfulMove = MakeMove (move, gameData);
			bool tableIsFull  = gameData.Table.Values.All (v => v == 5);
			bool continueGame = successfulMove && gameData.Deck.Count != 0 && !tableIsFull;
			SwitchPlayer (gameData);
			return continueGame;
		}

		private bool MakeMove(Move move, GameData gameData)
		{
			var action = actions [move.KindOfMove];
			return action (move, gameData);
		}

		private bool PlayCard(Move move, GameData gameData)
		{
			var cardIndex = move.AppliedFor [0];
			var cardToPlay = gameData.CurrentPlayer [cardIndex];
			if (gameData.Table [cardToPlay.Color] == cardToPlay.Rank - 1) 
			{
				gameData.Table [cardToPlay.Color]++;
				DropAndTakeCard (cardIndex, gameData);
				gameData.CurrentScore++;
				if (!gameData.KnownColor.Contains (cardToPlay) || !gameData.KnownRank.Contains (cardToPlay))
					gameData.WithRiskMoves++;
				return true;
			}
			return false;
		}

		private bool DropCard(Move move, GameData gameData)
		{
			var cardIndex = move.AppliedFor [0];
			if (gameData.CurrentPlayer.Count >= cardIndex && gameData.Deck.Count != 0) 
			{
				DropAndTakeCard (cardIndex, gameData);
				return true;
			}
			return false;
		}

		private bool Tell(Move move, GameData gameData)
		{
			if (move.KindOfMove == MoveType.TellColor) 
			{
				var cardColors = gameData.NextPlayer.Select (c => c.Color).ToArray ();
				if (CheckParameter (cardColors, move.Color.Value, move.AppliedFor)) 
				{
					MarkCards (move.AppliedFor, gameData.KnownColor, gameData);
					return true;
				}
			} 
			else 
			{
				var cardRanks = gameData.NextPlayer.Select (c => c.Rank).ToArray ();
				if (CheckParameter (cardRanks, move.Rank.Value, move.AppliedFor))
				{
					MarkCards (move.AppliedFor, gameData.KnownRank, gameData);
					return true;
				}
			}
			return false;
		}


		private void MarkCards(byte[] indexes, HashSet<Card> markType, GameData gameData)
		{
			foreach (var index in indexes)
				markType.Add (gameData.NextPlayer [index]);
		}

		private bool CheckParameter<T>(T[] cardValues, T moveValue, byte[] appliedFor)
		{
			var indexes = new List<byte> ();
			for (byte i = 0; i < cardValues.Length; i++)
				if (cardValues [i].Equals(moveValue))
					indexes.Add (i);
			if (indexes.SequenceEqual (appliedFor))
				return true;
			return false;

		}

		private void SwitchPlayer(GameData gameData)
		{
			var temp = gameData.CurrentPlayer;
			gameData.CurrentPlayer = gameData.NextPlayer;
			gameData.NextPlayer = temp;
		}

		private void DropAndTakeCard(byte index, GameData gameData)
		{
			gameData.CurrentPlayer.RemoveAt (index);
			var nextCard = gameData.Deck.Dequeue ();
			gameData.CurrentPlayer.Add (nextCard);
		}
			
	}
}