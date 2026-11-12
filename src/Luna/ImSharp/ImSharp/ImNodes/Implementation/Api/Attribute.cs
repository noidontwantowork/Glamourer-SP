namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    public static unsafe partial class Api
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void BeginInputAttribute(AttributeId id, PinShape shape)
            => Internal.BeginPinAttribute(id, Internal.AttributeType.Input, shape, Context->CurrentNodeIndex);

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void EndInputAttribute()
            => Internal.EndPinAttribute();

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void BeginOutputAttribute(AttributeId id, PinShape shape)
            => Internal.BeginPinAttribute(id, Internal.AttributeType.Output, shape, Context->CurrentNodeIndex);

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void EndOutputAttribute()
            => Internal.EndPinAttribute();

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void BeginStaticAttribute(AttributeId id)
        {
            ref var context = ref Internal.Scope.Node.Check(Internal.Scope.Node | Internal.Scope.Attribute);
            context.CurrentAttributeId = id;
            Im.Group();
            Im.Id.Push(id.Id);
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void EndStaticAttribute()
        {
            ref var context = ref Internal.Scope.Attribute.Check(Internal.Scope.Node);
            Im.IdDisposable.PopUnsafe();
            Im.GroupDisposable.EndUnsafe();
            if (Im.Item.Active && !context.CurrentAttributeFlags.IsDisabled)
            {
                context.ActiveAttribute   = true;
                context.ActiveAttributeId = context.CurrentAttributeId;
            }
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static ImBool IsAttributeActive()
        {
            ref readonly var context = ref Internal.Scope.Node.Check();
            return context.ActiveAttribute && context.ActiveAttributeId == context.CurrentAttributeId;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static ImBool IsAnyAttributeActive(AttributeId* id)
        {
            ref readonly var context = ref *Context;
            Debug.Assert(!context.CurrentScope.HasFlag(Internal.Scope.Node | Internal.Scope.Attribute));
            if (!context.ActiveAttribute)
                return false;

            if (id is not null)
                *id = context.ActiveAttributeId;
            return true;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static ImBool IsPinHovered(AttributeId* hovered)
        {
            ref var context = ref Internal.Scope.None.Check();
            var anyHovered = context.HoveredPinIndex.IsValid;
            if (anyHovered && hovered is not null)
                *hovered = Editor->Pins.Get(context.HoveredPinIndex).Id;

            return anyHovered;
        }
    }
}
