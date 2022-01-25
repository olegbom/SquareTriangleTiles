using System;
using System.Diagnostics;
using System.Numerics;
using Raylib_CsLo;

namespace SquareTriangleTiles
{
    class Program
    {
        static void Main(string[] args)
        {
			Raylib.InitWindow(1280, 720, "Hello, Raylib-CsLo");
            Raylib.SetTargetFPS(60);
            var st = new SquareTile() {SideSize = 40, Rotate = };
            var camera = new Camera2D() {zoom = 1,offset = new Vector2(Raylib.GetScreenWidth()/2.0f, Raylib.GetScreenHeight() / 2.0f) };
            // Main game loop
            while (!Raylib.WindowShouldClose()) // Detect window close button or ESC key
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib.GetColor(0x30_30_30_FF));
                Raylib.DrawFPS(10, 10);

                Raylib.BeginMode2D(camera);
                st.Draw();
                Raylib.EndMode2D();

                Raylib_CsLo.RayMath.Vector2Rotate()
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
		}
    }

    public abstract class Tile
    {
        public abstract Tile[] N { get; }
        protected bool IsChanged = true;
        private Vector2 _position;

        public Vector2 Position
        {
            get => _position;
            set
            {   
                if( _position == value ) return;
                _position = value;
                IsChanged = true;
            }
        }

        private float _rotate;

        public float Rotate
        {
            get => _rotate;
            set
            {
                if (_rotate == value) return;
                _rotate = value;
                IsChanged = true;
            }
        }

        private float _sideSize = 1;

        public float SideSize
        {
            get => _sideSize;
            set
            {
                if (_sideSize == value) return;
                _sideSize = value;
                IsChanged = true;
            }
        }

        public abstract void Draw();
    }

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
                _p[0] = _p[4] = Position + RayMath.Vector2Rotate(new Vector2(1, 1), Rotate) * SideSize / 2;
                _p[1] = Position + new Vector2(-1, 1) * SideSize / 2;
                _p[2] = Position + new Vector2(-1, -1) * SideSize / 2;
                _p[3] = Position + new Vector2(1, -1) * SideSize / 2;
            }

            unsafe
            {
                
                fixed (Vector2* p = &_p[0])
                {
                    Raylib.DrawLineStrip(p, 5, Raylib.WHITE);
                }
            }
        }
    }
}
