using UnityEngine;

namespace Main
{
    public class VehicleController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        private Animator animator;
        
        private Rigidbody2D rb;
        private PlayerController playerController;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            
            // Rigidbody2D 설정
            if (rb != null)
            {
                rb.gravityScale = 0; // 중력 제거
                rb.constraints = RigidbodyConstraints2D.FreezeRotation; // 회전 고정
            }
        }

        private void Update()
        {
            // 이동 입력 처리
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            // 8방향 애니메이션 파라미터 설정
            animator.SetBool("IsRight", horizontal > 0);
            animator.SetBool("IsLeft", horizontal < 0);
            animator.SetBool("IsUp", vertical > 0);
            animator.SetBool("IsDown", vertical < 0); 

            // 이동 벡터 계산
            Vector2 movement = new Vector2(horizontal, vertical).normalized;

            // 이동 적용
            rb.velocity = movement * moveSpeed;

            // E키로 차량에서 나가기
            if (Input.GetKeyDown(KeyCode.E) && playerController != null)
            {
                playerController.ExitVehicle();
            }
        }

        // 플레이어 컨트롤러 설정 메서드
        public void SetPlayerController(PlayerController controller)
        {
            playerController = controller;
        }
    }
}