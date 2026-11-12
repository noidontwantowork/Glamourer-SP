namespace Luna;

/// <summary> The button to set a folder locked. </summary>
/// <param name="fileSystem"> The file system. </param>
/// <param name="lockString"> The string displayed when this button locks the node on click. </param>
/// <param name="unlockString"> The string displayed when this button unlocks the node on click. </param>
public sealed class LockNodeButton(BaseFileSystem fileSystem, ReadOnlySpan<byte> lockString, ReadOnlySpan<byte> unlockString)
    : BaseButton<IFileSystemData>
{
    public readonly StringU8 LockString   = new(lockString);
    public readonly StringU8 UnlockString = new(unlockString);

    /// <inheritdoc/>
    public override ReadOnlySpan<byte> Label(in IFileSystemData node)
        => node.Locked ? UnlockString : LockString;

    /// <inheritdoc/>
    public override void OnClick(in IFileSystemData node)
        => fileSystem.ChangeLockState(node, !node.Locked);

    /// <inheritdoc/>
    public override bool HasTooltip
        => true;

    /// <inheritdoc/>
    public override void DrawTooltip(in IFileSystemData _)
        => Im.Text(
            "锁定一个物品可以防止它被拖动到其他位置。不会阻止对物品的其他操作。"u8);
}
