using UnityEngine;
using Photon.Bolt;

using ml_prompter.Pc;

// テスト.
using System.Text;
using System.IO;
using System.IO.Compression;
using UnityEngine.Experimental.Rendering;


namespace ml_prompter.Network
{
    /// <summary>
    /// サーバー側のクライアントからのイベント受信時処理.
    /// </summary>
    public class ServerEventListener : GlobalEventListener
    {
        
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
                    break;
            }
        }

        
        #region --- あとで別クラスに分けるキャプチャデータ圧縮 ---

        [SerializeField] private Camera testCamera;
        [SerializeField] private RenderTexture renderTexture;
        [SerializeField] private MeshRenderer m;
        [SerializeField] private MeshRenderer meshRenderer;


        public void Capture()
        {
            // uDesktopDuplicationの描画内容をRenderTextureに焼きこむ.
            // RenderTextureで撮影した内容をTexture2Dに変換.
            var tex2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false, false);
            testCamera.targetTexture = renderTexture;
            testCamera.Render();
            RenderTexture.active = renderTexture;
            tex2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0 );
            tex2D.Apply();
            var rawData = tex2D.EncodeToPNG();
            
            // RenderTextureは用済みなので解放.
            renderTexture.Release();

            //var compressedData = Compress(rawData);
            string compressedStringData = ByteArrayToString(rawData);
            File.WriteAllText(Application.persistentDataPath + "/jejeje.txt", compressedStringData);

            // stringにしたデータを送信.
            
            // 受信したstringデータ.

            //var decompressByteArray = Decompress(StringToByteArray(compressedStringData));
            var decompressByteArray = StringToByteArray(compressedStringData);
            Texture2D texture = new Texture2D(2,2);
            texture.LoadImage(decompressByteArray);


            // 何らかのテクスチャに張り付ける.
            m.material.mainTexture = texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            GameObject go = GameObject.Find("Hoge");
            go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        }


        
        /// <summary>
        /// 引数で渡したbyte配列を圧縮したbyte配列にして返す.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private byte[] Compress(byte[] src)
        {
            using (var ms = new MemoryStream())
            {
                using (var ds = new DeflateStream(ms, CompressionMode.Compress, true))
                {
                    ds.Write(src, 0, src.Length);                    
                }
                
                // 圧縮した内容をbyte配列にして取り出す.
                ms.Position = 0;
                var array = new byte[ms.Length];
                ms.Read(array, 0, array.Length);
                return array;
            }
        }


        private byte[] Decompress(byte[] src)
        {
            using (var memoryStream = new MemoryStream())
            using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
            using (var dest = new MemoryStream())
            {
                deflateStream.CopyTo(dest);
                dest.Position = 0;
                var bytes = new byte[dest.Length];
                dest.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }


        private string ByteArrayToString(byte[] src)
        {
            StringBuilder sb = new StringBuilder(2048);
            foreach (var data in src)
            {
                sb.Append($"{data},");
            }

            return sb.ToString();
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

    