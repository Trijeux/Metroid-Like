// Script by : Nanatchy
// Porject : Metroid Like

using System;
using UnityEngine;

public class Pojectil : MonoBehaviour
{
    #region Attributs

    private Rigidbody2D _rb;
    [SerializeField] private float speed;
    
    #endregion

    #region Methods

	

    #endregion

    #region InputSystem



    #endregion

    #region Behaviors

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Ground") )
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = transform.right * speed;
    }
    
	#endregion
}
