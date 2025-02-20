namespace Main
{
    using System.Collections;
    using UnityEngine;

    public class PlayerController : MonoBehaviour 
    {
        public SPUM_Prefabs spumPrefab;
        private Rigidbody2D rb;
        public Animator animator;
        public float moveSpeed = 2.5f;
        private bool isAttacking = false;
        private bool isDead = false;
        private float baseScaleX = 2f;

        [SerializeField] public VehicleController vehiclePrefab; // 차량 프리팹
        private VehicleController currentVehicle;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if(spumPrefab != null)
            {
                animator = spumPrefab._anim;
            }
            baseScaleX = transform.localScale.x;
        }

        void Update()
        {
            // 차량 상태일 때 E키로 나가기
            if (currentVehicle != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // SPUM 캐릭터 다시 활성화
                    if(spumPrefab != null)
                    {
                        spumPrefab.gameObject.SetActive(true);
                    }
                    
                    // 카메라 타겟을 다시 플레이어로 변경
                    CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
                    if (cameraFollow != null)
                    {
                        cameraFollow.target = this.transform;
                    }
                    
                    // 차량 제거
                    Destroy(currentVehicle.gameObject);
                    currentVehicle = null;
                    return;
                }
            }

            // x키는 먼저 체크하도록 변경
            if(Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(ToggleDie());
            }

            // Die 상태에서는 이동과 공격만 제한
            if(animator.GetBool("Die")) 
            {
                rb.velocity = Vector2.zero;
                return;
            }

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            
            Vector2 movement = new Vector2(horizontal, vertical).normalized;
            rb.velocity = movement * moveSpeed;

            if(horizontal != 0)
            {
                transform.localScale = new Vector3(baseScaleX * -Mathf.Sign(horizontal), baseScaleX, baseScaleX);
            }

            if(Input.GetKeyDown(KeyCode.Z) && !isAttacking)
            {
                StartCoroutine(Attack());
            }

            if(!isAttacking)
            {
                if(movement != Vector2.zero)
                    spumPrefab.PlayAnimation(1);
                else
                    spumPrefab.PlayAnimation(0);
            }

            // 차량 교체 시스템
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(vehiclePrefab != null)
                {
                    // SPUM 캐릭터 숨기기
                    if(spumPrefab != null)
                    {
                        spumPrefab.gameObject.SetActive(false);
                    }
                    
                    // 차량 생성
                    currentVehicle = Instantiate(vehiclePrefab, transform.position, Quaternion.identity);
                    
                    // 플레이어 컨트롤러 설정
                    currentVehicle.GetComponent<VehicleController>().SetPlayerController(this);
                    
                    // 카메라 타겟 변경
                    CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
                    if (cameraFollow != null)
                    {
                        cameraFollow.target = currentVehicle.transform;
                    }
                }
            }

        }

        IEnumerator Attack()
        {
            isAttacking = true;
            spumPrefab.PlayAnimation(3);
            yield return new WaitForSeconds(0.5f);
            isAttacking = false;
        }

        IEnumerator ToggleDie()
        {
            isDead = !isDead;
            if(isDead)
            {
                animator.SetBool("Die", true);
                rb.velocity = Vector2.zero;
            }
            else
            {
                animator.SetBool("Die", false);
                spumPrefab.PlayAnimation(0);
            }
            yield return null;
        }

        public void ExitVehicle()
        {
            // SPUM 캐릭터 다시 활성화
            if(spumPrefab != null)
            {
                spumPrefab.gameObject.SetActive(true);
            }
            
            // 카메라 타겟을 다시 플레이어로 변경
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.target = this.transform;
            }
            
            // 차량 제거
            Destroy(currentVehicle.gameObject);
            currentVehicle = null;
        }
    }
}