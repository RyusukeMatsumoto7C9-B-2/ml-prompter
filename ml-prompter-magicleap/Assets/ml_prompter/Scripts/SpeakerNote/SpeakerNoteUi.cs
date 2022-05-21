using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.XR.MagicLeap;


namespace ml_promter.SpeakerNote
{
    public class SpeakerNoteUi : MonoBehaviour, ISpeakerNoteUi
    {
        [SerializeField]
        private MagicLeapWebRTC webRtc;

        [SerializeField] 
        private Text speakerText;

        [SerializeField]
        private Button nextPageButton;

        [SerializeField]
        private Button nextSlideButton;

        [SerializeField]
        private Button prevPageButton;

        [SerializeField]
        private Button prevSlideButton;

        [SerializeField]
        private Image noteBackground;


        private IEnumerator Start()
        {
            Debug.Log("SpeakerNoteUI Start()");
            yield return new WaitUntil(() => MLInput.IsStarted);
            Debug.Log("MLInputが起動しました.");
            
            nextPageButton.onClick.AddListener(() =>
            {
                webRtc.SendTextMessage("NextButton");
                Debug.Log("nextPageButton押下したよ");
                
            });        
            nextSlideButton.onClick.AddListener(() =>
            {
                webRtc.SendTextMessage("NextSlide");
                Debug.Log("nextSlideButton押下したよ"); 
                
            });        

            prevPageButton.onClick.AddListener(() =>
            {
                webRtc.SendTextMessage("PrevPage");
                Debug.Log("prevPageButton押下したよ"); 
                
            });        
            prevSlideButton.onClick.AddListener(() =>
            {
                webRtc.SendTextMessage("PrevSlide");
                Debug.Log("prevSlide押下したよ"); 
                
            });
            
            MLInput.OnControllerTouchpadGestureStart += OnTouchpadGestureStart;
        }


        private void OnDestroy()
        {
            MLInput.OnControllerTouchpadGestureStart -= OnTouchpadGestureStart;
        }
        
        
        private void OnTouchpadGestureStart(byte id, MLInput.Controller.TouchpadGesture gesture)
        {
            float alphaAddValue = Mathf.Clamp(gesture.Speed, -0.1f, 0.1f);
            noteBackground.color = new Color(0f, 0f, 0f, Mathf.Clamp(noteBackground.color.a + alphaAddValue, 0f, 1f));
        }


        private void OnNoteBackgroundAlphaValueChange(float value)
        {
            noteBackground.color = new Color(0f, 0f, 0f, value);
        }

        
        public void AddPage(string text)
        {
            
        }
    }
}

