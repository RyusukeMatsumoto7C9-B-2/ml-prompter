using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;


namespace ml_promter.SpeakerNote
{
    public class SpeakerNoteUi : MonoBehaviour, ISpeakerNoteUi
    {
        // TODO : 後で参照周りを是正する.
        //[SerializeField]
        //private MagicLeapWebRTC webRtc;

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
        
        private SpeakerNote speakerNote;


        private void Start()
        {
            speakerNote = new SpeakerNote();

            // テストとしてダミーのページを登録.
            {
                speakerNote.AddPage(new SpeakerNotePage("1ページ目\nほげほげ"));
                speakerNote.AddPage(new SpeakerNotePage("2ページ目\nふがふが"));
                speakerNote.AddPage(new SpeakerNotePage("3ページ目\nじぇじぇじぇ"));
                speakerText.text = speakerNote.CurrentPage().Value;
            }

            Debug.Log("SpeakerNoteUI Start()");
            //yield return new WaitUntil(() => MLInput.IsStarted);
            Debug.Log("MLInputが起動しました.");

            nextPageButton.onClick.AddListener(OnNextPage);
            nextSlideButton.onClick.AddListener(OnNextSlide);
            prevPageButton.onClick.AddListener(OnPreviousPage);
            prevSlideButton.onClick.AddListener(OnPreviousSlide);
            
            // TODO : 後で参照周りを是正する.
            //MLInput.OnControllerTouchpadGestureStart += OnTouchpadGestureStart;
        }


        private void OnDestroy()
        {
            // TODO : 後で参照周りを是正する.
            //MLInput.OnControllerTouchpadGestureStart -= OnTouchpadGestureStart;
        }
        
        
        /*
        private void OnTouchpadGestureStart(byte id, MLInput.Controller.TouchpadGesture gesture)
        {
            // TODO : 後で参照周りを是正する.
            float alphaAddValue = Mathf.Clamp(gesture.Speed, -0.1f, 0.1f);
            noteBackground.color = new Color(0f, 0f, 0f, Mathf.Clamp(noteBackground.color.a + alphaAddValue, 0f, 1f));
        }
        */


        private void OnNoteBackgroundAlphaValueChange(float value)
        {
            noteBackground.color = new Color(0f, 0f, 0f, value);
        }


        private void OnNextPage()
        {
            speakerNote.Next();
            speakerText.text = speakerNote.CurrentPage().Value;
            Debug.Log("nextPageButton押下したよ");
        }


        private void OnNextSlide()
        {
            speakerNote.Next();
            speakerText.text = speakerNote.CurrentPage().Value;
            
            // スライド遷移のメッセージ送信.
            // TODO : 後で参照周りを是正する.
            //webRtc.SendTextMessage("NextSlide");
            
            Debug.Log("nextSlideButton押下したよ"); 
        }


        private void OnPreviousPage()
        {
            speakerNote.Previous();
            speakerText.text = speakerNote.CurrentPage().Value;
            Debug.Log("prevPageButton押下したよ"); 
        }


        private void OnPreviousSlide()
        {
            speakerNote.Previous();
            speakerText.text = speakerNote.CurrentPage().Value;
            
            // スライド遷移のメッセージ送信.
            // TODO : 後で参照周りを是正する.
            //webRtc.SendTextMessage("PrevSlide");
            Debug.Log("prevSlide押下したよ"); 
                
        }


        public void AddPage(string text)
        {
            speakerNote.AddPage(new SpeakerNotePage(text));    
        }
    }
}

