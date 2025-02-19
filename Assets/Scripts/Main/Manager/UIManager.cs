namespace Main
{
    using UnityEngine;

    public class UIManager : MonoBehaviour
    {
        public GameObject miniGameUI; // 미니게임 팝업 UI
        private MiniGameUI miniGameUIComponent;


        void Start()
        {
            miniGameUI.SetActive(false);
            miniGameUIComponent = miniGameUI.GetComponent<MiniGameUI>();
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
    }
}