namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public struct ImFontGlyphHotData
        {
            public  float AdvanceX;
            public  float OccupiedWidth;
            private uint  _data;

            public bool KerningPairUseBisect
            {
                get => (_data & 1u) == 1u;
                set => _data = value ? _data | 1u : _data & ~1u;
            }

            public uint KerningPairOffset
            {
                get => (_data >> 1) & 0x7FFFFu;
                set => _data = (_data & ~(0x7FFFFu << 1)) | ((value & 0x7FFFFu) << 1);
            }

            public uint KerningPairCount
            {
                get => _data >> 20;
                set => _data = (_data & 0xFFFFF) | (value << 20);
            }
        }
    }
}
