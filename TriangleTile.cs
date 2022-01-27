using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_CsLo;

namespace SquareTriangleTiles
{
    public class TriangleTile : Tile
    {
        public override Tile[] N { get; } = new Tile[3];
        private Vector2[] _p = new Vector2[4];
        

        public override void Draw()
        {
            if (IsChanged)
            {
                var c = MathF.Cos(Rotate);
                var s = MathF.Sin(Rotate);
                _p[0] = _p[3] = Position + new Vector2(0, sqrt3 / 3).Rotate(c, s) * SideSize ;
                _p[1] = Position + new Vector2( 0.5f, -sqrt3 / 6).Rotate(c, s) * SideSize;
                _p[2] = Position + new Vector2(-0.5f, -sqrt3 / 6).Rotate(c, s) * SideSize;
                _p[0] = Position + new Vector2(0, sqrt3 / 3).Rotate(c, s) * SideSize * 1.3f;
            }

            unsafe
            {
                fixed (Vector2* p = &_p[0])
                {
                    Raylib.DrawLineStrip(p, 4, Color);
                }
            }
        }

        public override Tile[] Substitution()
        {
            float side = SideSize / (1 + sqrt3);
            var c = MathF.Cos(Rotate);
            var s = MathF.Sin(Rotate);
            const int numberOfSubs = 7;
            Tile[] result = new Tile[numberOfSubs];
            (var c1, var s1) = Helper.CosSin(MathF.PI * 5 / 6);
            result[0] = new SquareTile()
            {
                Rotate = Rotate - MathF.PI/6,
                Position = Position + new Vector2(side * ( 0.5f + sqrt3 / 6), 0).Rotate(c, s).Rotate(c1, s1),
            };
            result[1] = new SquareTile()
            {
                Rotate = Rotate + MathF.PI / 6,
                Position = Position + new Vector2(side * (0.5f + sqrt3 / 6), 0).Rotate(c, s).Rotate(-c1, s1),
            };

            result[2] = new TriangleTile()
            {
                Rotate = Rotate,
                Position = Position,
            };
            (c1, s1) = Helper.CosSin( MathF.PI * 13 / 12);
            result[3] = new TriangleTile()
            {
                Rotate = Rotate + MathF.PI / 6,
                Position = Position + new Vector2(side * (0.5f + sqrt3 / 6)*sqrt2, 0).Rotate(c, s).Rotate(c1, s1),
            };
            result[4] = new TriangleTile()
            {
                Rotate = Rotate + MathF.PI / 2,
                Position = Position + new Vector2(side * (0.5f + sqrt3 / 6) * sqrt2, 0).Rotate(c, s).Rotate(-c1, s1),
            };
            (c1, s1) = Helper.CosSin(MathF.PI * 5 / 12);
            result[5] = new TriangleTile()
            {
                Rotate = Rotate - MathF.PI / 2,
                Position = Position + new Vector2(side * (0.5f + sqrt3 / 6) * sqrt2, 0).Rotate(c, s).Rotate(c1, s1),
            };
            result[6] = new TriangleTile()
            {
                Rotate = Rotate - MathF.PI * 5 / 6,
                Position = Position + new Vector2(side * (0.5f + sqrt3 / 6) * sqrt2, 0).Rotate(c, s).Rotate(-c1, s1),
            };
            foreach (var tile in result)
            {
                if (tile != null) tile.SideSize = side;
            }
            return result;
        }
    }
}