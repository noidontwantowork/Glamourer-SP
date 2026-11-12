namespace ImSharp;

public static unsafe partial class Interop
{
    /// <summary> Query information about <paramref name="address"/> from the OS. </summary>
    /// <returns> True on success. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool VirtualQuery(nint address, out MemoryBasicInformation info)
    {
        fixed (MemoryBasicInformation* ptr = &info)
        {
            return VirtualQuery(address, ptr, sizeof(MemoryBasicInformation)) is not 0;
        }
    }

    [LibraryImport("kernel32.dll", SetLastError = true)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static partial nint VirtualQuery(nint lpAddress, MemoryBasicInformation* lpBuffer, int dwLength);

    [StructLayout(LayoutKind.Sequential)]
    public struct MemoryBasicInformation
    {
        public nint             BaseAddress;
        public nint             AllocationBase;
        public MemoryProtection AllocationProtect;
        public ushort           PartitionId;
        public nint             RegionSize;
        public MemoryState      State;
        public MemoryProtection Protect;
        public MemoryType       Type;
    }

    [Flags]
    public enum MemoryState : uint
    {
        Commit  = 0x1000,
        Reserve = 0x2000,
        Free    = 0x10000,
    }

    [Flags]
    public enum MemoryType : uint
    {
        Private = 0x20000,
        Mapped  = 0x40000,
        Image   = 0x1000000,
    }

    [Flags]
    public enum MemoryProtection
    {
        Execute          = 0x10,
        ExecuteRead      = 0x20,
        ExecuteReadWrite = 0x40,
        ExecuteWriteCopy = 0x80,
        NoAccess         = 0x01,
        ReadOnly         = 0x02,
        ReadWrite        = 0x04,
        WriteCopy        = 0x08,
        TargetsInvalid   = 0x40000000,
        TargetsNoUpdate  = TargetsInvalid,
        Guard            = 0x100,
        NoCache          = 0x200,
        WriteCombine     = 0x400,
    }
}
