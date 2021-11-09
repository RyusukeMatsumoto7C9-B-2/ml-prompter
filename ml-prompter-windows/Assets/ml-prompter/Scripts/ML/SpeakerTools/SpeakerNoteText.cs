using System;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace ml_prompter.Ml.SpeakerTools
{
    public class SpeakerNoteText
    {
        private string NoteTextPath => Application.persistentDataPath + "/note.txt";

        private string[] values;
        private int currentIndex = 0;
        
        public SpeakerNoteText()
        {
            string[] del = {"---"};
            values = File.ReadAllText(NoteTextPath).Split(del, StringSplitOptions.None);
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