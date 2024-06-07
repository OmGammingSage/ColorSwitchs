using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorSwitch
{
    public class SoundManager : MonoBehaviour
    {
        #region GetInstance

        public static SoundManager Instance;

        private void GetInstance()
        {
            Instance = this;
        }

        #endregion

        public enum SoundType
        {
            BGMusic,
            Star,
            Dead,
            Jump,
            Button,
            ColorChange,
            Hit
        }

        [Serializable]
        private class Sounds
        {
            public SoundType _Sound;
            public AudioSource _AudioSource;
        }

        private void Awake()
        {
            GetInstance();
        }

        [SerializeField] private List<Sounds> _sounds;

        public void Play(int Index)
        {
            for (int i = 0; i < _sounds.Count; i++)
            {
                if ((int)_sounds[i]._Sound == Index)
                {
                    if (Index == 0)
                    {
                        if (GameData.Instance.GetBGSound)
                        {
                        _sounds[i]._AudioSource.Play();
                        }
                        return;
                    }
                    else if(GameData.Instance.GetOtherSound)
                    {
                        _sounds[i]._AudioSource.Play();
                    }
                    return;
                }
            }
        }

        public void StopBGSound(int index)
        {
            _sounds[index]._AudioSource.Stop();
        }
    }
}