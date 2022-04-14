using System.Runtime.InteropServices;


namespace ml_prompter_WebSocketServer
{
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
    
}