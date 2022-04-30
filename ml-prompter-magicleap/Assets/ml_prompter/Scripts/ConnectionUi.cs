using MagicLeap;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace ml_promter
{
    /// <summary>
    /// シグナリングサーバと接続、切断のUI挙動を担当.
    /// </summary>
    public class ConnectionUi : MonoBehaviour
    {

        [SerializeField]
        private GameObject connectUi;

        [SerializeField]
        private VirtualKeyboard connectionKeyboard;

        [SerializeField]
        private GameObject disconnectUi;

        [SerializeField]
        private Button disconnectButton;

        
        public void RegisterOnConnectionListener(UnityAction<string> listener) => connectionKeyboard.OnKeyboardSubmit.AddListener(listener);


        public void RegisterOnDisconnectListener(UnityAction listener) => disconnectButton.onClick.AddListener(listener);


        public void ShowConnectUi() => connectUi.SetActive(true);

        
        public void HideConnectUi() => connectUi.SetActive(false);


        public void ShowDisconnectUi() => disconnectUi.SetActive(true);


        public void HideDisconnectUi() => disconnectUi.SetActive(false);
    }
}