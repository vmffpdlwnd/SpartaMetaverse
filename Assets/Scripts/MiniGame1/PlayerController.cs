// MiniGame1/PlayerController.cs
namespace MiniGame1
{
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        public SPUM_Prefabs spumPrefab;
        private Rigidbody2D rb;
        private Animator animator;
        public float flapForce = 6f;
        public float forwardSpeed = 3f;
        float deathCooldown = 0f;
        
        bool isFlap = false;

        public bool godMode = false;

        MiniGame1.GameManager gameManager;

        void Start()
        {
            gameManager = MiniGame1.GameManager.Instance;

            rb = GetComponent<Rigidbody2D>();
            if(spumPrefab != null)
            {
                animator = spumPrefab._anim;
            }
        }

        void Update()
        {
            if (animator.GetBool("Die"))
            {
                if (deathCooldown <= 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        gameManager.RestartGame();
                    }
                }
                else
                {
                    deathCooldown -= Time.deltaTime;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) ||Input.GetMouseButtonDown(0))
                {
                    isFlap = true;
                }
            }
        }

        void FixedUpdate()
        {
            if (animator.GetBool("Die"))
                return;
            
            Vector3 velocity = rb.velocity;
            velocity.x = forwardSpeed;

            if (isFlap)
            {
                velocity.y += flapForce;
                isFlap = false;
            }
            
            rb.velocity = velocity;
            
            // 회전 로직 수정
            float velocityAngle = velocity.y * 10f;  // 속도에 따른 회전 각도
            float targetAngle = Mathf.Clamp(velocityAngle, -90f, 90f) - 90f;  // -90도 기준
            
            // 현재 각도를 -180 ~ 180 범위로 변환
            float currentAngle = transform.rotation.eulerAngles.z;
            if (currentAngle > 180f) currentAngle -= 360f;
            
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.fixedDeltaTime * 5f);
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
        }
        
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (godMode)
                return;
                
            if (animator.GetBool("Die"))
                return;

            animator.SetBool("Die", true);  // SetInteger 대신 SetBool로 변경
            deathCooldown = 1f;
            gameManager.GameOver();
        }
    }
}