using System;
using System.IO;


namespace ml_prompter_WebSocketServer
{
    /// <summary>
    /// スピーカーノート読み込みクラス.
    /// </summary>
    public class SpeakerNote
    {
        private const string FILE_NAME = "SpeakerNote.txt";

        public string Text { get; private set; } = string.Empty;
        

        public SpeakerNote() { }


        /// <summary>
        /// スピーカーノートの読み込み.
        /// </summary>
        /// <returns>成功 : true, 失敗 : false</returns>
        public bool Load()
        {
            if (!File.Exists(FILE_NAME)) return false;

            Console.WriteLine("mlp-40 : テキストファイルがあるよ.");
            using (var stream = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(stream);
                Text = reader.ReadToEnd();
                if (Text.Length <= 0) return false;
                Console.WriteLine(Text);
            }

            return true;
        }


        public void DumpAllPageText()
        {
            Console.WriteLine($"Dump ... size {Text.Length}");
            Console.WriteLine($"{Text}");
        }



    }
}