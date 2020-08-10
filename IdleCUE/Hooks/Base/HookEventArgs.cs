using System;

namespace IdleCUE.Hooks
{
    public class HookEventArgs : EventArgs
    {
        /// <summary>
        /// The hook code equivalent to the <see cref="HookType"/> enum.
        /// </summary>
        public int HookCode;
        /// <summary>
        /// Specifies whether the message is sent by the current process. 
        /// If the message is sent by the current process, it is nonzero; 
        /// otherwise, it is NULL.
        /// </summary>
        public IntPtr wParam;
        /// <summary>
        /// A pointer to a CWPRETSTRUCT structure that contains details about the message.
        /// </summary>
        public IntPtr lParam; 
    }
}
