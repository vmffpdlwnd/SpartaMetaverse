namespace MiniGame2
{
    using UnityEngine;

    public class Enemy : MonoBehaviour 
    {
        [SerializeField] private float moveSpeed = 2f;
        private Vector3 targetPosition = Vector3.zero;
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D boxCollider;

        void Start()
        {
            // 컴포넌트 추가
            if (GetComponent<BoxCollider2D>() == null)
            {
                boxCollider = gameObject.AddComponent<BoxCollider2D>();
                boxCollider.isTrigger = true;
            }

            if (GetComponent<Rigidbody2D>() == null)
            {
                Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            }
        }

        void Update() 
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // 방향에 따라 스프라이트 좌우 반전
            if(direction.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }

        void OnMouseDown() 
        {
            GameManager.Instance.AddScore(1);
            Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.CompareTag("Player")) 
            {
                GameManager.Instance.GameOver();
                Destroy(gameObject);
            }
        }
    }
}