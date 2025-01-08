// Script by : Nanatchy
// Porject : Metroid Like

using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Player
{
	public class Dead : MonoBehaviour
	{
		#region Attributs

		[SerializeField] private string deadZone;
		[SerializeField] private string enemy;

		public bool isDead;
		
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
			if (other.CompareTag(deadZone))
			{
				isDead = true;
			}

			if (other.CompareTag(enemy))
			{
				isDead = true;
			}
		}
		
		#endregion
	}
}
