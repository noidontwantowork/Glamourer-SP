namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public struct ImFontGlyph
        {
            private uint  _data;
            public  float AdvanceX;
            public  float X0;
            public  float Y0;
            public  float X1;
            public  float Y1;
            public  float U0;
            public  float V0;
            public  float U1;
            public  float V1;

            public bool Colored
            {
                get => (_data & 1u) is 1u;
                set => _data = value ? _data | 1u : _data & ~1u;
            }

            public bool Visible
            {
                get => (_data & 2u) is 2u;
                set => _data = value ? _data | 2u : _data & ~2u;
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
