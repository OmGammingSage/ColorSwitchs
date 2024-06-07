using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorSwitch
{
    public enum ColorType
    {
        Cyan,
        Magenta,
        Yellow,
        Pink
    }

    [Serializable]
    public class Colors
    {
        public ColorType _ColorType;
        public Color _Color;
    }

    public class ColorSwitchs : MonoBehaviour
    {

        internal bool IsDie;

        [SerializeField] private SpriteRenderer _Sprite;

        [SerializeField] private List<Colors> _colors;

        [SerializeField] private ParticleSystem _ParticleSystem;
        [SerializeField] private ParticleSystem _FadEffect;

        [SerializeField] private CameraShak _CameraShak;
   
        private string _CurrentColor;
        

        public void GameRestart()
        {
            _CameraShak.transform.position = new  Vector3(0,3.23f,-10);
            ReandomColor();
            IsDie = false;
            _Sprite.gameObject.SetActive(true);
            _FadEffect.startColor = ChangeColor(_CurrentColor);
            _FadEffect.Play();
        }
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsDie)
                return;

          
            if (other.tag == _CurrentColor)
            {
                _Sprite.color = ChangeColor(_CurrentColor);
            }
            else if (other.CompareTag("ColorChanger"))
            {
                ReandomColor();
                SoundManager.Instance.Play((int)SoundManager.SoundType.ColorChange);
                other.gameObject.SetActive(false);
                _FadEffect.startColor = ChangeColor(_CurrentColor);
            }
            else if (other.CompareTag("Star"))
            {
                other.gameObject.SetActive(false);
                SoundManager.Instance.Play((int)SoundManager.SoundType.Star);
                GameData.Instance.SetStar((GameData.Instance.GetStar)+1);
                SpawnerManager.Instance.ActiveObstacle();
                ScoreCounter.Instance.AddScore();
            }
            else  if (other.CompareTag("Score"))
            {
                SpawnerManager.Instance.ActiveObstacle();
                ScoreCounter.Instance.AddScore();
            }
            else if(other.CompareTag("Doundery"))
            {
                SoundManager.Instance.Play((int)SoundManager.SoundType.Dead);
                Die();
            }
            else
            {
                SoundManager.Instance.Play((int)SoundManager.SoundType.Hit);
                _ParticleSystem.startColor = ChangeColor(_CurrentColor);
                _FadEffect.Stop();
                _ParticleSystem.Play();
                Die();
            }
        }

        public void Die()
        {
            _CameraShak.ShakeCamera();
            _Sprite.gameObject.SetActive(false);
            IsDie = true;
            Invoke("GameOver",1f);
        }

        private void GameOver()
        {
          
            SpawnerManager.Instance.OffAllObstacle();
            UIManager.Instance.Gameover();
        }
        
        private void ReandomColor()
        {
            int count=0;
            Jumpl :
            
            if (count > 100)
            {
                print("Enfinity Loop ");
                return;
            }
            
            count++;
            int Index = Random.Range(0, _colors.Count);
            if (_CurrentColor == _colors[Index]._ColorType.ToString())
            {
                goto Jumpl;
            }
            _CurrentColor = _colors[Index]._ColorType.ToString();
            _Sprite.color = ChangeColor(_CurrentColor);
        }

        private Color ChangeColor(string ColorName)
        {
            Color color = new Color();
            for (int i = 0; i < _colors.Count; i++)
            {
                if (ColorName == _colors[i]._ColorType.ToString())
                {
                    color = _colors[i]._Color;
                    break;
                }
            }
            return color;
        }
        
    }
}