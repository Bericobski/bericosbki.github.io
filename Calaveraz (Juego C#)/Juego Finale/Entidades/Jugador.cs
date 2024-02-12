using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using System.Collections.Generic;

namespace Juego_Finale
{
    
    public class Jugador : AnimatedEntity
    {
        private float speed;
        private float jumpspeed;
        private float airtime = 0.2f;
        private float currentairtime = 0f;
        private int maxjumps = 2;
        public int Score = 0;

        private SoundEffect gethit;
        private SoundEffect getheal;

        public bool PassFloorPlat { get; set; } //Debe traspasar el piso?

        public bool IsAlive = true;
        public bool IsRolling = false;

        public event Action OnHpChange;

        public Hitbox hitbox;
        public Hitbox longhitbox;

        public Hitbox hitboxhp;
        public float damage = 10f; //Podia ponerlo en private y en set?
        public float hp = 100f;

        private const string IdleAnimName = "Idle";

        private const string LeftAnimName = "Left";
        private const string RightAnimName = "Right";

        private const string WalkAnimName = "Walk";

        private const string JumpAnimName = "Jump";

        private const string FallAnimName = "Fall";

        private const string RollAnimName = "Roll";

        private const string DeathAnimName = "Death";

        private const string AttackAnimName = "Attack";

        private const string LongAttackAnimName = "LongAttack";

        private string Side = "Right";

        
        public Jugador(Vector2f position, Vector2i size, float rotation, float speed, float jumpspeed, string imagePath) : base (position, size, rotation, imagePath) //Llamamos al constructor de la clase base con base
        {
            gethit = new SoundEffect("Extras/Sound/Taking Damage.wav");
            getheal = new SoundEffect("Extras/Sound/Healing.wav");

            //Sprite.Scale = new Vector2f(-1f, 1f); //Asi, cambio la escala, y lo roto
            this.speed = speed;
            this.jumpspeed = jumpspeed;
            Sprite.Scale = new Vector2f(2f, 2f);
            Vector2i hitboxSize = new Vector2i(60, 140);

            Vector2i hitboxSize3 = new Vector2i(220, 60);

            Vector2i hitboxSize2 = new Vector2i(60, 80);

            hitbox = new Hitbox(position, rotation, hitboxSize);

            longhitbox = new Hitbox(position, rotation, hitboxSize3);

            hitboxhp = new Hitbox(position, rotation, hitboxSize2);

            AnimationData idleRightAnimation = new AnimationData()
            {
                frameRate = 6f,
                rowIndex = 0,
                columnsCount = 8,
                loop = false
            };

            AnimationData idleLeftAnimation = new AnimationData()
            {
                frameRate = 6f,
                rowIndex = 1,
                columnsCount = 8, 
                loop = false
            };

            AnimationData walkRightAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 2,
                columnsCount = 8,
                loop = false
            };

            AnimationData walkLeftAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 3,
                columnsCount = 8,
                loop = false
            };

            AnimationData attackRightAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 4,
                columnsCount = 9,
                loop = true
            };

            AnimationData attackLeftAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 5,
                columnsCount = 9,
                loop = true
            };

            AnimationData jumpRightAnimation = new AnimationData()
            {
                frameRate = 12f,
                rowIndex = 6,
                columnsCount = 4,
                loop = false
            };

            AnimationData jumpLeftAnimation = new AnimationData()
            {
                frameRate = 2f,
                rowIndex = 7,
                columnsCount = 4,
                loop = false
            };

            AnimationData fallRigthAnimation = new AnimationData()
            {
                frameRate = 2f,
                rowIndex = 8,
                columnsCount = 3,
                loop = false
            };

            AnimationData fallLeftAnimation = new AnimationData()
            {
                frameRate = 2f,
                rowIndex = 9,
                columnsCount = 3,
                loop = false
            };

            AnimationData DeathRightAnimation = new AnimationData()
            {
                frameRate = 2f,
                rowIndex = 10,
                columnsCount = 4,
                loop = true
            };

            AnimationData DeathLeftAnimation = new AnimationData()
            {
                frameRate = 2f,
                rowIndex = 11,
                columnsCount = 4,
                loop = true
            };

            AnimationData rollRightAnimation = new AnimationData()
            {
                frameRate = 6f,
                rowIndex = 12,
                columnsCount = 3,
                loop = false
            };

            AnimationData rollLeftAnimation = new AnimationData()
            {
                frameRate = 6f,
                rowIndex = 13,
                columnsCount = 3,
                loop = false
            };

            AnimationData longAttackLeftAnimation = new AnimationData()
            {
                frameRate = 8f,
                rowIndex = 15,
                columnsCount = 4,
                loop = true
            };

            AnimationData longAttackRightAnimation = new AnimationData()
            {
                frameRate = 8f,
                rowIndex = 14,
                columnsCount = 4,
                loop = true
            };

            AddAnimation(IdleAnimName + RightAnimName, idleRightAnimation);
            AddAnimation(IdleAnimName + LeftAnimName, idleLeftAnimation);

            AddAnimation(WalkAnimName + RightAnimName, walkRightAnimation);
            AddAnimation(WalkAnimName + LeftAnimName, walkLeftAnimation);

            AddAnimation(AttackAnimName + RightAnimName, attackRightAnimation);
            AddAnimation(AttackAnimName + LeftAnimName, attackLeftAnimation);

            AddAnimation(JumpAnimName + RightAnimName, jumpRightAnimation);
            AddAnimation(JumpAnimName + LeftAnimName, jumpLeftAnimation);

            AddAnimation(FallAnimName + RightAnimName, fallRigthAnimation);
            AddAnimation(FallAnimName + LeftAnimName, fallLeftAnimation);

            AddAnimation(DeathAnimName + RightAnimName, DeathRightAnimation);
            AddAnimation(DeathAnimName + LeftAnimName, DeathLeftAnimation);

            AddAnimation(RollAnimName + RightAnimName, rollRightAnimation);
            AddAnimation(RollAnimName + LeftAnimName, rollLeftAnimation);

            AddAnimation(LongAttackAnimName + RightAnimName, longAttackRightAnimation);
            AddAnimation(LongAttackAnimName + LeftAnimName, longAttackLeftAnimation);

            SetCurrentAnimation(IdleAnimName + RightAnimName);

            Side = RightAnimName;

            this.PassFloorPlat = false;
        }

        public void GetHit(float damage)
        {
            if (gethit.Status != SoundStatus.Playing)
            {
                gethit.Play();
                hp -= damage;
                OnHpChange?.Invoke();
            }
            
        }

        public void GetHeal(float heal)
        {
            if (getheal.Status != SoundStatus.Playing)
            {
                getheal.Play();
                hp += heal;
                OnHpChange?.Invoke();
            }
                                       
        }

        public float GetHp()
        {
            return hp;
        }

        public float GetXPos()
        {
            return Position.X;
        }

        public void GoUp(float deltatime)
        {
            if (currentairtime < airtime)
            {

                this.IsCharacter = false;
                SetCurrentAnimation(JumpAnimName + Side);
                //Position = new Vector2f(Position.X, Position.Y - jumpspeed * deltatime);

                //Moverse en el aire izq o der
                if (Keyboard.IsKeyPressed(Keyboard.Key.Right) || Keyboard.IsKeyPressed(Keyboard.Key.D))
                {
                    Position = new Vector2f(Position.X + speed * deltatime, Position.Y - jumpspeed * deltatime);
                    Side = RightAnimName;
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Left) || Keyboard.IsKeyPressed(Keyboard.Key.A))
                {
                    Position = new Vector2f(Position.X - speed * deltatime, Position.Y - jumpspeed * deltatime);
                    Side = LeftAnimName;
                }
                else
                    Position = new Vector2f(Position.X, Position.Y - jumpspeed * deltatime);

                currentairtime += deltatime;
            }
            else
            {
                this.PassFloor = true;
                this.IsCharacter = true;
                currentairtime = 0;
            }
        }

        // En update, podemos decir, que cuando salte, pase X cosa
        public override void Update(float deltatime)
        {
            
            

            if (GetHp() <= 0)
                SetCurrentAnimation(DeathAnimName + Side);

            if (!IsFalling && !PassFloor && IsCharacter && !PassFloorPlat)
                maxjumps = 2;

            

            base.Update(deltatime); //Ejecuta update de la clase base, AnimationEntity
            if (base.GetLoop()) //Poner animaciones hasta que terminen, ejemplo ataque
            {
                if (base.GetEndLoop())
                {
                    if (GetHp() <= 0)
                        IsAlive = false;
                    
                    SetCurrentAnimation(IdleAnimName + Side);
                }
            }
            else if (this.IsFalling /*estoy NO tocando piso*/ && currentairtime == 0 && this.PassFloor && !PassFloorPlat) //Caida al aire libre, no traspaso mas
            {                                               
                this.PassFloor = false;  //Para caer IsFalling, soy un personaje          
                                         //Problema, al caer, y colisionar con las hitbox de plataforma, jugador no sabe que esta por entrar a la platafora, entonces vuelve a apagar PassFloor, lo que hace que despues de entrar en la plataforma, no caiga
            }
            else if (0 < currentairtime)
            {
                GoUp(deltatime);                                                  
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && maxjumps > 0)
            {
                currentairtime = 0;
                maxjumps -= 1;
                GoUp(deltatime);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Left) || Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                if (this.IsFalling || this.PassFloor)
                    SetCurrentAnimation(FallAnimName + LeftAnimName);
                else
                    SetCurrentAnimation(WalkAnimName + LeftAnimName);
                Position = new Vector2f(Position.X - speed * deltatime, Position.Y);                
                Side = LeftAnimName;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Right) || Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                if (this.IsFalling || this.PassFloor)
                    SetCurrentAnimation(FallAnimName + RightAnimName);
                else
                    SetCurrentAnimation(WalkAnimName + RightAnimName);
                Position = new Vector2f(Position.X + speed * deltatime, Position.Y);               
                Side = RightAnimName;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.E))
            {
                SetCurrentAnimation(AttackAnimName + Side);
                if(Side == RightAnimName)
                    hitbox.sprite.Position = new Vector2f(Position.X + 70, Position.Y); 
                else
                    hitbox.sprite.Position = new Vector2f(Position.X - 10, Position.Y);                
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                SetCurrentAnimation(LongAttackAnimName + Side);
                if (Side == RightAnimName)
                    longhitbox.sprite.Position = new Vector2f(Position.X + 70, Position.Y);
                else
                    longhitbox.sprite.Position = new Vector2f(Position.X - 10, Position.Y);
            }         
            else
            {
               if(this.IsFalling || this.PassFloor)
                    SetCurrentAnimation(FallAnimName + Side);
               else
                    SetCurrentAnimation(IdleAnimName + Side);
            }

            

            Position = new Vector2f(Math.Clamp(Position.X, -35f, 20000f), Math.Clamp(Position.Y, -30f, 9600f));

            hitboxhp.sprite.Position = new Vector2f(Position.X + 28, Position.Y + 28); ;
        }



    }
}
