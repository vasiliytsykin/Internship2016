using System;
using System.Linq;
using System.Collections.Generic;

namespace Internship2016
{

	public interface IGame
	{
		void StartNewGame (Card[] deck);
		void MakeMove (MoveInfo move);
		GameResult Result { get; }
	}
	
	public class Game: IGame
	{

		const int handSize = 5;
		GameData gameData = new GameData();

		public GameResult Result 
		{
			get { return new GameResult (gameData.Turn, gameData.Score, gameData.WithRisk); }
		}

		public void StartNewGame(Card[] deck)
		{
			gameData = new GameData ();

			InitPossibleCardValues (deck);

			DealCards (deck);
		}

		void InitPossibleCardValues(Card[] deck)
		{
			foreach (var card in deck) 
			{
				gameData.PossibleColor [card] = 
					new List<Color> { Color.Red, Color.Green, Color.Blue, Color.White, Color.Yellow };
				gameData.PossibleRank [card] = 
					new List<byte> { 1, 2, 3, 4, 5 };
			}
		}

		void DealCards(Card[] deck)
		{

			for (int i = 0; i < handSize; i++) 
			{
				gameData.CurrentPlayer.Add (deck [i]);
				gameData.NextPlayer.Add (deck [i + handSize]);
			}

			for (int i = handSize * 2; i < deck.Length; i++)
				gameData.Deck.Enqueue (deck [i]);
		}


		public void MakeMove(MoveInfo moveInfo)
		{
			gameData.Turn++;
			TryMakeMove (moveInfo);
			if (NoMoreMoves ())
				throw new GameOverException ();
			SwitchPlayer ();
		}

		void TryMakeMove(MoveInfo moveInfo)
		{
			try
			{
				ChooseMove (moveInfo);
			}
			catch (IncorrectMoveException) 
			{
				throw new GameOverException ();
			}
		}			

		void ChooseMove(MoveInfo moveInfo)
		{

			if (moveInfo is PlayInfo)
				PlayCard (moveInfo);
			if (moveInfo is DropInfo)
				DropCard (moveInfo);
			if (moveInfo is TellColorInfo)
				TellColor (moveInfo);
			if (moveInfo is TellRankInfo)
				TellRank (moveInfo);
			
		}

		bool NoMoreMoves()
		{
			bool tableIsFull  = gameData.Table.Values.All (v => v == 5);
			return gameData.Deck.Count == 0 || tableIsFull;
		}

		void SwitchPlayer()
		{
			var temp = gameData.CurrentPlayer;
			gameData.CurrentPlayer = gameData.NextPlayer;
			gameData.NextPlayer = temp;
		}

		void PlayCard(MoveInfo moveInfo)
		{
			var cardIndex = moveInfo.AppliedFor [0];
			var cardToPlay = gameData.CurrentPlayer [cardIndex];

			if (IsPlayableCard(cardToPlay)) 
			{
				UpdateScore (cardToPlay);
				DropAndTakeCard (cardIndex);
			} 
			else
				throw new IncorrectMoveException ();
		}

		bool IsPlayableCard(Card card)
		{
			return gameData.Table [card.color] == card.rank - 1;
		}

		void UpdateScore (Card card)
		{
			if (IsRiskyMove (card))
				gameData.WithRisk++;
			
			gameData.Score++;
			gameData.Table [card.color]++;
		}


		bool IsRiskyMove(Card card)
		{
			if (gameData.PossibleRank [card].Count == 1
			   && gameData.Table
				.Where (x => x.Value == card.rank - 1)
				.Select (y => y.Key)
				.Intersect (gameData.PossibleColor [card])
				.SequenceEqual (gameData.PossibleColor [card]))
				return false;
			return true;
		}

		void DropCard(MoveInfo moveInfo)
		{
			var cardIndex = moveInfo.AppliedFor [0];
			DropAndTakeCard (cardIndex);
		}

		void TellColor(MoveInfo moveInfo)
		{
			var tellColorInfo = (TellColorInfo)moveInfo;
			var nextPlayerCardColors = gameData.NextPlayer.Select (c => c.color).ToArray ();
			if (CurrentPlayerTellsTruth (nextPlayerCardColors, tellColorInfo.Color, tellColorInfo.AppliedFor))
				MarkNextPlayerCardsWithColor (tellColorInfo.AppliedFor, tellColorInfo.Color);
			else
				throw new IncorrectMoveException ();
		}
			
		void MarkNextPlayerCardsWithColor(byte[] indexes, Color color)
		{
			for (byte i = 0; i < 5; i++) 
			{
				if (indexes.Any (x => x == i))
					gameData.PossibleColor [gameData.NextPlayer [i]] = gameData.PossibleColor [gameData.NextPlayer [i]].Where (x => x == color).ToList ();
				else
					gameData.PossibleColor [gameData.NextPlayer [i]] = gameData.PossibleColor [gameData.NextPlayer [i]].Where (x => x != color).ToList ();
			}
		}

		void TellRank(MoveInfo moveInfo)
		{
			var tellRankInfo = (TellRankInfo)moveInfo;
			var nextPlayerCardRanks = gameData.NextPlayer.Select (c => c.rank).ToArray ();
			if (CurrentPlayerTellsTruth (nextPlayerCardRanks, tellRankInfo.Rank, tellRankInfo.AppliedFor))
				MarkNextPlayerCardsWithRank (tellRankInfo.AppliedFor, tellRankInfo.Rank);
			else
				throw new IncorrectMoveException ();

		}

		void MarkNextPlayerCardsWithRank(byte[] indexes, byte rank)
		{
			for (byte i = 0; i < 5; i++) 
			{
				if (indexes.Any (x => x == i))
					gameData.PossibleRank [gameData.NextPlayer [i]] = gameData.PossibleRank [gameData.NextPlayer [i]].Where (x => x == rank).ToList ();
				else
					gameData.PossibleRank [gameData.NextPlayer [i]] = gameData.PossibleRank [gameData.NextPlayer [i]].Where (x => x != rank).ToList ();
			}
		}

		bool CurrentPlayerTellsTruth<T>(T[] cardValues, T moveValue, byte[] appliedFor)
		{
			var indexes = new List<byte> ();
			for (byte i = 0; i < cardValues.Length; i++)
				if (cardValues [i].Equals(moveValue))
					indexes.Add (i);
			if (indexes.SequenceEqual (appliedFor))
				return true;
			return false;

		}
			
		void DropAndTakeCard(byte index)
		{
			gameData.CurrentPlayer.RemoveAt (index);
			var nextCard = gameData.Deck.Dequeue ();
			gameData.CurrentPlayer.Add (nextCard);
		}			
	}
}