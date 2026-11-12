namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        /// <summary> Callback-data for <seealso cref="Im.Window.SetNextSizeConstraints"/>. </summary>
        public unsafe struct SizeCallbackData
        {
            /// <summary> Whatever a consumer passed to <seealso cref="Im.Window.SetNextSizeConstraints"/>. </summary>
            public void* UserData;

            /// <summary> The current window position. Read-only. </summary>
            public ImVec2 Position;

            /// <summary> The current window size. Read-only. </summary>
            public ImVec2 CurrentSize;

            /// <summary> The desired window size. Read/Write. </summary>
            public ImVec2 DesiredSize;
        }
    }
}
