using System;
using System.Linq;

namespace Internship2016
{
	public class InputConverter
	{
		
		public Card[] ConvertToDeck(string input)
		{
			return input.Split (' ')
				.Skip (5)
				.Select (card => {
					var color = ConvertColor (card [0].ToString()); 
					var rank = byte.Parse (card [1].ToString()); 
				return new Card (color, rank);
			}).ToArray ();
		}

		public Move ConvertToMove(string input)
		{
			var parts = input.Split (' ');
			MoveType moveType;
			Color? color = null;
			byte? rank = null;
			byte temp = 0;
			byte[] appliedFor;

			if (parts.Length == 3) 
			{
				moveType = ConvertMoveType (parts [0]);
				appliedFor = parts.Skip (2).Select (x => byte.Parse (x)).ToArray ();
			} 
			else 
			{
				moveType = ConvertMoveType (parts [0] + parts [1]);
				appliedFor = parts.Skip(5).Select (x => byte.Parse (x)).ToArray ();
				bool isNumber = byte.TryParse (parts [2], out temp);
				if (isNumber)
					rank = temp;
				else
					color = ConvertColor (parts [2]);
			}

			return new Move (moveType, appliedFor, color, rank);				
		}

		Color ConvertColor(string colorStr)
		{
			var firstChar = colorStr [0];

			switch (firstChar) 
			{
			case 'R':
				return Color.Red;
			case 'G':
				return Color.Green;
			case 'B':
				return Color.Blue;
			case 'W':
				return Color.White;
			default:
				return Color.Yellow;
			}
		}

		MoveType ConvertMoveType(string moveTypeStr)
		{
			switch (moveTypeStr) 
			{
			case "Play":
				return MoveType.Play;
			case "Drop":
				return MoveType.Drop;
			case "Tellcolor":
				return MoveType.TellColor;
			default:
				return MoveType.TellRank;
			}
		}
	}
}