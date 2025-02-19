// MiniGame1/PlayerController.cs
namespace MiniGame1
{
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        public SPUM_Prefabs spumPrefab;
        private Rigidbody2D rb;
        private Animator animator;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if(spumPrefab != null)
            {
                animator = spumPrefab._anim;
            }
        }

        void Update()
        {
            // 플러피버드 스타일 컨트롤 구현
        }
    }
}