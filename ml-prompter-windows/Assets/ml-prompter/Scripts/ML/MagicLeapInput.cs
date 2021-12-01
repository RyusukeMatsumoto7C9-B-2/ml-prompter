using System.Collections;
using System.Collections.Generic;
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
        private bool IsTriggerOn => 1f <= triggerValue;

        
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
        [SerializeField] private float distance = 0.4f;
        
        public float triggerValue = 0f;
        private readonly List<GameObject> buttonList = new List<GameObject>();
        
     
            
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => MLInput.IsStarted);

            // コントローラのトリガー入力.
            MLInput.OnTriggerDown += OnTriggerDown;
            MLInput.OnTriggerUp += OnTriggerUp;

            // コントローラのボタン入力.
            MLInput.OnControllerButtonDown += OnButtonDown;

            InitButton();
            
            // とりあえず完了の合図に振動させておく.
            HapticVibration();
        }


        private void Update()
        {
            // ここで移動処理.
            Vector3 temp = mainCamera.position + (mainCamera.forward * distance);
            transform.position = Vector3.Lerp(transform.position, temp, Time.deltaTime * 3);
            transform.LookAt(mainCamera);
        }
        

        private void OnDestroy()
        {
            MLInput.OnTriggerDown -= OnTriggerDown;
            MLInput.OnTriggerUp -= OnTriggerUp;
            MLInput.OnControllerButtonDown -= OnButtonDown;
        }


        private void InitButton()
        {
            nextSlideButton.RegisterListener(OnNextSlideButton);
            prevSlideButton.RegisterListener(OnPrevSlideButton);
            nextNoteButton.RegisterListener(OnNextNoteButton);
            prevNoteButton.RegisterListener(OnPrevNoteButton);
            screenShotButton.RegisterListener(OnScreenShotButton);
            
            buttonList.Add(nextSlideButton.gameObject);
            buttonList.Add(prevSlideButton.gameObject);
            buttonList.Add(nextNoteButton.gameObject);
            buttonList.Add(prevNoteButton.gameObject);
            buttonList.Add(screenShotButton.gameObject);
            
            HideButton();
        }


        private void HideButton()
        {
            foreach (var button in buttonList)
            {
                button.SetActive(false);
            }
        }


        private void ShowButton()
        {
            foreach (var button in buttonList)
            {
                button.SetActive(true);
            }
        }


        private void HapticVibration()
        {
            var controller = MLInput.GetController(0);
            controller.StartFeedbackPatternVibe(MLInput.Controller.FeedbackPatternVibe.Click, MLInput.Controller.FeedbackIntensity.Medium);
        }


        private void OnTriggerDown(byte id, float value)
        {
            triggerValue = value;
            if (IsTriggerOn)
            {
                ShowButton();
            }
        }


        private void OnTriggerUp(byte id, float value)
        {
            triggerValue = value;
            if (!IsTriggerOn)
            {
                HideButton();
            }
        }


        private void OnButtonDown(byte controllerId, MLInput.Controller.Button button)
        {
            switch (button)
            {
                case MLInput.Controller.Button.Bumper:
                    if (!speakerNote.TimerRunning)
                    {
                        speakerNote.StartTimer();
                    }
                    else
                    {
                        speakerNote.StopTimer();
                    }
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

