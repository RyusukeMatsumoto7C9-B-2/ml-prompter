using Photon.Bolt;

namespace ml_prompter.Network
{
    /// <summary>
    /// PC側のイベント送信.
    /// </summary>
    public class ServerEventSender : GlobalEventListener
    {
        
        /// <summary>
        /// 撮影したスクリーンショットの送信.
        /// </summary>
        public void SendScreenShot(
            string captureString)
        {
            var ev = ScreenCaptureEvent.Create();
            ev.CaptureString = captureString;
            ev.Send();
        }
        
    }
}