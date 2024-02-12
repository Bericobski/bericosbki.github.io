using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Window;
using SFML.Audio;



namespace Juego_Finale
{
    public class Esqueleto : AnimatedEntity
    {
        private float speed;
        private float hp = 10f;
        private float dm = 5f;

        public int points = 5;

        private SoundEffect dies;

        private const string IdleAnimName = "Idle";
        private const string WalkAnimName = "Walk";

        private const string LeftAnimName = "Left";
        private const string RightAnimName = "Right";
        
        private const string AttackAnimName = "Attack";

        private const string SpawnAnimName = "Spawn";
        private const string DeadAnimName = "Death";

        public string Side = "Right";

        public bool attackcontact = false; //Cuando toca el suelo, mi animacion se sigue actualizando varias veces, yo quiero que se active 1 vez, y listo

        public enum States
        {
            Walk,            
            AttackLeft,
            AttackRight, 
            Idle
        }

        public States state = States.Idle;

        public Hitbox LeftVision;
        public Hitbox RightVision;

        public Hitbox Attack;
        

        public Esqueleto(Vector2f position, Vector2i size, float rotation, float speed, string imagePath) : base(position, size, rotation, imagePath) //Llamamos al constructor de la clase base con base
        {
            //Sprite.Scale = new Vector2f(-1f, 1f); //Asi, cambio la escala, y lo roto
            this.speed = speed;
            Sprite.Scale = new Vector2f(2f, 2f);

            Vector2i limitsize = new Vector2i(60, 60);
            
            Vector2i attlimitsize = new Vector2i(60, 20);



            LeftVision = new Hitbox(new Vector2f(Position.X - 50, Position.Y), 0, limitsize);
            RightVision = new Hitbox(new Vector2f(Position.X + 50, Position.Y), 0, limitsize);

            Attack = new Hitbox(new Vector2f(Position.X, Position.Y), 0, attlimitsize);
            

            AnimationData idleRightAnimation = new AnimationData()
            {
                frameRate = 6f,
                rowIndex = 0,
                columnsCount = 11,
                loop = false
            };           

            AnimationData DeathRightAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 2,
                columnsCount = 15,
                loop = true

            };

            AnimationData DeathLeftAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 3,
                columnsCount = 15,
                loop = true

            };

            AnimationData WalkRightAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 4,
                columnsCount = 13,
                loop = false

            };

            AnimationData   WalkLeftAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 5,
                columnsCount = 13,                
                loop = false
            };

            AnimationData SpawnAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 1,
                columnsCount = 15,
                loop = true
            };

            AnimationData AttackRightAnimation = new AnimationData()
            {
                frameRate = 10f,
                rowIndex = 6,
                columnsCount = 15,
                loop = true
            };

            AnimationData AttackLeftAnimation = new AnimationData()
            {
                frameRate = 10f,
                rowIndex = 7,
                columnsCount = 15,
                loop = true
            };



            AddAnimation(IdleAnimName + RightAnimName, idleRightAnimation);
            //AddAnimation(IdleAnimName + LeftAnimName, idleLeftAnimation);

            AddAnimation(DeadAnimName + RightAnimName, DeathRightAnimation);
            AddAnimation(DeadAnimName + LeftAnimName, DeathLeftAnimation);

            AddAnimation(WalkAnimName + LeftAnimName, WalkLeftAnimation);
            AddAnimation(WalkAnimName + RightAnimName, WalkRightAnimation);

            AddAnimation(AttackAnimName + RightAnimName, AttackRightAnimation);
            AddAnimation(AttackAnimName + LeftAnimName, AttackLeftAnimation);

            AddAnimation(SpawnAnimName, SpawnAnimation);

            SetCurrentAnimation(SpawnAnimName);

            dies = new SoundEffect("Extras/Sound/Breaking Sound Effect.wav");

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
            base.Update(deltatime); //Ejecuta update de la clase base, AnimationEntity

            if(hp <= 0)
            {
                if (dies.Status != SoundStatus.Playing)
                {
                    dies.Play();                  
                }
                SetCurrentAnimation(DeadAnimName + Side);
            }                            
                

            if (base.GetLoop()) //Poner animaciones hasta que terminen, ejemplo ataque
            {
                if (base.GetEndLoop()) //Termine el loop de animacion
                {
                    if (attackcontact)
                        attackcontact = false;

                    if(hp <= 0)
                        IsAlive = false;

                    SetCurrentAnimation(WalkAnimName + Side);
                    state = States.Walk;
                }
            }                     
            else 
            {
                switch (state){
                    case States.Walk:
                        SetCurrentAnimation(WalkAnimName + Side);
                        if (Side == LeftAnimName)
                        {
                            Position = new Vector2f(Position.X - speed * deltatime, Position.Y);
                            LeftVision.Position = new Vector2f(Position.X - 50 - speed * deltatime, Position.Y);
                            RightVision.Position = new Vector2f(Position.X + 50 - speed * deltatime, Position.Y);
                        }
                        else
                        {
                            Position = new Vector2f(Position.X + speed * deltatime, Position.Y);
                            LeftVision.Position = new Vector2f(Position.X - 50 + speed * deltatime, Position.Y);
                            RightVision.Position = new Vector2f(Position.X + 50 + speed * deltatime, Position.Y);
                        }                        
                        break;
                    
                    case States.AttackLeft:
                        Side = LeftAnimName;
                        SetCurrentAnimation(AttackAnimName + Side);
                        Attack.sprite.Position = new Vector2f(Position.X - 64 , Position.Y);
                        break;

                    case States.AttackRight:
                        Side = RightAnimName;
                        SetCurrentAnimation(AttackAnimName + Side);
                        Attack.sprite.Position = new Vector2f(Position.X + 64, Position.Y);
                        break;
                }
                

            }
            Position = new Vector2f(Math.Clamp(Position.X, -35f, 20000f), Math.Clamp(Position.Y, 0f, 445f));
        }
    }
}