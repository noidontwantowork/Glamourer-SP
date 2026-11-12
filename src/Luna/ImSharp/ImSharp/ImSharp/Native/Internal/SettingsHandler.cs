namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct SettingsHandler
            {
                public byte*                                                     TypeName;
                public ImGuiId                                                   TypeHash;
                public delegate*<Context*, SettingsHandler*, void>               ClearAll;
                public delegate*<Context*, SettingsHandler*, void>               ReadInit;
                public delegate*<Context*, SettingsHandler*, byte*, void*>       ReadOpen;
                public delegate*<Context*, SettingsHandler*, void*, byte*, void> ReadLine;
                public delegate*<Context*, SettingsHandler*, void>               ApplyAll;
                public delegate*<Context*, SettingsHandler*, TextBuffer*, void>  WriteAll;
                public void*                                                     UserData;
            }
        }
    }
}
