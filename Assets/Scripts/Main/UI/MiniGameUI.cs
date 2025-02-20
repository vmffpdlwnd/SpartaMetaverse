using TMPro;
using UnityEngine;
using System.Linq; // LINQ 사용을 위해 추가

public class MiniGameUI : MonoBehaviour
{
    [System.Serializable]
    public class GameInfo
    {
        public string sceneName;
        public string gameName;
        public string gameRule;
        public string gameControl;
    }

    public TextMeshProUGUI gameNameText;
    public TextMeshProUGUI gameRuleText;
    public TextMeshProUGUI gameControlText;
    public TextMeshProUGUI bestScoreText;

    public GameInfo[] games;

    void Awake()
    {
        games = new GameInfo[]
        {
            new GameInfo
            {
                sceneName = "MiniGame1",
                gameName = "플러피 플레이어",
                gameRule = "장애물을 피해 최대한 멀리 날아가세요!",
                gameControl = "마우스 좌클릭 또는 스페이스바로 점프",
            },
            new GameInfo
            {
                sceneName = "MiniGame2",
                gameName = "플레이어를 지켜라",
                gameRule = "주변에서 다가오는 적들을 클릭해서 제거하고 생존하세요!\n적이 플레이어에게 닿으면 게임 오버!",
                gameControl = "마우스로 적을 클릭하여 제거",
            }
        };
    }

    public void SetGameInfo(string gameName, bool isReady)
    {
        if (!isReady)
        {
            gameNameText.text = "게임 준비 중입니다.";
            gameRuleText.text = " ";
            gameControlText.text = " ";
            bestScoreText.text = " ";
        }
        else
        {
            GameInfo currentGame = games.FirstOrDefault(g => g.sceneName == gameName);

            // currentGame이 null인 경우 처리
            if (currentGame == null)
            {
                gameNameText.text = "게임 정보를 찾을 수 없습니다.";
                gameRuleText.text = " ";
                gameControlText.text = " ";
                bestScoreText.text = " ";
                return;
            }

            gameNameText.text = currentGame.gameName;
            gameRuleText.text = currentGame.gameRule;
            gameControlText.text = currentGame.gameControl;
            
            // PlayerPrefs에서 직접 최고 점수 읽기
            int bestScore = PlayerPrefs.GetInt("MiniGame1BestScore", 0);
            bestScoreText.text = $"{bestScore}";
        }
    }
}