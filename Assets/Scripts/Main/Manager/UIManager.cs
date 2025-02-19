namespace Main
{
    using UnityEngine;

    public class UIManager : MonoBehaviour
    {
        public GameObject miniGameUI; // 미니게임 팝업 UI
        private MiniGameUI miniGameUIComponent;

<<<<<<< Updated upstream
=======
        public GameObject talkUI; // NPC 대화 UI  
        private TalkUI talkUIComponent;

>>>>>>> Stashed changes

        void Start()
        {
            miniGameUI.SetActive(false);
            miniGameUIComponent = miniGameUI.GetComponent<MiniGameUI>();
<<<<<<< Updated upstream
=======

            talkUI.SetActive(false);
            talkUIComponent = talkUI.GetComponent<TalkUI>();
>>>>>>> Stashed changes
        }

        public void ShowMiniGame(string gameName, bool isReady)
        {
            if (miniGameUI != null)
            {   
                if (miniGameUIComponent != null)
                {
                    miniGameUIComponent.SetGameInfo(gameName, isReady);
                    miniGameUI.SetActive(true);
                }
            }
        }
        public void HideMiniGame()
        {
            if (miniGameUI != null)
            {
                miniGameUI.SetActive(false);
            }
        }
<<<<<<< Updated upstream
=======

        public void ShowNPCDialog(string[] dialogText)
        {
            if (talkUI != null && talkUIComponent != null)
            {
                talkUIComponent.SetDialogText(dialogText);
                talkUI.SetActive(true);
            }
        }

        public void HideNPCDialog()
        {
            if (talkUI != null)
            {
                talkUI.SetActive(false);
            }
        }
>>>>>>> Stashed changes
    }
}