using Microsoft.Extensions.Logging;

namespace ImSharp;

/// <summary> Utility to debug input text state. </summary>
internal readonly unsafe struct InputTextState(ImGuiId id, byte* buffer, bool edited, int length) : IEquatable<InputTextState>
{
    private static InputTextState _store;

    public ImGuiId  LastId    { get; init; } = id;
    public bool     WasActive { get; init; } = id.ActivePreviousFrame;
    public bool     IsActive  { get; init; } = id.Active;
    public bool     Edited    { get; init; } = edited;
    public nint     Buffer    { get; init; } = (nint)buffer;
    public int      Length    { get; init; } = length;
    public StringU8 Text      { get; init; } = new(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(buffer), false);

    public bool Equals(InputTextState other)
        => LastId.Equals(other.LastId)
         && WasActive == other.WasActive
         && IsActive == other.IsActive
         && Edited == other.Edited
         && Length == other.Length
         && Buffer == other.Buffer
         && Text.Equals(other.Text);

    public override bool Equals(object? obj)
        => obj is InputTextState other && Equals(other);

    public override int GetHashCode()
        => HashCode.Combine(LastId, WasActive, IsActive, Edited, Buffer, Text);

    public static bool operator ==(InputTextState left, InputTextState right)
        => left.Equals(right);

    public static bool operator !=(InputTextState left, InputTextState right)
        => !left.Equals(right);

    public static void Check(ImGuiId id, byte* buffer, bool edited, int length)
    {
        var s = new InputTextState(id, buffer, Im.Item.Edited, (int)length);
        if (s != _store)
        {
            _store = s;
            _store.Log();
        }
    }

    public void Log()
    {
        ImSharpConfiguration.Logger.LogInformation(
            "Last ID: 0x{LastId:X8} | Is Active: {Active,5} | Was Active: {WasActive,5} | Edited: {Edited,5} | Buffer Address: 0x{Buffer:X} | Length: {Length:D3}| Text: {Text:l}",
            LastId.Id, IsActive, WasActive, Edited, Buffer, Length, Text.ToString());
    }
}
