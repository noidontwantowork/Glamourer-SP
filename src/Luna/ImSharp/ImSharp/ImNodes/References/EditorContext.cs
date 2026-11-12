namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    /// <summary> A custom node editor context and related static functions. </summary>
    /// <remarks> The non-static functionality of this class should usually not be required. Any node editor creates its own context implicitly. </remarks>
    public sealed unsafe class EditorContext : IDisposable
    {
        /// <summary> The address of the native object. </summary>
        public Internal.EditorContext* Pointer { get; private set; } = Api.EditorContextCreate();

        /// <inheritdoc/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (Pointer is null)
                return;

            Api.EditorContextFree(Pointer);
            GC.SuppressFinalize(this);
            Pointer = null;
        }

        [MethodImpl(ImSharpConfiguration.OptInl)]
        ~EditorContext()
            => Dispose();

        /// <summary> Set the current editor context to this. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Set()
            => Api.EditorContextSet(Pointer);

        /// <summary> Obtain an unowned UTF8 configuration string of this editor context. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ReadOnlySpan<byte> IniString()
        {
            ulong size;
            var   text = Api.SaveEditorStateToIniString(Pointer, &size);
            return new ReadOnlySpan<byte>(text, (int)size);
        }

        /// <summary> Obtain an owned UTF8 configuration string of this editor context. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StringU8 IniStringOwned()
            => new(IniString());

        /// <summary> Obtain an owned UTF16 configuration string of this editor context. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public string IniStringUtf16()
            => Encoding.UTF8.GetString(IniString());

        /// <summary> Load a configuration string into this editor context. </summary>
        /// <param name="text"> The configuration string as text. Does not have to be null-terminated. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void LoadIniString(Utf8TextHandler text)
            => Api.LoadEditorStateFromIniString(Pointer, text.Start(out var end), (ulong)(end - text.Begin));

        /// <summary> Save the configuration string of this editor context to a file. </summary>
        /// <param name="file"> The path to the file to save to as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void SaveToFile(Utf8LabelHandler file)
            => Api.SaveEditorStateToIniFile(Pointer, file.Start());

        /// <summary> Load a configuration string from a file into this editor context. </summary>
        /// <param name="file"> The path to the file to load from as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void LoadFromFile(Utf8LabelHandler file)
            => Api.LoadEditorStateFromIniFile(Pointer, file.Start());

        /// <summary> Obtain an unowned UTF8 configuration string of the current editor context. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ReadOnlySpan<byte> CurrentIniString()
        {
            ulong size;
            var   text = Api.SaveCurrentEditorStateToIniString(&size);
            return new ReadOnlySpan<byte>(text, (int)size);
        }

        /// <summary> Obtain an owned UTF8 configuration string of the current editor context. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static StringU8 CurrentIniStringOwned()
            => new(CurrentIniString());

        /// <summary> Obtain an owned UTF16 configuration string of the current editor context. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static string CurrentIniStringUtf16()
            => Encoding.UTF8.GetString(CurrentIniString());

        /// <summary> Load a configuration string into the current editor context. </summary>
        /// <param name="text"> The configuration string as text. Does not have to be null-terminated. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void LoadCurrentIniString(Utf8TextHandler text)
            => Api.LoadCurrentEditorStateFromIniString(text.Start(out var end), (ulong)(end - text.Begin));

        /// <summary> Save the configuration string of the current editor context to a file. </summary>
        /// <param name="file"> The path to the file to save to as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SaveCurrentToFile(Utf8LabelHandler file)
            => Api.SaveCurrentEditorStateToIniFile(file.Start());

        /// <summary> Load a configuration string from a file into the current editor context. </summary>
        /// <param name="file"> The path to the file to load from as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void LoadCurrentFromFile(Utf8LabelHandler file)
            => Api.LoadCurrentEditorStateFromIniFile(file.Start());

        /// <summary> Get or set the panning value of the current editor context. </summary>
        public static Vector2 Panning
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImVec2 ret;
                Api.GetPanning(&ret);
                return ret;
            }
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Api.ResetPanning(value);
        }

        /// <summary> Move the view of the current editor context to a given node. </summary>
        /// <param name="id"> The ID of the node to move to. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void MoveToNode(NodeId id)
            => Api.MoveToNode(id);
    }
}
