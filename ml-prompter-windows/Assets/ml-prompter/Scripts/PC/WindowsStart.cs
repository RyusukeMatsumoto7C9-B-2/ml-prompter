using ml_prompter.Network;
using UnityEngine;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;


namespace ml_prompter.Pc
{
    public class WindowsStart : GlobalEventListener
    {
        
        private void Start()
        {
            Screen.SetResolution(1024, 760, false);
            NetworkConnectionManager.Instance.StartServer();
        }

    }
}

