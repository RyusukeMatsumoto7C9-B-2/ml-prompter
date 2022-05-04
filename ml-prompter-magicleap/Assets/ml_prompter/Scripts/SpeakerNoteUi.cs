using UnityEngine;
using UnityEngine.UI;


namespace ml_promter
{
    public class SpeakerNoteUi : MonoBehaviour
    {
        [SerializeField]
        private MagicLeapWebRTC webRtc;

        [SerializeField]
        private Button nextPageButton;

        [SerializeField]
        private Button nextSlideButton;

        [SerializeField]
        private Button prevPageButton;

        [SerializeField]
        private Button prevSlideButton;


        private void Start()
        {
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
        }

        private void Update()
        {
        
        }
    }
}

