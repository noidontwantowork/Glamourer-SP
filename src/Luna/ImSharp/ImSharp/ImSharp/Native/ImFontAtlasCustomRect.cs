namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe struct ImFontAtlasCustomRect
        {
            public  ushort  Width;
            public  ushort  Height;
            public  ushort  X;
            public  ushort  Y;
            private uint    _data;
            public  float   GlyphAdvanceX;
            public  ImVec2  GlyphOffset;
            public  ImFont* Font;

            public uint Colored
            {
                get => _data & 3u;
                set => _data = (_data & ~3u) | (value & 3u);
            }

            public uint TextureIndex
            {
                get => (_data >> 2) & 0x1FFu;
                set => _data = (_data & ~(0x1FFu << 2)) | ((value & 0x1FFu) << 2);
            }

            public uint CodePoint
            {
                get => _data >> 11;
                set => _data = (_data & 0x7FF) | (value << 11);
            }
        }
    }
}
