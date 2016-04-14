using System;

namespace Internship2016
{
	public abstract class MoveInfo
	{
		public byte[] AppliedFor { get; private set; }

		protected MoveInfo(byte[] applyedFor)
		{
			AppliedFor = applyedFor;
		}
	}

	class PlayInfo: MoveInfo
	{
		public PlayInfo(byte[] applyedFor): base(applyedFor)
		{

		}
	}

	class DropInfo: MoveInfo
	{
		public DropInfo(byte[] applyedFor): base(applyedFor)
		{

		}
	}

	class TellColorInfo: MoveInfo
	{

		public Color Color { get; private set; }

		public TellColorInfo(byte[] applyedFor, Color color): base(applyedFor)
		{
			Color = color;
		}

	}

	class TellRankInfo: MoveInfo
	{

		public byte Rank { get; private set; }

		public TellRankInfo(byte[] applyedFor, byte rank): base(applyedFor)
		{
			Rank = rank;
		}
	}
}