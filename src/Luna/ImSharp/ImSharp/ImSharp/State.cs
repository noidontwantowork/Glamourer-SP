namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper class around methods related to the current ImGui state. </summary>
    public static unsafe class State
    {
        /// <summary> Get the current frame count of this ImGui framework. </summary>
        public static int FrameCount
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Native.Methods.Utility.GetFrameCount();
        }

        /// <summary> Get the current time of this ImGui framework. </summary>
        public static double Time
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Native.Methods.Utility.GetTime();
        }

        /// <summary> Get or set the state storage of the current window. </summary>
        public static StateStorage Storage
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Native.Methods.Utility.GetStateStorage();
            [MethodImpl(ImSharpConfiguration.Inl)]
            set => Native.Methods.Utility.SetStateStorage(value.Pointer);
        }
    }
}
