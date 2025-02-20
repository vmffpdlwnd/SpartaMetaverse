namespace MiniGame1
{
    using TMPro;
    using UnityEngine;

    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI survivalTimeText;
        public TextMeshProUGUI gameoverText;

        private float survivalTime = 0f;
        private bool isGameOver = false;

        public void Start()
        {
            if (gameoverText == null)
            {
                Debug.LogError("restart text is null");
            }
            
            if (scoreText == null)
            {
                Debug.LogError("scoreText is null");
                return;
            }
            
            if(gameoverText != null)
            {
                gameoverText.gameObject.SetActive(false);
            }

            UpdateSurvivalTime(); // 초기화
        }
        void Update()
        {
            if(!isGameOver)
            {
                survivalTime += Time.deltaTime;
                UpdateSurvivalTime();
            }
        }
         private void UpdateSurvivalTime()
        {
            if(survivalTimeText != null)
            {
                int minutes = Mathf.FloorToInt(survivalTime / 60);
                int seconds = Mathf.FloorToInt(survivalTime % 60);
                survivalTimeText.text = $"{minutes:00}:{seconds:00}";
            }
        }

        public float GetSurvivalTime()
        {
            return survivalTime;
        }

        public void SetRestart()
        {
            isGameOver = true;
            if(gameoverText != null)
            {
                gameoverText.gameObject.SetActive(true);
            }
        }

        public void UpdateScore(int score)
        {
            if(scoreText != null)
            {
                scoreText.text = score.ToString();
            }
        }
    }
}
