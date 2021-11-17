using System.IO;
using UnityEditor;
using UnityEngine;


public class SwitchPlatform
{
    private static string BuildTempFolderPath => Directory.GetParent(Application.dataPath) + @"\BuildTempFolder";

    private static string UDesktopDuplicationPluginFolder => @"\uDesktopDuplication\Plugins";

    
    // ビルド前ビルドに邪魔になるにディレクトリを退避する.
    [MenuItem("Custom/SwitchToMagicLeap")]
    public static void SwitchToMagicLeap()
    {
        CheckExistsBuildTempFolder();
        
        // ここで退避するプラグインを記述.
        #if PLATFORM_LUMIN
        MoveDirectory(Application.dataPath + UDesktopDuplicationPluginFolder, BuildTempFolderPath + UDesktopDuplicationPluginFolder);
        #endif
    }


    // ビルド後に退避したディレクトリをもとに戻す.
    [MenuItem("Custom/SwitchToWindows")]
    public static void SwitchToWindows()
    {
        CheckExistsBuildTempFolder();
        
        #if PLATFORM_LUMIN
        MoveDirectory( BuildTempFolderPath + UDesktopDuplicationPluginFolder, Application.dataPath + UDesktopDuplicationPluginFolder);
        #endif
    }


    private static void MoveDirectory(string from, string to)
    {
        UnityEngine.Debug.Log($"Pluginsフォルダを移動します {from} -> {to}");
        CheckMoveDirectory(to);
        Directory.Move(from, to);
        AssetDatabase.Refresh();
    }


    // 退避用フォルダが存在するか確認、無ければ作成する.
    private static void CheckExistsBuildTempFolder()
    {
        if (!File.Exists(BuildTempFolderPath))
        {
            Directory.CreateDirectory(BuildTempFolderPath);
        }
    }


    /// <summary>
    /// 移動先のパスのチェック.
    /// </summary>
    /// <param name="toFolderPath"></param>
    private static void CheckMoveDirectory(string toFolderPath)
    {
        // 親ディレクトリが無ければ作成する.
        var parent = Directory.GetParent(toFolderPath);
        if (parent.Exists)
        {
            parent.Create();
        }

        // 移動先のディレクトリが既にある場合は一度削除する.
        if (Directory.Exists(toFolderPath))
        {
            Directory.Delete(toFolderPath);
        }
    }


}
