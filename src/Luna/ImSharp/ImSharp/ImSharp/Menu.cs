// ReSharper disable MemberHidesStaticFromOuterClass

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace ImSharp;

public static partial class Im
{
    public static unsafe class Menu
    {
        /// <inheritdoc cref="MenuBarDisposable()"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static MenuBarDisposable Bar()
            => new();

        /// <inheritdoc cref="MainMenuBarDisposable()"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static MainMenuBarDisposable Main()
            => new();

        /// <inheritdoc cref="MenuDisposable(ref Utf8LabelHandler,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static MenuDisposable Begin(Utf8LabelHandler label, bool enabled = true)
            => new(ref label, enabled);

        /// <inheritdoc cref="MenuDisposable.Item(Utf8LabelHandler,Utf8HintHandler,bool,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Item(Utf8LabelHandler label, Utf8HintHandler shortcut, bool selected = false, bool enabled = true)
            => Native.Methods.Menu.MenuItem(label.Start(), shortcut.Start(), selected, enabled);

        /// <inheritdoc cref="MenuDisposable.Item(Utf8LabelHandler,Utf8HintHandler,ref bool,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Item(Utf8LabelHandler label, Utf8HintHandler shortcut, ref bool selected, bool enabled = true)
            => Native.Methods.Menu.MenuItem(label.Start(), shortcut.Start(), (ImBool*)Unsafe.AsPointer(ref selected), enabled);

        /// <inheritdoc cref="MenuDisposable.Item(Utf8LabelHandler,bool,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Item(Utf8LabelHandler label, bool selected = false, bool enabled = true)
            => Native.Methods.Menu.MenuItem(label.Start(), null, selected, enabled);

        /// <inheritdoc cref="MenuDisposable.Item(Utf8LabelHandler,ref bool,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Item(Utf8LabelHandler label, ref bool selected, bool enabled = true)
            => Native.Methods.Menu.MenuItem(label.Start(), null, (ImBool*)Unsafe.AsPointer(ref selected), enabled);
    }
}
