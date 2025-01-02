// Script by : Nanatchy
// Porject : Metroid Like

using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Attributs

        [SerializeField] private CheckIsGround checkIsGround;
        [SerializeField] private Spawn spawn;
        [SerializeField] private Dead dead;
        [SerializeField] private KillEnemy killEnemy;
        
        private Rigidbody2D _rb;
        
        [SerializeField] private float speed = 5f;
        [SerializeField] private float powerJump = 5f;
        [SerializeField] private bool isDead;

        private float _upDownInput;
        private float _leftRightInput;
        private bool _jumpInput;
        private bool _isFall = false;

        #endregion

        #region Methods

        private void OnUpDown(InputValue value)
        {
            _upDownInput = value.Get<float>();
        }

        private void OnLeftRight(InputValue value)
        {
            _leftRightInput = value.Get<float>();
        }

        private void OnJump(InputValue value)
        {
            _jumpInput = value.isPressed;
        }

        #endregion

        #region Behaviors

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            isDead = dead.isDead;
            if (isDead)
            {
                transform.position = spawn.CheckPoint;
                isDead = false;
                dead.isDead = false; 
            }
        }

        private void FixedUpdate()
        {
            if (Mathf.Abs(_leftRightInput) > 0.5f)
            {
                _rb.linearVelocity = new Vector2(_leftRightInput * speed, _rb.linearVelocity.y);
            }
            else
            {
                _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            }

            if (_jumpInput && !_isFall)
            {
                _isFall = true;
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, speed);
            }

            switch (checkIsGround.IsGround)
            {
                case true:
                    _isFall = false;
                    killEnemy.gameObject.SetActive(false);
                    break;
                case false:
                    _isFall = true;
                    killEnemy.gameObject.SetActive(true);
                    break;
            }

            if (killEnemy.isKill)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, speed);
                killEnemy.isKill = false;
            }
        }

        #endregion
    }
}