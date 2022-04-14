using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.WebSockets;
using System.Threading;



namespace ml_prompter_WebSocketServer
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("5秒後にキーボードシミュレートします.");
            Thread.Sleep(5000);
            Win32Api.KeyBoardEvent(Win32Api.VK_1);

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
