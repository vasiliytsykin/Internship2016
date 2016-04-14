using System.Collections.Generic;

namespace Internship2016
{
	static class GameInitializer
	{
		const int handSize = 5;

		public static void Initialize(Card[] deck, GameData gameData)
		{
			InitPossibleCardValues (deck, gameData);

			DealCards (deck, gameData);
		}

		static void InitPossibleCardValues(Card[] deck, GameData gameData)
		{
			foreach (var card in deck) 
			{
				gameData.PossibleColor [card] = 
					new List<Color> { Color.Red, Color.Green, Color.Blue, Color.White, Color.Yellow };
				gameData.PossibleRank [card] = 
					new List<byte> { 1, 2, 3, 4, 5 };
			}
		}

		static void DealCards(Card[] deck, GameData gameData)
		{

			for (int i = 0; i < handSize; i++) 
			{
				gameData.CurrentPlayer.Add (deck [i]);
				gameData.NextPlayer.Add (deck [i + handSize]);
			}

			for (int i = handSize * 2; i < deck.Length; i++)
				gameData.Deck.Enqueue (deck [i]);
		}
	}
}

