using System;

namespace IdleCUE.Hooks
{
    public class MouseHook : IDisposable
    {
        private readonly WindowsHook _baseHook;

        public event EventHandler<bool> MouseEvent;
        public MouseHook()
        {
            _baseHook = new WindowsHook(HookType.WH_MOUSE_LL);

            _baseHook.HookInvoked += OnBaseHookInvoked;
        }

        private void OnBaseHookInvoked(object sender, HookEventArgs e)
        {
            MouseEvent?.Invoke(this, true);
        }

        public void Dispose() => _baseHook.Dispose();
    }
}
