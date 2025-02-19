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
    
    public bool isNPCDialog = false;

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
            
            if (isNPCDialog)
            {
                // NPC 대화 UI 표시 요청
                if (uiManager != null)
                {
                    uiManager.ShowNPCDialog();
                }
            }
            else
            {
                // 미니게임 UI 표시
                if (uiManager != null)
                    uiManager.ShowMiniGame(gameName, isReady);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
            spriteRenderer.color = originalColor;

            if (isNPCDialog)
            {
                // NPC 대화 UI 숨김 요청
                if (uiManager != null) 
                    uiManager.HideNPCDialog();
            }
            else
            {
                // 미니게임 UI 숨김
                if (uiManager != null)
                    uiManager.HideMiniGame();
            }
        }
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.F))
        {
            if (isNPCDialog)
            {
                // 대화 진행 요청
                if (uiManager != null)
                {
                    uiManager.ProgressDialog();
                }
            }
            else if (isReady)
            {
                // 미니게임 씬 로드
                SceneManager.LoadScene(gameName);
            }
        }
    }
}