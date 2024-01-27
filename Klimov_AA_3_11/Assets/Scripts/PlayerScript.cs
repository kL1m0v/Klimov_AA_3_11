using Cards;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour, ICanHeart, IPointerClickHandler
{
	[SerializeField]
	private TextMeshProUGUI _healthText;

	[HideInInspector]
	public int _health;

	private void Awake()
	{
		_health = 30;
	}
	public void SetHeart(int hp)
	{
		_health += hp;
		_healthText.text = _health.ToString();
		CheckHeart();
	}
	
	public void CheckHeart()
	{
		if(_health <= 0)
		{
			Debug.Log($"Победил {GamePlayController.CurrentPlayer}");
		}
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GamePlayController.FindTargetBattleCry == true)
		{
			GamePlayController.TargetBattleCry = eventData.pointerClick.gameObject;
		}
    }
}
