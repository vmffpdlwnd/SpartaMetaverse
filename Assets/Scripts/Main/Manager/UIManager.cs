namespace Main
{
    using UnityEngine;

    public class UIManager : MonoBehaviour
    {
        public GameObject miniGameUI; // 미니게임 팝업 UI
        private MiniGameUI miniGameUIComponent;

        public GameObject talkUI; // NPC 대화 UI  
        private TalkUI talkUIComponent;

        public GameObject customizationUI;
        private CustomizationUI customizationUIComponent;

        void Start()
        {
            miniGameUI.SetActive(false);
            if (miniGameUI != null)
            {
                miniGameUIComponent = miniGameUI.GetComponent<MiniGameUI>();
            }

            talkUI.SetActive(false);
            if (talkUI != null) 
            {
                talkUIComponent = talkUI.GetComponent<TalkUI>();
            }
             if (customizationUI != null)
            {
                customizationUIComponent = customizationUI.GetComponent<CustomizationUI>();
            }
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

        public void ShowNPCDialog()
        {
            if (talkUI != null && talkUIComponent != null)
            {
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
        public void ProgressDialog()
        {
            if (talkUIComponent != null)
            {
                talkUIComponent.ShowNextDialogLine();
            }
        }

    }
}