using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkUI : MonoBehaviour
{
    public Image playerImage;
    public Image targetImage;
    public TextMeshProUGUI dialogText;

    // 대화 세트 정의
    private string[][] dialogSets = new string[][] 
    {
        new string[] {
            "똑똑",
            "누구야! 보험 팔러 왔으면 안 받아!",
            "아니요...",
            "그럼 뭐야?",
            "좋은 말씀 전하러 왔습니다.",
            "........"
        },
        new string[] {
            "똑똑",
            "(놀란 표정) 어? 너야?",
            "아니요?",
            "아 전 애인 줄 알았어! 빨리 가!",
            "???",
            "하, 잘못 왔네."
        },
        new string[] {
            "똑똑",
            "누구세요?",
            "사~랑해요~!",
            "쾅!",
            "너무 심하게 기분 좋겠 했나 보다" 
        }
    };

    private int dialogIndex = 0;
    private string[] currentDialogLines = null;
    private float originalCameraSize; // 원래 카메라 크기 저장용

    private void Start()
    {
        if (Camera.main != null)
        {
            originalCameraSize = Camera.main.orthographicSize; // 시작할 때 원래 카메라 크기 저장
        }
    }

    private void OnEnable()
    {
        // 랜덤하게 대화 세트 선택
        int randomSetIndex = Random.Range(0, dialogSets.Length);
        currentDialogLines = dialogSets[randomSetIndex];
        
        dialogIndex = 0;
        ShowDialog();
    }

    public void ShowNextDialogLine()
    {
        dialogIndex++;
        
        // 대화가 첫 번째 줄일 때만 캡처 실행
        if(dialogIndex == 1 && Camera.main != null)
        {
            StartCoroutine(CaptureCamera(Camera.main));
        }
        
        ShowDialog();
    }

    private System.Collections.IEnumerator CaptureCamera(Camera cam)
    {
        // 현재 카메라 크기 저장
        float currentSize = cam.orthographicSize;

        // UI 숨기기
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in canvases)
        {
            canvas.enabled = false;
        }

        // 캡처용 확대
        cam.orthographicSize = 1.5f;
        
        yield return new WaitForEndOfFrame();
        
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        // 카메라 크기 복구
        cam.orthographicSize = currentSize;

        // UI 복구
        foreach (Canvas canvas in canvases)
        {
            canvas.enabled = true;
        }

        // 스프라이트 설정
        Sprite sprite = Sprite.Create(screenShot, 
            new Rect(0, 0, screenShot.width, screenShot.height),
            new Vector2(0.5f, 0.5f));

        playerImage.sprite = sprite;
        playerImage.preserveAspect = true;
    }

     private void ShowDialog()
    {
        // 강력한 null 체크
        if (currentDialogLines == null || 
            dialogIndex >= currentDialogLines.Length)
        {
            gameObject.SetActive(false);
            return;
        }

        // 대화 텍스트 설정
        dialogText.text = currentDialogLines[dialogIndex];

        // 첫 대화는 항상 플레이어 대사로 취급
        if (dialogIndex %2 == 0)
        {
            playerImage.color = Color.white;
            targetImage.color = new Color(1f, 1f, 1f, 0.3f);
        }
        else
        {
            // 대화 순서에 따라 이미지 알파값 토글
            playerImage.color = new Color(1f, 1f, 1f, 0.3f);
            targetImage.color = Color.white;
        }
    }
}