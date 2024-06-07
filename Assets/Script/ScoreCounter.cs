using System;
using TMPro;
using UnityEngine;

namespace ColorSwitch
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _Score;
        [SerializeField] private TextMeshProUGUI _CurrentScore;
        [SerializeField] private TextMeshProUGUI _HighScore1;
        [SerializeField] private TextMeshProUGUI _HighScore2;
        [SerializeField] private TextMeshProUGUI _Star;

        #region GetInstance

        public static ScoreCounter Instance;
        
        private void GetInstance()
        {
            Instance = this;
        }

        #endregion

        private void Awake()
        {
            GetInstance();
        }

        private void Start()
        {
            _Score.text = "0";
            _CurrentScore.text = "0";
            _HighScore1.text = GameData.Instance.HighScore.ToString();
            _HighScore2.text = _HighScore1.text;
            _Star.text = GameData.Instance.GetStar.ToString();
        }

        public void SetStar()
        {
            _HighScore2.text = GameData.Instance.HighScore.ToString();
            _Star.text = GameData.Instance.GetStar.ToString();
            _CurrentScore.text = 0.ToString();
        }

        public void AddScore()
        {
            int.TryParse(_CurrentScore.text, out int newScore);
            newScore++;
            _CurrentScore.text = (newScore).ToString();
        }
        
        public void SetScore()
        {
            _Score.text = _CurrentScore.text;
            int.TryParse(_Score.text, out int newScore);
            if (newScore > GameData.Instance.HighScore)
            {
                GameData.Instance.SetHighScore(newScore);
            }
            _HighScore1.text = GameData.Instance.HighScore.ToString(); 
        }

        public void ResetScore()
        {
            _CurrentScore.text = "0";
        }
    }
}
