// Script by : Nanatchy
// Porject : Metroid Like

using Script.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Script.Old_Script.Player
{
    public class PlayerControllerOld : MonoBehaviour
    {
        #region Attributs

        public GameObject heart1;
        public GameObject heart2;
        public GameObject heart3;
        public GameObject uiInGame;
        public GameObject uiGameOver;
        public GameObject uiPauseGame;
        public GameObject textGameOver;
        public GameObject textEndLevel;
        
        
        public CheckIsGround checkIsGround;
        public Spawn spawn;
        public Dead dead;
        public KillEnemy killEnemy;
        public FinishLevel finishLevel;
        
        public int life = 3;
        
        public float speed = 5f;
        public float powerJump = 5f;
        
        public bool isHit;
        public bool isEnd;
        public bool isDead;
        public bool isPause;
        
        public string run;
        public string ground;
        
        private Rigidbody2D _rb;
        private Animator _animator;
        private float _upDownInput;
        private float _leftRightInput;
        private bool _jumpInput;
        private bool _exitInput;
        private bool  _pauseInputRelease;
        private bool _isFall = false;
        private bool _isFacingLeft = false;

       
        
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
            if (!isEnd)
            {
                switch (value.isPressed)
                {
                    case true when !isPause && !_pauseInputRelease:
                        _pauseInputRelease = true;
                        isPause = true;
                        uiPauseGame.SetActive(true);
                        uiInGame.SetActive(false);
                        Time.timeScale = 0;
                        break;
                    case true when isPause && !_pauseInputRelease:
                        _pauseInputRelease = true;
                        isPause = false;
                        uiPauseGame.SetActive(false);
                        uiInGame.SetActive(true);
                        Time.timeScale = 1;
                        break;
                    case false:
                        _pauseInputRelease = false;
                        break;
                }
            }
        }

        private void OnExitGame(InputValue value)
        {
            _exitInput = value.isPressed;
        }
        
        private void RotateToFace()
        {
            var rotationY = _isFacingLeft ? 180f : 0f;
            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }
        
        #endregion

        #region Behaviors

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            
        }

        private void Update()
        {
            if (!isEnd)
            {
                isHit = dead.isDead;
            
                if (isHit)
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
            
                if (isHit)
                {
                    transform.position = spawn.CheckPoint;
                    isHit = false;
                    dead.isDead = false;
                }

                RotateToFace();
            }
            
            if (life <= 0)
            {
                isEnd = true;
                isDead = true;
            }
            else
            {
                isEnd = finishLevel.IsFinish;
            }
            
            

            if (isPause)
            {
                if (_exitInput)
                {
                    Application.Quit();
                }
            }
            
            if (isEnd)
            {
                uiInGame.SetActive(false);
                uiGameOver.SetActive(true);
                if (isDead)
                {
                    textGameOver.SetActive(true);
                    textEndLevel.SetActive(false);
                }
                else
                {
                    textGameOver.SetActive(false);
                    textEndLevel.SetActive(true);
                }

                if (_jumpInput)
                {
                    SceneManager.LoadScene(0);
                    Canvas.ForceUpdateCanvases();
                }
                
                if (_exitInput)
                {
                    Application.Quit();
                }
            }
        }

        private void FixedUpdate()
        {
            if (!isEnd)
            {
                if (Mathf.Abs(_leftRightInput) > 0.5f)
                {
                    if (_leftRightInput > 0.5f)
                    {
                        _isFacingLeft = false;
                    }
                    else
                    {
                        _isFacingLeft = true;
                    }
                    if (!_animator.GetBool(run))
                    {
                        _animator.SetBool(run, true);
                    }
                    _rb.linearVelocity = new Vector2(_leftRightInput * speed, _rb.linearVelocity.y);
                }
                else
                {
                    if (_animator.GetBool(run))
                    {
                        _animator.SetBool(run, false);
                    }
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
                        if (!_animator.GetBool(ground))
                        {
                            _animator.SetBool(ground, true);
                        }
                        killEnemy.gameObject.SetActive(false);
                        break;
                    case false:
                        _isFall = true;
                        if (_animator.GetBool(ground))
                        {
                            _animator.SetBool(ground, false);
                        }
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
