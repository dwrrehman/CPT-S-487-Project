using Microsoft.Xna.Framework;
using System;

namespace STG2.Strategy
{

    internal class ThreadPearlMovement : Movement
    {
        private readonly Vector2 _baseDir;
        private readonly float _angularVel;
        private float _radius = 0;

        public ThreadPearlMovement(Vector2 dir, float angularVel = 0.01f)
        {
            _baseDir = dir;
            _angularVel = angularVel;
        }

        public void MoveStrategy(Entity e)
        {

            _radius += e.Speed * 0.6f;

            float ang = _angularVel * _radius;
            float cos = MathF.Cos(ang);
            float sin = MathF.Sin(ang);
            Vector2 dir = new Vector2(_baseDir.X * cos - _baseDir.Y * sin, _baseDir.X * sin + _baseDir.Y * cos);

            e.Position += dir * e.Speed * 0.8f;
        }
        public void Move() { }
    }
}