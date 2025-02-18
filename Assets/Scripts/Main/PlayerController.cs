using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    private SPUM_Prefabs spumPrefab;
    private Rigidbody2D rb;
    private Animator animator;
    public float moveSpeed = 2.5f;
    private bool isAttacking = false;
    private bool isDead = false;
    private float baseScaleX = 2f;

    void Start()
    {
        spumPrefab = GetComponent<SPUM_Prefabs>();
        rb = GetComponent<Rigidbody2D>();
        animator = spumPrefab._anim;
        baseScaleX = transform.localScale.x;
    }

    void Update()
    {
        

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
        if(Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(ToggleDie());
        }
        if(isDead) return;

        if(!isAttacking)
        {
            if(movement != Vector2.zero)
                spumPrefab.PlayAnimation(1); // Run
            else
                spumPrefab.PlayAnimation(0); // Idle
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
            animator.SetBool("Die", true);  // 누움
            rb.velocity = Vector2.zero;
        }
        else
        {
            animator.SetBool("Die", false);  // 일어남
            spumPrefab.PlayAnimation(0);    // Idle로 복귀
        }
        yield return null;
    }
}