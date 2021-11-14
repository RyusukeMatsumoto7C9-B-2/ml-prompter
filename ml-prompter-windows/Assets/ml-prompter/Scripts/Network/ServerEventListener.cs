using UnityEngine;
using Photon.Bolt;

using ml_prompter.Pc;

// テスト.
using System.Text;
using System.IO;
using System.IO.Compression;
using DefaultNamespace;
using UnityEngine.Experimental.Rendering;


namespace ml_prompter.Network
{
    /// <summary>
    /// サーバー側のクライアントからのイベント受信時処理.
    /// </summary>
    public class ServerEventListener : GlobalEventListener
    {

        [SerializeField] 
        private DesktopScreenCapture screenCapture;

        
        private WindowsInputProxy windowsInputProxy = new WindowsInputProxy();


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
                    windowsInputProxy.RightArrowKey();
                    Capture();
                    break;

                case 2:
                    Debug.Log("ページ戻す");
                    windowsInputProxy.LeftArrowKey();
                    Capture();
                    break;
            }
        }


        #region --- あとで別クラスに分けるキャプチャデータ圧縮 ---

        [SerializeField] private MeshRenderer m;


        // TODO : 後で private メソッドに変更する.
        public void Capture()
        {
            //var compressedData = Compress(rawData);
            string captureString = screenCapture.CaptureToString();
            Debug.Log($"before  : {captureString.Length}");

            // 受け取ったstringを圧縮.
            var compressString = StringCompressor.CompressFromString(captureString);
            Debug.Log($"after  : {compressString.Length}");
            
            
            // stringにしたデータを送信.

            // 受信したstringを解凍.
            var decompressString = StringCompressor.DecompressToStr(compressString);
            
            //var texture = screenCapture.StringToTexture2D(captureString);
            var texture = screenCapture.StringToTexture2D(decompressString);
            

            // 何らかのテクスチャに張り付ける.
            m.material.mainTexture = texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            GameObject go = GameObject.Find("Hoge");
            go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        }
        
        private byte[] StringToByteArray(string src)
        {
            var stringArray = src.Split(',');
            // 最後尾にはEOFが付くためstringのLength - 1を利用する.
            var byteArray = new byte[stringArray.Length - 1];
            for (var i = 0; i < byteArray.Length; ++i)
            {
                byteArray[i] = byte.Parse(stringArray[i]);
            }

            return byteArray;
        }




        #endregion --- あとで別クラスに分けるキャプチャデータ圧縮 ---

    }
   
}

    