// Script by : Nanatchy
// Porject : Metroid Like

using System.Collections.Generic;
using Script.My_Tool_Script;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Attributs
        
        //Unity Element
        private Rigidbody2D _rb;
        
        //Script Element
        private CheckIsGround _checkIsGround;
        private TrigerCollidePlayer _triggerPlayer;
        
        //Jump
        private bool _doubleJump = false;
        private bool _jumpIsPressed = false;
        private int _numbJumpInAir = 0;
        private bool _isFall = false;
        
        //Crunch
        private bool _isCrunch = false;
        
        //Input
        private float _inputMove = 0;
        private bool _inputJump = false;
        private float _inputCrunch = 0;
        
        
        [Header("Unity Element")]
        [SerializeField] private CapsuleCollider2D capsuleNoCrunch;
        [SerializeField] private CapsuleCollider2D capsuleCrunch;

        [Header("Payer State")] 
        [SerializeField] private int pv = 0;
        [SerializeField] private bool isDead = false;
        [SerializeField] private float speed = 0;
        [SerializeField] private float forceJump = 0;
        [SerializeField] private float mutliForceDoubleJump = 0; 
        
        [Header("Power")]
        [SerializeField] private bool isPowerDoubleJump = false;
        
        #endregion

        #region Methods
        
        private void PlayerMove()
        {
            _rb.linearVelocity = new Vector2(_inputMove * speed, _rb.linearVelocity.y);
        }

        private void PlayerJump()
        {
            switch (_inputJump)
            {
                case true when !_jumpIsPressed:
                {
                    _jumpIsPressed = true; 
                    _numbJumpInAir++;

                    switch (_numbJumpInAir)
                    {
                        case 1:
                            _rb.AddForce(transform.up * (forceJump * mutliForceDoubleJump), ForceMode2D.Impulse);
                            if (isPowerDoubleJump)
                            {
                                _doubleJump = true;
                            }
                            break;
                        case 2 when _doubleJump:
                            _rb.AddForce(transform.up * (forceJump * mutliForceDoubleJump * 0.8f), ForceMode2D.Impulse);
                            _doubleJump = false;
                            break;
                    }

                    _isFall = true;
                    break;
                }
                case false:
                {
                    _jumpIsPressed = false;

                    if (_rb.linearVelocityY > 0)
                    {
                        _rb.linearVelocityY *= 0.9f;
                    }
                    break;
                }
            }

        }

        private void PlayerIsGround()
        {
            switch (_checkIsGround.IsGround)
            {
                case true:
                    _isFall = false;
                    _numbJumpInAir = 0;
                    break;
                case false:
                    _isFall = true;
                    break;
            }
        }

        private void PlayerCrunch()
        {
            if (capsuleNoCrunch.enabled && _inputCrunch <= -0.1)
            {
                capsuleNoCrunch.enabled = false;
                capsuleCrunch.enabled = true;
                _isCrunch = true;
                _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            }

            if (capsuleCrunch.enabled && _inputCrunch >= 0.1)
            {
                capsuleNoCrunch.enabled = true;
                capsuleCrunch.enabled = false;
                _isCrunch = false;
            }
        }

        private void AddDamage()
        {
            if (_triggerPlayer.IsHitDamage)
            {
                pv -= _triggerPlayer.EnemyDamage;
                _triggerPlayer.IsHitDamage = false;
            }
        }
        
        private void CheckLife()
        {
            if (pv <= 0)
            {
                isDead = true;
                _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            }
        }

        #endregion

        #region InputSystem

        private void OnMove(InputValue value)
        {
            _inputMove = value.Get<float>();
        }

        private void OnJump(InputValue value)
        {
            _inputJump = value.isPressed;
        }

        private void OnCrunch(InputValue value)
        {
            _inputCrunch = value.Get<float>();
        }

        #endregion

        #region Behaviors

        private void Start()
        {
            _rb = transform.MyGetComponentObject<Rigidbody2D>();
            _checkIsGround = transform.MyGetComponentObject<CheckIsGround>();
            _triggerPlayer = transform.MyGetComponentObject<TrigerCollidePlayer>();
        }

        private void FixedUpdate()
        {
            AddDamage();
            CheckLife();
            if (!isDead)
            {
                PlayerIsGround();
                if (!_isCrunch)
                {
                    PlayerMove();
                    PlayerJump();
                }
                if (!_isFall)
                {
                    PlayerCrunch();
                }
            }
        }

        #endregion
    }
}
