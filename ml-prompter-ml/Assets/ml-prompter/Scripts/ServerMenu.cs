using System;
using UnityEngine;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit;

public class ServerMenu : GlobalEventListener 
{
    private void Start()
    {
        #if PLATFORM_STANDALONE_WIN
        Screen.SetResolution(800, 600, false);

        // START SERVER
        BoltLauncher.StartServer();
        #elif PLATFORM_LUMIN
        // START CLIENT
        BoltLauncher.StartClient();

        #endif
        
        
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            string matchName = "MLPrompter_Server";

            BoltMatchmaking.CreateSession(
                sessionID: matchName,
                sceneToLoad: "Windows_Main"
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