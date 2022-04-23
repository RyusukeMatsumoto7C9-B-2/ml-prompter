namespace ml_prompter_WebSocketServer
{
    /// <summary>
    /// 現在実行しているディレクトリパス、自動で実行させるファイルパスを管理.
    /// </summary>
    public class ExecuteFilePathManager
    {
        public readonly string ExecuteDirectoryPath;
        public string BrowserPath => ExecuteDirectoryPath + "/PCSetup/Browser/index.html";
        public string SignalingServerPath => ExecuteDirectoryPath + "/PCSetup/SignalingServer/server.py";

        public ExecuteFilePathManager()
        {
            // 現在実行しているディレクトリのパスを決定.
            var executeAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            ExecuteDirectoryPath = System.IO.Path.GetDirectoryName(executeAssembly.Location);
        }
    }
}