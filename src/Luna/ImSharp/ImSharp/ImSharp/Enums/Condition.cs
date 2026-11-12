namespace ImSharp;

/// <summary> An enum to control the behavior of SetNext... methods. </summary>
/// <remarks> These values are not flags. They are mutually exclusive. </remarks>
public enum Condition
{
    /// <summary> No specific condition. Same as <seealso cref="Always"/>. </summary>
    None = 0,

    /// <summary> No specific condition. Same as <seealso cref="None"/>. </summary>
    Always = 1 << 0,

    /// <summary> Set the variable only once per runtime session. </summary>
    Once = 1 << 1,

    /// <summary> Set the variable only if the object has no persistent data yet. </summary>
    FirstUseEver = 1 << 2,

    /// <summary> Set the variable only if the object is appearing after being hidden or inactive (or on the first time). </summary>
    Appearing = 1 << 3,
}
