using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

using ml_prompter.Pc;

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
        private TextureCompressor textureCompressor = new TextureCompressor();


        // TODO : 後で private メソッドに変更する.
        public void Capture()
        {
            string[] captureStrings = screenCapture.CaptureToStrings();

            // 受け取ったstringを圧縮.
            var compressStrings = new List<string>();
            foreach (var data in captureStrings)
            {
                compressStrings.Add(StringCompressor.CompressFromString(data));
            }
            compressStrings.Add("-e");

            // stringにしたデータを送信.
            if (!sender.IsSendingScreenShotData)
            {
                sender.SendScreenShot(compressStrings.ToArray());
            }
            
            // TODO : ここから下は本実装の時は不要になる.
            // 受信、イベントを受け取るときは List にキャッシュし -e マーカーのついているデータが来るまで Listにデータを積む.
            string[] recieve = compressStrings.ToArray();
            var texture = textureCompressor.DecompressToTexture2D(recieve);

            // 何らかのテクスチャに張り付ける.
            m.material.mainTexture = texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            GameObject go = GameObject.Find("Hoge");
            go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        }


        #endregion --- あとで別クラスに分けるキャプチャデータ圧縮 ---

    }
   
}

    