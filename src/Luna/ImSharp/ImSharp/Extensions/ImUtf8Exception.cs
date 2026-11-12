namespace ImSharp;

/// <summary>
/// Exception thrown when the formatter encounters an error while trying to format,
/// or if the supplied format results in text longer than the static buffers allow.
/// </summary>
public class Utf8FormatException() : Exception("Could not format UTF8 String.");

/// <summary>
/// Exception thrown when the internal buffers do not suffice to store data.
/// </summary>
public class ImSharpSizeException() : Exception("Input data is longer than buffer size.");

/// <summary>
/// Exception thrown only in debug mode when state-dependent ImGui functions are used in invalid state.
/// </summary>
public class ImGuiStateException(string dependentFunction, string text) : Exception($"Could not invoke {dependentFunction}: {text}")
{
    [Conditional("STATECHECKS")]
    [MethodImpl(ImSharpConfiguration.Opt)]
    internal static void CheckState(bool success, bool alive, string dependent, string type)
    {
        if (!success)
            throw new ImGuiStateException(dependent, $"{type} was not successfully begun.");

        if (alive)
            return;

        throw new ImGuiStateException(dependent, $"{type} was already disposed.");
    }
}
