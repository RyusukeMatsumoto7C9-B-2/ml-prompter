

// WebSocketサーバと接続した情報.
var connection;

// WebSocket側から受け取ったスピーカーノートテキスト、MagicLeapと同期したらMagicLeapに送信する.
var speakerNote;

function connectingWebSocket() {
  console.log(location.pathname);

  console.log("WebSocketでサーバーに接続.");
  //var connection = new WebSocket('ws://localhost:8000/ws/');
  connection = new WebSocket('ws://localhost:8000/ws/');
  connection.onopen = function(event) {
    console.log("通信接続イベント受信");
    console.log(event.data);
  };

  // エラー発生.
  connection.onerror = function(error) {
    console.log("エラー発生イベント受信");
    console.log(error.data);
  };

  // WebSocketサーバからメッセージ受信.
  connection.onmessage = function(event) {
    console.log("メッセージ受信");
    console.log(event.data);
    speakerNote = event.data;
  };

  // WebSocketと切断.
  connection.onclose = function() {
    console.log("通信切断イベント受信");
  };
}
 

// WebSocket側にメッセージを送信.
function sendMessageToWebSocket(message)
{
  console.log("Hogeメッセージを送信します : " + message);
  connection.send(message.toString());
}






