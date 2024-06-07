using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorSwitch
{
    public class Collector : MonoBehaviour
    {
        [SerializeField] private bool IsPlayer;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!IsPlayer)
            {
                if (other.transform.parent.TryGetComponent<Rotations>( out Rotations _rotation))
                {
                    _rotation.OffParent();
                }
                else
                {
                    print(" Rotation class not found ");
                }
            }
            else if (other.CompareTag("Player"))
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
