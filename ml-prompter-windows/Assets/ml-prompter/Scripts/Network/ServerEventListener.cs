using UnityEngine;
using Photon.Bolt;

using ml_prompter.Pc;

// テスト.
using System.Text;
using System.IO;
using System.IO.Compression;


namespace ml_prompter.Network
{
    /// <summary>
    /// サーバー側のクライアントからのイベント受信時処理.
    /// </summary>
    public class ServerEventListener : GlobalEventListener
    {
        [SerializeField] 
        private MeshRenderer meshRenderer;

        [SerializeField] private uDesktopDuplication.Texture tex;
        [SerializeField] private MeshRenderer m;
        
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
        
        public void Capture()
        {
            // uDesktopDuplicationからデスクトップのキャプチャを取得する.
            var capture = tex.monitor.texture;
            var rawData = capture.GetRawTextureData<byte>();

            //var compressedData = Compress(rawData);
            //string compressedStringData = ByteArrayToString(compressedData);

            // stringにしたデータを送信.
            
            // 受診したstringデータ.

            //var decompressByteArray = Decompress(StringToByteArray(compressedStringData));
            Texture2D texture = new Texture2D(2, 2);
            //texture.LoadImage(rawData); // 一旦生のデータでテクスチャを生成できるのを確認する必要がある.
            texture.LoadRawTextureData(rawData.ToArray());
            texture.Apply();
            m.material.mainTexture = texture;
            
            /*
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            GameObject go = GameObject.Find("Hoge");
            go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        */
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
            var byteArray = new byte[stringArray.Length];
            for (var i = 0; i < stringArray.Length; ++i)
            {
                byteArray[i] = byteArray[i];
            }

            return byteArray;
        }



        #endregion --- あとで別クラスに分けるキャプチャデータ圧縮 ---

    }
   
}

    