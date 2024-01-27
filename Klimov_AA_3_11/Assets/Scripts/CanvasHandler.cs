using System.Collections.Generic;
using Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{

	[SerializeField]
	private Button _finishCardSelection;
	[SerializeField]
	private Button _endTurn;
	[SerializeField]
	private TMP_Text _healthTextPlayer1;
	[SerializeField]
	private TMP_Text _healthTextPlayer2;
	[SerializeField]
	private TMP_Text _manaTextPlayer1;
	[SerializeField]
	private TMP_Text _manaTextPlayer2;
	private Dictionary<Player, TMP_Text> _playersHealth; 
	private Dictionary<Player, TMP_Text> _playersMana; 
	
	

	public Button FinishCardSelection { get => _finishCardSelection; set => _finishCardSelection = value; }
	public Button EndTurn { get => _endTurn; set => _endTurn = value; }
	public Dictionary<Player, TMP_Text> PlayersHealth { get => _playersHealth; set => _playersHealth = value; }
	public Dictionary<Player, TMP_Text> PlayersMana { get => _playersMana; set => _playersMana = value; }

	public void Initialize()
	{
		PlayersHealth = new()
		{
			{Player.PlayerOne, _healthTextPlayer1},
			{Player.PlayerTwo, _healthTextPlayer2}
		};
		PlayersMana = new()
		{
			{Player.PlayerOne, _manaTextPlayer1},
			{Player.PlayerTwo, _manaTextPlayer2}
		};
	}
}
