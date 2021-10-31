using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

namespace ml_prompter
{
    [BoltGlobalBehaviour]
    public class NetworkCallbacks : GlobalEventListener
    {
        public override void SceneLoadLocalDone(string scene, IProtocolToken token)
        {
            // randomize a position
            var spawnPosition = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));

            // instantiate cube
            BoltNetwork.Instantiate(BoltPrefabs.MagicLeapState, spawnPosition, Quaternion.identity);
        }
    }
}

    