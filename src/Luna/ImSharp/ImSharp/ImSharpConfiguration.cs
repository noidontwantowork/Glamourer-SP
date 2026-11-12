using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ImSharp;

public static unsafe class ImSharpConfiguration
{
    internal const MethodImplOptions Opt    = MethodImplOptions.AggressiveOptimization;
    internal const MethodImplOptions Inl    = MethodImplOptions.AggressiveInlining;
    internal const MethodImplOptions OptInl = Opt | Inl;

    internal static ImSharpContext*  Context = ImSharpContext.EmptyPointer;
    internal static ILogger          Logger  = NullLogger.Instance;
    internal static Action<ILogger>? LoggerChanged;
    private static  bool             _contextOwned = true;

    /// <summary> Whether the context has been initialized. </summary>
    public static bool IsInitialized
        => Context != ImSharpContext.EmptyPointer;

    /// <summary> Set or remove the global ImSharp context. </summary>
    /// <param name="context"> The address of the context to set. If this is null, the empty context will be set. </param>
    /// <param name="owned"> Whether the new context is owned by this assembly or not. </param>
    /// <exception cref="Exception"/>
    /// <remarks> Do not call this within a draw frame of the contained <see cref="Im.Native.Internal.Context"/>, only outside of drawing. </remarks>
    public static void SetContext(ImSharpContext* context, bool owned)
    {
        if (context is not null)
        {
            var imguiContext = (Im.Native.Internal.Context*)context->ImGuiContext;

            if (imguiContext is not null && imguiContext->WithinFrameScope)
                throw new Exception("Can not set a new context while in a frame.");

            if (context->Version is not ImSharpContext.CurrentVersion)
                throw new Exception(
                    $"Can not set a context of version {context->Version}, at least version {ImSharpContext.CurrentVersion} is required.");
        }

        if (_contextOwned && Context is not null)
            Context->Dispose();

        _contextOwned = owned;
        Context       = context is null ? ImSharpContext.EmptyPointer : context;
        Logger.LogDebug("Set ImSharp context to {Context:l}.", context is null ? "Empty Context" : $"0x{(nint)context:X}");
        ImNodes.ImNodes.Api.SetCurrentContext(context is null ? null : (ImNodes.Internal.NodesContext*)context->ImNodesContext);
    }

    /// <summary> Set or remove a global logger for ImSharp. </summary>
    public static void SetLogger(ILogger? logger)
    {
        if (ReferenceEquals(logger, Logger))
            return;

        // ReSharper disable once SuspiciousTypeConversion.Global
        if (Logger is IDisposable disposable)
        {
            disposable.Dispose();
            logger?.LogDebug("Disposed old ImSharp Logger.");
        }

        Logger = logger ?? NullLogger.Instance;
        LoggerChanged?.Invoke(Logger);
        Logger.LogDebug("Set ImSharp Logger to {Logger:l}.", logger is null ? "NullLogger" : "new logger");
    }

    /// <summary> The array pool used internally to rent arrays. </summary>
    internal static ArrayPool<byte> ArrayPool = ArrayPool<byte>.Shared;

    /// <summary> Get or set the array request size when requesting assumed to be large arrays of entirely unknown size. </summary>
    /// <remarks> Default: 2^22. Clamped to [2^10, 2^28]. </remarks>
    public static int ArrayPoolRequestSizeLarge
    {
        get;
        set => field = Math.Clamp(value, 1 << 10, 1 << 28);
    } = 1 << 22;

    /// <summary> Get or set the array request size when requesting assumed to be medium arrays of entirely unknown size. </summary>
    /// <remarks> Default: 2^16. Clamped to [2^8, 2^28]. </remarks>
    public static int ArrayPoolRequestSizeMedium
    {
        get;
        set => field = Math.Clamp(value, 1 << 8, 1 << 28);
    } = 1 << 16;

    /// <summary> Get or set the array request size when requesting assumed to be small arrays of entirely unknown size. </summary>
    /// <remarks> Default: 2^10. Clamped to [2^5, 2^28]. </remarks>
    public static int ArrayPoolRequestSizeSmall
    {
        get;
        set => field = Math.Clamp(value, 1 << 5, 1 << 28);
    } = 1 << 10;

    /// <summary> Get or set the estimated size of each hole in a formatted string to a new value. </summary>
    /// <remarks> Default: 2^7. Clamped to [2^2, 2^10]. </remarks>
    public static int FormatHoleEstimate
    {
        [MethodImpl(OptInl)]
        get;
        [MethodImpl(OptInl)]
        set => field = Math.Clamp(value, 1 << 2, 1 << 10);
    } = 1 << 7;

    private static bool _arrayPoolOwned;

    /// <summary> Set a specific array pool used to rent temporary arrays. </summary>
    /// <param name="newArrayPool"> The array pool to use. If null, the default array pool will be used. </param>
    /// <param name="owned"> Whether the array pool is owned, in which case it will be disposed when replaced. </param>
    public static void SetArrayPool(ArrayPool<byte>? newArrayPool, bool owned = false)
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        if (_arrayPoolOwned && !ReferenceEquals(ArrayPool, newArrayPool) && ArrayPool is IDisposable disposable)
            disposable.Dispose();

        if (newArrayPool is null)
        {
            ArrayPool       = ArrayPool<byte>.Shared;
            _arrayPoolOwned = false;
            Logger.LogDebug("Set ImSharp array pool to the default shared pool.");
        }
        else
        {
            ArrayPool       = newArrayPool;
            _arrayPoolOwned = owned;
            Logger.LogDebug("Set ImSharp array pool to new {Owned} array pool.", owned ? "owned" : "unowned");
        }
    }

    [UsedImplicitly]
    private static readonly CleanupType Cleanup = new();

    private class CleanupType
    {
        ~CleanupType()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (!_arrayPoolOwned || ArrayPool is not IDisposable disposable)
                return;

            disposable.Dispose();
            Logger.LogDebug("Disposing owned ImSharp array pool.");
        }
    }
}
