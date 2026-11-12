namespace ImSharp.ImNodes;

public static partial class Internal
{
    public unsafe struct Io()
    {
        public ImBool*     EmulateThreeButtonMouse;
        public ImBool*     LinkDetachWithModifierClick;
        public ImBool*     MultipleSelectModifier;
        public MouseButton AltMouseButton   = MouseButton.Middle;
        public float       AutoPanningSpeed = 1000;

        public readonly bool AltMouseClicked
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => EmulateThreeButtonMouse is not null && *EmulateThreeButtonMouse && ImNodes.Context->LeftMouseClicked
             || Im.Mouse.IsClicked(AltMouseButton);
        }

        public readonly bool AltMouseDragging
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => EmulateThreeButtonMouse is not null && *EmulateThreeButtonMouse && ImNodes.Context->LeftMouseDragging
             || Im.Mouse.IsDragging(AltMouseButton, 0);
        }

        public readonly bool MultipleSelectActive
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => MultipleSelectModifier is not null ? *MultipleSelectModifier : Im.Io.KeyControl;
        }
    }
}
