using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class OpenPersistentDataPath
{

    [MenuItem("Custom/OpenPersistentDataFolder")]
    private static void OpenPersistentDataFolder()
    {
        string path = Application.persistentDataPath + "/note.txt";
        if (!File.Exists(path))
        {
            var stream = File.Create(path);
            stream.Close();
        }
        EditorUtility.RevealInFinder(Application.persistentDataPath + "/note.txt");

    }



}
