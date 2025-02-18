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
        Debug.Log("Spawning Player...");
        if(currentPlayerPrefab != null)
        {
            Debug.Log("Player Prefab found!");
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            GameObject player = Instantiate(currentPlayerPrefab, spawnPosition, Quaternion.identity);
            
            player.transform.localScale = new Vector3(2f, 2f, 2f);

            Transform unitRoot = player.transform.GetChild(0);  // UnitRoot는 첫 번째 자식
            if(unitRoot != null)
            {
                GameObject unitObj = unitRoot.gameObject;
                
                // 태그와 레이어 설정
                unitObj.tag = "Player";
                unitObj.layer = LayerMask.NameToLayer("Layer 1");

                // SortingGroup이 없을 때만 추가
                SortingGroup sortingGroup = unitObj.GetComponent<SortingGroup>();
                if(sortingGroup == null)
                {
                    sortingGroup = unitObj.AddComponent<SortingGroup>();
                }
                sortingGroup.sortingLayerName = "Layer 1";
                sortingGroup.sortingOrder = 2;

                // Rigidbody2D가 없을 때만 추가
                Rigidbody2D rb = unitObj.GetComponent<Rigidbody2D>();
                if(rb == null)
                {
                    rb = unitObj.AddComponent<Rigidbody2D>();
                }
                rb.gravityScale = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;

                // BoxCollider2D가 없을 때만 추가
                if(unitObj.GetComponent<BoxCollider2D>() == null)
                {
                    unitObj.AddComponent<BoxCollider2D>();
                }

                // PlayerController가 없을 때만 추가
                if(unitObj.GetComponent<PlayerController>() == null)
                {
                    unitObj.AddComponent<PlayerController>();
                }
            }
            else
            {
                Debug.LogError("UnitRoot not found in prefab!");
            }
        }
        else
        {
            Debug.Log("Player Prefab is null!");
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