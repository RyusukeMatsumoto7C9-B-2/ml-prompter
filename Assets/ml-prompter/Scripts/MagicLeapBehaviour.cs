using System;
using System.Diagnostics;
using UnityEngine;
using Photon.Bolt;
using Debug = UnityEngine.Debug;

namespace ml_prompter
{
    public class MagicLeapBehaviour : EntityBehaviour<IMagicLeapState>
    {
        public override void Attached()
        {
            state.AddCallback("InputButtonID", ChangeInputButtonID);
        }
        

        public override void SimulateOwner()
        {
        }


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state.InputButtonID = 1;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                state.InputButtonID = 2;
            }
        }


        private void ChangeInputButtonID()
        {
            switch (state.InputButtonID)
            {
                case 0:
                    Debug.Log("待機");
                    break;
                
                case 1:
                    Debug.Log("左クリック");
                    break;
                
                case 2:
                    Debug.Log("右クリック");
                    break;
            }

            Debug.Log($"InputButtonID Changed : {state.InputButtonID.ToString()}");

            if (entity.IsOwner)
            {
                state.InputButtonID = 0;
            }
        }
    }
}