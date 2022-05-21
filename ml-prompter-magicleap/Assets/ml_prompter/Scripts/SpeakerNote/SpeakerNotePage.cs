namespace ml_promter.SpeakerNote
{
    /// <summary>
    /// SpeakerNoteの各ページクラス.
    /// </summary>
    public class SpeakerNotePage
    {
        public string Value { get; }
        public bool IsValidPage => !string.IsNullOrEmpty(Value);
        
        public SpeakerNotePage(string pageText)
        {
            Value = pageText;
        }

    }
}