// ReSharper disable MemberHidesStaticFromOuterClass

namespace Luna;

/// <summary> An interface for different sort modes that can be used with a file system. </summary>
public interface ISortMode : IEquatable<ISortMode>
{
    /// <summary> The display name of the sort mode for combo selection. </summary>
    public ReadOnlySpan<byte> Name { get; }

    /// <summary> The description of the sort mode for combo tooltips. </summary>
    public ReadOnlySpan<byte> Description { get; }

    /// <summary> The method the sort mode uses to get the children of a folder in its specific order. </summary>
    /// <param name="folder"> The folder to fetch children from. </param>
    /// <returns> The children of <paramref name="folder"/> ordered according to this sort mode. </returns>
    public IEnumerable<IFileSystemNode> GetChildren(IFileSystemFolder folder);

    /// <summary> See <see cref="Types.FoldersFirst.Description"/>. </summary>
    public static readonly ISortMode FoldersFirst = new Types.FoldersFirst();

    /// <summary> See <see cref="Types.Lexicographical.Description"/>. </summary>
    public static readonly ISortMode Lexicographical = new Types.Lexicographical();

    /// <summary> See <see cref="Types.InverseFoldersFirst.Description"/>. </summary>
    public static readonly ISortMode InverseFoldersFirst = new Types.InverseFoldersFirst();

    /// <summary> See <see cref="Types.InverseLexicographical.Description"/>. </summary>
    public static readonly ISortMode InverseLexicographical = new Types.InverseLexicographical();

    /// <summary> See <see cref="Types.FoldersLast.Description"/>. </summary>
    public static readonly ISortMode FoldersLast = new Types.FoldersLast();

    /// <summary> See <see cref="Types.InverseFoldersLast.Description"/>. </summary>
    public static readonly ISortMode InverseFoldersLast = new Types.InverseFoldersLast();

    /// <summary> See <see cref="Types.InternalOrder.Description"/>. </summary>
    public static readonly ISortMode InternalOrder = new Types.InternalOrder();

    /// <summary> See <see cref="Types.InverseInternalOrder.Description"/>. </summary>
    public static readonly ISortMode InverseInternalOrder = new Types.InverseInternalOrder();

    /// <inheritdoc/>
    bool IEquatable<ISortMode>.Equals(ISortMode? other)
        => Equals(this, other);

    /// <summary> Whether two sort modes are equal. </summary>
    public static bool Equals(ISortMode? lhs, ISortMode? rhs)
        => lhs is null ? rhs is null : rhs is not null && lhs.GetType() == rhs.GetType();

    private static class Types
    {
        public struct FoldersFirst : ISortMode
        {
            public ReadOnlySpan<byte> Name
                => "折叠组优先"u8;

            public ReadOnlySpan<byte> Description
                => "在每个折叠组中，按字典顺序排序所有子折叠组，然后按字典顺序排序所有数据节点。"u8;

            public IEnumerable<IFileSystemNode> GetChildren(IFileSystemFolder folder)
                => GetFolderLike(folder).Concat(GetLeaveLike(folder));
        }

        public struct Lexicographical : ISortMode
        {
            public ReadOnlySpan<byte> Name
                => "字典顺序"u8;

            public ReadOnlySpan<byte> Description
                => "在每个折叠组中，按字典顺序排序所有子折叠组。"u8;

            public IEnumerable<IFileSystemNode> GetChildren(IFileSystemFolder folder)
                => folder.Children;
        }

        public struct InverseFoldersFirst : ISortMode
        {
            public ReadOnlySpan<byte> Name
                => "折叠组优先 (反转)"u8;

            public ReadOnlySpan<byte> Description
                => "在每个折叠组中，按反字典顺序排序所有子折叠组，然后按反字典顺序排序所有数据节点。"u8;

            public IEnumerable<IFileSystemNode> GetChildren(IFileSystemFolder folder)
                => GetFolderLike(folder).Reverse().Concat(GetLeaveLike(folder)).Reverse();
        }

        public struct InverseLexicographical : ISortMode
        {
            public ReadOnlySpan<byte> Name
                => "字典顺序 (反转)"u8;

            public ReadOnlySpan<byte> Description
                => "在每个折叠组中，按反字典顺序排序所有子折叠组。"u8;

            public IEnumerable<IFileSystemNode> GetChildren(IFileSystemFolder folder)
                => folder.Children.Reverse();
        }

        public struct FoldersLast : ISortMode
        {
            public ReadOnlySpan<byte> Name
                => "折叠组最后"u8;

            public ReadOnlySpan<byte> Description
                => "在每个折叠组中，按字典顺序排序所有数据节点，然后按字典顺序排序所有子折叠组。"u8;

            public IEnumerable<IFileSystemNode> GetChildren(IFileSystemFolder folder)
                => GetLeaveLike(folder).Concat(GetFolderLike(folder));
        }

        public struct InverseFoldersLast : ISortMode
        {
            public ReadOnlySpan<byte> Name
                => "折叠组最后 (反转)"u8;

            public ReadOnlySpan<byte> Description
                => "在每个折叠组中，按反字典顺序排序所有数据节点，然后按反字典顺序排序所有子折叠组。"u8;

            public IEnumerable<IFileSystemNode> GetChildren(IFileSystemFolder folder)
                => GetLeaveLike(folder).Reverse().Concat(GetFolderLike(folder)).Reverse();
        }

        public struct InternalOrder : ISortMode
        {
            public ReadOnlySpan<byte> Name
                => "内部顺序"u8;

            public ReadOnlySpan<byte> Description
                => "在每个折叠组中，按标识符顺序排序所有子折叠组（即按其在文件系统中的创建顺序排序）。"u8;

            public IEnumerable<IFileSystemNode> GetChildren(IFileSystemFolder folder)
                => folder.Children.OrderBy(c => c.Identifier);
        }

        public struct InverseInternalOrder : ISortMode
        {
            public ReadOnlySpan<byte> Name
                => "内部顺序 (反转)"u8;

            public ReadOnlySpan<byte> Description
                => "在每个折叠组中，按反标识符顺序排序所有子折叠组（即按其在文件系统中的创建顺序反序排序）。"u8;

            public IEnumerable<IFileSystemNode> GetChildren(IFileSystemFolder folder)
                => folder.Children.OrderByDescending(c => c.Identifier);
        }
    }

    /// <summary> Get all children of a folder that behave like leaves. </summary>
    /// <param name="folder"></param>
    /// <returns></returns>
    public static IEnumerable<IFileSystemNode> GetLeaveLike(IFileSystemFolder folder)
        => folder.Children.Where(c => !c.BehavesLikeFolder);

    public static IEnumerable<IFileSystemNode> GetFolderLike(IFileSystemFolder folder)
        => folder.Children.Where(c => c.BehavesLikeFolder);
}
