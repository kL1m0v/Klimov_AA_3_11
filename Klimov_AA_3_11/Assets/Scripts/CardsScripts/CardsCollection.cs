using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards.ScriptableObjects;
using UnityEngine;

namespace Cards
{
	
	public static class CardsCollection
	{
		private static CardPackConfiguration[] cardPackConfigurations;
		
		/// <summary>
		/// Коллекция всех карт в игре
		/// </summary>
		public static List<CardPropertiesData> _cardsCollection;
		
		
		public static void Initialize()
		{
			cardPackConfigurations = Resources.LoadAll<CardPackConfiguration>("Decks By Cost");
			_cardsCollection = new();
			Generate();
		}
		private static void Generate()
		{
			foreach(CardPackConfiguration cardPackConfig in cardPackConfigurations)
			{
				_cardsCollection = cardPackConfig.UnionProperties(_cardsCollection).ToList();
			}
		}
	}
}
