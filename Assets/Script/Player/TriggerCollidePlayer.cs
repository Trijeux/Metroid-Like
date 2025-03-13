// Script by : Nanatchy
// Porject : Metroid Like

using System;
using System.Collections;
using System.Transactions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Player
{
	public class TrigerCollidePlayer : MonoBehaviour
	{
		#region Attributs

		[SerializeField] private string enemy;
		[SerializeField] private string item;
		[SerializeField] private float cooldown;
		
		[Header("Damage")]
		[SerializeField] private int enemyDamage = 1;
		
		
		
		private bool _invincibility = false; 
		public bool IsHitDamage { get; set; } = false;
		public int EnemyDamage => enemyDamage;

		#endregion

		#region Methods



		#endregion

		#region InputSystem



		#endregion

		#region Behaviors

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(enemy) && !_invincibility)
			{
				IsHitDamage = true;
				StartCoroutine("InvincibilityCooldown");
			}

			if (other.CompareTag(item))
			{
				Destroy(other.gameObject);
			}
		}

		private void Start()
		{
			
		}

		private void Update()
		{
        
		}

		private IEnumerator InvincibilityCooldown()
		{
			_invincibility = true;
			yield return new WaitForSeconds(cooldown);
			_invincibility = false;
		}
		
		#endregion
	}
}
