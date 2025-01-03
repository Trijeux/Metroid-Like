// Script by : Nanatchy
// Porject : Metroid Like

using UnityEngine;

namespace Script.Platform
{
    public class Moving : MonoBehaviour
    {
        #region Attributs

        private Rigidbody2D _rb;

        [SerializeField] private string limitMove;
        [SerializeField] private float speedRightLeft = 5f;
        [SerializeField] private float speedUpDown = 5f; 

        #endregion

        #region Methods

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
            _rb.linearVelocity = new Vector2(speedRightLeft, speedUpDown);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(limitMove))
            {
                speedRightLeft = -speedRightLeft;
                speedUpDown = -speedUpDown;
            }
        }
        
        #endregion
    }
}
