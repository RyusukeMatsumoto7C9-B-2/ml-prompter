using System.Collections;
using Photon.Bolt;
using Photon.Bolt.Tokens;
using UnityEngine;

namespace ml_prompter.Network
{
    /// <summary>
    /// PC側のイベント送信.
    /// </summary>
    public class ServerEventSender : GlobalEventListener
    {
        public bool IsSendingScreenShotData { get; private set; }


        /// <summary>
        /// 撮影したスクリーンショットの送信.
        /// </summary>
        public void SendScreenShot(
            string[] captureStrings)
        {
            if (IsSendingScreenShotData)
            {
                Debug.LogError("スクリーンキャプチャを送信中です,今送信することはできません.");
                return;
            }

            StartCoroutine(SendScreenShotData(captureStrings));
        }



        private IEnumerator SendScreenShotData(string[] captureStrings)
        {
            IsSendingScreenShotData = true;

            Debug.Log($"ML_ CaptureStringsCount {captureStrings.Length} {captureStrings[captureStrings.Length - 1]}");
            float time = Time.realtimeSinceStartup;
            foreach (var data in captureStrings)
            {
                var ev = ScreenCaptureEvent.Create();
                ev.CaptureString1 = data;
                ev.Send();
                yield return new WaitForEndOfFrame();
            }
            Debug.Log($"ML_ 送信完了. 経過時間 : {Time.realtimeSinceStartup - time}");
            IsSendingScreenShotData = false;
        }





    }
}