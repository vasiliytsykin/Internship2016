namespace Internship2016
{
	class Program
	{
		public static void Main (string[] args)
		{
			var IO = new InputOutput ();
			var game = new Game ();
			var programShell = new ProgramShell (IO, game);
			programShell.Start ();
		}
	}
}