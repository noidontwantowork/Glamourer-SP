using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            [StructLayout(LayoutKind.Explicit)]
            public struct InputEvent
            {
                [FieldOffset(0)] public InputEventType          Type;
                [FieldOffset(4)] public InputSource             Source;
                [FieldOffset(8)] public InputEventMousePos      MousePos;
                [FieldOffset(8)] public InputEventMouseWheel    MouseWheel;
                [FieldOffset(8)] public InputEventMouseButton   MouseButton;
                [FieldOffset(8)] public InputEventMouseViewport MouseViewport;
                [FieldOffset(8)] public InputEventKey           Key;
                [FieldOffset(8)] public InputEventText          Text;
                [FieldOffset(8)] public InputEventAppFocused    AppFocused;

                [FieldOffset(20)] public ImBool AddedByTestEngine;
            }

            public struct InputEventMousePos
            {
                public float PosX;
                public float PosY;
            }

            public struct InputEventMouseWheel
            {
                public float WheelX;
                public float WheelY;
            }

            public struct InputEventMouseButton
            {
                public int    Button;
                public ImBool Down;
            }

            public struct InputEventMouseViewport
            {
                public ImGuiId HoveredViewportId;
            }

            public struct InputEventKey
            {
                public Key    Key;
                public ImBool Down;
                public float  AnalogValue;
            }

            public struct InputEventText
            {
                public uint Char;
            }

            public struct InputEventAppFocused
            {
                public ImBool Focused;
            }
        }
    }
}
