using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Window;


namespace Juego_Finale
{
    class Background : AnimatedEntity
    {
        private const string IdleAnimName = "Still";
        private List<Background> fondos = new List<Background>();
        public Background(Vector2f position, Vector2i size, float rotation, string imagePath, Vector2f scale) : base(position, size, rotation, imagePath) //Llamamos al constructor de la clase base con base
        {
            //Sprite.Scale = new Vector2f(-1f, 1f); //Asi, cambio la escala, y lo roto

            Sprite.Scale = scale;

            AnimationData idleAnimation = new AnimationData()
            {
                frameRate = 6f,
                rowIndex = 0,
                columnsCount = 1,
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
