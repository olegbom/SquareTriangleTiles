using System;
using System.Numerics;
using Raylib_CsLo;

namespace SquareTriangleTiles
{
    public class SquareTile: Tile
    {
        public override Tile[] N { get; } = new Tile[4];
        private Vector2[] _p = new Vector2[5]; 
        
        
        public  override void Draw()
        {
            if (IsChanged)
            {
                var c = MathF.Cos(Rotate);
                var s = MathF.Sin(Rotate);
                _p[0] = _p[4] = Position + new Vector2(c - s, s + c) * SideSize / 2;
                _p[1] = Position + new Vector2(-c - s, -s + c) * SideSize / 2;
                _p[2] = Position + new Vector2(-c + s, -s - c) * SideSize / 2;
                _p[3] = Position + new Vector2(c + s, s - c) * SideSize / 2;
                _p[0] = Position + new Vector2(c - s, s + c) * SideSize / 2 * 1.3f;
            }

            unsafe
            {
                
                fixed (Vector2* p = &_p[0])
                {
                    Raylib.DrawLineStrip(p, 5, Color);
                }
            }
        }

        public override Tile[] Substitution()
        {
            float side = SideSize / (1 + sqrt3);
            var c = MathF.Cos(Rotate);
            var s = MathF.Sin(Rotate);
            const int numberOfSubs = 12;
            Tile[] result = new Tile[numberOfSubs];
            (var c1, var s1) = Helper.CosSin(MathF.PI * 5 / 6);
            result[0] = new SquareTile()
            {
                Rotate = Rotate + MathF.PI/3,
                Position = Position + new Vector2(side*(0.5f + sqrt3/2),0).Rotate(c,s).Rotate(c1, s1),
            };
            result[1] = new SquareTile()
            {
                Rotate = Rotate + MathF.PI / 3,
                Position = Position + new Vector2(side * (0.5f + sqrt3 / 2), 0).Rotate(c, s).Rotate(-c1, -s1),
            };
            result[4] = new TriangleTile()
            {
                Rotate = Rotate - MathF.PI / 6,
                Position = Position + new Vector2(side * (1 + sqrt3 / 3), 0).Rotate(c, s),
            };
            result[5] = new TriangleTile()
            {
                Rotate = Rotate + MathF.PI* 5 / 6,
                Position = Position + new Vector2( -side * (1 + sqrt3 / 3), 0).Rotate(c, s),
            };
            result[8] = new TriangleTile()
            {
                Rotate = Rotate + MathF.PI * 2/3,
                Position = Position + new Vector2(side * (sqrt3 / 3), 0).Rotate(c, s).Rotate(c1, s1),
            };
            result[9] = new TriangleTile()
            {
                Rotate = Rotate - MathF.PI / 3,
                Position = Position + new Vector2(side * (sqrt3 / 3), 0).Rotate(c, s).Rotate(-c1, -s1),
            };


            (c1, s1) = Helper.CosSin(MathF.PI / 6);
            result[2] = new SquareTile()
            {
                Rotate = Rotate + MathF.PI / 6,
                Position = Position + new Vector2(side * (0.5f + sqrt3 / 2), 0).Rotate(c, s).Rotate(c1, s1),
            };
            result[3] = new SquareTile()
            {
                Rotate = Rotate + MathF.PI / 6,
                Position = Position + new Vector2(side * (0.5f + sqrt3 / 2), 0).Rotate(c, s).Rotate(-c1, -s1),
            };
            result[6] = new TriangleTile()
            {
                Rotate = Rotate,
                Position = Position + new Vector2(side * (sqrt3 / 3), 0).Rotate(c, s).Rotate(c1, s1),
            };
            result[7] = new TriangleTile()
            {
                Rotate = Rotate + MathF.PI,
                Position = Position + new Vector2(side * (sqrt3 / 3), 0).Rotate(c, s).Rotate(-c1, -s1),
            };

            result[10] = new TriangleTile()
            {
                Rotate = Rotate + MathF.PI,
                Position = Position + new Vector2(0, side * (sqrt3 / 3)).Rotate(c, s),
            };
            result[11] = new TriangleTile()
            {
                Rotate = Rotate,
                Position = Position + new Vector2(0, -side * (sqrt3 / 3)).Rotate(c, s),
            };

            foreach (var tile in result)
            {
                if (tile != null) tile.SideSize = side;
            }

            return result;

        }
    }
}