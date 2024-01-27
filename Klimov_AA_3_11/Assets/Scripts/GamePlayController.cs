using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
	private CardsHandler _cardsHandler;
	private static GameStatus gameStatus;
	[SerializeField]
	private Canvas _canvas;
	private static CanvasHandler _canvasHandler;

	private static Player _currentPlayer = Player.PlayerTwo;
	private static Dictionary<Player, uint> _playersMana;
	private static Dictionary<Player, uint> _playersManaConstantValue;
	private static GameObject _targetBattleCry;
	public static bool FindTargetBattleCry = false;

	public static Player CurrentPlayer { get => _currentPlayer; private set => _currentPlayer = value; }
	public static GameStatus GameStatus { get => gameStatus; private set => gameStatus = value; }
	public static CanvasHandler MyCanvasHandler { get => _canvasHandler; set => _canvasHandler = value; }
	public static Dictionary<Player, uint> PlayersMana { get => _playersMana; private set => _playersMana = value; }
	public static Dictionary<Player, uint> PlayersManaConstantValue { get => _playersManaConstantValue; set => _playersManaConstantValue = value; }
	public static GameObject TargetBattleCry { get => _targetBattleCry; set => _targetBattleCry = value; }

	public void Initialize()
	{
		_cardsHandler = GetComponent<CardsHandler>();
		MyCanvasHandler = _canvas.GetComponent<CanvasHandler>();
		MyCanvasHandler.FinishCardSelection.onClick.AddListener(StartGame);
		MyCanvasHandler.EndTurn.onClick.AddListener(StartNewTurn);
		PlayersMana = new()
		{
			{Player.PlayerOne, 0},
			{Player.PlayerTwo, 0}
		};
		PlayersManaConstantValue = new()
		{
			{Player.PlayerOne, 0},
			{Player.PlayerTwo, 0}
		};

	}
	private void Start()
	{
		GameStatus = GameStatus.Beginning;
		_cardsHandler.SelectStartingCard(3, DecksConteiner.Decks, Player.PlayerOne, PositionKeeper.CardPositionsOnTable[Player.PlayerOne]);
		_cardsHandler.SelectStartingCard(3, DecksConteiner.Decks, Player.PlayerTwo, PositionKeeper.CardPositionsOnTable[Player.PlayerTwo]);
	}
	private void OnDisable()
	{
		MyCanvasHandler.FinishCardSelection.onClick.RemoveAllListeners();
		MyCanvasHandler.EndTurn.onClick.RemoveAllListeners();
	}
	private void StartGame()
	{
		int i = 0;
		foreach (GameObject card in DecksConteiner.Decks[Player.PlayerOne].Where(c => c.GetComponent<Card>().StateType == CardStateType.OnTableNotMove ^ c.GetComponent<Card>().StateType == CardStateType.OnTableNotReplaceNotMove))
		{
			card.GetComponent<Card>().StateType = CardStateType.InHand;
			card.GetComponent<CardBehaviour>().InGamePosition = PositionKeeper.CardPositionsInHand[Player.PlayerOne][i];
			StartCoroutine(CardsHandler.MoveCard(card.transform, PositionKeeper.CardPositionsInHand[Player.PlayerOne][i].position));
			PositionKeeper.CardPositionsInHand[Player.PlayerOne][i].status = PositionStatus.Occupied;
			i++;
		}
		i = 0;
		foreach (GameObject card in DecksConteiner.Decks[Player.PlayerTwo].Where(c => c.GetComponent<Card>().StateType == CardStateType.OnTableNotMove ^ c.GetComponent<Card>().StateType == CardStateType.OnTableNotReplaceNotMove))
		{
			card.GetComponent<Card>().StateType = CardStateType.InHand;
			card.GetComponent<CardBehaviour>().InGamePosition = PositionKeeper.CardPositionsInHand[Player.PlayerTwo][i];
			StartCoroutine(CardsHandler.MoveCard(card.transform, PositionKeeper.CardPositionsInHand[Player.PlayerTwo][i].position));
			PositionKeeper.CardPositionsInHand[Player.PlayerTwo][i].status = PositionStatus.Occupied;
			i++;
		}
		MyCanvasHandler.FinishCardSelection.gameObject.SetActive(false);
		MyCanvasHandler.EndTurn.gameObject.SetActive(true);
		GameStatus = GameStatus.MainGame;
		StartNewTurn();	
		
	}
	private void StartNewTurn()
	{
		CurrentPlayer = CurrentPlayer == Player.PlayerOne ? Player.PlayerTwo : Player.PlayerOne;
		AddMana(CurrentPlayer);
		CardsHandler.AddCardToHand(CurrentPlayer);
		foreach(GameObject card in DecksConteiner.Decks[CurrentPlayer].Where(c => c.GetComponent<Card>().StateType == CardStateType.OnTableAndWaiting))
		{
			card.GetComponent<Card>().StateType = CardStateType.OnTableAndReady;
		}
	}
	public static void AddMana(Player player)
	{
		if(PlayersManaConstantValue[player] == 10)
		{
			PlayersMana[player] = PlayersManaConstantValue[player];
			MyCanvasHandler.PlayersMana[player].text = PlayersManaConstantValue[player].ToString();
			return;
		}
		PlayersManaConstantValue[player] ++;
		PlayersMana[player] = PlayersManaConstantValue[player];
		MyCanvasHandler.PlayersMana[player].text = PlayersManaConstantValue[player].ToString();
	}
	public static void SpendMana(Player player, uint mana)
	{
		PlayersMana[player] -= mana;
		MyCanvasHandler.PlayersMana[player].text = PlayersMana[player].ToString();
	}
}
