// Script by : Nanatchy
// Porject : Metroid Like

using System;
using UnityEngine;
using UnityEngine.Serialization;

public class FinishLevel : MonoBehaviour
{
    #region Attributs

    [SerializeField] private string finish;
	[SerializeField] private bool isFinish = false;

	public bool IsFinish => isFinish;

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
	    if (other.CompareTag(finish))
	    {
		    isFinish = true;
	    }
    }
    
	#endregion
}