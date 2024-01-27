using UnityEngine;

namespace Cards
{

    public class BootsTrap : MonoBehaviour
	{
		[SerializeField]
		private Canvas _canvas;
		private void Awake()
		{
			_canvas.GetComponent<CanvasHandler>().Initialize();
			GetComponent<DecksConteiner>().Initialize();
			GetComponent<PositionCollector>().Initialize();
			CardsCollection.Initialize();
			GetComponent<DecksGerenator>().Initialize();
			GetComponent<GamePlayController>().Initialize();
			GetComponent<CardsHandler>().Initialize();
		}
	}
}
