namespace ImSharp.ImNodes;

/// <summary> The internally used ID type for ImNodes links. </summary>
public static partial class ImNodes
{
    public static class Link
    {
        /// <summary> Returns true if the user started dragging a new link from any pin. </summary>
        /// <param name="startPin"> The pin the user started dragging from, if any. </param>
        /// <returns> True if the user is creating a new link. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool NewLinkStarted(out AttributeId startPin)
        {
            startPin = 0;
            return Api.IsLinkStarted((AttributeId*)Unsafe.AsPointer(ref startPin));
        }

        /// <summary> Returns true if the user dropped a dragged link before attaching it to a pin. </summary>
        /// <param name="startPin"> The pin the user started dragging from, if any. </param>
        /// <param name="includingDetachedLinks"> Whether to include existing links the user detached from one pin and then dropped, or only newly created links being dropped. </param>
        /// <returns> True if the user dropped a new or existing link, depending on <paramref name="includingDetachedLinks"/>. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool LinkDropped(out AttributeId startPin, bool includingDetachedLinks = true)
        {
            startPin = 0;
            return Api.IsLinkDropped((AttributeId*)Unsafe.AsPointer(ref startPin), includingDetachedLinks);
        }

        /// <inheritdoc cref="LinkCreated(out NodeId,out ImSharp.ImNodes.AttributeId,out NodeId,out ImSharp.ImNodes.AttributeId,out bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool LinkCreated(out AttributeId startPin, out AttributeId endPin, out bool snapped)
        {
            startPin = 0;
            endPin   = 0;
            snapped  = false;
            return Api.IsLinkCreated((AttributeId*)Unsafe.AsPointer(ref startPin),
                (AttributeId*)Unsafe.AsPointer(ref endPin), (ImBool*)Unsafe.AsPointer(ref snapped));
        }

        /// <summary> Returns true if the user finished creating a new link. </summary>
        /// <param name="startNode"> The node the <paramref name="startPin"/> belongs to, if any. </param>
        /// <param name="startPin"> The pin the user started dragging from, if any. </param>
        /// <param name="endNode"> The node the <paramref name="endPin"/> belongs to, if any. </param>
        /// <param name="endPin"> The pin the user ended dragging at, if any. </param>
        /// <param name="snapped"> Whether the link was created by snapping onto the pin or not. </param>
        /// <returns> True if a new link was created. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool LinkCreated(out NodeId startNode, out AttributeId startPin, out NodeId endNode, out AttributeId endPin,
            out bool snapped)
        {
            startPin  = 0;
            startNode = 0;
            endPin    = 0;
            endNode   = 0;
            snapped   = false;
            return Api.IsLinkCreated((NodeId*)Unsafe.AsPointer(ref startNode),
                (AttributeId*)Unsafe.AsPointer(ref startPin),
                (NodeId*)Unsafe.AsPointer(ref endNode), (AttributeId*)Unsafe.AsPointer(ref endPin), (ImBool*)Unsafe.AsPointer(ref snapped));
        }

        /// <summary> Returns true if the user detached an existing link from a pin. </summary>
        /// <param name="id"> The ID of the detached link, if any. </param>
        /// <returns> True if the user detached an existing link from a pin. </returns>
        public static unsafe bool LinkDestroyed(out LinkId id)
        {
            id = LinkId.Invalid;
            return Api.IsLinkDestroyed((LinkId*)Unsafe.AsPointer(ref id));
        }
    }
}
