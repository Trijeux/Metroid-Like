// Script by : Nanatchy
// Porject : Metroid Like

using UnityEngine;

namespace Script.Old_Script.Item
{
	public class ItemRemove : MonoBehaviour
	{
		#region Attributs

		[SerializeField] private string pikerItem;

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
			if (other.CompareTag(pikerItem))
			{
				Destroy(gameObject);
			}
		}

		#endregion
	}
}
