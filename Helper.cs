using System;
using System.Numerics;

namespace SquareTriangleTiles
{
    public static class Helper
    {
        public static Vector2 Rotate(this Vector2 v, float cos, float sin)
        {
            return new Vector2(v.X * cos - v.Y * sin, v.X * sin + v.Y * cos);
        }

        public static (float, float) CosSin(float alpha) => (MathF.Cos(alpha), MathF.Sin(alpha));
    }
}