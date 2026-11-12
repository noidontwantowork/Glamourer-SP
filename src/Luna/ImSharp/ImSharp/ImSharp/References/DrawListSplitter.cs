namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to a draw list splitter. </summary>
    /// <param name="pointer"> The native pointer to the draw list splitter. </param>
    public readonly unsafe ref struct DrawListSplitter(Native.ImDrawListSplitter* pointer, DrawList parent)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.ImDrawListSplitter* Pointer = pointer;

        /// <summary> The associated draw list for this splitter. </summary>
        public readonly DrawList Parent = parent;

        /// <summary> The current number of split channels. </summary>
        public int Count
            => Pointer->Count;

        /// <summary> The currently selected channel. </summary>
        public int Current
            => Pointer->Current;

        /// <summary> Split the parent draw list into the supplied number of channels. </summary>
        /// <param name="numChannels"> The number of channels to split to. </param>
        public void Split(int numChannels)
            => Native.ImDrawListSplitter.Split(Pointer, Parent.Pointer, numChannels);

        /// <summary> Merge the split channels of the parent draw list back together. </summary>
        public void Merge()
            => Native.ImDrawListSplitter.Merge(Pointer, Parent.Pointer);

        /// <summary> Set the currently drawn to channel of the parent draw list. </summary>
        /// <param name="channelIndex"> The channel to select. </param>
        public void SetChannel(int channelIndex)
            => Native.ImDrawListSplitter.SetCurrentChannel(Pointer, Parent.Pointer, channelIndex);
    }
}
