using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public GameObject playerPrefab;  // 현재 사용중인 캐릭터 프리팹
    private float currentScore = 0f;
    private float bestScore = 0f;
    public bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 씬 로드될 때마다 호출
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MiniGame")  // 미니게임 씬 이름
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        // 플레이어 스폰 위치에 캐릭터 생성
        Vector3 spawnPosition = new Vector3(0, 0, 0);  // 적절한 스폰 위치 설정
        Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    }

    public void GameOver()
    {
        isGameOver = true;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
        }
    }

    public void RestartGame()
    {
        isGameOver = false;
        currentScore = 0;
        SceneManager.LoadScene("MiniGame");
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("Main");  // 메인 씬 이름
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}