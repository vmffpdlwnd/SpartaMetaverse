namespace MiniGame2
{
    using UnityEngine;
    using TMPro;

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI killCountText;
        [SerializeField] private TextMeshProUGUI survivalTimeText;

        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TextMeshProUGUI finalScoreText;  // 최종 점수 표시

        private float survivalTime = 0f;
        private int killCount = 0;
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

        public void UpdateScore(int newKillCount)
        {
            this.killCount = newKillCount;  // 이 부분이 누락되어 있었습니다
            killCountText.text = $"처치: {killCount}";
        }

        private void UpdateSurvivalTime()
        {
            int minutes = Mathf.FloorToInt(survivalTime / 60);
            int seconds = Mathf.FloorToInt(survivalTime % 60);
            survivalTimeText.text = $"생존: {minutes:00}:{seconds:00}";
        }

        public float GetSurvivalTime()
        {
            return survivalTime;
        }

        public void SetGameOver()
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
            finalScoreText.text = $"처치: {killCount}\n생존시간: {Mathf.FloorToInt(survivalTime / 60):00}:{Mathf.FloorToInt(survivalTime % 60):00}";
        }
    }
}