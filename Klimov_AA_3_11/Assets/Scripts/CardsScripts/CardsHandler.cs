using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class CardsHandler : MonoBehaviour
	{
		private static CardsHandler cardsHandler;
		private static Vector3 initialCardSize;
		private static GameObject[] _attackerAndTarget;

		public static Vector3 InitialCardSize { get => initialCardSize; set => initialCardSize = value; }

		public void Initialize()
		{
			cardsHandler = this;
			InitialCardSize = new Vector3(110, 1, 140);
			_attackerAndTarget = new GameObject[2];

		}

		public void SelectStartingCard(int countCards, Dictionary<Player, List<GameObject>> decks, Player player, List<PositionOnTable> positions)
		{

			for (int i = 0; i < countCards; i++)
			{
				GameObject selectedCard = GetRardomCardFromDeck(DecksConteiner.Decks[player]);
				selectedCard.GetComponent<Card>().StateType = CardStateType.OnTableNotMove;
				PutCardOnTableFromDeck(selectedCard.transform, positions[2 + i].position);
			}
		}

		public void PutCardOnTableFromDeck(Transform cardPosition, Vector3 positionOnTable)
		{
			cardPosition.GetComponent<Card>().StateType = CardStateType.OnTableNotMove;
			StartCoroutine(MoveCard(cardPosition.transform, positionOnTable));
			StartCoroutine(RotateAndShowCard(cardPosition.transform));

		}
		public static void AddCardToHand(Player player)
		{
			GameObject selectedCard = GetRardomCardFromDeck(DecksConteiner.Decks[player]);
			selectedCard.GetComponent<Card>().StateType = CardStateType.InHand;
			foreach (PositionInHand pos in PositionKeeper.CardPositionsInHand[player])
			{
				if (pos.status != PositionStatus.Occupied)
				{
					cardsHandler.StartCoroutine(MoveCard(selectedCard.transform, pos.position));
					cardsHandler.StartCoroutine(RotateAndShowCard(selectedCard.transform));
					pos.status = PositionStatus.Occupied;
					return;
				}
			}
		}

		public static GameObject GetRardomCardFromDeck(List<GameObject> deck)
		{
			while (true)
			{
				GameObject card = deck[Random.Range(0, deck.Count)];
				if (card.GetComponent<Card>().StateType == CardStateType.InDeck)
				{
					return card;
				}
			}

		}


		/// <summary>
		/// Меняет карту в начале игры
		/// </summary>
		/// <param name="cardOnTable">карта, которую нужно заменить</param>
		/// <param name="deckPosition">расположение колоды</param>
		/// <param name="player">игрок, для которого меняется карта</param>
		/// <returns></returns>
		public static IEnumerator ReplaceCards(Transform cardOnTable, Transform deckPosition, Player player)
		{
			cardOnTable.GetComponent<Card>().StateType = CardStateType.InDeck;
			GameObject cardInDeck = GetRardomCardFromDeck(DecksConteiner.Decks[player]);
			cardInDeck.GetComponent<Card>().StateType = CardStateType.OnTableNotReplaceNotMove;
			
			yield return MoveCard(cardInDeck.transform, cardOnTable.position);
			yield return MoveCard(cardOnTable.transform, deckPosition.position);
			yield return RotateAndShowCard(cardInDeck.transform);
			yield return RotateAndShowCard(cardOnTable.transform);
		}

		
		public static IEnumerator MoveCard(Transform cardPosition, Vector3 position)
		{
			while (Vector3.Distance(cardPosition.transform.position, position) >= 2)
			{
				cardPosition.transform.position = Vector3.Lerp(cardPosition.transform.position, position, 4 * Time.deltaTime);
				yield return null;
			}
			cardPosition.transform.position = position;
		}

		public static IEnumerator RotateAndShowCard(Transform card)
		{
			card.GetComponent<Card>().HideOrShowFrondCard();
			while (Quaternion.Angle(card.transform.rotation, Quaternion.Euler(0, 0, 180)) > 0.01f)
			{
				card.transform.rotation = Quaternion.Slerp(card.transform.rotation, Quaternion.Euler(0, 0, 180), 2f * Time.deltaTime);
				yield return null;
			}
			card.transform.rotation = Quaternion.Euler(0, 0, 180);

		}

	




	}
}
