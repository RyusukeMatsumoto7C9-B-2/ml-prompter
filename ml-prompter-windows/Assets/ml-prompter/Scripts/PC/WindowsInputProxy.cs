using System.Runtime.InteropServices;
using UnityEngine;

namespace ml_prompter.Pc
{
    /// <summary>
    /// Windows側の入力を代行する.
    /// MagicLeapから来たイベントをもとに処理を代行.
    /// </summary>
    public class WindowsInputProxy
    {

        #region --- mouse event ---
        // マウスのイベント値.
        // https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-mouse_event?redirectedfrom=MSDN
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;

        
        // Keyboardのイベント値.
        // https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        
        // Keyの押上フラグ.
        private const int KEYEVENTF_KEYUP = 0x0002;
        
        // 方向キー.
        private const byte VK_LEFT= 0x25;
        private const byte VK_RIGHT = 0x27;

        #endregion --- mouse event ---
        
        
        
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void SetCursorPos(int X, int Y);

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);



        /// <summary>
        /// マウス左クリック.
        /// </summary>
        public void MouseLeftButtonDown()
        {
            var mousePosition = Input.mousePosition;
            mouse_event(MOUSEEVENTF_LEFTDOWN, (int)mousePosition.x, (int)mousePosition.y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, (int)mousePosition.x, (int)mousePosition.y, 0, 0);
        }


        /// <summary>
        /// マウス右クリック.
        /// </summary>
        public void MouseRightButtonDown()
        {
            var mousePosition = Input.mousePosition;
            mouse_event(MOUSEEVENTF_RIGHTDOWN, (int)mousePosition.x, (int)mousePosition.y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, (int)mousePosition.x, (int)mousePosition.y, 0, 0);
        }


        /// <summary>
        /// 左矢印キー.
        /// </summary>
        public void LeftArrowKey()
        {
            keybd_event(VK_LEFT, 0, 0, 0 );
            keybd_event(VK_LEFT, 0, KEYEVENTF_KEYUP, 0 );
        }


        /// <summary>
        /// 右矢印キー.
        /// </summary>
        public void RightArrowKey()
        {
            keybd_event(VK_RIGHT, 0, 0, 0 );
            keybd_event(VK_RIGHT, 0, KEYEVENTF_KEYUP, 0 );
        }
    }
}