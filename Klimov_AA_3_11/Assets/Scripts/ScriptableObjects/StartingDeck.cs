using System.Collections;
using System.Collections.Generic;
using Cards.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;


namespace Cards.ScriptableObjects
{
	[CreateAssetMenu(fileName = "NewStartingDeck", menuName = "CardConfigs/New Starting Deck")]
	public class StartingDeck : ScriptableObject
	{
		[SerializeField, Tooltip("Выбираем в инспекторе ID тех карт, которые ходит добавить в стартовую колоду")]
		public List<uint> id = new(30);
	}
	
}
