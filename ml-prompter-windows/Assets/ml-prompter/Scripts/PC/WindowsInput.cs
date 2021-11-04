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
        // TODO : テスト用.
        private WindowsInputProxy ttt = new WindowsInputProxy();
        
        
        // Start is called before the first frame update
        void Start()
        {
            //StartCoroutine(A());
        }


        private IEnumerator A()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                ttt.MouseLeftButtonDown();
            }
        }
        

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                ttt.MouseLeftButtonDown();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ttt.MouseRightButtonDown();
            }
        }
    }
}

    