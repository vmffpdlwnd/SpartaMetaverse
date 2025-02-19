namespace MiniGame1
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI gameoverText;

        public void Start()
        {
            if (gameoverText == null)
            {
                Debug.LogError("restart text is null");
            }
            
            if (scoreText == null)
            {
                Debug.LogError("scoreText is null");
                return;
            }
            
            gameoverText.gameObject.SetActive(false);
        }

        public void SetRestart()
        {
            gameoverText.gameObject.SetActive(true);
        }

        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}
