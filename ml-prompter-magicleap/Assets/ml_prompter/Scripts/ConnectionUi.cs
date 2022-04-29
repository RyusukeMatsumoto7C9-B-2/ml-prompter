using MagicLeap;
using UnityEngine;
using UnityEngine.Events;



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
        private GameObject disconnectUi;

        [SerializeField]
        private VirtualKeyboard connectionKeyboard;


        private void Awake()
        {
            // サーバと接続する際のキーボード.
            //connectionKeyboard = connectUi.GetComponentInChildren<VirtualKeyboard>();
        }


        public void RegisterOnConnectionListener(UnityAction<string> listener)
        {
            Debug.Log($"キーボードイベントをセット. {connectionKeyboard == null}");
            connectionKeyboard.OnKeyboardSubmit.AddListener(listener);
        }


        public void ShowConnectUi() => connectUi.SetActive(true);

        public void HideConnectUi() => connectUi.SetActive(false);
    }
}