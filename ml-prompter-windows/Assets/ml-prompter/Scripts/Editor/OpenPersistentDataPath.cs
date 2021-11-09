using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OpenPersistentDataPath
{

    [MenuItem("Custom/OpenPersistentDataFolder")]
    private static void OpenPersistentDataFolder()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath + "/");
    }



}
