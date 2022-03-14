using System.Diagnostics;
using UnityEngine;

namespace ml_prompter.WindowsPc
{
    public class BrowserProcess
    {
        private string Path => Application.streamingAssetsPath + "/PCSetup/Browser/index.html";
        private Process process = new Process();


        public void Start()
        {
            process.StartInfo.FileName = Path;
            process.Start();
        }


        public void Close()
        {
            if (!process.HasExited)
            {
                process.CloseMainWindow();
            }
            process.Close();
            process.Dispose();
        }


    }
}