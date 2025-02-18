using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionObject : MonoBehaviour
{
    private bool canInteract = false;
    public string sceneName;
    public bool isReady = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color highlightColor = new Color(0.8f, 0.8f, 1f, 1f); // 밝은 하늘색

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true;
            spriteRenderer.color = highlightColor;

            if (!isReady)
            {
                Debug.Log("준비중입니다.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
            spriteRenderer.color = originalColor;
        }
    }

    private void Update()
    {
        if (canInteract && isReady && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}