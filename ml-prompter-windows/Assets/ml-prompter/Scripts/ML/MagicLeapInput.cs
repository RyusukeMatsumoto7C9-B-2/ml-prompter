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
        [SerializeField] 
        private ClientEventSender clientEventSender;

        [SerializeField] 
        private SpeakerNote speakerNote;
        
        
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => MLInput.IsStarted);

            // コントローラのトリガー入力.
            MLInput.OnTriggerDown += OnTriggerDown;

            // コントローラのボタン入力.
            MLInput.OnControllerButtonDown += OnButtonDown;

            // タッチパッド入力.
            MLInput.OnControllerTouchpadGestureStart += OnTouchpadGestureStart;
        }


        private void OnDestroy()
        {
            MLInput.OnTriggerDown -= OnTriggerDown;
            MLInput.OnControllerButtonDown -= OnButtonDown;
            MLInput.OnControllerTouchpadGestureStart -= OnTouchpadGestureStart;
        }


        private void OnTriggerDown(byte id, float value)
        {
            speakerNote.ResetTimer();
            speakerNote.StartTimer();
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


        private void OnTouchpadGestureStart(byte id, MLInput.Controller.TouchpadGesture gesture)
        {
            switch (gesture.Direction)
            {
                case MLInput.Controller.TouchpadGesture.GestureDirection.Left:
                    Debug.Log("Left");
                    clientEventSender.SendInputEvent(2);
                    speakerNote.PreviousPage();
                    break;
                
                case MLInput.Controller.TouchpadGesture.GestureDirection.Right:
                    Debug.Log("Right");
                    clientEventSender.SendInputEvent(1);
                    speakerNote.NextPage();
                    break;
                
                case MLInput.Controller.TouchpadGesture.GestureDirection.Clockwise:
                    Debug.Log("Clockwise");
                    break;
                
                case MLInput.Controller.TouchpadGesture.GestureDirection.Down:
                    Debug.Log("Down");
                    break;
                
                case MLInput.Controller.TouchpadGesture.GestureDirection.Up:
                    Debug.Log("Up" );
                    break;
                
                case MLInput.Controller.TouchpadGesture.GestureDirection.In:
                    Debug.Log("In");
                    break;
                
                case MLInput.Controller.TouchpadGesture.GestureDirection.Out:
                    Debug.Log("Out");
                    break;
            }
        }

        #endif
    }
}

