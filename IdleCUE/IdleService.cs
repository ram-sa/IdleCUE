using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Mouse;
using IdleCUE.Hooks;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleCUE
{
    public class IdleService
    {
        private MouseHook _mouseHook;
        private KeyboardHook _keyboardHook;

        public void Start()
        {
            _mouseHook = new MouseHook();
            _keyboardHook = new KeyboardHook();

            _mouseHook.MouseEvent += OnDeviceEvent;
            _keyboardHook.KeyboardEvent += OnDeviceEvent;

            CueSDK.Initialize();

            CorsairKeyboard kb = CueSDK.KeyboardSDK;
            CorsairMouse mouse = CueSDK.MouseSDK;

            IBrush brush = new SolidColorBrush(Color.Black);
            kb.Brush = brush;
            mouse.Brush = brush;
            kb.Update();
            mouse.Update();

            Application.Run();
        }

        private void OnDeviceEvent(object sender, bool e)
        {
            _mouseHook.Dispose();
            _keyboardHook.Dispose();

            Application.Exit();
        }
    }
}
