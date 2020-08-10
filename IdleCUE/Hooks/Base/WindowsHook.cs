using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IdleCUE.Hooks
{
    public class WindowsHook : IDisposable
    {
        #region Win32

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)] 
        static extern IntPtr SetWindowsHookEx(HookType code, HookProc func, IntPtr hInstance, int threadID);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnhookWindowsHookEx(IntPtr hhook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr CallNextHookEx(IntPtr hhook, int code, IntPtr wParam, IntPtr lParam);
        
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        #endregion

        readonly IntPtr _hhook;
        readonly HookProc _filterFunc;

        delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

        public event HookEventHandler HookInvoked;

        public delegate void HookEventHandler(object sender, HookEventArgs e);

        public WindowsHook(HookType hookType)
        {
            _hhook = IntPtr.Zero;
            _filterFunc = OnHookInvoked;

            using(ProcessModule module = Process.GetCurrentProcess().MainModule)
                _hhook = SetWindowsHookEx(hookType, _filterFunc, GetModuleHandle(module.ModuleName), 0);
        }

        ~WindowsHook()
        {
            UnhookWindowsHookEx(_hhook);
        }

        IntPtr OnHookInvoked(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0) return CallNextHookEx(_hhook, code, wParam, lParam);

            HookInvoked?.Invoke(this, new HookEventArgs
            {
                HookCode = code,
                lParam = lParam,
                wParam = wParam
            });

            return CallNextHookEx(_hhook, code, wParam, lParam);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_hhook);
        }
    }
}
