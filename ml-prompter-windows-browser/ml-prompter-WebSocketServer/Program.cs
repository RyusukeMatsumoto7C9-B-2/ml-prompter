using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.WebSockets;
using System.Threading;


// DLLImportに必要.
using System.Runtime.InteropServices;

namespace ml_prompter_WebSocketServer
{

    
    #region --- Win32Api ---
    /// <summary>
    /// Win32APIを呼び出すためのクラス.
    /// </summary>
    public static class Win32Api
    {
        // キーの押上.
        private static uint KEYEVENT_KEYUP => 0x0002;
        
        // 0キー.
        public static byte VK_0 => 0x30;
        public static byte VK_1 => 0x31;
        public static byte VK_2 => 0x32;
        public static byte VK_3 => 0x33;
        public static byte VK_4 => 0x34;
        public static byte VK_5 => 0x35;
        public static byte VK_6 => 0x36;
        public static byte VK_7 => 0x37;
        public static byte VK_8 => 0x38;
        public static byte VK_9 => 0x39;

        // アルファベット.
        public static byte VK_A => 0x41;
        public static byte VK_B => 0x42;
        public static byte VK_C => 0x43;
        public static byte VK_D => 0x44;
        public static byte VK_E => 0x45;
        public static byte VK_F => 0x46;
        public static byte VK_G => 0x47;
        public static byte VK_H => 0x48;
        public static byte VK_I => 0x49;
        public static byte VK_J => 0x4A;
        public static byte VK_K => 0x4B;
        public static byte VK_L => 0x4C;
        public static byte VK_M => 0x4D;
        public static byte VK_N => 0x4E;
        public static byte VK_O => 0x4F;
        public static byte VK_P => 0x50;
        public static byte VK_Q => 0x51;
        public static byte VK_R => 0x52;
        public static byte VK_S => 0x53;
        public static byte VK_T => 0x54;
        public static byte VK_U => 0x55;
        public static byte VK_V => 0x56;
        public static byte VK_W => 0x57;
        public static byte VK_X => 0x58;
        public static byte VK_Y => 0x59;
        public static byte VK_Z => 0x5A;
        
        // 方向キー.
        public static byte VK_LEFT => 0x25;
        public static byte VK_RIGHT => 0x27;
        public static byte VK_UP => 0x26;
        public static byte VK_DOWN => 0x28;
        
        
        [DllImport("user32.dll")]
        private static extern uint keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        
        /// <summary>
        /// 指定した仮想キーコードのキーを押下 -> 押上 処理を行う.
        /// </summary>
        /// <param name="keycode"></param>
        public static void KeyBoardEvent(byte keycode)
        {
            keybd_event(keycode, 0, 0, 0);
            keybd_event(keycode, 0, Win32Api.KEYEVENT_KEYUP, 0);
        }
    }
    
    
    #endregion --- Win32Api ---
    


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("5秒後にキーボードシミュレートします.");
            Thread.Sleep(5000);
            Win32Api.KeyBoardEvent(Win32Api.VK_RIGHT);

            Console.WriteLine("キーシミュレートしました、何かキーを入力してください.");
            Console.ReadLine();

            return;
            Task task = Run();
            while (!task.IsCompleted)
            {
            }
            Console.ReadLine();

            if (task.IsCompleted)
            {
                Console.WriteLine("IsComplete = true");
            }
            else
            {
                Console.WriteLine("IsComplete = false");
            }
        }
        
        
        /// <summary>
        /// WebSocketサーバーを立ち上げ.
        /// </summary>
        /// <returns></returns>
        static async Task Run()
        {
            Console.WriteLine("非同期処理Run()");

            //Httpリスナーを立ち上げ、クライアントからの接続を待つ
            Console.WriteLine("Httpリスナー立ち上げ、クライアントからの接続を待つ");
            HttpListener s = new HttpListener();
            s.Prefixes.Add("http://localhost:8000/ws/");
            s.Start();
            var hc = await s.GetContextAsync();

            //クライアントからのリクエストがWebSocketでない場合は処理を中断
            if (!hc.Request.IsWebSocketRequest)
            {
                //クライアント側にエラー(400)を返却し接続を閉じる
                Console.WriteLine("クライアント側にエラーを返却し接続を閉じる");
                hc.Response.StatusCode = 400;
                hc.Response.Close();
                return;
            }

            //WebSocketでレスポンスを返却
            Console.WriteLine("WebSocketでレスポンスを返却.");
            var wsc = await hc.AcceptWebSocketAsync(null);
            var ws = wsc.WebSocket;


            var buffer = new byte[1024];
            while (true)
            {
                //所得情報確保用の配列を準備
                var segment = new ArraySegment<byte>(buffer);
                
                //クライアントからのレスポンス情報を取得
                var result = await ws.ReceiveAsync(segment, CancellationToken.None);
                
                //バイナリの場合は、当処理では扱えないため、処理を中断
                if (result.MessageType == WebSocketMessageType.Binary)
                {
                    await ws.CloseAsync(WebSocketCloseStatus.InvalidMessageType,
                        "I don't do binary", CancellationToken.None);
                    return;
                }
                
                //メッセージの最後まで取得
                int count = result.Count;
                while (!result.EndOfMessage)
                {
                    if (count >= buffer.Length)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.InvalidPayloadData,
                            "That's too long", CancellationToken.None);
                        return;
                    }
                    segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                    result = await ws.ReceiveAsync(segment, CancellationToken.None);

                    count += result.Count;
                }

                //メッセージを取得
                var message = Encoding.UTF8.GetString(buffer, 0, count);
                Console.WriteLine("> " + message);
                if (message == "close")
                {
                    Console.WriteLine("接続を閉じる");
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure,
                        "Done", CancellationToken.None);
                }
            }


            //１０回のレスポンスを返却
            /*
            Console.WriteLine("10回のレスポンスを返却.");
            for (int i = 0; i != 10; ++i)
            {
                //1回のレスポンスごとに2秒のウエイトを設定
                await Task.Delay(2000);

                //レスポンスのテストメッセージとして、現在時刻の文字列を取得
                var time = DateTime.Now.ToLongTimeString();

                //文字列をByte型に変換
                var buffer = Encoding.UTF8.GetBytes(time);
                var segment = new ArraySegment<byte>(buffer);

                //クライアント側に文字列を送信
                await ws.SendAsync(segment, WebSocketMessageType.Text,
                    true, CancellationToken.None);
                Console.WriteLine("レスポンスを返却");
            }
            
            //接続を閉じる
            Console.WriteLine("接続を閉じる");
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure,
                "Done", CancellationToken.None);
            */
        }
    }
}
