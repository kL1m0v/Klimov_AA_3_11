using System.Collections.Generic;
using Cards;
using UnityEngine;

public static class PositionKeeper
{
	private static Dictionary<Player, Transform> _deckPosition;
	private static Dictionary<Player, List<PositionOnTable>> _cardPositionsOnTable;
	private static Dictionary<Player, List<PositionInHand>> _cardPositionsInHand;
	private static Dictionary<Player, BoxCollider> _cardZones;

	public static Dictionary<Player, Transform> DeckPosition { get => _deckPosition; set => _deckPosition = value; }
	public static Dictionary<Player, List<PositionOnTable>> CardPositionsOnTable { get => _cardPositionsOnTable; set => _cardPositionsOnTable = value; }
	public static Dictionary<Player, List<PositionInHand>> CardPositionsInHand { get => _cardPositionsInHand; set => _cardPositionsInHand = value; }
	public static Dictionary<Player, BoxCollider> CardZones { get => _cardZones; set => _cardZones = value; }
	
	public static void TurnBoxColliderCardZoneOnTable(bool trueOrFalse)
	{
		CardZones[Player.PlayerOne].enabled = CardZones[Player.PlayerOne].enabled = trueOrFalse;
		CardZones[Player.PlayerTwo].enabled = CardZones[Player.PlayerOne].enabled = trueOrFalse;
	}
}
