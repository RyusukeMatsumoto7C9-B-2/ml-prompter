namespace ml_prompter_websocket_server_test;

using ml_prompter_WebSocketServer;


public class Tests
{
    
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var hoge = new Hoge();
        Assert.True(hoge.HogeHoge());
    }
}