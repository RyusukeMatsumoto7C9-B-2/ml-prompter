using System.Collections;
using Photon.Bolt;
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
                Debug.LogError("スクリーンキャプチャを送信中です.");
                return;
            }

            StartCoroutine(SendScreenShotData(captureStrings));
        }



        private IEnumerator SendScreenShotData(string[] captureStrings)
        {
            IsSendingScreenShotData = true;
            foreach (var data in captureStrings)
            {
                var ev = ScreenCaptureEvent.Create();
                ev.CaptureString = data;
                ev.Send();
                yield return new WaitForEndOfFrame();
            }
            IsSendingScreenShotData = false;
        }





    }
}