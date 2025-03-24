using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    internal interface Movement
    {
        void MoveStrategy(Entity entity);
        void Move();
    }

    internal class DownMovement : Movement
    {
        public void Move()
        {
            throw new NotImplementedException();
        }

        public void MoveStrategy(Entity entity)
        {
            entity.Position = new Vector2(entity.Position.X, entity.Position.Y+entity.Speed);
        }
    }

    internal class UpMovement : Movement
    {
        public void Move()
        {
            throw new NotImplementedException();
        }

        public void MoveStrategy(Entity entity)
        {
            entity.Position = new Vector2(entity.Position.X, entity.Position.Y - entity.Speed);
        }
    }

    internal class HorizonalMovement : Movement
    {

        private float _direction = 1;

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void MoveStrategy(Entity entity)
        {
            entity.Position = new Vector2(
                entity.Position.X + entity.Speed * _direction,
                entity.Position.Y
            );
            if (entity.Position.X < 0)
            {
                entity.Position = new Vector2(0, entity.Position.Y);
                _direction = 1; 
            }

            else if (entity.Position.X >= 420)
            {
                entity.Position = new Vector2(420, entity.Position.Y);
                _direction = -1; 
            }
        }
    }


    internal class CircleMovement : Movement
    {
        public void Move()
        {
            throw new NotImplementedException();
        }

        public void MoveStrategy(Entity entity)
        {
            throw new NotImplementedException();
        }
    }


}
