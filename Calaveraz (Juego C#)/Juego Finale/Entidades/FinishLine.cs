using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Window;

namespace Juego_Finale
{

    class FinishLine : AnimatedEntity
    {

        private const string IdleAnimName = "Still";
        /*Hacer un pixel a la izq y derecha de las platafromas, que no supere sus superficies, 
        si el jugador las toca a ellas YYYYYY toca piso, para de caer. 
        si jugador toca a ella, sin tocar piso, sigue cayendo
        Si esqueleto toca pixel, camina hacia el otro lado.
        */


        public FinishLine(Vector2f position, Vector2i size, float rotation, string imagePath) : base(position, size, rotation, imagePath) //Llamamos al constructor de la clase base con base
        {
            //Sprite.Scale = new Vector2f(-1f, 1f); //Asi, cambio la escala, y lo roto

            Sprite.Scale = new Vector2f(4f, 4f);
           
            AnimationData idleAnimation = new AnimationData()
            {
                frameRate = 1f,
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