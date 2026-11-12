namespace Luna;

/// <summary> The button to expand the descendants of a specific folder </summary>
/// <param name="fileSystem"> The file system. </param>
/// <param name="filter"> The filter used by the file system drawer. </param>
public sealed class ExpandDescendantsButton(BaseFileSystem fileSystem, IFilter filter) : BaseButton<IFileSystemFolder>
{
    /// <inheritdoc/>
    public override ReadOnlySpan<byte> Label(in IFileSystemFolder _)
        => "展开所有子折叠组"u8;

    /// <inheritdoc/>
    public override void OnClick(in IFileSystemFolder folder)
        => fileSystem.ExpandAllDescendants(folder, !filter.IsEmpty);

    /// <inheritdoc/>
    public override bool HasTooltip
        => true;

    /// <inheritdoc/>
    public override void DrawTooltip(in IFileSystemFolder _)
        => Im.Text("依次展开这个折叠组的所有子折叠组，包括它自己。"u8);
}
