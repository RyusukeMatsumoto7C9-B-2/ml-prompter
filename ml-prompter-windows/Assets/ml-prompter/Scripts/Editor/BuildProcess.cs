using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;


public class BuildProcess : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    // 退避先のディレクトリ.
    private static string BuildTempFolderPath => Directory.GetParent(Application.dataPath) + @"\BuildTempFolder";

    #region --- MagicLeap ---
    // 退避するディレクトリ.
    private static string UDesktopDuplicationPluginFolder => @"\uDesktopDuplication\Plugins";
    #endregion --- MagicLeap ---

    
    public int callbackOrder { get; }

    
    
    public void OnPreprocessBuild(BuildReport report)
    {
        CheckExistsBuildTempFolder();
        
        // ここで退避するプラグインを記述.
#if PLATFORM_LUMIN
        MoveDirectory(Application.dataPath + UDesktopDuplicationPluginFolder, BuildTempFolderPath + UDesktopDuplicationPluginFolder);
#endif
    }


    public void OnPostprocessBuild(BuildReport report)
    {
        CheckExistsBuildTempFolder();

        // 退避したフォルダを元に戻す.
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
