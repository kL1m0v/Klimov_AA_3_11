using TMPro;
using UnityEngine;


namespace Cards
{
    public class Card : MonoBehaviour, ICanHeart
	{
		[SerializeField]
		private GameObject _frontCard;
		[SerializeField]
		private MeshRenderer _picture;
		[SerializeField]
		private TextMeshPro _cost;
		[SerializeField]
		private TextMeshPro _name;
		[SerializeField]
		private TextMeshPro _type;
		[SerializeField]
		public TextMeshPro _description;
		[SerializeField]
		private TextMeshPro _attack;
		[SerializeField]
		private TextMeshPro _health;
		private CardStateType stateType;
		private Player _cardholder;
		public Ability ability;

		public uint Cost;
		public int Health;
		public int Attack;

		public CardStateType StateType { get => stateType; set => stateType = value; }
		public Player CardHolder { get => _cardholder; set => _cardholder = value; }
		public TextMeshPro Type { get => _type; set => _type = value; }

		public void Configuration(Material picture, CardPropertiesData data, string description, CardStateType stateType, Player player)
		{
			_picture.sharedMaterial = picture;
			_cost.text = data.Cost.ToString();
			Cost = data.Cost;
			_name.text = data.Name;
			Type.text = data.Type.ToString();
			_description.text = description;
			_attack.text = data.Attack.ToString();
			Attack = data.Attack;
			_health.text = data.Health.ToString();
			Health = data.Health;
			StateType = stateType;
			CardHolder = player;
		}

		public void HideOrShowFrondCard()
		{
			_frontCard.SetActive(_frontCard.activeSelf == true ? false : true);
			_picture.gameObject.SetActive(_picture.gameObject.activeSelf == true ? false : true);
		}

		public void SetHeart(int hp)
		{
			Health += hp;
			_health.text = Health.ToString();
		}
		public int GetAttack()
		{
			return Attack;
		}

		public void SetAttack(int at)
		{
			Attack += at;
			_attack.text = Attack.ToString();
			CheckHeart();
		}

		public void CheckHeart()
		{
			if (Health < 1)
			{
				Debug.Log("Убийство");
				DecksConteiner.Decks[GetComponent<Card>().CardHolder].Remove(gameObject);
				GetComponent<CardBehaviour>().InGamePosition.status = PositionStatus.NotOccupied;
				gameObject.SetActive(false);
				Destroy(this);
			}
		}




	}
}
