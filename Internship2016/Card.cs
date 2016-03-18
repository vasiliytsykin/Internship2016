namespace Internship2016
{
	public enum Color
	{
		Red,
		Green,
		Blue,
		White,
		Yellow
	}

	public class Card
	{
		public readonly Color Color;
		public readonly byte Rank;

		public Card (Color color, byte rank)
		{
			Color = color;
			Rank = rank;
		}
	}
}