using System.Text;
using System.IO;
using System.IO.Compression;


namespace ml_prompter
{

    
    /// <summary>
    /// デスクトップキャプチャの圧縮/解凍を担当する.
    /// </summary>
    public class StringCompressor
    {

        /// <summary>
        /// 文字列を圧縮した文字列として返却.
        /// </summary>
        public static string CompressFromString(string message)
        {
            return ByteArrayToString(Compress(Encoding.UTF8.GetBytes(message)));
        }


        /// <summary>
        /// 圧縮データを文字列として復元します。
        /// </summary>
        public static string DecompressToStr(string src)
        {
             return Encoding.UTF8.GetString(Decompress(StringToByteArray(src)));
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private static byte[] Compress(byte[] src)
        {
            using (var ms = new MemoryStream())
            {
                using (var ds = new DeflateStream(ms, CompressionMode.Compress, true))
                {
                    ds.Write(src, 0, src.Length);
                }

                // 圧縮した内容をbyte配列にして取り出す
                ms.Position = 0;
                byte[] comp = new byte[ms.Length];
                ms.Read(comp, 0, comp.Length);
                return comp;
            }
        }
        

        /// <summary>
        /// 圧縮されたバイナリを解凍.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private static byte[] Decompress(byte[] src)
        {
            using (var memoryStream = new MemoryStream(src))
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
        
        
        // バイト配列を文字列として返却.
        private static string ByteArrayToString(byte[] src)
        {
            StringBuilder sb = new StringBuilder(8128);
            foreach (var data in src)
            {
                sb.Append($"{data},");
            }

            return sb.ToString();
        }

        
        private static byte[] StringToByteArray(string src)
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