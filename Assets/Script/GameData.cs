using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorSwitch
{
    public class GameData : MonoBehaviour
    {
        #region GetInstance

        public static GameData Instance;

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

        private bool IsBgSounPlay;
        private bool IsOtherSounPlay;

        private bool IsFirstTimeTutorial;
        private int _HighScore;
        private int _Star;

        private void Awake()
        {
            GetInstance();
            LoadData();
        }

        public bool GetTutorial
        {
            get { return IsFirstTimeTutorial; }
        }

        public void TutorialOff()
        {
            IsFirstTimeTutorial = true;
        }

        public int HighScore
        {
            get { return _HighScore; }
        }

        public void SetHighScore(int Score)
        {
            _HighScore = Score;
        }


        public int GetStar
        {
            get { return _Star; }
        }

        public void SetStar(int star)
        {
            _Star = star;
        }


        public bool GetBGSound
        {
            get { return IsBgSounPlay; }
        }

        public void SetBgSound()
        {
            IsBgSounPlay = !IsBgSounPlay;
        }


        public bool GetOtherSound
        {
            get { return IsOtherSounPlay; }
        }

        public void SetOtherSound()
        {
            IsOtherSounPlay = !IsOtherSounPlay;
        }


        private void LoadData()
        {
            IsFirstTimeTutorial = (PlayerPrefs.GetInt("Tutorial", 0) == 1 ? true : false);
            _HighScore = PlayerPrefs.GetInt("HighScore", 0);
            _Star = PlayerPrefs.GetInt("Star", 0);
            IsBgSounPlay = (PlayerPrefs.GetInt("GbSound", 1) == 1 ? true : false);
            IsOtherSounPlay = (PlayerPrefs.GetInt("OtherSound", 1) == 1 ? true : false);
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt("Tutorial", ((IsFirstTimeTutorial) ? 1 : 0));
            PlayerPrefs.SetInt("HighScore", _HighScore);
            PlayerPrefs.SetInt("Star", _Star);
            PlayerPrefs.SetInt("GbSound", ((IsBgSounPlay) ? 1 : 0));
            PlayerPrefs.SetInt("OtherSound", ((IsOtherSounPlay) ? 1 : 0));
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }
    }
}