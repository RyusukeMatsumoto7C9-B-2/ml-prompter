using System.Diagnostics;
using TMPro;
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
        private TextMeshProUGUI noteText;

        private Vector3 lastPosition;
        private SpeakerNoteText note = new SpeakerNoteText();
        private SpeakerTimer timer = new SpeakerTimer();
        
        
        private void Start()
        {
            transform.position = mainCamera.TransformDirection(Vector3.forward) * distance;
            lastPosition = transform.position;

            note.Setup();
            noteText.SetText(note.GetCurrentText());

            timer = new SpeakerTimer();
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
            timerText.text = timer.GetCurrentTime();
        }


        public void NextPage() => noteText.SetText(note.Next());


        public void PreviousPage() => noteText.SetText(note.Previous());


        public void StartTimer() => timer.Start();

        
        public void StopTimer() => timer.Stop();


        public void ResetTimer() => timer.Reset();
    }
}

    