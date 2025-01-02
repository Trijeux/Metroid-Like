// Script by : Nanatchy
// Porject : Metroid Like

using UnityEngine;

namespace Script.Player
{
	public class KillEnemy : MonoBehaviour
	{
		#region Attributs
		
		[SerializeField] private string enemy;
		
		public bool isKill;
		
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
			if (other.CompareTag(enemy))
			{
				isKill = true;
			}
		}
		
		#endregion
	}
}
