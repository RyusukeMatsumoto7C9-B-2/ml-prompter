using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ml_prompter.Pc
{
    /// <summary>
    /// Windows用の入力クラス.
    /// </summary>
    public class WindowsInput : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}

    