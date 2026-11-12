namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static class Stb
        {
            public struct UndoRecord
            {
                public int Where;
                public int InsertLength;
                public int DeleteLength;
                public int CharacterStorage;
            }

            public struct UndoState
            {
                public UndoRecordArray    UndoRecords;
                public UndoCharacterArray UndoCharacters;
                public short              UndoPoint;
                public short              RedoPoint;
                public int                UndoCharacterPoint;
                public int                RedoCharacterPoint;

                [InlineArray(99)]
                public struct UndoRecordArray
                {
                    private UndoRecord _element;
                }

                [InlineArray(999)]
                public struct UndoCharacterArray
                {
                    private ImWchar _element;
                }
            }

            public struct TextEditState
            {
                public int       Cursor;
                public int       SelectStart;
                public int       SelectEnd;
                public byte      InsertMode;
                public int       RowCountPerPage;
                public ImBool    CursorAtEndOfLine;
                public ImBool    Initialized;
                public ImBool    HasPreferredX;
                public ImBool    SingleLine;
                public byte      Padding1;
                public byte      Padding2;
                public byte      Padding3;
                public float     PreferredX;
                public UndoState UndoState;
            }
        }
    }
}
