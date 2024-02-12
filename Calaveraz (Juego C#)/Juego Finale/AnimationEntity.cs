using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace Juego_Finale
{
    public abstract class AnimatedEntity : Entity
    {
        private readonly Vector2i size;
        private Dictionary<string, AnimationData> animations = new Dictionary<string, AnimationData>();
        // ID del sprite con el que trabajo en el momento
        private Vector2i imagePosition; 

        //Nombre de animacion actual
        private string currentAnimationName;

        //Tiempo entre frames actual
        private float currentFrameTime;
        
        //
        private float animationTimer;

        private bool loopEnd;

        public AnimatedEntity(Vector2f position, Vector2i size, float rotation, string imagePath) : base(position, rotation, imagePath)
        {
            this.size = size; //Tamaño particular del sprite, osea del cuadro actual
        }

        protected void AddAnimation(string name, AnimationData animationData) //Añado animacion al dicc
        {
            if (animations.ContainsKey(name)) //Chequea que no se repita una animacion en el dicc
            {
                Console.WriteLine($"There already is animation named '{name}' added for this entity.");
                return;
            }

            animations.Add(name, animationData);
        }

        protected void RemoveAnimation(string name) //Remuevo animacion del dicc
        {
            if (!animations.ContainsKey(name)) //Chequeo que exista esa animacion en dicc
            {
                Console.WriteLine($"There is no animation named '{name}' added for this entity.");
                return;
            }

            animations.Remove(name);
        }

        protected void SetCurrentAnimation(string name) //Seteo la animacion actual
        {
            if (!animations.ContainsKey(name)) //Chequeo si existe la animacion en dicc
            {
                Console.WriteLine($"There is no animation named '{name}' added for this entity.");
                return;
            }

            if (name != currentAnimationName)
            {
                currentAnimationName = name; //Seteo la nueva animacion
                currentFrameTime = 1f / animations[currentAnimationName].frameRate; //Seteo el framerate de la animacion actual, 1/cant frames por seg

                imagePosition = new Vector2i(0, animations[currentAnimationName].rowIndex); //Seteo la animacion en su nueva fila de sprites
            }
        }

        public int GetCurrentFrame()
        {
            return imagePosition.X;
        }

        public bool GetLoop()
        {
            return animations[currentAnimationName].loop;
        }
        public bool GetEndLoop()
        {
            return loopEnd;
        }

        public override void Update(float deltaTime)
        {
            if (currentAnimationName == null)
                return;

            animationTimer += deltaTime; //Aumento lo que paso de un frame al otro

            if (animationTimer >= currentFrameTime)
            {
                animationTimer -= currentFrameTime;
                //Si la animacion no llego a la columna final, sigo, sino, vuelve a 0


                imagePosition.X = (imagePosition.X < animations[currentAnimationName].columnsCount - 1) ? imagePosition.X + 1 : 0;

                if (imagePosition.X == animations[currentAnimationName].columnsCount - 1)
                    loopEnd = true;
                else
                    loopEnd = false;

                //Cual es rectangulo de la textura
                Sprite.TextureRect = new IntRect()
                {
                    //Los 4 lados del rectangulo
                    Left = imagePosition.X * size.X, //Lado izq
                    Top = imagePosition.Y * size.Y, //Lado superior
                    Width = size.X, //Lado inferior
                    Height = size.Y //Lado der
                };
            }
        }
    }
}
