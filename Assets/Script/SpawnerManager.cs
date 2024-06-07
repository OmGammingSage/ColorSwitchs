using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorSwitch
{
    public class SpawnerManager : MonoBehaviour
    {
        public static SpawnerManager Instance;
        
        [SerializeField] private GameObject _StarePrefab;

        [SerializeField] private GameObject _ColorShangePrefab;

        [SerializeField] private List<GameObject> _Obstacle;

        [SerializeField] private int _NoOfObstacleSame;

        [SerializeField] private float _DistanceOfObstacle;

        private float _LastPosY = 0;

        private List<GameObject> _Objects;
        private List<GameObject> _StareObstacle;
        private List<GameObject> _ColorChangerObstacle;

        private void Awake()
        {
            Instance = this;
            SpawneObstacle();
            SpawneStare();
            SpawneColorChange();
        }
        
        public void RestartGame()
        {
            SoundManager.Instance.Play((int)SoundManager.SoundType.BGMusic);
            _LastPosY = 0;
            _LastPosY += _DistanceOfObstacle;
            for (int i = 0; i < 3; i++)
            {
                ActiveObstacle();
            }
        }

        public void OffAllObstacle()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            SoundManager.Instance.StopBGSound((int)SoundManager.SoundType.BGMusic);
        }

        private void SpawneObstacle()
        {
            _Objects = new List<GameObject>(_Obstacle.Count * _NoOfObstacleSame);
            for (int i = 0; i < _Obstacle.Count; i++)
            {
                for (int j = 0; j < _NoOfObstacleSame; j++)
                {
                    GameObject obj = Instantiate(_Obstacle[i], _Obstacle[i].transform.position, Quaternion.identity,
                        transform);
                    if (j / 2 == 0)
                    {
                        _Objects.Add(obj);
                    }
                    else
                    {
                        _Objects.Insert(0, obj);
                    }

                    obj.SetActive(false);
                }
            }
        }

        private void SpawneStare()
        {
            _StareObstacle = new List<GameObject>(_NoOfObstacleSame);
            for (int i = 0; i < _NoOfObstacleSame; i++)
            {
                GameObject Obj = Instantiate(_StarePrefab, transform.position, Quaternion.identity,transform);
                _StareObstacle.Add(Obj);
                Obj.SetActive(false);
            }
        }

        private void SpawneColorChange()
        {
            _ColorChangerObstacle = new List<GameObject>(_NoOfObstacleSame);
            for (int i = 0; i < _NoOfObstacleSame; i++)
            {
                GameObject Obj = Instantiate(_ColorShangePrefab, transform.position, Quaternion.identity,transform);
                _ColorChangerObstacle.Add(Obj);
                Obj.SetActive(false);
            }
        }


        public void ActiveObstacle()
        {
            int count = 0;
            a:
            if (_Objects.Count < count)
            {
                return;
            }

            count++;
            int index = Random.Range(0, _Objects.Count);

            if (!_Objects[index].gameObject.activeInHierarchy)
            {
                _Objects[index].transform.position = new Vector3(_Objects[index].transform.position.x, _LastPosY, 0);
                _Objects[index].gameObject.SetActive(true);
                if (Random.Range(0, 2) == 1)
                {
                    ActiveColorChanger(_LastPosY);
                }
                if (Random.Range(0, 2) == 1)
                {
                    ActiveStare(_LastPosY);
                }
                _LastPosY += _DistanceOfObstacle;
                return;
            }

            goto a;
        }

        private void ActiveStare(float PosY)
        {
            for (int i = 0; i < _StareObstacle.Count; i++)
            {
                if (!_StareObstacle[i].gameObject.activeInHierarchy)
                {
                    _StareObstacle[i].transform.position = new Vector3(0, PosY, 0);
                    _StareObstacle[i].gameObject.SetActive(true);
                    return;
                }
            }
        }

        private void ActiveColorChanger(float YPos)
        {
            for (int i = 0; i < _ColorChangerObstacle.Count; i++)
            {
                if (!_ColorChangerObstacle[i].gameObject.activeInHierarchy)
                {
                    _ColorChangerObstacle[i].transform.position = new Vector3(0, YPos + 3.3f, 0);
                    _ColorChangerObstacle[i].gameObject.SetActive(true);
                    return;
                }
            }
        }
    }
}