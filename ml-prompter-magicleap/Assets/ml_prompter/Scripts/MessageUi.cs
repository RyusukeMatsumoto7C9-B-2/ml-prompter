using UnityEngine;

namespace ml_promter
{
    /// <summary>
    /// Messageボタン周り挙動を担当.
    /// </summary>
    public class MessageUi : MonoBehaviour
    {
        [SerializeField]
        private GameObject messageUi;


        public void ShowMessageUiButton() => messageUi.SetActive(true);


        public void HideMessageUiButton() => messageUi.SetActive(false);

    }
}