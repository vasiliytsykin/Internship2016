namespace Internship2016
{

	public class Card
	{
		public readonly Color color;
		public readonly byte rank;
		public readonly int id;

		public Card (Color color, byte rank, int id)
		{
			this.color = color;
			this.rank = rank;
			this.id = id;
		}

		public override bool Equals (object obj)
		{
			var anotherCard = obj as Card;
			if (anotherCard != null)
				return id == anotherCard.id;
			return false;
		}

		public override int GetHashCode ()
		{
			return id;
		}
	}
}