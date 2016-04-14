using System;
using System.Linq;

namespace Internship2016
{

	static class DeckParser
	{
		
		public static Card[] Parse (string input)
		{
			int id = 0;
			return input.Split (' ')
				.Skip (5)
				.Select (card => {
					var color = ColorParser.Parse (card [0].ToString()); 
					var rank = byte.Parse (card [1].ToString()); 
					return new Card (color, rank, id++);
				}).ToArray ();
		}
	}

	static class MoveParser
	{
		
		public static MoveInfo Parse (string input)
		{
			var parts = input.Split (' ');

			if (parts.Length == 3) 
				return ParsePlayAndDropCommands (parts);		
			else 
				return ParseTellCommands (parts);						
		}

		static MoveInfo ParsePlayAndDropCommands(string[] commandParts)
		{
			var moveType = commandParts [0];
			var appliedFor = commandParts.Skip (2).Select (x => byte.Parse (x)).ToArray ();
			if (moveType == "Play")
				return new PlayInfo (appliedFor);
			else
				return new DropInfo (appliedFor);
		}

		static MoveInfo ParseTellCommands(string[] commandParts)
		{
			byte rank = 0;
			var appliedFor = commandParts.Skip(5).Select (x => byte.Parse (x)).ToArray ();
			bool isNumber = byte.TryParse (commandParts [2], out rank);
			if (isNumber)
				return new TellRankInfo (appliedFor, rank);
			else 
			{
				var color = ColorParser.Parse (commandParts [2]);
				return new TellColorInfo (appliedFor, color);
			}
		}
			
	}


	static class ColorParser
	{

		public static Color Parse(string colorStr)
		{

			var firstChar = colorStr [0];

			if (firstChar == 'R')
				return Color.Red;
			else if (firstChar == 'G')
				return Color.Green;
			else if (firstChar == 'B')
				return Color.Blue;
			else if (firstChar == 'W')
				return Color.White;
			else 
				return Color.Yellow;
			
		}
	}
}