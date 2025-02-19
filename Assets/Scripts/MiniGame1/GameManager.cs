namespace MiniGame1
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
                    // 씬에서 찾아보기
                    instance = FindObjectOfType<GameManager>();
                    
                    // 여전히 없다면 새로 생성
                    if (instance == null)
                    {
                        GameObject managerObject = new GameObject("MiniGameManager");
                        instance = managerObject.AddComponent<GameManager>();
                    }
                }
                return instance; 
            }
        }
        
        private int currentScore = 0;
        private int bestScore = 0;
        MiniGame1.UIManager uiManager;
        public MiniGame1.UIManager UIManager
        {
            get { return uiManager; }
        }
                
        private void Awake()
        {
            // 이미 다른 인스턴스가 있다면 현재 객체 파괴
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            uiManager = FindObjectOfType<UIManager>();
            DontDestroyOnLoad(gameObject);
            bestScore = PlayerPrefs.GetInt("MiniGame1BestScore", 0);
        }
        void Start()
        {
            uiManager.UpdateScore(0);   
        }
        public void GameOver()
        {
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                PlayerPrefs.SetInt("MiniGame1BestScore", bestScore);
                PlayerPrefs.Save(); // 변경사항 즉시 저장
            }

            uiManager.SetRestart();
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene("MainScene"); // 게임오버 시 메인씬으로 이동
        }

        public void AddScore(int score)
        {
            currentScore += score;
            uiManager.UpdateScore(currentScore);
        }
    }
}