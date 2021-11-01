using Photon.Bolt;



namespace ml_prompter
{
    public class ClientEventSender : GlobalEventListener
    {
        public void SendInputEvent(int id)
        {
            var ev = InputEvent.Create();
            ev.InputID = id;
            ev.Send();
        }
    }
    
}
