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
                    instance = FindObjectOfType<GameManager>();
                    
                    if (instance == null)
                    {
                        GameObject managerObject = new GameObject("MiniGame2Manager");
                        instance = managerObject.AddComponent<GameManager>();
                    }
                }
                return instance; 
            }
        }
        
        private int currentScore = 0;
        private int bestScore = 0;
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
        }

        void Start()
        {
            // Start에서 UIManager 찾기
            uiManager = FindObjectOfType<MiniGame2.UIManager>();
            if(uiManager != null)
            {
                uiManager.UpdateScore(0);   
            }
            else
            {
                Debug.LogError("UIManager not found!");
            }
        }

        public int GetCurrentScore()
        {
            return currentScore;
        }

        public void GameOver()
        {
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                PlayerPrefs.SetInt("MiniGame2BestScore", bestScore);
                PlayerPrefs.Save();
            }

            if(uiManager != null)
            {
                uiManager.SetGameOver();
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
        
    }
}