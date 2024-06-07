using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorSwitch
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _RB;

        [SerializeField] 
        private float _Force =5f;

        [SerializeField]
        private ColorSwitchs _colorSwitch;
        
        private bool IsFirstTimeClick;
        

        public void GameRestart()
        {
            IsFirstTimeClick = true;
            UnActiveBody();
            gameObject.SetActive(true);
            transform.position = Vector2.zero;
        }
        public void Jump()
        {
            if (IsFirstTimeClick)
            {
                ActiveJump();
                IsFirstTimeClick = false;
                return;
            }
            if(_colorSwitch.IsDie )
                return;
            
            _RB.velocity = Vector2.up *_Force; 
            SoundManager.Instance.Play((int)SoundManager.SoundType.Jump);
        }

        private void ActiveJump()
        {
            _RB.bodyType = RigidbodyType2D.Dynamic;
        }

        private void UnActiveBody()
        {
            _RB.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
