using System;
using System.Text;
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
        private readonly string[] SPLIT_TEXT = new [] { "---" };

        private string fullText = string.Empty;

        private List<string> texts = new List<string>();
        public IReadOnlyList<string> Texts => texts;


        public SpeakerNote() { }


        /// <summary>
        /// スピーカーノートの読み込み.
        /// </summary>
        /// <returns>成功 : true, 失敗 : false</returns>
        public void Load()
        {
            if (!File.Exists(FILE_NAME)) throw new Exception("SpeakerNote.text is not found.");

            using (var stream = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(stream);
                fullText = reader.ReadToEnd();
                if (fullText.Length <= 0) throw new Exception("Speaker Note is Empty.");
                Console.WriteLine(fullText);
            }
            Molding();
        }


        /// <summary>
        /// テキスト配列をページごとに配列分け.
        /// </summary>
        private void Molding()
        {
            if (string.IsNullOrEmpty(fullText)) throw new Exception("fullText is null or Empty.");

            foreach (var text in fullText.Split(SPLIT_TEXT, StringSplitOptions.None))
            {
                if (string.IsNullOrEmpty(text)) continue;
                texts.Add(RemoveFirstNewLine(text));   
            }
        }

        
        private string RemoveFirstNewLine(string text)
            => !text.StartsWith(Environment.NewLine) ? text : text.Remove(0, Environment.NewLine.Length);


        public void DumpAllPageText()
        {
            Console.WriteLine($"Dump ... size {Texts.Count}");
            var sb = new StringBuilder(1024);
            foreach (var item in Texts)
            {
                sb.AppendLine(item);
            }
            Console.WriteLine(sb.ToString());
        }
        
    }
}





