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
		public readonly int ID;

		public Card (Color color, byte rank, int uniqueID)
		{
			Color = color;
			Rank = rank;
			ID = uniqueID;
		}

		public override bool Equals (object obj)
		{
			var anotherCard = obj as Card;
			if (anotherCard != null)
				return ID == anotherCard.ID;
			return false;
		}

		public override int GetHashCode ()
		{
			return ID;
		}
	}
}