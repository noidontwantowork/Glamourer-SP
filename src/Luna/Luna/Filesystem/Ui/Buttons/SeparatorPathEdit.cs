namespace Luna;

/// <summary> Provides a text input to move the separators sort order or parent folder. </summary>
/// <param name="fileSystem"> The parent file system. </param>
public sealed class SeparatorPathEdit(BaseFileSystem fileSystem) : BaseButton<IFileSystemSeparator>
{
    /// <inheritdoc/>
    public override ReadOnlySpan<byte> Label(in IFileSystemSeparator data)
        => "路径编辑"u8;

    /// <inheritdoc/>
    public override bool DrawMenuItem(in IFileSystemSeparator data)
    {
        Im.Item.SetNextWidthScaled(250);
        if (!ImEx.InputOnDeactivation.Text("排序路径"u8, data.FullPath, out string newPath))
            return false;

        try
        {
            fileSystem.RenameAndMove(data, newPath);
            return true;
        }
        catch
        {
            // ignored
        }

        return false;
    }
}
