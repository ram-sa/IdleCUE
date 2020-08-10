using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Mouse;
using IdleCUE.Hooks;
using System;
using System.Collections.Generic;
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

            List<AbstractCueDevice> devices = new List<AbstractCueDevice>
            {
                CueSDK.KeyboardSDK,
                CueSDK.HeadsetSDK,
                CueSDK.HeadsetStandSDK,
                CueSDK.MouseSDK,
                CueSDK.MousematSDK
            };

            IBrush brush = new SolidColorBrush(Color.Black);

            devices.ForEach(device =>
            {
                if(device != null)
                {
                    device.Brush = brush;
                    device.Update();
                }
            });

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
