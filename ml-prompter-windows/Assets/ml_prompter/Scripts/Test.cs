using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ml_prompter.WindowsPc;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Test : MonoBehaviour
{

    private string BrowserPath => Application.streamingAssetsPath + "/PCSetup/Browser/index.html";
    private string ServerPath => Application.streamingAssetsPath + "/PCSetup/Server/server.py";


    private BrowserProcess browserProcess = new BrowserProcess();
    private ServerProcess serverProcess = new ServerProcess();

    
    void Start()
    {
        
    }


    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            serverProcess.Start();
            browserProcess.Start();
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            serverProcess.Close();
            browserProcess.Close();
        }
    }
}
