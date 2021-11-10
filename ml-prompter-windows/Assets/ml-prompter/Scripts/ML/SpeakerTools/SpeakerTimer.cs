using System.Diagnostics;


namespace ml_prompter.Ml.SpeakerTools
{
    /// <summary>
    /// スピーカーノート用のタイマー.
    /// </summary>
    public class SpeakerTimer
    {
        private Stopwatch stopwatch = new Stopwatch();


        public SpeakerTimer()
        {
        }

        
        public string GetCurrentTime()
        {
            if (!stopwatch.IsRunning) return "0:00:00";

            var elapsed = stopwatch.Elapsed;
            return  $"{elapsed.Minutes}:{elapsed.Seconds:00}:{elapsed.Milliseconds:00}";
        }


        public void Start()
        {
            if (stopwatch.IsRunning) return;
            stopwatch.Start();
        }


        public void Stop()
        {
            if (!stopwatch.IsRunning) return;
            stopwatch.Stop();
        }


        public void Reset()
        {
            Stop();
            stopwatch.Reset();
        }
    }
}