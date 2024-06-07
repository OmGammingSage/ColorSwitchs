using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorSwitch
{
    public class CameraShak : MonoBehaviour
    {
        [SerializeField]private float duration, magnitude;

        
        public void ShakeCamera()
        {
            StartCoroutine(Shake(duration, magnitude));
        }
        IEnumerator Shake(float duration, float magnitude) {
            Vector3 originalPos = transform.localPosition;
            float elapsed = 0f;
           
            while (elapsed < duration) {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;
                transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
                elapsed += Time.deltaTime;
                magnitude -= 1.5f * Time.deltaTime;
                yield return null;
            }
            transform.localPosition = originalPos;
        }
    }
}
