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
                _p[0] = _p[3] = Position + new Vector2(0, sqrt3 / 3).Rotate(c, s) * SideSize;
                _p[1] = Position + new Vector2(0.5f, -sqrt3 / 6).Rotate(c, s) * SideSize;
                _p[2] = Position + new Vector2(-0.5f, -sqrt3 / 6).Rotate(c, s) * SideSize;
                //_p[0] = Position + new Vector2(0, sqrt3 / 3).Rotate(c, s) * SideSize * 1.3f;
                float hue = (Rotate * 180 / MathF.PI) % 360.0f;
                if (hue < 0) hue += 360.0f;
                Color = Raylib.ColorFromHSV(hue, 1, 1);

                if (Subclass == 0)
                {
                    Color = Raylib.GetColor(0xDD_C5_A2_FF);
                }
                else
                {
                    Color = Raylib.GetColor(0xB6_45_2C_FF);
                }
            }

            unsafe
            {
                fixed (Vector2* p = &_p[0])
                {
                
                    Raylib.DrawTriangleStrip(p, 3, Color);
                    // Raylib.DrawLineStrip(p, 4, Color);
                }
            }
        }

        public override Tile[] Substitution()
        {
            var side = SideSize / (1 + sqrt3);
            var c = MathF.Cos(Rotate);
            var s = MathF.Sin(Rotate);
            var numberOfSubs = Subclass == 0 ? 7 : 1;
            var result = new Tile[numberOfSubs];
            if (Subclass == 0)
            {
                var (c1, s1) = Helper.CosSin(MathF.PI * 5 / 6);
                result[0] = new SquareTile
                {
                    Rotate = Rotate - MathF.PI / 6,
                    Position = Position + new Vector2(side * (0.5f + sqrt3 / 6), 0).Rotate(c, s).Rotate(c1, s1)
                };
                result[1] = new SquareTile
                {
                    Rotate = Rotate + MathF.PI / 6,
                    Position = Position + new Vector2(side * (0.5f + sqrt3 / 6), 0).Rotate(c, s).Rotate(-c1, s1)
                };

                result[2] = new TriangleTile
                {
                    Subclass = 1,
                    Rotate = Rotate,
                    Position = Position
                };
                (c1, s1) = Helper.CosSin(MathF.PI * 13 / 12);
                result[3] = new TriangleTile
                {
                    Rotate = Rotate + MathF.PI / 6,
                    Position = Position + new Vector2(side * (0.5f + sqrt3 / 6) * sqrt2, 0).Rotate(c, s).Rotate(c1, s1)
                };
                result[4] = new TriangleTile
                {
                    Rotate = Rotate + MathF.PI / 2,
                    Position = Position + new Vector2(side * (0.5f + sqrt3 / 6) * sqrt2, 0).Rotate(c, s).Rotate(-c1, s1)
                };
                (c1, s1) = Helper.CosSin(MathF.PI * 5 / 12);
                result[5] = new TriangleTile
                {
                    Rotate = Rotate - MathF.PI / 2,
                    Position = Position + new Vector2(side * (0.5f + sqrt3 / 6) * sqrt2, 0).Rotate(c, s).Rotate(c1, s1)
                };
                result[6] = new TriangleTile
                {
                    Rotate = Rotate - MathF.PI * 5 / 6,
                    Position = Position + new Vector2(side * (0.5f + sqrt3 / 6) * sqrt2, 0).Rotate(c, s).Rotate(-c1, s1)
                };
              
            }
            else
            {
                result[0] = new TriangleTile
                {
                    Subclass = 1,
                    Rotate = Rotate,
                    Position = Position
                };
            }
            foreach (var tile in result)
                tile.SideSize = side;
            return result;
        }
    }
}