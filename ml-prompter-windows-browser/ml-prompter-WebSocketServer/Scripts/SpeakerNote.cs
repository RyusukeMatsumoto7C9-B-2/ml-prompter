using System;
using System.IO;
using System.Collections.Generic;


namespace ml_prompter_WebSocketServer
{
    /// <summary>
    /// スピーカーノート読み込みクラス.
    /// </summary>
    public class SpeakerNote
    {
        private const string FILE_NAME = "SpeakerNote.txt";

        public string Text { get; private set; } = string.Empty;
        private List<string> pageTexts = new List<string>();
        

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

            Molding();
            return true;
        }


        public void DumpAllPageText()
        {
            Console.WriteLine($"Dump ... Lines > {pageTexts.Count}");
            foreach (var text in pageTexts)
            {
                Console.WriteLine("/===============================================/");
                Console.WriteLine(text);
                Console.WriteLine("/===============================================/");
            }
        }


        private void Molding()
        {
            string[] split = new[] {"---"};
            pageTexts = new List<string>(Text.Split(split, StringSplitOptions.None));
        }

    }
}