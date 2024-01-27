using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
	public class CardBehaviour : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler
	{
		private Card _card;
		private Vector3 _defaultPosition;
		private bool InDropZone = false;
		private GameObject _target;
		private InGamePosition _inGamePosition;


		public Card Card { get => _card; set => _card = value; }
		public InGamePosition InGamePosition { get => _inGamePosition; set => _inGamePosition = value; }

		private void Start()
		{
			_inGamePosition = new();
			Card = GetComponent<Card>();
			_target = null;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (GamePlayController.GameStatus == GameStatus.Beginning && DecksConteiner.Decks[Card.CardHolder].Contains(gameObject) &&
			Card.StateType == CardStateType.OnTable ^
			Card.StateType == CardStateType.OnTableNotMove)
			{
				ReplaceCard();
			}
			transform.localScale = CardsHandler.InitialCardSize;
			if (GamePlayController.FindTargetBattleCry == true)
			{
				GamePlayController.TargetBattleCry = eventData.pointerClick.gameObject;
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (Card.StateType != CardStateType.InDeck)
				IncreaseSizeCard();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (Card.StateType != CardStateType.InDeck)
				ReduceSizeCard();

		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (Card.StateType == CardStateType.InHand)
				PositionKeeper.TurnBoxColliderCardZoneOnTable(true);
		}

		public void OnDrag(PointerEventData eventData)
		{
			switch (Card.StateType)
			{
				case CardStateType.InHand:
					MoveCard(eventData);
					break;
				case CardStateType.OnTableAndReady:
					MoveCard(eventData);
					break;
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			switch (Card.StateType)
			{
				case CardStateType.InHand:
					PositionKeeper.TurnBoxColliderCardZoneOnTable(false);
					if (InDropZone)
					{
						foreach (PositionOnTable pos in PositionKeeper.CardPositionsOnTable[GamePlayController.CurrentPlayer])
						{
							if (pos.status == PositionStatus.NotOccupied)
							{
								pos.status = PositionStatus.Occupied;
								_inGamePosition.status = PositionStatus.NotOccupied;
								InGamePosition = pos;
								transform.position = pos.position;
								GamePlayController.SpendMana(GamePlayController.CurrentPlayer, Card.Cost);
								Card.StateType = CardStateType.OnTableAndWaiting;
								AbilityManager.CheckingCardForCharge(Card);
								if (Card._description.text.ToString().ToLower().Contains("battlecry"))
								{
									Debug.Log("разыграна карта с боевым кличем");
									StartCoroutine(AbilityManager.CheckBattleCry(Card));
								}
								return;
							}
						}
					}
					else
					{
						transform.position = _defaultPosition;
					}
					break;

				case CardStateType.OnTableAndReady:
					Debug.Log("карта готова к атаке");
					transform.position = _defaultPosition;
					if (_target != null)
					{
						if (AbilityManager.CheckingCardsForTaunt().Count == 0 ^ AbilityManager.CheckingCardsForTaunt().Contains(_target))
						{
							Attack(_target);
						}
						else
						{
							Debug.Log("Нужно атаковать существо с провокацией");
						}

					}
					break;
			}
		}


		public void OnPointerDown(PointerEventData eventData)
		{
			SetStartingPosition();

		}

		private void OnTriggerEnter(Collider other)
		{

			switch (Card.StateType)
			{
				case CardStateType.InHand:
					InDropZone = true;
					break;
				case CardStateType.OnTableAndReady:
					_target = other.gameObject;
					Debug.Log(_target);
					break;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			switch (Card.StateType)
			{
				case CardStateType.InHand:
					InDropZone = false;
					break;
				case CardStateType.OnTableAndReady:
					_target = null;
					break;
			}
		}
		private void ReplaceCard()
		{
			GameObject newCardFromDeck = CardsHandler.GetRardomCardFromDeck(DecksConteiner.Decks[Card.CardHolder]);
			StartCoroutine(CardsHandler.ReplaceCards(transform, PositionKeeper.DeckPosition[Card.CardHolder], Card.CardHolder));
			newCardFromDeck.GetComponent<Card>().StateType = CardStateType.OnTable;
			Card.StateType = CardStateType.InDeck;
		}
		private void Attack(GameObject target)
		{
			target.GetComponent<ICanHeart>().SetHeart(-Card.Attack);
			Card.StateType = CardStateType.OnTableAndWaiting;
			target.GetComponent<ICanHeart>().CheckHeart();
		}

		private void SetStartingPosition()
		{
			_defaultPosition = transform.position;
		}

		private void IncreaseSizeCard()
		{
			transform.localScale *= 1.5f;
		}
		private void ReduceSizeCard()
		{
			transform.localScale = CardsHandler.InitialCardSize;
		}

		private void MoveCard(PointerEventData eventData)
		{
			if (GamePlayController.GameStatus == GameStatus.MainGame && GamePlayController.PlayersMana[GamePlayController.CurrentPlayer] >= Card.Cost ||
					GamePlayController.GameStatus == GameStatus.MainGame && GamePlayController.PlayersMana[GamePlayController.CurrentPlayer] >= Card.Cost)
			{
				Vector3 mouseDelta = new Vector3(eventData.delta.x, 0, eventData.delta.y);
				transform.position += mouseDelta;
			}
		}
	}
}
