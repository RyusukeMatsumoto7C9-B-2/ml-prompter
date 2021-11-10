using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace ml_prompter.Ml.SpeakerTools
{
    public class SpeakerNoteText
    {
        private string NoteTextPath => Application.persistentDataPath + "/note.txt";

        private string[] values;
        private int currentIndex = 0;
        
        
        public SpeakerNoteText()
        {
        }


        public void Setup()
        {
            string[] del = {"---"};
            using (var sr = new StreamReader(NoteTextPath, Encoding.UTF8))
            {
                values = sr.ReadToEnd().Split(del, StringSplitOptions.None);
                sr.Close();
            }
        }
        

        public string Next()
        {
            if (currentIndex < values.Length - 1)
                currentIndex++;
            return GetCurrentText();
        }


        public string Previous()
        {
            if (0 < currentIndex)
                currentIndex--;
            return GetCurrentText();
        }


        public string GetCurrentText() => values[currentIndex] ?? "";
    }
}