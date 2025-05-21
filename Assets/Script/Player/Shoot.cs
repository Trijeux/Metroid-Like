// Script by : Nanatchy
// Porject : Metroid Like

using System;
using System.Collections.Generic;
using Script.Player;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    #region Attributs

    private PlayerController _playerController;
    [SerializeField] private List<GameObject> pointShoots;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject rocket;
    [SerializeField] private float cooldownShoot;
    [SerializeField] private float cooldownRocket;
    
    private bool _haveShoot;
    private float _timerCooldownShoot;
    private bool _haveRocket;
    private float _timerCooldownRocket;

    private bool _inputShoot;
    private bool _inputRocket;
    
    #endregion

    #region Methods
    
    #endregion

    #region InputSystem
    
    private void OnShoot(InputValue value)
    {
        _inputShoot = value.isPressed;
    }

    private void OnRocket(InputValue value)
    {
        _inputRocket = value.isPressed;
    }
    
    #endregion

    #region Behaviors

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_inputShoot && !_haveShoot)
        {
            Instantiate(bullet, pointShoots[_playerController.DirectionAim].transform.position, pointShoots[_playerController.DirectionAim].transform.rotation);
            _haveShoot = true;
        }

        if (_playerController.IsPowerRocket)
        {
            if (_inputRocket && !_haveRocket)
            {
                Instantiate(rocket, pointShoots[_playerController.DirectionAim].transform.position, pointShoots[_playerController.DirectionAim].transform.rotation);
                _haveRocket = true;
            }
        }
        
        if (_haveShoot)
        {
            _timerCooldownShoot += Time.deltaTime;
        }
        
        if (cooldownShoot < _timerCooldownShoot)
        {
            _timerCooldownShoot = 0;
            _haveShoot = false;
        }
        
        if (_haveRocket)
        {
            _timerCooldownRocket += Time.deltaTime;
        }

        if (cooldownRocket < _timerCooldownRocket)
        {
            _timerCooldownRocket = 0;
            _haveRocket = false;
        }
    }

    #endregion
}
