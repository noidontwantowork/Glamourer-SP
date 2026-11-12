namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct InputTextState
            {
                public ImGuiId           Id;
                public int               CurrentLengthW;
                public int               CurrentLengthA;
                public ImVector<ImWchar> TextW;
                public ImVector<byte>    TextA;
                public ImVector<byte>    InitialTextA;
                public ImBool            TextAIsValid;
                public int               BufferCapacityA;
                public float             ScrollX;
                public Stb.TextEditState Stb;
                public float             CursorAnim;
                public ImBool            CursorFollow;
                public ImBool            SelectedAllMouseLock;
                public ImBool            Edited;
                public InputTextFlags    Flags;
            }
        }
    }
}
