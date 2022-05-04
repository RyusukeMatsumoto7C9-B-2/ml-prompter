namespace ml_promter
{
    /// <summary>
    /// WebRTCでのメッセージやり取り.
    /// </summary>
    public interface IWebRTCMessage
    {
        void SendTextMessage(string message);
    }
}