namespace Main
{
   using UnityEngine;
   using UnityEngine.UI;
   using UnityEngine.SceneManagement;

   public class CustomizationUI : MonoBehaviour
   {
       [Header("UI Elements")]
       [SerializeField] private Button characterButton;    // 메인 토글 버튼
       [SerializeField] private GameObject characterListUI;

       [Header("List Buttons")]
       [SerializeField] private Button searchButton;       // 파일 탐색 버튼
       [SerializeField] private Button createButton;       // 캐릭터 생성 버튼
       [SerializeField] private Button refreshButton;      // 월드 새로고침 버튼

       private UIManager uiManager;
       private bool isListVisible = false;

       private void Start()
       {
           uiManager = FindObjectOfType<UIManager>();
           
           // 메인 버튼 토글 기능
           characterButton.onClick.AddListener(ToggleCharacterList);
           
           // 리스트 내부 버튼들 기능 연결
           searchButton.onClick.AddListener(CheckCharacterFiles);
           createButton.onClick.AddListener(GoToCustomizationScene);
           refreshButton.onClick.AddListener(RefreshWorld);

           if(characterListUI != null)
               characterListUI.SetActive(false);

           CheckCharacterFiles(); // 초기 파일 체크
       }

       private void ToggleCharacterList()
       {
           isListVisible = !isListVisible;
           characterListUI.SetActive(isListVisible);
       }

       private void CheckCharacterFiles()
       {
           Object[] characterFiles = Resources.LoadAll("SPUM/SPUM_Units", typeof(GameObject));

           if (characterFiles == null || characterFiles.Length == 0)
           {
               Debug.Log("No character files found. Moving to customization scene.");
               SceneManager.LoadScene("Customization");
           }
           else
           {
               Debug.Log($"Found {characterFiles.Length} character files.");
           }
       }

       private void GoToCustomizationScene()
       {
            SceneManager.LoadScene("Customization");
       }

       private void RefreshWorld()
        {
            // 먼저 GameManager의 플레이어 프리팹을 강제로 다시 로드
            GameManager.Instance.LoadPlayerPrefab();
            
            // 그 다음 씬 리로드
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}