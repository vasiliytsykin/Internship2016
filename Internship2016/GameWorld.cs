using System;
using System.Collections.Generic;
using System.Linq;


namespace Internship2016
{

	public class GameWorld
	{
		GameData gameData;
		IGameLogic gameLogic;

		public event Action<int, int, int> GameOver;

		public GameWorld ()
		{
			gameData = new GameData ();
			gameLogic = new GameLogic ();
		}


	}
}