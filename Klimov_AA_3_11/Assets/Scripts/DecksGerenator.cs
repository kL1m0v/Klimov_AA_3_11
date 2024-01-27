using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
using Cards.ScriptableObjects;
using UnityEngine;

namespace Cards
{
		

	public class DecksGerenator : MonoBehaviour
	{
		[SerializeField]
		private StartingDeck _startingDeck;
		[SerializeField]
		private GameObject _cardPrefab;
		private Material _baseMaterial;
		private PositionCollector _containerOfPosition;
		
		public void Initialize()
		{
			_containerOfPosition = GetComponent<PositionCollector>();
			_baseMaterial = new Material(Shader.Find("TextMeshPro/Sprite"));
			Generate(DecksConteiner.Decks[Player.PlayerOne], PositionKeeper.DeckPosition[Player.PlayerOne], Player.PlayerOne);
			Generate(DecksConteiner.Decks[Player.PlayerTwo], PositionKeeper.DeckPosition[Player.PlayerTwo], Player.PlayerTwo);
		}
		
		/// <summary>
		/// Генерирует колоду
		/// </summary>
		/// <param name="deck">колода, в которую нужно положить карты</param>
		/// <param name="tr">место, где будут распологаться карты(колода)</param>
		/// <param name="player">владелец карт</param>
		private void Generate(List<GameObject> deck, Transform tr, Player player)
		{
			foreach(uint id in _startingDeck.id)
			{
				CardPropertiesData cardPropData = CardsCollection._cardsCollection.First(c => c.Id == id);
				GameObject card = Instantiate(_cardPrefab, tr.position, tr.rotation, tr);
				card.GetComponent<Card>().HideOrShowFrondCard();
				Material picture = new Material(_baseMaterial);
				picture.mainTexture = cardPropData.Texture;
				picture.renderQueue = 2999;
				card.GetComponent<Card>().Configuration(picture, cardPropData, CardUtility.GetDescriptionById(id), CardStateType.InDeck, player);
				deck.Add(card);
			}
			Shuffle(deck);
		}
		
		private void Shuffle(List<GameObject> cards)
		{
			int n = cards.Count;
			while (n > 1)
			{
				n--;
				int k = Random.Range(0, n + 1);
				GameObject value = cards[k];
				cards[k] = cards[n];
				cards[n] = value;
			}
		}
	}
}
