// Script by : Nanatchy
// Porject : Metroid Like

using UnityEngine;

namespace Script.Player
{
	public class PickItem : MonoBehaviour
	{
		#region Attributs

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
        
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(gem))
			{
				itemCount++;
			}
			
			if (other.CompareTag(redGem))
			{
				itemCount =+ 10;
			}
		}

		#endregion
	}
}
