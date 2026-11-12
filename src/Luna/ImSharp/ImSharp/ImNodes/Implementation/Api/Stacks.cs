using static ImSharp.ImNodes.Internal;

namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    public static unsafe partial class Api
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void PushColorStyle(ImNodesColor color, Rgba32 value)
        {
            ref var context = ref *Context;
            context.ColorModifierStack.Add<ColorStackData>(new ColorStackData(context.Style.Colors[(int)color], color));
            context.Style.Colors[(int)color] = value;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void PopColorStyle()
        {
            ref var context = ref *Context;
            var     ret     = context.ColorModifierStack.PopBack(out var last);
            Debug.Assert(ret);
            context.Style.Colors[(int)last.Type] = last.Color;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void PushStyleVar(ImNodesStyleSingle type, float value)
        {
            ref var context = ref *Context;
            var     style   = ImNodesStyleWritable.Get();
            context.StyleModifierStack.Add<StyleStackData>(new StyleStackData(type, style[type]));
            style[type] = value;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void PushStyleVar(ImNodesStyleDouble type, ImVec2 value)
        {
            ref var context = ref *Context;
            var     style   = ImNodesStyleWritable.Get();
            context.StyleModifierStack.Add<StyleStackData>(new StyleStackData(type, style[type]));
            style[type] = value;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void PopStyle(int count)
        {
            ref var context = ref *Context;
            var     style   = ImNodesStyleWritable.Get();
            while (count > 0)
            {
                var ret = context.StyleModifierStack.PopBack(out var last);
                Debug.Assert(ret);
                if (float.IsNaN(last.Value.Y))
                    style[(ImNodesStyleSingle)last.Type] = last.Value.X;
                else
                    style[last.Type] = last.Value;
                --count;
            }
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void PushAttributeFlag(AttributeFlags flag)
        {
            ref var context = ref *Context;
            context.CurrentAttributeFlags |= flag;
            context.AttributeFlagStack.Add<TrivialTypeInformation<AttributeFlags>>(context.CurrentAttributeFlags);
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void PopAttributeFlag()
        {
            ref var context = ref *Context;
            Debug.Assert(context.AttributeFlagStack.Count > 1);
            context.AttributeFlagStack.PopBack(out _);
            context.CurrentAttributeFlags = context.AttributeFlagStack[^1];
        }
    }
}
