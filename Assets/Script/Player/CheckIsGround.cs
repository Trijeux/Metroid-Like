// Script by : Nanatchy
// Porject : Metroid Like

using UnityEngine;

namespace Script.Player
{
	public class CheckIsGround : MonoBehaviour
	{
		#region Attributs

		[SerializeField] private new string tag;

		public bool IsGround { get; private set; }
		
		#endregion

		#region Methods

		
		#endregion

		#region Behaviors

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(tag))
			{
				IsGround = true;
			}
		}
    
		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.CompareTag(tag))
			{
				IsGround = false;
			}
		}
    
		private void Start()
		{
			IsGround = true;
		}

		private void Update()
		{
        
		}

		#endregion
	}
}
