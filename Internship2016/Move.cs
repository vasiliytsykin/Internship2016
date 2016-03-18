namespace Internship2016
{
	public enum MoveType
	{
		Play,
		Drop,
		TellColor,
		TellRank
	}


	public class Move
	{
		public readonly MoveType KindOfMove;
		public readonly Color? Color;
		public readonly byte? Rank;
		public readonly byte[] AppliedFor;


		public Move (MoveType kindOfMove, byte[] appliedFor, Color? color, byte? rank)
		{
			KindOfMove = kindOfMove;
			AppliedFor = appliedFor;
			Color = color;
			Rank = rank;
		}
	}
}