namespace MiniGame2
{
    using UnityEngine;
    using TMPro;

    public class UIManager : MonoBehaviour
    {
        [Header("Score UI")]
        [SerializeField] private TextMeshProUGUI killCountText;
        [SerializeField] private TextMeshProUGUI survivalTimeText;

        [Header("Game Over UI")]
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TextMeshProUGUI finalScoreText;  // 최종 점수 표시

        private float survivalTime = 0f;
        private bool isGameOver = false;

        void Start()
        {
            gameOverPanel.SetActive(false);
            UpdateScore(0);
        }

        void Update()
        {
            if(!isGameOver)
            {
                survivalTime += Time.deltaTime;
                UpdateSurvivalTime();
            }
            else if(Input.GetMouseButtonDown(0))  // 게임오버 상태에서 클릭 시
            {
                MiniGame2.GameManager.Instance.RestartGame();
            }
        }

        public void UpdateScore(int killCount)
        {
            killCountText.text = $"처치: {killCount}";
        }

        private void UpdateSurvivalTime()
        {
            int minutes = Mathf.FloorToInt(survivalTime / 60);
            int seconds = Mathf.FloorToInt(survivalTime % 60);
            survivalTimeText.text = $"생존: {minutes:00}:{seconds:00}";
        }

        public void SetGameOver()
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
            finalScoreText.text = $"최종 점수: {MiniGame2.GameManager.Instance.GetCurrentScore()}";
        }
    }
}