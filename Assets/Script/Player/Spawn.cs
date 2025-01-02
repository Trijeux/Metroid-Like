// Script by : Nanatchy
// Porject : Metroid Like

using UnityEngine;

namespace Script.Player
{
	public class Spawn : MonoBehaviour
	{
		#region Attributs

		[SerializeField] private GameObject player;
    
		public Vector3 CheckPoint { get; private set; }
		
		[SerializeField] private string checkPoint;
		[SerializeField] private float space;
		
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
			if (other.CompareTag(checkPoint))
			{
				CheckPoint = player.transform.position;
				
				var point = CheckPoint;
				point.x = CheckPoint.x + space;
				CheckPoint = point;
			}
		}
    
		#endregion
	}
}
