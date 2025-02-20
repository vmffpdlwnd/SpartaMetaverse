namespace Main
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using TMPro;
    using System.Collections.Generic;

    public class CustomizationUI : MonoBehaviour
    {
        [SerializeField] private Button characterButton;    //메인 토글 버튼튼
        [SerializeField] private GameObject characterListUI;

        [SerializeField] private Button createButton;      // 캐릭터 생성
        [SerializeField] private Button refreshButton;      // 월드 새로고침
        [SerializeField] private Button selectButton;      // 캐릭터 선택 => 누를때마다 캐릭터 바뀜
        [SerializeField] private TextMeshProUGUI selectButtonText; // 현재 캐릭터 번호 출력력

        private int currentNumber = 0;
        private int maxCharacterCount = 0;
        private UIManager uiManager;
        private bool isListVisible = false;

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
            
            characterButton.onClick.AddListener(ToggleCharacterList); //메인 버튼 기능
            createButton.onClick.AddListener(GoToCustomizationScene); // 캐릭터 생성 기능
            selectButton.onClick.AddListener(NextCharacter); // 캐릭터 변경 기능능
            
            if (refreshButton != null)
                refreshButton.onClick.AddListener(RefreshWorld); // 월드 새로 고침 기능능

            if(characterListUI != null)
                characterListUI.SetActive(false);

            UpdateCharacterCount();
            UpdateSelectButtonNumber();
        }

        private List<int> characterNumbers = new List<int>();

        private void UpdateCharacterCount()
        {
            Object[] characterFiles = Resources.LoadAll("SPUM/SPUM_Units", typeof(GameObject));
            characterNumbers.Clear();
            
            foreach(Object file in characterFiles)
            {
                if(file.name.StartsWith("Player"))
                {
                    string numStr = file.name.Substring("Player".Length);
                    if(int.TryParse(numStr, out int num))
                    {
                        characterNumbers.Add(num);
                    }
                }
            }
            characterNumbers.Sort();  // 번호 순서대로 정렬
            maxCharacterCount = characterNumbers.Count;
            
            Debug.Log($"Found {maxCharacterCount} character files");
            foreach(int num in characterNumbers)
            {
                Debug.Log($"Character number found: {num}");
            }
        }

        private void NextCharacter()
        {
            if (maxCharacterCount > 0)
            {
                int currentIndex = characterNumbers.IndexOf(currentNumber);
                currentIndex = (currentIndex + 1) % maxCharacterCount;
                currentNumber = characterNumbers[currentIndex];
                
                Debug.Log($"Changed to character number: {currentNumber}");
                UpdateSelectButtonNumber();
                SelectCharacter();
            }
        }

        private void UpdateSelectButtonNumber()
        {
            selectButtonText.text = $"캐릭터 {currentNumber}";
        }

        private void SelectCharacter()
        {
            if(GameManager.Instance != null)
            {
                GameManager.Instance.LoadPlayerPrefab(currentNumber);
            }
        }

        private void ToggleCharacterList()
        {
            isListVisible = !isListVisible;
            characterListUI.SetActive(isListVisible);
            
            // 리스트를 열 때 현재 GameManager의 캐릭터 번호로 UI 업데이트
            if(isListVisible && GameManager.Instance != null)
            {
                currentNumber = GameManager.Instance.currentPlayerNumber;
                UpdateSelectButtonNumber();
            }
        }

        private void RefreshWorld()
        {
            int previousNumber = currentNumber;  // 현재 번호 저장
            SceneManager.LoadScene("MainScene");
            currentNumber = previousNumber;  // 번호 복원
        }

        private void GoToCustomizationScene()
        {
            SceneManager.LoadScene("Customization");
        }
    }
}