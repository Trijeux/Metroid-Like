// Script by : Nanatchy
// Porject : Metroid Like

using System;
using System.Collections.Generic;
using Script.My_Tool_Script;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
        private float _inputUpDown = 0;
        private bool _inputDontMove = false;
        
        //Rotation
        private bool _isFacingLeft = false;
        
        //Aim Direction & Hands
        private enum DirectionAimEnum
        {
            Up = 0,
            MidleUp = 1,
            Midle = 2,
            MidleDown = 3,
            Down = 4
        }
        private DirectionAimEnum _directionAim;
        public int DirectionAim => _directionAim.GetHashCode();
        
        private Vector3 _handsNoCrunch = new Vector3(0,0,0);
        
        [Header("Unity Element")]
        [SerializeField] private CapsuleCollider2D capsuleNoCrunch;
        [SerializeField] private CircleCollider2D capsuleCrunch;
        [SerializeField] private CapsuleCollider2D triggerCapsuleNoCrunch;
        [SerializeField] private CircleCollider2D triggerCapsuleCrunch;
        [SerializeField] private Animator animatorPlayer;
        [SerializeField] private Vector3 handsCrunch;
        [SerializeField] private List<GameObject> hands;

        [Header("Payer State")] 
        [SerializeField] private int pv = 0;
        [SerializeField] private bool isDead = false;
        [SerializeField] private float speed = 0;
        [SerializeField] private float forceJump = 0;
        [SerializeField] private float mutliForceDoubleJump = 0; 
        
        [Header("Power")]
        [SerializeField] private bool isPowerDoubleJump = false;
        [SerializeField] private bool isPowerRocket = false;

        public bool IsPowerRocket => isPowerRocket;

        #endregion

        #region Methods
        
        private void PlayerMove()
        {
            _rb.linearVelocity = new Vector2(_inputMove * speed, _rb.linearVelocity.y);
        }

        private void UpdateRotationPlayer()
        {
            if (Mathf.Abs(_inputMove) > 0.5f)
            {
                if (_inputMove > 0.5f)
                {
                    _isFacingLeft = false;
                }
                else
                {
                    _isFacingLeft = true;
                }
            }
            
            var rotationY = _isFacingLeft ? 180f : 0f;
            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }

        private void UpdateAimDirection()
        {
            var currentDirectionAim = _directionAim;
            if (_inputUpDown >= 0.1 && Mathf.Abs(_inputMove) > 0.5f)
            {
                _directionAim = DirectionAimEnum.MidleUp;
            }
            else if (_inputUpDown >= 0.1 && !_isCrunch)
            {
                _directionAim = DirectionAimEnum.Up;
            }
            else if (_inputUpDown <= -0.1 && Mathf.Abs(_inputMove) > 0.5f)
            {
                _directionAim = DirectionAimEnum.MidleDown;
            }
            else if (_inputUpDown <= -0.1 && _isFall)
            {
                _directionAim = DirectionAimEnum.Down;
            }
            else
            {
                _directionAim = DirectionAimEnum.Midle;
            }
            if (_directionAim != currentDirectionAim)
            {
                hands[currentDirectionAim.GetHashCode()].SetActive(false);
                hands[_directionAim.GetHashCode()].SetActive(true);
            }
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
            if (Mathf.Abs(_inputMove) > 0.5f && capsuleCrunch.enabled 
                || capsuleCrunch.enabled && _inputUpDown >= 0.1 
                || _inputJump && capsuleCrunch.enabled)
            {
                capsuleNoCrunch.enabled = true;
                triggerCapsuleNoCrunch.enabled = true;
                capsuleCrunch.enabled = false;
                triggerCapsuleCrunch.enabled = false;
                _isCrunch = false;
            }
            else if (Mathf.Abs(_inputMove) < 0.1f && capsuleNoCrunch.enabled && _inputUpDown <= -0.1)
            {
                capsuleNoCrunch.enabled = false;
                triggerCapsuleNoCrunch.enabled = false;
                capsuleCrunch.enabled = true;
                triggerCapsuleCrunch.enabled = true;
                _isCrunch = true;
                _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
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

        private void OnUpDown(InputValue value)
        {
            _inputUpDown = value.Get<float>();
        }

        private void OnDontMove(InputValue value)
        {
            _inputDontMove = value.isPressed;
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
            animatorPlayer.SetBool("IsCrunch", _isCrunch);
            AddDamage();
            CheckLife();
            UpdateRotationPlayer();
            UpdateAimDirection();
            if (!isDead)
            {
                PlayerIsGround();
                if (!_isFall)
                {
                    PlayerCrunch();
                }
                if (!_inputDontMove)
                {
                    PlayerMove();
                    PlayerJump();
                }
                else
                {
                    _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
                }
            }
            else
            {
                _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            }
        }

        #endregion
    }
}
