using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
	public enum CardUnitType : byte
	{
		None = 0,
		Murloc = 1,
		Beast = 2,
		Elemental = 3,
		Mech = 4
	}

	public enum SideType : byte
	{
		Common = 0,
		Mage = 1,
		Warrior = 2
	}
	
	public enum CardStateType : byte
	{
		InDeck,
		InHand,
		OnTable,
		OnTableNotReplace,
		OnTableNotMove,
		OnTableNotReplaceNotMove,
		OnTableAndReady,
		OnTableAndWaiting
	}
	
	
	public enum GameStatus: byte
	{
		Beginning,
		MainGame
	}
	
	public enum Player: byte
	{
		PlayerOne = 1,
		PlayerTwo = 2
	}
	
	public enum PositionStatus: byte
	{
		Occupied,
		NotOccupied
		
	}
	
	public enum TypeAbility
	{
		BattleCry,
		Taunt,
		Charge,
		TempEffect,
		WithoutAbility
	}
}
