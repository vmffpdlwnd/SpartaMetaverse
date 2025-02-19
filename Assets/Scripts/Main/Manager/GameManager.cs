using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private GameObject currentPlayerPrefab; 

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

    void Start()
    {
        float miniGame1BestScore = PlayerPrefs.GetFloat("MiniGame1BestScore",0);
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
        if (scene.name == "MainScene")  
        {
            SpawnMainPlayer();
        }
        else if(scene.name == "MiniGame1")
        {
            SpawnMiniGamePlayer();
        }
    }

    void SpawnMainPlayer()
    {
        if(currentPlayerPrefab != null)
        {
            // 캐릭터 생성
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            GameObject player = Instantiate(currentPlayerPrefab, spawnPosition, Quaternion.identity);
            player.transform.localScale = new Vector3(2f, 2f, 2f);

            // 태그와 레이어 설정
            player.tag = "Player";
            player.layer = LayerMask.NameToLayer("Layer 1");

            Transform unitRoot = player.transform.GetChild(0);
            if(unitRoot != null)
            {
                GameObject unitObj = unitRoot.gameObject;
                SPUM_Prefabs spumPrefab = player.GetComponent<SPUM_Prefabs>();

                // 태그와 레이어 설정
                unitObj.tag = "Player";
                unitObj.layer = LayerMask.NameToLayer("Layer 1");

                // SortingGroup 설정
                SortingGroup sortingGroup = unitObj.GetComponent<SortingGroup>();
                if(sortingGroup == null)
                {
                    sortingGroup = unitObj.AddComponent<SortingGroup>();
                }
                sortingGroup.sortingLayerName = "Layer 1";
                sortingGroup.sortingOrder = 3;

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

                // 컨트롤러 추가
                Main.PlayerController controller = unitObj.AddComponent<Main.PlayerController>();
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

   void SpawnMiniGamePlayer()
   {
       if(currentPlayerPrefab != null)
       {
           // 캐릭터 생성
           Vector3 spawnPosition = new Vector3(0, 0, 0);
           GameObject player = Instantiate(currentPlayerPrefab, spawnPosition, Quaternion.identity);
           player.transform.localScale = new Vector3(2f, 2f, 2f);

           Transform unitRoot = player.transform.GetChild(0);
           if(unitRoot != null)
           {
                GameObject unitObj = unitRoot.gameObject;
                SPUM_Prefabs spumPrefab = player.GetComponent<SPUM_Prefabs>();

                // 태그와 레이어 설정
                unitObj.tag = "Player";
                unitObj.layer = LayerMask.NameToLayer("Default");

                // SortingGroup 설정
                SortingGroup sortingGroup = unitObj.GetComponent<SortingGroup>();
                if(sortingGroup == null)
                {
                    sortingGroup = unitObj.AddComponent<SortingGroup>();
                }
                sortingGroup.sortingLayerName = "Default";
                sortingGroup.sortingOrder = 100;

                // 캐릭터 회전
                unitObj.transform.rotation = Quaternion.Euler(0, 0, -90);

                // Rigidbody2D 설정
                Rigidbody2D rb = unitObj.GetComponent<Rigidbody2D>();
                if(rb == null)
                {
                    rb = unitObj.AddComponent<Rigidbody2D>();
                }
                rb.gravityScale = 1f;
                

                // BoxCollider2D 설정
                BoxCollider2D boxCollider = unitObj.GetComponent<BoxCollider2D>();
                if(boxCollider == null)
                {
                    boxCollider = unitObj.AddComponent<BoxCollider2D>();
                }
                boxCollider.offset = new Vector2(0f, 0.3f);
                boxCollider.size = new Vector2(0.5f, 0.6f);

                // 카메라 설정
                Camera mainCamera = Camera.main;
                if(mainCamera != null)
                {
                    FollowCamera followCamera = mainCamera.GetComponent<FollowCamera>();
                    if(followCamera != null)
                    {
                        followCamera.target = unitRoot;
                    }
                }

                // 컨트롤러 설정
                MiniGame1.PlayerController controller = unitObj.AddComponent<MiniGame1.PlayerController>();
                if(spumPrefab != null)
                {
                    controller.spumPrefab = spumPrefab;
                }
            }
        }
    }
}