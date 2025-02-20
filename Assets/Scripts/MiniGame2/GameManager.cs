namespace MiniGame2
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance
        {
            get 
            { 
                if (instance == null)
                {
                    instance = FindObjectOfType<MiniGame2.GameManager>();
                    
                    if (instance == null)
                    {
                        GameObject managerObject = new GameObject("GameManager");
                        instance = managerObject.AddComponent<MiniGame2.GameManager>();
                    }
                }
                return instance; 
            }
        }
        
        private int currentScore = 0;
        private int bestScore = 0;
        private float bestSurvivalTime = 0f;
        private MiniGame2.UIManager uiManager;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            bestScore = PlayerPrefs.GetInt("MiniGame2BestScore", 0);
            bestSurvivalTime = PlayerPrefs.GetFloat("MiniGame2BestTime", 0f);
        }

        void Start()
        {
            // Start에서 UIManager 찾기
            uiManager = FindObjectOfType<MiniGame2.UIManager>();
            if(uiManager != null)
            {
                uiManager.UpdateScore(0);   
            }
        }

        public void GameOver()
        {
            if(uiManager != null)
            {
                float survivalTime = uiManager.GetSurvivalTime();

                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                    PlayerPrefs.SetInt("MiniGame2BestScore", bestScore);
                }
                    
                if (survivalTime > bestSurvivalTime)
                {
                    bestSurvivalTime = survivalTime;
                    PlayerPrefs.SetFloat("MiniGame2BestTime", bestSurvivalTime);
                }

                PlayerPrefs.Save();
                uiManager.SetGameOver();
            }
            else
            {
                // UIManager가 없을 때는 점수만 저장
                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                    PlayerPrefs.SetInt("MiniGame2BestScore", bestScore);
                    PlayerPrefs.Save();
                }
                
                // 바로 메인씬으로 이동
                SceneManager.LoadScene("MainScene");
            }
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene("MainScene");
        }

        public void AddScore(int score)
        {
            if(uiManager != null)  // null 체크 추가
            {
                currentScore += score;
                uiManager.UpdateScore(currentScore);
            }
        }
        public int GetCurrentScore()
        {
            return currentScore;
        }
    }
}