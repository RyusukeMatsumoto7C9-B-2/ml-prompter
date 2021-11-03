using UnityEngine;
using Photon.Bolt;

namespace ml_prompter
{
    /// <summary>
    /// サーバー側のクライアントからのイベント受信時処理.
    /// </summary>
    [BoltGlobalBehaviour(BoltNetworkModes.Server)]
    public class ServerEventListener : GlobalEventListener
    {
        public override void Connected(BoltConnection connection)
        {

        }


        public override void Disconnected(BoltConnection connection)
        {
        }


        public override void OnEvent(InputEvent ev)
        {
            switch (ev.InputID)
            {
                case 0:
                    Debug.Log("Default");
                    break;

                case 1:
                    Debug.Log("ページ進める");
                    break;
                
                case 2:
                    Debug.Log("ページ戻す");
                    break;
            }
        }
    }
   
}

    