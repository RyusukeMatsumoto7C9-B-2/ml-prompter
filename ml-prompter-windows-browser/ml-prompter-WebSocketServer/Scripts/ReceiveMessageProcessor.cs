namespace ml_prompter_WebSocketServer
{
    /// <summary>
    /// 受信したメッセージを解約して処理を行う.
    /// </summary>
    public class ReceiveMessageProcessor
    {

        
        public void Handle(string message)
        {
            if (message == "PrevSlide")
            {
                Win32Api.KeyBoardEvent(Win32Api.VK_LEFT);
            }

            if (message == "NextSlide")
            {
                Win32Api.KeyBoardEvent(Win32Api.VK_RIGHT);
            }

        }

    }
}