
using SFML.System;
using SFML.Window;
using SFML.Graphics;


namespace Juego_Finale
{
    public class Boss : AnimatedEntity
    {
        private float speed;
        private float hp = 10f;
        

        private SoundEffect dies;

        private const string IdleAnimName = "Idle";
                
        private const string AttackAnimName = "Attack";

        

        

        

        


        public Boss(Vector2f position, Vector2i size, float rotation, float speed, string imagePath) : base(position, size, rotation, imagePath) //Llamamos al constructor de la clase base con base
        {
            //Sprite.Scale = new Vector2f(-1f, 1f); //Asi, cambio la escala, y lo roto
            this.speed = speed;
            Sprite.Scale = new Vector2f(1f, 1f);

            


            AnimationData idleAnimation = new AnimationData()
            {
                frameRate = 6f,
                rowIndex = 1,
                columnsCount = 6,
                loop = false
            };

            

            AnimationData AttackAnimation = new AnimationData()
            {
                frameRate = 9f,
                rowIndex = 0,
                columnsCount = 11,
                loop = true
            };

            


            AddAnimation(IdleAnimName, idleAnimation);
                       
            AddAnimation(AttackAnimName , AttackAnimation);
                        
            SetCurrentAnimation(IdleAnimName);

            dies = new SoundEffect("Extras/Sound/Breaking Sound Effect.wav");

           

        }

        public void GetHit(float damage)
        {
            hp -= damage;
        }

        public float GetHp()
        {
            return hp;
        }

        public void SetPosition(Vector2f positionnew)
        {
            Position = positionnew;
        }
        
        public void SetInteraction()
        {
            SetCurrentAnimation(AttackAnimName);
        }

        // En update, podemos decir, que cuando salte, pase X cosa
        public override void Update(float deltatime)
        {
            base.Update(deltatime); //Ejecuta update de la clase base, AnimationEntity

            


            if (base.GetLoop()) //Poner animaciones hasta que terminen, ejemplo ataque
            {
                if (base.GetEndLoop()) //Termine el loop de animacion
                {                   
                    SetCurrentAnimation(IdleAnimName);
                    
                }
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                SetCurrentAnimation(AttackAnimName);
            }
            else
            {
                SetCurrentAnimation(IdleAnimName);


            }


        }
    }
}