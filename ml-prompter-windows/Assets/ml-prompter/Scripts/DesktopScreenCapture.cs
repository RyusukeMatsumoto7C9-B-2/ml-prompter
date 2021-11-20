using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;


namespace ml_prompter
{
    /// <summary>
    /// デスクトップのスクリーンキャプチャを行うクラス.
    /// </summary>
    public class DesktopScreenCapture : MonoBehaviour
    {
        [SerializeField] private Camera captureCamera;

        [SerializeField] private RenderTexture renderTexture;


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


            // モノクロ.
            /*
            Color[] inputColors = tex2D.GetPixels();
            Color[] outputColors = new Color[tex2D.width * tex2D.height];
            for (int y = 0; y < tex2D.height; y++)
            {
                for (int x = 0; x < tex2D.width; x++)
                {
                    var color = inputColors[(tex2D.width * y) + x];
                    float average = (color.r + color.g + color.b) / 3;
                    float value = average < 0.5f ? 0.5f : 1f;
                    outputColors[(tex2D.width * y) + x] = new Color(value, value, value);

                    //outputColors[(tex2D.width * y) + x] = new Color(average, average, average);
                }
            }
            tex2D.SetPixels(outputColors);
            tex2D.Apply();
            */


            // ポスタリゼーションをかけたうえで灰、白のに職に変換.
            Color[] inputColors = tex2D.GetPixels();
            Color[] outputColors = new Color[tex2D.width * tex2D.height];
            int split = 2;
            for (int y = 0; y < tex2D.height; y++)
            {
                for (int x = 0; x < tex2D.width; x++)
                {
                    var color = inputColors[(tex2D.width * y) + x];
                    for (int i = 0; i < split; i++)
                    {
                        if (y % 4 == 0)
                        {
                            float col1 = i * (1f / split);
                            float col2 = (i + 1f) * (1f / split);
                            if (col1 <= color.r && color.r <= col2)
                            {
                                color.r = (col1 + col2) / 2f;
                            }

                            if (col1 <= color.g && color.g <= col2)
                            {
                                color.g = (col1 + col2) / 2f;
                            }

                            if (col1 <= color.b && color.b <= col2)
                            {
                                color.b = (col1 + col2) / 2f;
                            }

                            float average = (color.r + color.g + color.b) / 3;
                            byte value = (byte) (average < 0.5f ? 0 : 1);
                            outputColors[(tex2D.width * y) + x] = new Color(value, value, value);
                        }
                        else
                        {
                            outputColors[(tex2D.width * y) + x] = new Color(0, 0,0);
                        }
                    }

                }
            }

            tex2D.SetPixels(outputColors);
            tex2D.Apply();

            return ByteArrayToStringArray(tex2D.EncodeToPNG());
        }


        private string[] ByteArrayToStringArray(byte[] src)
        {
            var result = new List<string>();
            var sb = new StringBuilder(512);
            int count = 0;

            foreach (var data in src)
            {
                count++;
                sb.Append($"{data},");
                if (140 <= count)
                {
                    result.Add(sb.ToString());
                    sb.Clear();
                    count = 0;
                }
            }
            result.Add(sb.ToString());
            sb.Clear();

            return result.ToArray();
        }
    }
}