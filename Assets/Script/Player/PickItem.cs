// Script by : Nanatchy
// Porject : Metroid Like

using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Player
{
	public class PickItem : MonoBehaviour
	{
		#region Attributs

		[SerializeField] private TextMeshProUGUI gemInGame;
		[SerializeField] private TextMeshProUGUI gemGameOver;
		
		[SerializeField] private string gem;
		[SerializeField] private string redGem;
		[SerializeField] private int itemCount;

		#endregion

		#region Methods



		#endregion

		#region Behaviors

		private void Start()
		{
        
		}

		private void Update()
		{
			gemInGame.text = itemCount.ToString();
			gemGameOver.text = itemCount.ToString();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(gem))
			{
				itemCount++;
			}
			
			if (other.CompareTag(redGem))
			{
				itemCount += 10;
			}
		}

		#endregion
	}
}
