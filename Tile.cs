using System;
using System.Numerics;
using Raylib_CsLo;

namespace SquareTriangleTiles
{
    public abstract class Tile
    {
        public abstract Tile[] N { get; }
        protected bool IsChanged = true;
        private Vector2 _position;
        protected static readonly float sqrt3 = MathF.Sqrt(3);
        protected static readonly float sqrt2 = MathF.Sqrt(2);
        public Color Color { get; set; } = Raylib.WHITE;
        public int Subclass { get; init; } = 0;


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
        public abstract Tile[] Substitution();
    }
}