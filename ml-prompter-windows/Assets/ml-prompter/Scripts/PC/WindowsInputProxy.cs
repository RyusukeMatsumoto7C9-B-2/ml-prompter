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
        
        private const int MouseEventLeftDown = 0x0002;
        private const int MouseEventLeftUp = 0x0004;

        private const int MouseEventRightDown = 0x0008;
        private const int MouseEventRightUp = 0x0010;
        
        private const int MouseEventMiddleDown = 0x0020;
        private const int MouseEventMiddleUp = 0x0040;
        
        #endregion --- mouse event ---
        
        
        
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void SetCursorPos(int X, int Y);

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);



        /// <summary>
        /// マウス左クリック.
        /// </summary>
        public void MouseLeftButtonDown()
        {
            var mousePosition = Input.mousePosition;
            mouse_event(MouseEventLeftDown, (int)mousePosition.x, (int)mousePosition.y, 0, 0);
            mouse_event(MouseEventLeftUp, (int)mousePosition.x, (int)mousePosition.y, 0, 0);
        }


        /// <summary>
        /// マウス右クリック.
        /// </summary>
        public void MouseRightButtonDown()
        {
            var mousePosition = Input.mousePosition;
            mouse_event(MouseEventRightDown, (int)mousePosition.x, (int)mousePosition.y, 0, 0);
            mouse_event(MouseEventRightUp, (int)mousePosition.x, (int)mousePosition.y, 0, 0);
        }
    }
}