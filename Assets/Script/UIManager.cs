using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorSwitch
{
    public class UIManager : MonoBehaviour
    {
        #region GetInstance

        public static UIManager Instance;
        private void GetInstance()
        {
            if (Instance == null)
            {
            Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        #endregion

        [SerializeField] private GameObject _GamePlay;
        [SerializeField] private GameObject _Tutorial;
        [SerializeField] private GameObject _HomeMenu;
        [SerializeField] private GameObject _GameOverPanal;

        [SerializeField] private ColorSwitchs _ColorSwitch;
        [SerializeField] private PlayerMove _PlayerMove;

        private void Awake()
        {
            GetInstance();
        }

        public void Tutorial()
        {
            if (!GameData.Instance.GetTutorial)
            {
                _Tutorial.SetActive(true);
                _HomeMenu.SetActive(false);
            }
            else
            {
                _HomeMenu.SetActive(false);
                GameStart();
            }
        }

        public void Gameover()
        {
            ScoreCounter.Instance.SetScore();
            _GameOverPanal.SetActive(true);
        }

        public void BackToHome(GameObject obj)
        {
            obj.SetActive(false);
            _HomeMenu.SetActive(true);
        }
        
        public void GameStart()
        {
            _GamePlay.SetActive(true);
            _Tutorial.SetActive(false);
            _PlayerMove.GameRestart();
            _ColorSwitch.GameRestart();
            SpawnerManager.Instance.RestartGame();
            ScoreCounter.Instance.ResetScore();
        }
    }
}
