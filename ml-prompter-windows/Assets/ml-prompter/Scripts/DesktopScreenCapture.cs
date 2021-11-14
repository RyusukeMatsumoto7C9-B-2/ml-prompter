using UnityEngine;
using System.Text;
using System.IO;
using System.IO.Compression;
using DefaultNamespace;
using UnityEngine.Experimental.Rendering;


namespace DefaultNamespace
{
    /// <summary>
    /// デスクトップのスクリーンキャプチャを行うクラス.
    /// </summary>
    public class DesktopScreenCapture : MonoBehaviour
    {
        [SerializeField] 
        private Camera captureCamera;
        
        [SerializeField]
        private RenderTexture renderTexture;

        
        /// <summary>
        /// スクリーンショットを撮影し、stringデータとして返却.
        /// </summary>
        public string CaptureToString()
        {
            // uDesktopDuplicationの描画内容をRenderTextureに焼きこむ.
            // RenderTextureで撮影した内容をTexture2Dに変換.
            var tex2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false, false);
            captureCamera.targetTexture = renderTexture;
            captureCamera.Render();
            RenderTexture.active = renderTexture;
            tex2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            tex2D.Apply();
            renderTexture.Release();

            return ByteArrayToString(tex2D.EncodeToPNG());
        }


        public Texture2D StringToTexture2D(string src)
        {
            var bytes = StringToByteArray(src);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);

            return texture;
        }



        private string ByteArrayToString(byte[] src)
        {
            StringBuilder sb = new StringBuilder(8128);
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
        
    }
}