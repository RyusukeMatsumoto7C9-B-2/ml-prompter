using System.Collections;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

using ml_prompter.Network;
using ml_prompter.Ml.SpeakerTools;

namespace ml_prompter.Ml
{
    /// <summary>
    /// MagicLeap側の入力処理.
    /// </summary>
    public class MagicLeapInput : MonoBehaviour
    {
        #if PLATFORM_LUMIN
        [SerializeField] private ClientEventSender clientEventSender;
        [SerializeField] private ClientEventListener clientEventListener;
        [SerializeField] private SpeakerNote speakerNote;
        [SerializeField] private Transform mainCamera;

        [Space]

        // 次/前のスライド ( スクリーンショットも撮影する )
        [SerializeField] private SpeakerNoteButton nextSlideButton;
        [SerializeField] private SpeakerNoteButton prevSlideButton;

        // 次/前のノート ( ノートのページ送りのみ )
        [SerializeField] private SpeakerNoteButton nextNoteButton;
        [SerializeField] private SpeakerNoteButton prevNoteButton;

        // スクリーンショットのみ.
        [SerializeField] private SpeakerNoteButton screenShotButton;

        public float triggerValue = 0f;
        public bool IsTriggerOn => 1f <= triggerValue;
        
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => MLInput.IsStarted);

            // コントローラのトリガー入力.
            MLInput.OnTriggerDown += OnTriggerDown;
            MLInput.OnTriggerUp += OnTriggerUp;

            // コントローラのボタン入力.
            MLInput.OnControllerButtonDown += OnButtonDown;

            var a = MLInput.GetController(0);
            a.StartFeedbackPatternVibe(MLInput.Controller.FeedbackPatternVibe.Click, MLInput.Controller.FeedbackIntensity.Medium);
            
            nextSlideButton.RegisterListener(OnNextSlideButton);
            prevSlideButton.RegisterListener(OnPrevSlideButton);
            nextNoteButton.RegisterListener(OnNextNoteButton);
            prevNoteButton.RegisterListener(OnPrevNoteButton);
            screenShotButton.RegisterListener(OnScreenShotButton);
        }


        private void Update()
        {
            // ここで移動処理.
            Vector3 temp = mainCamera.position + (mainCamera.forward * 0.5f);
            transform.position = temp;
            transform.LookAt(mainCamera);
        }
        

        private void OnDestroy()
        {
            MLInput.OnTriggerDown -= OnTriggerDown;
            MLInput.OnTriggerUp -= OnTriggerUp;
            MLInput.OnControllerButtonDown -= OnButtonDown;
        }


        private void HapticVibration()
        {
            var controller = MLInput.GetController(0);
            controller.StartFeedbackPatternVibe(MLInput.Controller.FeedbackPatternVibe.Click, MLInput.Controller.FeedbackIntensity.Medium);
        }


        private void OnTriggerDown(byte id, float value)
        {
            speakerNote.ResetTimer();
            speakerNote.StartTimer();
            triggerValue = value;
        }


        private void OnTriggerUp(byte id, float value)
        {
            triggerValue = value;
        }


        private void OnButtonDown(byte controllerId, MLInput.Controller.Button button)
        {
            switch (button)
            {
                case MLInput.Controller.Button.Bumper:
                    speakerNote.StopTimer();
                    break;

                case MLInput.Controller.Button.HomeTap:
                    Application.Quit();
                    break;
            }
        }

        
        private void OnNextSlideButton()
        {
            if (!IsTriggerOn) return;
            HapticVibration();
            clientEventSender.SendInputEvent(1);
        }


        private void OnPrevSlideButton()
        {
            if (!IsTriggerOn) return;
            HapticVibration();
            clientEventSender.SendInputEvent(2);
        }


        private void OnNextNoteButton()
        {
            if (!IsTriggerOn) return;
            HapticVibration();
            speakerNote.NextPage();
        }


        private void OnPrevNoteButton()
        {
            if (!IsTriggerOn) return;
            HapticVibration();
            speakerNote.PreviousPage();
        }


        private void OnScreenShotButton()
        {
            if (!IsTriggerOn) return;
            HapticVibration();
            clientEventSender.SendInputEvent(3);
        }

#endif
    }
}

