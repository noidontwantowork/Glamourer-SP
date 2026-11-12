namespace Luna;

/// <summary> Provides a button to toggle sorting as a folder on or off. </summary>
/// <param name="fileSystem"> The parent file system. </param>
public sealed class SeparatorSortAsFolderButton(BaseFileSystem fileSystem) : BaseButton<IFileSystemSeparator>
{
    /// <inheritdoc/>
    public override ReadOnlySpan<byte> Label(in IFileSystemSeparator data)
        => data.BehavesLikeFolder ? "按数据项排序"u8 : "按折叠组排序"u8;

    /// <inheritdoc/>
    public override void OnClick(in IFileSystemSeparator data)
        => fileSystem.ChangeSeparator(data, !data.BehavesLikeFolder);
}
