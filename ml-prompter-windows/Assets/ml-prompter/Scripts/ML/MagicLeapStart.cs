using Photon.Bolt;

using ml_prompter.Network;

namespace ml_prompter.Ml
{
    /// <summary>
    /// MagicLeapのStartシーン.
    /// </summary>
    public class MagicLeapStart : GlobalEventListener 
    {
        
        private void Start()
        {
            NetworkConnectionManager.Instance.StartClient();
        }
        
    }    
}


    