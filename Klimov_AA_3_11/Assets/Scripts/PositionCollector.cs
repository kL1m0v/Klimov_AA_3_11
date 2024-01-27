using System.Collections.Generic;
using Cards;
using UnityEngine;

public class PositionCollector : MonoBehaviour
{
	[SerializeField]
	private Transform _deckPositionPlayer1;
	[SerializeField]
	private Transform _deckPositionPlayer2;
	[SerializeField]
	private Transform[] _cardPositionOnTablePlayer1;
	[SerializeField]
	private Transform[] _cardPositionOnTablePlayer2;
	[SerializeField]
	private Transform[] _cardPositionInHandPlayer1;
	[SerializeField]
	private Transform[] _cardPositionInHandPlayer2;
	[SerializeField]
	private BoxCollider _cardZonePlayer1;
	[SerializeField]
	private BoxCollider _cardZonePlayer2;
	
	
	public void Initialize()
	{
		CreateDeckPosition();
		CreatePositionInHand();
		CreatePositionOnTable();
		CreateCardZone();
	}
	private void CreateDeckPosition()
	{
		PositionKeeper.DeckPosition = new()
		{
			{Player.PlayerOne,  _deckPositionPlayer1},
			{Player.PlayerTwo, _deckPositionPlayer2}
		};
	}
	private void CreatePositionInHand()
	{
		
		List<PositionInHand> posInHandCollection1 = new();
		List<PositionInHand> posInHandCollection2 = new();
		foreach(Transform tr in _cardPositionInHandPlayer1)
		{
			PositionInHand p = new();
			p.position = tr.transform.position;
			p.status = PositionStatus.NotOccupied;
			posInHandCollection1.Add(p);
		}
		foreach(Transform tr in _cardPositionInHandPlayer2)
		{
			PositionInHand p = new();
			p.position = tr.transform.position;
			p.status = PositionStatus.NotOccupied;
			posInHandCollection2.Add(p);
		}
		PositionKeeper.CardPositionsInHand = new()
		{
			{Player.PlayerOne, posInHandCollection1},	
			{Player.PlayerTwo, posInHandCollection2}	
		};
	}
	private void CreatePositionOnTable()
	{
		List<PositionOnTable> posOnTableCollection1 = new();
		List<PositionOnTable> posOnTableCollection2 = new();
		foreach(Transform tr in _cardPositionOnTablePlayer1)
		{
			PositionOnTable p = new();
			p.position = tr.transform.position;
			p.status = PositionStatus.NotOccupied;
			posOnTableCollection1.Add(p);
		}
		foreach(Transform tr in _cardPositionOnTablePlayer2)
		{
			PositionOnTable p = new();
			p.position = tr.transform.position;
			p.status = PositionStatus.NotOccupied;
			posOnTableCollection2.Add(p);
		}
		PositionKeeper.CardPositionsOnTable = new()
		{
			{Player.PlayerOne, posOnTableCollection1},	
			{Player.PlayerTwo, posOnTableCollection2}	
		};
	}
	
	private void CreateCardZone()
	{
		PositionKeeper.CardZones = new()
		{
			{Player.PlayerOne, _cardZonePlayer1},
			{Player.PlayerTwo, _cardZonePlayer2}
		};
	}
	
	
}

	
