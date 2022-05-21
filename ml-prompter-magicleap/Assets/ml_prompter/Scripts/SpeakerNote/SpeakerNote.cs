using System.Collections.Generic;
using UnityEngine.XR.MagicLeap;

namespace ml_promter.SpeakerNote
{
    public class SpeakerNote
    {

        private int index = 0;
        public int Index
        {
            get => index;

            set
            {
                index = value;
                if (pages?.Count <= index) index = pages.Count - 1;
                if (index < 0) index = 0;
            }
        }

        private List<SpeakerNotePage> pages = new List<SpeakerNotePage>();

        public SpeakerNote(string[] pageTexts)
        {
            foreach (string pageText in pageTexts)
            {
                pages.Add(new SpeakerNotePage(pageText));
            }
        }


        public void AddPage(SpeakerNotePage page)
        {
            if (page == null) return;
            if (!page.IsValidPage) return;
            
            pages?.Add(page);
        }


        public void RemoveAll()
        {
            pages?.Clear();
        }


        public SpeakerNotePage CurrentPage() => pages[Index];

        
        public void Next() => Index++;


        public void Previous() => Index--;
    }
}