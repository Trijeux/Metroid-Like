// Script by : Nanatchy
// Porject : Metroid Like

using UnityEngine;

namespace Script.Old_Script.Enemy
{
	public class MoveEnemy : MonoBehaviour
	{
		#region Attributs
		
		private Rigidbody2D _rb;
		
		[SerializeField] private string limitZone;
		[SerializeField] private string killerZone;
		[SerializeField] private float speed = -5f;
		
		public bool IsDead { get; private set; }
		
		private bool _isFacingLeft = true;

		#endregion

		#region Methods

		private void RotateToFace()
		{
			var rotationY = _isFacingLeft ? 0f : 180f;
			transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
		}
		
		#endregion

		#region Behaviors

		private void Start()
		{
			_rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			
		}

		private void FixedUpdate()
		{
			_rb.linearVelocity = new Vector2(speed, _rb.linearVelocity.y);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(limitZone))
			{
				_isFacingLeft = !_isFacingLeft;
				speed = -speed;
				RotateToFace();
			}

			if (other.CompareTag(killerZone))
			{
				IsDead = true;
				Destroy(gameObject);
			}
		}
		
		#endregion
	}
}
