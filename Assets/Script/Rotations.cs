using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorSwitch
{
    public class Rotations : MonoBehaviour
    {
        public GameObject _Parent;

        [SerializeField] private float _RotationSpeed;

        private void Update()
        {
            transform.Rotate(0, 0, _RotationSpeed * Time.deltaTime);
        }

        public void OffParent()
        {
            _Parent.gameObject.SetActive(false);
        }
    }
}