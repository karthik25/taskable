using System;

namespace TaskableApp
{
    public class CodeEditorNavEventArgs : EventArgs
    {
        public NavEventType EventType { get; private set; }

        public CodeEditorNavEventArgs(NavEventType eventType)
        {
            this.EventType = eventType;
        }
    }

    public enum NavEventType
    {
        Up,
        Down
    }
}
