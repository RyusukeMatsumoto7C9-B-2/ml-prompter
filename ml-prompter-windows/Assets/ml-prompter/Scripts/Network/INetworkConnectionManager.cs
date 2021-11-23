﻿using UnityEngine.Events;

namespace ml_prompter.Network
{
    /// <summary>
    /// NetworkConnectionManagerのインターフェース.
    /// </summary>
    public interface INetworkConnectionManager
    {
        
        void StartClient();
        void StartServer();
        void ReJoinSession();
        void Disconnection();
        void RegisterDisconnectedListener(UnityAction listener);
    }
}