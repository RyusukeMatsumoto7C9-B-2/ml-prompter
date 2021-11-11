using System.Collections;
using System.Collections.Generic;
using ml_prompter.Network;
using UnityEngine;



namespace ml_prompter.Pc
{
    /// <summary>
    /// Windows用の入力クラス.
    /// </summary>
    public class WindowsInput : MonoBehaviour
    {

        [SerializeField] private ServerEventListener e;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            if (Input.GetKeyDown(KeyCode.Space))
                e.Capture();
        }
    }
}

    