using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ml_prompter
{
    /// <summary>
    /// Mainシーン.
    /// WindowsとMagicLeapで生成するシーンオブジェクトを切り替える.
    /// </summary>
    public class MainScene : MonoBehaviour
    {
        [SerializeField] 
        private GameObject magicLeapSceneObject;
        
        [SerializeField] 
        private GameObject windowsSceneObject;
        
        private void Start()
        {
            #if UNITY_STANDALONE_WIN
            Instantiate(windowsSceneObject);
            #elif PLATFORM_LUMIN
            Instantiate(magicLeapSceneObject);
            #endif
        }
    }
}
    