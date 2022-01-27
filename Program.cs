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
            var st = new SquareTile() {SideSize = 80};

            var tt = new TriangleTile() { SideSize = 400 };

            

            var camera = new Camera2D() {zoom = 1,offset = new Vector2(Raylib.GetScreenWidth()/2.0f, Raylib.GetScreenHeight() / 2.0f) };
            // Main game loop
            while (!Raylib.WindowShouldClose()) // Detect window close button or ESC key
            {
                // st.Rotate += 0.01f;
                var subs = tt.Substitution();
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib.GetColor(0x30_30_30_FF));
                Raylib.DrawFPS(10, 10);

                Raylib.BeginMode2D(camera);
                st.Draw();
                tt.Draw();
                foreach (var tile in subs)
                {
                    if (tile != null)
                    {
                        tile.Color = Raylib.YELLOW;
                        tile.Draw();
                    }
                    
                }
                Raylib.EndMode2D();

         
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
		}
    }
}
