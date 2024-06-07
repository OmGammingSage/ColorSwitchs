using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorSwitch
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform player;

        [SerializeField] private float _Movespeed;
        
        private bool IsDie;

        private void LateUpdate()
        {
            if (!IsDie)
            {
                if (transform.position.y < player.position.y)
                {
                    transform.position = new Vector3(0, player.position.y, transform.position.z);
                }
            }
        }

        public bool SetDie
        {
            set { IsDie = value; }
        }
    }
}