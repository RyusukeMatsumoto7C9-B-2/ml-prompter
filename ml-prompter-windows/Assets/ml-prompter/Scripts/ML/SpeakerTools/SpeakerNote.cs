using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


namespace ml_prompter.Ml.SpeakerTools
{
    /// <summary>
    /// スピーカーノートUI.
    /// </summary>
    public class SpeakerNote : MonoBehaviour
    {

        [SerializeField] 
        private Transform mainCamera;

        [SerializeField] 
        private Text timerText;

        private Vector3 lastPosition;
        private Stopwatch stopwatch = new Stopwatch();

        private void Start()
        {
            transform.position = mainCamera.TransformDirection(Vector3.forward) * 2;
            lastPosition = transform.position;
        }

        
        private void Update()
        {
            var elapsed = stopwatch.Elapsed;
            timerText.text = $"{elapsed.Minutes}:{elapsed.Seconds:00}:{elapsed.Milliseconds:00}";
        }

        
        private void LateUpdate()
        {
            if (mainCamera == null) return;

            Vector3 tempPosition = mainCamera.TransformDirection(Vector3.forward) * 2;
            lastPosition = transform.position;
            transform.position = Vector3.Slerp(lastPosition, tempPosition, Time.deltaTime * 3);
            transform.LookAt(mainCamera);
        }


        public void StartTimer()
        {
            if (stopwatch.IsRunning) return;
            stopwatch.Start();
        }


        public void StopTimer()
        {
            stopwatch.Stop();
        }


        public void ResetTimer()
        {
            stopwatch.Reset();
        }
    }
}

    