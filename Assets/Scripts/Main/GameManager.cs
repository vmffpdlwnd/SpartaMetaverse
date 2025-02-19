using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private GameObject currentPlayerPrefab; 
    private float currentScore = 0f;
    private float bestScore = 0f;
    public bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPlayerPrefab();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadPlayerPrefab()
    {
        currentPlayerPrefab = Resources.Load<GameObject>("SPUM/SPUM_Units/Player0");
        if(currentPlayerPrefab == null)
        {
            Debug.LogError("Player prefab not found in SPUM/SPUM_Units/Player0");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene" || scene.name == "MiniGame1")  
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
    // 프리팹이 존재하는지 확인
    if(currentPlayerPrefab != null)
    {
        // 캐릭터 생성 및 크기 설정
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        GameObject player = Instantiate(currentPlayerPrefab, spawnPosition, Quaternion.identity);
        player.transform.localScale = new Vector3(2f, 2f, 2f);

        // UnitRoot 찾기
        Transform unitRoot = player.transform.GetChild(0);
        if(unitRoot != null)
        {
            GameObject unitObj = unitRoot.gameObject;
            SPUM_Prefabs spumPrefab = player.GetComponent<SPUM_Prefabs>();

            // 태그와 레이어 설정
            unitObj.tag = "Player";
            unitObj.layer = LayerMask.NameToLayer("Layer 1");

            // Sorting Group 설정
            SortingGroup sortingGroup = unitObj.GetComponent<SortingGroup>();
            if(sortingGroup == null)
            {
                sortingGroup = unitObj.AddComponent<SortingGroup>();
            }
            sortingGroup.sortingLayerName = "Layer 1";
            sortingGroup.sortingOrder = 2;

            // Rigidbody2D 설정
            Rigidbody2D rb = unitObj.GetComponent<Rigidbody2D>();
            if(rb == null)
            {
                rb = unitObj.AddComponent<Rigidbody2D>();
            }
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            // BoxCollider2D 설정
            BoxCollider2D boxCollider = unitObj.GetComponent<BoxCollider2D>();
            if(boxCollider == null)
            {
                boxCollider = unitObj.AddComponent<BoxCollider2D>();
            }
            boxCollider.offset = new Vector2(0f, 0.3f);
            boxCollider.size = new Vector2(0.5f, 0.6f);

            // PlayerController 설정 및 SPUM 연결
            PlayerController controller = unitObj.AddComponent<PlayerController>();
            if(spumPrefab != null)
            {
                controller.spumPrefab = spumPrefab;
            }

            // 카메라 설정
            Camera mainCamera = Camera.main;
            if(mainCamera != null)
            {
                CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
                if(cameraFollow != null)
                {
                    cameraFollow.target = unitRoot;
                }
            }
        }
    }
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
        SceneManager.LoadScene("MiniGame1");
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}