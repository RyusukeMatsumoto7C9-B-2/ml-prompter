using MagicLeap;
using UnityEngine;
using UnityEngine.Events;

namespace ml_promter
{
    /// <summary>
    /// Messageボタン周り挙動を担当.
    /// </summary>
    public class MessageUi : MonoBehaviour
    {
        [SerializeField]
        private GameObject messageUi;

        [SerializeField]
        private VirtualKeyboard virtualKeyboard;


        public void RegisterOnKeyboardSubmit(UnityAction<string> listener) => virtualKeyboard.OnKeyboardSubmit.AddListener(listener);

        public void ShowMessageUiButton() => messageUi.SetActive(true);


        public void HideMessageUiButton() => messageUi.SetActive(false);

    }
}