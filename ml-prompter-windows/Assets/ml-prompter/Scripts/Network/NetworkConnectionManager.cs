using System;
using UnityEngine;
using UnityEngine.Events;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit;


namespace ml_prompter.Network
{
    public class NetworkConnectionManager : GlobalEventListener, INetworkConnectionManager
    {
        private static NetworkConnectionManager instance;
        public static INetworkConnectionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var go = new GameObject("NetworkConnectionManager");
                    instance = go.AddComponent<NetworkConnectionManager>();
                }

                return instance;
            }
        }

        private const string NextSceneName = "Main";

        private UnityEvent connectedEvent = new UnityEvent();
        private UnityEvent disconnectedEvent = new UnityEvent();
        
        private BoltConnection currentConnection;
        private UdpSession currentSession;


        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        

        #region --- GlobalEventListenerの継承メソッド、外部には公開しない ---
        
        public override void Connected(BoltConnection connection)
        {
            currentConnection = connection;
            connectedEvent?.Invoke();
        }


        public override void Disconnected(BoltConnection connection)
        {
            disconnectedEvent?.Invoke();
        }

        
        public override void BoltStartDone()
        {
            // サーバーならセッションを生成する.
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
                    currentSession = photonSession;
                    BoltMatchmaking.JoinSession(currentSession);
                }
            }
        }

        #endregion --- GlobalEventListenerの継承メソッド、外部には公開しない ---

        
        public void StartServer()
        {
            BoltLauncher.StartServer();
        }


        public void StartClient()
        {
            BoltLauncher.StartClient();
        }


        public void RegisterConnectedListener(UnityAction listener)
        {
            connectedEvent?.AddListener(listener);
        }

        public void RegisterDisconnectedListener(UnityAction listener)
        {
            disconnectedEvent?.AddListener(listener);
        }


        public void ReJoinSession()
        {
            BoltMatchmaking.JoinSession(currentSession);
        }


        public void Disconnection()
        {
            Debug.Log("切断");
            
            //currentConnection.Disconnect();
        }
        
    }
    
}