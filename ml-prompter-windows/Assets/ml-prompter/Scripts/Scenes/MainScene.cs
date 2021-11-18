using UnityEngine;

namespace ml_prompter.Scenes
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
            #if PLATFORM_LUMIN
            Instantiate(magicLeapSceneObject);
            #else
            Instantiate(windowsSceneObject);
            #endif
        }
    }
}
    