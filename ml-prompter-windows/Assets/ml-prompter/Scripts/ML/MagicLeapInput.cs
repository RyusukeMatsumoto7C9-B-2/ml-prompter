using System.Collections;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

using ml_prompter.Network;


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
        
        private MLInput.Controller controller;
        
        
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => MLInput.IsStarted);

            MLInput.OnTriggerDown += (id, value) =>
            {
                clientEventSender.SendInputEvent(1);
                
            };

            MLInput.OnControllerButtonDown += (id, button) =>
            {
                switch (button)
                {
                    case MLInput.Controller.Button.Bumper:
                        clientEventSender.SendInputEvent(2);
                        break;

                    case MLInput.Controller.Button.HomeTap:
                        Application.Quit();
                        break;
                }
            };
        }

        
        private void Update()
        {
            // テスト用.
            if (Input.GetKeyDown(KeyCode.Space) || 1f <= controller.TriggerValue)
            {
            }
            else if (Input.GetKeyDown(KeyCode.A) || controller.IsBumperDown)
            {
            }

        }
        
        
        #endif
    }
}

    