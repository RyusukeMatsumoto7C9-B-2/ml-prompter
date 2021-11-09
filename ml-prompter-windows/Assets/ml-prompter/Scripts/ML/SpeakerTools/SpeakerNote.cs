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
        private float distance;

        [SerializeField] 
        private Transform mainCamera;

        [SerializeField] 
        private Text timerText;

        [SerializeField] 
        private Text speakerNoteText;
        
        

        private Vector3 lastPosition;
        private Stopwatch stopwatch = new Stopwatch();
        private SpeakerNoteText note;
        
        private void Start()
        {
            transform.position = mainCamera.TransformDirection(Vector3.forward) * distance;
            lastPosition = transform.position;

            SetupNote();
        }

        
        private void Update()
        {
            UpdateTimer();
        }

        
        private void LateUpdate()
        {
            if (mainCamera == null) return;

            UpdatePosition();
        }

        private void UpdatePosition()
        {
            Vector3 tempPosition = mainCamera.TransformDirection(Vector3.forward) * 3;
            lastPosition = transform.position;
            transform.position = Vector3.Slerp(lastPosition, tempPosition, Time.deltaTime * 3);
            transform.LookAt(mainCamera);
        }


        private void UpdateTimer()
        {
            var elapsed = stopwatch.Elapsed;
            timerText.text = $"{elapsed.Minutes}:{elapsed.Seconds:00}:{elapsed.Milliseconds:00}";
        }


        /// <summary>
        /// ローカルに保存してあるノートファイルを展開.
        /// </summary>
        private void SetupNote()
        {
            // テキストのロード.
            note = new SpeakerNoteText();
            speakerNoteText.text = note.GetText();
        }


        public void NextPage()
        {
            note.Next();
            speakerNoteText.text = note.GetText();
        }


        public void PreviousPage()
        {
            note.Previous();
            speakerNoteText.text = note.GetText();
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

    