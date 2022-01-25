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
            var st = new SquareTile() {SideSize = 10};
            var camera = new Camera2D() {zoom = 320,offset = new Vector2(Raylib.GetScreenWidth()/2.0f, Raylib.GetScreenHeight() / 2.0f) };
            // Main game loop
            while (!Raylib.WindowShouldClose()) // Detect window close button or ESC key
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib.GetColor(0x30_30_30_FF));
                Raylib.DrawFPS(10, 10);

                Raylib.BeginMode2D(camera);
                st.Draw();
                Raylib.EndMode2D();


                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
		}
    }

    public abstract class Tile
    {
        public abstract Tile[] N { get; }
        public Vector2 Position;
        public float SideSize = 1;
        public abstract void Draw();
    }

    public class SquareTile: Tile
    {
        public override Tile[] N { get; } = new Tile[4];
        private Vector2[] _p = new Vector2[5]; 
        
        
        public  override void Draw()
        {
            _p[0] = _p[4] = new Vector2(1, 1) * SideSize /2 ;
            _p[1] = new Vector2(1, 1) * SideSize /2 ;
            _p[2] = new Vector2(1, 1) * SideSize /2 ;
            _p[3] = new Vector2(1, 1) * SideSize /2 ;
           
            Raylib.DrawLineStrip(_p, 5, Raylib.WHITE);
            
            
        }
    }
}
