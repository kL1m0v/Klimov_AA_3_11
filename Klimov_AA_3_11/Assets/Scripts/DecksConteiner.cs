using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
	public class DecksConteiner: MonoBehaviour
	{
        private static Dictionary<Player, List<GameObject>> decks;
        
        public static Dictionary<Player, List<GameObject>> Decks { get => decks; set => decks = value; }
       	
        public void Initialize()
		{
			List<GameObject> deck1 = new();
			List<GameObject> deck2 = new();
			List<GameObject> cardInHand1 = new();
			List<GameObject> cardInHand2 = new();
			List<GameObject> cardOnTable1 = new();
			List<GameObject> cardOnTable2 = new();
			Decks = new()
			{
				{Player.PlayerOne, deck1},
				{Player.PlayerTwo, deck2}
			};
			
		}
	}
}