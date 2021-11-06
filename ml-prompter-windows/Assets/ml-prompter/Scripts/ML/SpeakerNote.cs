using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ml_prompter.Ml
{
    /// <summary>
    /// スピーカーノートUI.
    /// </summary>
    public class SpeakerNote : MonoBehaviour
    {

        [SerializeField] 
        private Transform mainCamera;

        private Vector3 lastPosition;


        private void Start()
        {
            transform.position = mainCamera.TransformDirection(Vector3.forward) * 2;
            lastPosition = transform.position;
        }

        
        private void Update()
        {
            
        }

        
        private void LateUpdate()
        {
            if (mainCamera == null) return;

            Vector3 tempPosition = mainCamera.TransformDirection(Vector3.forward) * 2;
            lastPosition = transform.position;
            transform.position = Vector3.Slerp(lastPosition, tempPosition, Time.deltaTime * 3);
            transform.LookAt(mainCamera);
        }
    }
}

    