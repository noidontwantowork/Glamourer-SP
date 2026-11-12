namespace Luna;

/// <summary> Provides a checkbox to toggle the separator state of a folder. </summary>
/// <param name="drawer"> The parent drawer. </param>
public sealed class FolderAsSeparatorCheckbox(FileSystemDrawer drawer) : BaseButton<IFileSystemFolder>
{
    /// <inheritdoc/>
    public override ReadOnlySpan<byte> Label(in IFileSystemFolder data)
        => "IsSeparator"u8;

    /// <inheritdoc/>
    public override bool DrawMenuItem(in IFileSystemFolder data)
    {
        if (Im.Checkbox("显示为分隔符"u8, data.DrawAsSeparator))
            drawer.FileSystem.ChangeFolderSeparatorState(data, !data.DrawAsSeparator);

        Im.Tooltip.OnHover(
            "启用后，折叠组将显示为分隔符，使用展开颜色。它将始终展开，所有子项将显示在分隔符下，但不缩进。"u8);
        return false;
    }
}
