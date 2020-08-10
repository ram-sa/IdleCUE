using System;

namespace IdleCUE.Hooks
{
    public class KeyboardHook
    {
        private readonly WindowsHook _baseHook;

        public event EventHandler<bool> KeyboardEvent;
        public KeyboardHook()
        {
            _baseHook = new WindowsHook(HookType.WH_KEYBOARD_LL);

            _baseHook.HookInvoked += OnBaseHookInvoked;
        }

        private void OnBaseHookInvoked(object sender, HookEventArgs e)
        {
            KeyboardEvent?.Invoke(this, true);
        }

        public void Dispose() => _baseHook.Dispose();
    }
}
