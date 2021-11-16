using System.Collections;
using System.Collections.Generic;
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

        // TODO : ここで使えるようにするのは適切かはいまいちわからない.
        [SerializeField] 
        private ServerEventSender sender;

        
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
            string[] captureStrings = screenCapture.CaptureToStrings();

            // 受け取ったstringを圧縮.
            var compressStrings = new List<string>();
            for (var i = 0; i < captureStrings.Length; ++i)
            {
                if (i == captureStrings.Length - 1)
                {
                    // 最後の要素にはマーカーをつける.
                    compressStrings.Add(StringCompressor.CompressFromString(captureStrings[i]) + "-e");
                }
                else
                {
                    compressStrings.Add(StringCompressor.CompressFromString(captureStrings[i]));
                }
            }

            // stringにしたデータを送信.
            //StartCoroutine(SendCapture(compressStrings));
            if (!sender.IsSendingScreenShotData)
            {
                sender.SendScreenShot(compressStrings.ToArray());
            }
            // TODO : ここから下は本実装の時は不要になる.
            
            
            // 受信、イベントを受け取るときは List にキャッシュし -e マーカーのついているデータが来るまで Listにデータを積む.
            string[] recieve = compressStrings.ToArray();


            var decompressStrings = new StringBuilder();
            for (var i = 0; i < recieve.Length; ++i)
            {
                // 最後の要素のマーカーを取り除く.
                decompressStrings.Append(StringCompressor.DecompressToStr(recieve[i].Replace("-e", "")));
            }
            File.WriteAllText(Application.persistentDataPath + "/jetson.txt", decompressStrings.ToString());
            var texture = screenCapture.StringToTexture2D(decompressStrings.ToString());

            // 何らかのテクスチャに張り付ける.
            m.material.mainTexture = texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            GameObject go = GameObject.Find("Hoge");
            go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        }

        
        
        private IEnumerator SendCapture(List<string> sendData)
        {
            float startTime = Time.realtimeSinceStartup;
            foreach (var data in sendData)
            {
                // 一旦送信しない.
                /*
                var ev = ScreenCaptureEvent.Create();
                ev.CaptureString = data;
                ev.Send();
                */
                yield return new WaitForEndOfFrame();
            }
            Debug.Log($"SendingTime {Time.realtimeSinceStartup - startTime}");
        }



        /*
        // TODO : 古い方.
        public void CaptureA()
        { 
            ///var compressedData = Compress(rawData);
            string captureString = screenCapture.CaptureToString();
            Debug.Log($"before  : {captureString.Length}");

            // 受け取ったstringを圧縮.
            var compressString = StringCompressor.CompressFromString(captureString);
            Debug.Log($"after  : {compressString.Length}");

            // stringにしたデータを送信.

            // 受信したstringを解凍.
            var decompressString = StringCompressor.DecompressToStr(compressString);
            Debug.Log($"afterafter  : {decompressString.Length}");
            
            //var texture = screenCapture.StringToTexture2D(captureString);
            var texture = screenCapture.StringToTexture2D(decompressString);
            

            // 何らかのテクスチャに張り付ける.
            m.material.mainTexture = texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            GameObject go = GameObject.Find("Hoge");
            go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        }
        */


        #endregion --- あとで別クラスに分けるキャプチャデータ圧縮 ---

    }
   
}

    