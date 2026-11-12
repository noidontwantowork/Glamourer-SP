namespace ImSharp;

public static class VectorExtensions
{
    extension(Vector2 vector)
    {
        /// <summary> Return a new vector with both values in <paramref name="vector"/> rounded up. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 Ceiling()
            => new(MathF.Ceiling(vector.X), MathF.Ceiling(vector.Y));

        /// <summary> Return a new vector with both values in <paramref name="vector"/> rounded down. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 Floor()
            => new(MathF.Floor(vector.X), MathF.Floor(vector.Y));

        /// <summary> Return a new vector with both values in <paramref name="vector"/> rounded. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 Round()
            => new(MathF.Round(vector.X), MathF.Round(vector.Y));

        /// <summary> Return a new vector with the X-value incremented by <paramref name="x1"/>. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 AddX(float x1)
            => vector with { X = vector.X + x1 };

        /// <summary> Return a new vector with the Y-value incremented by <paramref name="y"/>. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 AddY(float y)
            => vector with { Y = vector.Y + y };
    }
}
