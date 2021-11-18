using System.Text;
using UnityEngine;



namespace ml_prompter
{
    /// <summary>
    /// Texture2Dを圧縮されたstringに圧縮、解凍クラス.
    /// </summary>
    public class TextureCompressor
    {


        public void Compress()
        {
        }



        /// <summary>
        /// string[]からTexture2Dを構築し、返却.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public Texture2D DecompressToTexture2D(string[] src)
        {
            var decompressStrings = new StringBuilder();
            for (var i = 0; i < src.Length; ++i)
            {
                // 最後の要素のマーカーを取り除く.
                decompressStrings.Append(StringCompressor.DecompressToStr(src[i].Replace("-e", "")));
            }
            return StringToTexture2D(decompressStrings.ToString());
        }

        
        private Texture2D StringToTexture2D(string src)
        {
            var bytes = StringToByteArray(src);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);

            return texture;
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