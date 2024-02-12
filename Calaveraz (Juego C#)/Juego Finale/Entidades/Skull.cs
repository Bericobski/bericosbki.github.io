using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Audio;

namespace Juego_Finale
{
    public class Skull : AnimatedEntity
    {
        
        private float hp = 10f;
        private float dm = 10f;

        public int points = 10;

        private double Y;

        private SoundEffect dies;

        private const string NormalAnimName = "Normal";

        private const string FireAnimName = "Fire";

        private const string DeathAnimName = "Death";

        private const string LeftAnimName = "Left";
        private const string RightAnimName = "Right";        

        public string Side = "Left";

        private float horizontalSpeed;        
        private float amplitude;
        private float frequency;
        private float b;
        private float originalVerticalPosition;
        private float time;
        private bool movingRight;

        
        private const float MaxDistanceX = 1000f;


        public Skull(Vector2f position, Vector2i size, float rotation, float speed, string imagePath, float horizontalSpeed, float amplitude, float frequency) : base(position, size, rotation, imagePath) //Llamamos al constructor de la clase base con base
        {
            dies = new SoundEffect("Extras/Sound/Skull Death.wav");

            this.horizontalSpeed = horizontalSpeed;
            this.amplitude = amplitude;
            this.frequency = frequency;
            b = 2f * MathF.PI * frequency;
            originalVerticalPosition = position.Y;
            movingRight = false;

            Sprite.Scale = new Vector2f(2f, 2f);

            AnimationData idleLeftAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 0,
                columnsCount = 4,
                loop = false
            };

            AnimationData DeathAnimation = new AnimationData()
            {
                frameRate = 8f,
                rowIndex = 1,
                columnsCount = 4,
                loop = true
            };

            AddAnimation(NormalAnimName + RightAnimName, idleLeftAnimation);
            //AddAnimation(IdleAnimName + LeftAnimName, idleLeftAnimation);           

            AddAnimation(DeathAnimName, DeathAnimation); 

            SetCurrentAnimation(NormalAnimName + RightAnimName);

            //dies = new SoundEffect("Extras/Sound/Breaking Sound Effect.wav");

            Side = LeftAnimName;
        }


        public void GetHit(float damage)
        {
            hp -= damage;
        }

        public float GetHp()
        {
            return hp;
        }

        public float GetDm()
        {
            return dm;
        }
             
        // En update, podemos decir, que cuando salte, pase X cosa
        public override void Update(float deltatime)
        {
            base.Update(deltatime);

            if(hp <= 0)
            {
                if (base.GetLoop()) //Poner animaciones hasta que terminen, ejemplo ataque
                {
                    if (base.GetEndLoop())
                    {
                        if (dies.Status != SoundStatus.Playing)                      
                            dies.Play();
                                                   
                        IsAlive = false;                       
                    }
                    return;
                }
                SetCurrentAnimation(DeathAnimName);
                return;
            }

            Vector2f newPosition = new Vector2f();

            float distanceX = horizontalSpeed * deltatime;

            newPosition.X = Position.X + ((movingRight) ? distanceX : -distanceX);
            newPosition.Y = amplitude * MathF.Sin(b * time) + originalVerticalPosition;

            time += deltatime;
            /*
            distanceTraveled += distanceX;

            if (distanceTraveled >= MaxDistanceX)
            {
                distanceTraveled = 0f;
                movingRight = !movingRight;
            }
            */

            Position = newPosition;
            //Position = new Vector2f(Math.Clamp(Position.X, -35f, 9600f), Math.Clamp(Position.Y, 0f, 460f));
        }
    }
}
