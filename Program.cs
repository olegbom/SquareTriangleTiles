using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Raylib_CsLo;

namespace SquareTriangleTiles
{
    class Program
    {
        static void Main(string[] args)
        {

            //Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);

            Raylib.InitWindow(1280, 720, "Hello, Raylib-CsLo");
            Raylib.SetTargetFPS(60);
         
            var tt = new SquareTile() { SideSize = 650, Subclass = 0};
            List<Tile> tiles = new List<Tile>();
            tiles.AddRange(tt.Substitution());

            for (int i = 0; i < 2; i++)
            {
                var nextGen = new List<Tile>();
                foreach (var tile in tiles)
                {
                    nextGen.AddRange(tile.Substitution());
                }

                tiles = nextGen;
            }

          /*  foreach (var tile in tiles)
            {
                tile.Color = tile.Subclass == 0 ? Raylib.GetColor(0x007ACCFF) : Raylib.GREEN;
            }*/

            var camera = new Camera2D() {zoom = 1,offset = new Vector2(Raylib.GetScreenWidth()/2.0f, Raylib.GetScreenHeight() / 2.0f) };
            // Main game loop
            while (!Raylib.WindowShouldClose()) // Detect window close button or ESC key
            {
                // st.Rotate += 0.01f;
              
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib.GetColor(0x30_30_30_FF));
                Raylib.DrawFPS(10, 10);

                Raylib.BeginMode2D(camera);
         
                foreach (var tile in tiles)
                {
                    tile.Draw();
                }

               
                Raylib.EndMode2D();
                
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
		}
    }
}
