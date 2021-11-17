using System.Collections.Generic;
using UnityEngine;
using System.Text;


namespace ml_prompter
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
        /// スクリーンショットを撮影し、string配列データとして返却.
        /// </summary>
        public string[] CaptureToStrings()
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

            return ByteArrayToStringArray(tex2D.EncodeToPNG());
        }

        // TODO : こっちは今は使ってない.
        /*
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
        */


        private string[] ByteArrayToStringArray(byte[] src)
        {
            var result = new List<string>();
            var sb = new StringBuilder(512);
            int count = 0;

            for (var i = 0; i < src.Length; ++i)
            {
                count++;
                sb.Append($"{src[i]},");
                if (100 <= count)
                {
                    result.Add(sb.ToString());
                    sb.Clear();
                    count = 0;
                }
                else if (i == src.Length - 1)
                {
                    result.Add(sb.ToString());
                    sb.Clear();
                }
            }
            return result.ToArray();
        }


        private byte[] StringToByteArray(string src)
        {
            var stringArray = src.Split(',');
            var byteArray = new byte[stringArray.Length - 1];
            for (var i = 0; i < byteArray.Length; ++i)
            {
                byteArray[i] = byte.Parse(stringArray[i]);
            }

            return byteArray;
        }
        
    }
}