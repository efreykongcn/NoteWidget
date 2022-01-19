using Microsoft.Office.Interop.OneNote;
using System;
using System.Collections;

namespace NoteWidgetAddIn
{
    public class DummyWindows : Windows
    {
        public DummyWindows(Window window)
        {
            CurrentWindow = window;
        }

        public IEnumerator GetEnumerator()
        {
            return new ArrayList() { CurrentWindow }.GetEnumerator();
        }

        public Window this[uint Index]
        {
            get 
            {
                if (Index == 0)
                {
                    return CurrentWindow;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public uint Count => 1;

        public Window CurrentWindow { get; private set; }
    }
}
