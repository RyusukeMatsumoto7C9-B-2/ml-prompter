using System;
using UnityEngine;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit;


namespace ml_prompter.Ml
{
    /// <summary>
    /// MagicLeapのStartシーン.
    /// </summary>
    public class MagicLeapStart : GlobalEventListener 
    {

        private string NextSceneName => "Main";


        private void Start()
        {
            // START CLIENT
            BoltLauncher.StartClient();
        }


        public override void BoltStartDone()
        {
            if (BoltNetwork.IsServer)
            {
                string matchName = "MLPrompter_Server";

                BoltMatchmaking.CreateSession(
                    sessionID: matchName,
                    sceneToLoad: NextSceneName
                );
            }
        }

        
        public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
        {
            Debug.LogFormat("Session list updated: {0} total sessions", sessionList.Count);

            foreach (var session in sessionList)
            {
                UdpSession photonSession = session.Value as UdpSession;

                if (photonSession.Source == UdpSessionSource.Photon)
                {
                    BoltMatchmaking.JoinSession(photonSession);
                }
            }
        }
    }    
}


    