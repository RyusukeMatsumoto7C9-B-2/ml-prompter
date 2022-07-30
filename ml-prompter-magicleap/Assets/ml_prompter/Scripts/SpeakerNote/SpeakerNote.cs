using System.Collections.Generic;
using UnityEngine;

namespace ml_promter.SpeakerNote
{
    public class SpeakerNote
    {

        private int index = 0;
        public int Index
        {
            get => index;
            private set => index = Mathf.Clamp(value, 0, pages.Count);
        }

        public int PageCount => pages.Count;


        private List<SpeakerNotePage> pages = new List<SpeakerNotePage>();

        
        public SpeakerNote() { }


        public void AddPage(SpeakerNotePage page)
        {
            if (page == null) return;
            if (!page.IsValidPage) return;
            
            pages?.Add(page);
        }


        public void RemoveAll() => pages?.Clear();


        public SpeakerNotePage CurrentPage() => pages.Count != 0 ? pages[Index] : new SpeakerNotePage("");


        public void Next() => Index++;


        public void Previous() => Index--;
    }
}