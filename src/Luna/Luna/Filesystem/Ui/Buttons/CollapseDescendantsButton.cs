namespace Luna;

/// <summary> The button to collapse the descendants of a specific folder </summary>
/// <param name="fileSystem"> The file system. </param>
/// <param name="filter"> The filter used by the file system drawer. </param>
public sealed class CollapseDescendantsButton(BaseFileSystem fileSystem, IFilter filter) : BaseButton<IFileSystemFolder>
{
    /// <inheritdoc/>
    public override ReadOnlySpan<byte> Label(in IFileSystemFolder _)
        => "收起所有子折叠组"u8;

    /// <inheritdoc/>
    public override void OnClick(in IFileSystemFolder folder)
        => fileSystem.CollapseAllDescendants(folder, !filter.IsEmpty);

    /// <inheritdoc/>
    public override bool HasTooltip
        => true;

    /// <inheritdoc/>
    public override void DrawTooltip(in IFileSystemFolder _)
        => Im.Text("依次收起此折叠组及其所有下级折叠组，包括它自己。"u8);
}
