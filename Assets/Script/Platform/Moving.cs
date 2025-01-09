// Script by : Nanatchy
// Porject : Metroid Like

using UnityEngine;

namespace Script.Platform
{
    public class Moving : MonoBehaviour
    {
        #region Attributs

        [SerializeField] private string limitMove;
        [SerializeField] private float speedRightLeft = 5f;
        [SerializeField] private float speedUpDown = 5f; 

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

        private void FixedUpdate()
        {
            transform.position += new Vector3(speedRightLeft * Time.deltaTime,speedUpDown * Time.deltaTime);
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
