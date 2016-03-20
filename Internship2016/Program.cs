namespace Internship2016
{
	class Program
	{
		public static void Main (string[] args)
		{
			var converter = new InputConverter ();
			var IO = new InputOutput (converter);
			var game = new Game ();
			var programShell = new ProgramShell (IO, game);
			programShell.Start ();
		}
	}
}