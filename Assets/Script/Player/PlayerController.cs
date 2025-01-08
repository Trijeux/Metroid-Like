// Script by : Nanatchy
// Porject : Metroid Like

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Attributs

        [SerializeField] private GameObject heart1;
        [SerializeField] private GameObject heart2;
        [SerializeField] private GameObject heart3;
        [SerializeField] private GameObject uiInGame;
        [SerializeField] private GameObject uiGameOver;
        [SerializeField] private GameObject textGameOver;
        [SerializeField] private GameObject textEndLevel;
        
        private Rigidbody2D _rb;
        
        [SerializeField] private CheckIsGround checkIsGround;
        [SerializeField] private Spawn spawn;
        [SerializeField] private Dead dead;
        [SerializeField] private KillEnemy killEnemy;
        [SerializeField] private FinishLevel finishLevel;

        [SerializeField] private float speed = 5f;
        [SerializeField] private float powerJump = 5f;
        [SerializeField] private bool isDead;
        [SerializeField] private bool isEnd;
        [SerializeField] private int life = 3;
        [SerializeField] private bool isNoLife;
        [SerializeField] private bool isPause;

        private float _upDownInput;
        private float _leftRightInput;
        private bool _jumpInput;
        private bool  _pauseInputRelease;
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

        private void OnPause(InputValue value)
        {
            switch (value.isPressed)
            {
                case true when !isPause && !_pauseInputRelease:
                    _pauseInputRelease = true;
                    isPause = true;
                    Time.timeScale = 0;
                    break;
                case true when isPause && !_pauseInputRelease:
                    _pauseInputRelease = true;
                    isPause = false;
                    Time.timeScale = 1;
                    break;
                case false:
                    _pauseInputRelease = false;
                    break;
            }
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
                life--;
                switch (life)
                {
                    case 3:
                        heart3.SetActive(true);
                        heart2.SetActive(true);
                        heart1.SetActive(true);
                        break;
                    case 2:
                        heart3.SetActive(false);
                        heart2.SetActive(true);
                        heart1.SetActive(true);
                        break;
                    case 1:
                        heart3.SetActive(false);
                        heart2.SetActive(false);
                        heart1.SetActive(true);
                        break;
                    case 0:
                        heart3.SetActive(false);
                        heart2.SetActive(false);
                        heart1.SetActive(false);
                        break;
                }
            }
            
            if (isDead)
            {
                transform.position = spawn.CheckPoint;
                isDead = false;
                dead.isDead = false;
            }

            if (life <= 0)
            {
                isEnd = true;
                isNoLife = true;
            }
            else
            {
                isEnd = finishLevel.IsFinish;
            }

            if (isEnd)
            {
                uiInGame.SetActive(false);
                uiGameOver.SetActive(true);
                if (isNoLife)
                {
                    textGameOver.SetActive(true);
                    textEndLevel.SetActive(false);
                }
                else
                {
                    textGameOver.SetActive(false);
                    textEndLevel.SetActive(true);
                }
            }
        }

        private void FixedUpdate()
        {
            if (!isEnd)
            {
                if (Mathf.Abs(_leftRightInput) > 0.5f)
                {
                    _rb.linearVelocity = new Vector2(_leftRightInput * speed, _rb.linearVelocity.y);
                }
                else
                {
                    _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
                }

                switch (_jumpInput)
                {
                    case true when !_isFall:
                        _rb.AddForce(transform.up * powerJump, ForceMode2D.Impulse);
                        _isFall = true;
                        break;
                    case false:
                    {
                        if (_rb.linearVelocityY > 0)
                        {
                            _rb.linearVelocityY *= 0.5f;
                        }

                        break;
                    }
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
            else
            {
                _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            }
        }

        #endregion
    }
}
