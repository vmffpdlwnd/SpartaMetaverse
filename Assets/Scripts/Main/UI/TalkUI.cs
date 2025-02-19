using UnityEngine;
using UnityEngine.UI;

public class TalkUI : MonoBehaviour
{
    public Image playerImage;
    public Image targetImage;
    public Text dialogText;

    private int dialogIndex = 0;
    private string[] dialogLines;

    public void SetDialogText(string[] lines)
    {
        dialogLines = lines;
        dialogIndex = 0;
        ShowNextDialogLine();
    }

    private void ShowNextDialogLine()
    {
        if (dialogIndex < dialogLines.Length)
        {
            dialogText.text = dialogLines[dialogIndex];

            // 말하는 대상에 따라 이미지 알파값 조정
            if (dialogLines[dialogIndex].StartsWith("플레이어"))
            {
                playerImage.color = Color.white;
                targetImage.color = new Color(1f, 1f, 1f, 0.5f);
            }
            else
            {
                playerImage.color = new Color(1f, 1f, 1f, 0.5f);
                targetImage.color = Color.white;
            }

            dialogIndex++;
        }
        else
        {
            // 대화 종료
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShowNextDialogLine();
        }
    }
}