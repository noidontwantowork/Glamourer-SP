namespace Luna;

/// <summary> The button to set a folder locked. </summary>
/// <param name="fileSystem"> The file system. </param>
public sealed class LockFolderButton(BaseFileSystem fileSystem) : BaseButton<IFileSystemFolder>
{
    /// <inheritdoc/>
    public override ReadOnlySpan<byte> Label(in IFileSystemFolder folder)
        => folder.Locked ? "解锁折叠组"u8 : "锁定折叠组"u8;

    /// <inheritdoc/>
    public override void OnClick(in IFileSystemFolder folder)
        => fileSystem.ChangeLockState(folder, !folder.Locked);

    /// <inheritdoc/>
    public override bool HasTooltip
        => true;

    /// <inheritdoc/>
    public override void DrawTooltip(in IFileSystemFolder _)
        => Im.Text(
            "锁定一个折叠组可以防止它被拖动到其他位置。不会阻止对折叠组的其他操作。"u8);
}
