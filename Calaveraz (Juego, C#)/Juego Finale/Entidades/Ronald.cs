using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Window;

namespace Juego_Finale
{

    public class Support : AnimatedEntity
    {
        private float speed;
        private const string IdleAnimName = "Idle";


        private const string RightAnimName = "Right";

        private const string MagicAnimName = "Magic";

        private const string FoodAnimName = "Food";

        private string Side = "Right";

        

        public enum States
        {
            Powerup,
            Magic, 
            Idle
        }

        public States state;

        enum PowerUps
        {
            Heal,
            Kill,
            Roll
        }


        public Support(Vector2f position, Vector2i size, float rotation, float speed, string imagePath) : base(position, size, rotation, imagePath) //Llamamos al constructor de la clase base con base
        {
            //Sprite.Scale = new Vector2f(-1f, 1f); //Asi, cambio la escala, y lo roto
            this.speed = speed;
            Sprite.Scale = new Vector2f(3f, 3f);

            state = States.Powerup;

            AnimationData idleRightAnimation = new AnimationData()
            {
                frameRate = 6f,
                rowIndex = 0,
                columnsCount = 2,
                loop = false
            };

            AnimationData attackRightAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 2,
                columnsCount = 10,
                loop = true
            };

            AnimationData foodAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 3,
                columnsCount = 4,
                loop = false
            };

            AddAnimation(IdleAnimName + RightAnimName, idleRightAnimation);

            AddAnimation(MagicAnimName + RightAnimName, attackRightAnimation);

            AddAnimation(FoodAnimName, foodAnimation);

            SetCurrentAnimation(FoodAnimName);

            Side = RightAnimName;

            

        }

        /*
        public static PowerUps GetPowerup(){

        }
        */

        public float GetXPos()
        {
            return sprite.Position.X;
        }
        

        // En update, podemos decir, que cuando salte, pase X cosa
        public override void Update(float deltatime)
        {
            base.Update(deltatime); //Ejecuta update de la clase base, AnimationEntity
            
            if (base.GetLoop()) //Poner animaciones hasta que terminen, ejemplo ataque
            {
                if (base.GetEndLoop())
                {
                    SetCurrentAnimation(IdleAnimName + Side);
                    state = States.Idle;
                }
            }            
            else if (state == States.Magic)
            {
                SetCurrentAnimation(MagicAnimName + Side);
            }
            else if(state == States.Idle)
            {
                SetCurrentAnimation(IdleAnimName + Side);
            }
        }

    }
}
