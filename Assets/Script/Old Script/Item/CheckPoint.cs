// Script by : Nanatchy
// Porject : Metroid Like

using UnityEngine;

namespace Script.Old_Script.Item
{
	public class CheckPoint : MonoBehaviour
	{
		#region Attributs
		
		[SerializeField] private Color newColor;

		private SpriteRenderer _renderer;
		private CircleCollider2D _circleCollider;
		
		[SerializeField] private string pick;
		
		#endregion

		#region Methods



		#endregion

		#region Behaviors

		private void Start()
		{
			_renderer = GetComponent<SpriteRenderer>();
			_circleCollider = GetComponent<CircleCollider2D>();
		}

		private void Update()
		{
        
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(pick))
			{
				_renderer.color = newColor;
				_circleCollider.enabled = false;
			}
		}
    
		#endregion
	}
}
