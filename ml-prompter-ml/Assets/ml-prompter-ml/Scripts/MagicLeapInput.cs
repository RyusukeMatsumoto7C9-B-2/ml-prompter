using UnityEngine;


namespace ml_prompter
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                clientEventSender.SendInputEvent(1);
            }

        }
    }

}

    