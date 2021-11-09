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


        public void Next()
        {
            if (currentIndex < values.Length)
                currentIndex++;
        }


        public void Previous()
        {
            if (0 < currentIndex)
                currentIndex--;
        }


        public string GetText() => values[currentIndex] ?? "";
    }
}