using Microsoft.Xna.Framework;

namespace STG2.Strategy
{

    internal class RadialOutMovement : Movement
    {
        private readonly Vector2 _dir;

        public RadialOutMovement(Vector2 dir) => _dir = dir;

        public void MoveStrategy(Entity e)
        {
            e.Position += _dir * e.Speed;
        }
        public void Move() { }
    }
}