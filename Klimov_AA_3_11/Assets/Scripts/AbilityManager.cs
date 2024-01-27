using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

public static class AbilityManager
{
	public static void CheckingCardForCharge(Card card)
	{
		var description = card._description.text.ToString().ToLower();
		if (description.Contains("charge"))
		{
			card.StateType = CardStateType.OnTableAndReady;
		}
	}



	public static List<GameObject> CheckingCardsForTaunt()
	{
		Player oppponent = GamePlayController.CurrentPlayer == Player.PlayerOne ? Player.PlayerTwo : Player.PlayerOne;
		List<GameObject> cardWithTaunt = new();
		foreach (GameObject card in DecksConteiner.Decks[oppponent].Where(c => c.GetComponent<Card>().StateType != CardStateType.InDeck &&
		c.GetComponent<Card>().StateType != CardStateType.InHand))
		{
			var description = card.GetComponent<Card>()._description.text.ToString().ToLower();
			if (description.Contains("taunt"))
			{
				cardWithTaunt.Add(card);
			}
		}
		return cardWithTaunt;
	}

	public static IEnumerator CheckBattleCry(Card card)
	{
		var description = card._description.text.ToString();
		if (description.Contains("Deal 1 damage"))
		{
			Debug.Log("Выберите цель");
			GamePlayController.FindTargetBattleCry = true;
			while (GamePlayController.TargetBattleCry == null)
			{
				
				yield return null;
			}
			GamePlayController.TargetBattleCry.GetComponent<ICanHeart>().SetHeart(-1);
			GamePlayController.TargetBattleCry = null;
			Debug.Log("Боевой клич сыгран");
		}
		if (description.Contains("Restore 2 Health"))
		{
			Debug.Log("Выберите цель");
			GamePlayController.FindTargetBattleCry = true;
			while (GamePlayController.TargetBattleCry == null)
			{
				yield return null;
			}
			GamePlayController.TargetBattleCry.GetComponent<ICanHeart>().SetHeart(2);
			GamePlayController.TargetBattleCry = null;
			Debug.Log("Боевой клич сыгран");
		}
		if (description.Contains("Give a friendly miniom +1/+1"))
		{
			Debug.Log("Выберите цель");
			GamePlayController.FindTargetBattleCry = true;
			while (GamePlayController.TargetBattleCry == null)
			{
				Debug.Log("ZHDU CEL");
				yield return null;
			}
			GamePlayController.TargetBattleCry.GetComponent<Card>().SetHeart(1);
			GamePlayController.TargetBattleCry.GetComponent<Card>().SetAttack(1);
			GamePlayController.TargetBattleCry = null;
			Debug.Log("Боевой клич сыгран");
		}
		if (description.Contains("Gain +1/+1 for each other friendly minion on the battlefield"))
		{
			int count = DecksConteiner.Decks[card.CardHolder].Where(c => c.GetComponent<Card>().StateType != CardStateType.InDeck && c.GetComponent<Card>().StateType != CardStateType.InHand).Count();
			card.SetAttack(count);
			card.SetHeart(count);
		}
	}

}
