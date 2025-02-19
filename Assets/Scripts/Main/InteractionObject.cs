using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionObject : MonoBehaviour
{
    bool canInteract = false;
    public string gameName;
    public bool isReady = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color highlightColor = new Color(0.8f, 0.8f, 1f, 1f); // 밝은 하늘색

    private Main.UIManager uiManager;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        uiManager = FindObjectOfType<Main.UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true;
            spriteRenderer.color = highlightColor;
            
            // 게임 이름만 UIManager에 전달
            if (uiManager != null)
                uiManager.ShowMiniGame(gameName, isReady);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
            spriteRenderer.color = originalColor;

            if (uiManager != null)
                uiManager.HideMiniGame();
        }
    }

    private void Update()
    {
        if (canInteract && isReady && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(gameName);
        }
    }
}