using UnityEngine;

using ml_prompter.Network;


namespace ml_prompter.Ml
{
    /// <summary>
    /// MagicLeap側の入力処理.
    /// </summary>
    public class MagicLeapInput : MonoBehaviour
    {
        [SerializeField] private ClientEventSender clientEventSender;
        
        void Start()
        {
            
        }

        void Update()
        {
            
            // テスト用.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                clientEventSender.SendInputEvent(1);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                clientEventSender.SendInputEvent(2);
            }

        }
    }

}

    