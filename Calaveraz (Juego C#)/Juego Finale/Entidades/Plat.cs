

using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Window;

namespace Juego_Finale
{

    class Plat : AnimatedEntity
    {

        private const string IdleAnimName = "Still";        

        public Hitbox LeftLimit;
        public Hitbox RightLimit;
        public Plat(Vector2f position, Vector2i size, float rotation, string imagePath) : base(position, size, rotation, imagePath) //Llamamos al constructor de la clase base con base
        {
            //Sprite.Scale = new Vector2f(-1f, 1f); //Asi, cambio la escala, y lo roto

            Sprite.Scale = new Vector2f(2f, 2f);

            Vector2i limitsize = new Vector2i(3, 6);

            
            
            LeftLimit = new Hitbox(new Vector2f(Position.X - 3, Position.Y), 0, limitsize);
            RightLimit = new Hitbox(new Vector2f(Position.X + 160, Position.Y), 0, limitsize);
            


            AnimationData idleAnimation = new AnimationData()
            {
                frameRate = 6f,
                rowIndex = 0,
                columnsCount = 8,
                loop = false
            };

            AddAnimation(IdleAnimName, idleAnimation);
            SetCurrentAnimation(IdleAnimName);
        }


        // En update, podemos decir, que cuando salte, pase X cosa
        public override void Update(float deltatime)
        {

        }

    }
}
