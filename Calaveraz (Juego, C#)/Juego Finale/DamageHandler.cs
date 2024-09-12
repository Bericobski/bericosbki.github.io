using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using SFML.System;
using SFML.Window;


namespace Juego_Finale
{
    public class DamageHandler
    {
        private readonly List<Entity> mobs = new List<Entity>();           

        private Jugador jugador;

        
            
        public void AssignJugador(Jugador jugador) //Puse public y se soluciono, por que?
        {
            this.jugador = jugador;
        }

        public void AddMob(Entity entity)
        {
            if (!mobs.Contains(entity))
                mobs.Add(entity);
        }

        public void RemoveMob(Entity entity)
        {
            if (mobs.Contains(entity))
                mobs.Remove(entity);
        }
            

        public void IsHit(Jugador entityA, Entity entityB) //No lo pude generlaizar
        {
            if (entityB is Esqueleto)
            {
                if (entityA.hitbox.sprite.GetGlobalBounds().Intersects(entityB.Sprite.GetGlobalBounds()))
                {
                    (entityB as Esqueleto).GetHit(entityA.damage);
                    if ((entityB as Esqueleto).GetHp() <= 0)
                        mobs.Remove(entityB);
                }
            }
            else if(entityB is Skull)
            {
                if (entityA.hitbox.sprite.GetGlobalBounds().Intersects(entityB.Sprite.GetGlobalBounds()))
                {
                    (entityB as Skull).GetHit(entityA.damage);
                    if ((entityB as Skull).GetHp() <= 0)
                        mobs.Remove(entityB);
                }
            }
        }

        public void IsHitLong(Jugador entityA, Entity entityB) //No lo pude generlaizar
        {
            if (entityB is Esqueleto)
            {
                if (entityA.longhitbox.sprite.GetGlobalBounds().Intersects(entityB.Sprite.GetGlobalBounds()))
                {
                    (entityB as Esqueleto).GetHit(entityA.damage);
                    if ((entityB as Esqueleto).GetHp() <= 0)
                        mobs.Remove(entityB);
                }
            }
            else if (entityB is Skull)
            {
                if (entityA.longhitbox.sprite.GetGlobalBounds().Intersects(entityB.Sprite.GetGlobalBounds()))
                {
                    (entityB as Skull).GetHit(entityA.damage);
                    if ((entityB as Skull).GetHp() <= 0)
                        mobs.Remove(entityB);
                }
            }
        }

        public void MobAttack(Jugador entityA, Entity entityB) //No lo pude generlaizar
        {      
            if(entityB is Esqueleto)
            {
                if ((entityB as Esqueleto).Attack.sprite.GetGlobalBounds().Intersects(entityA.Sprite.GetGlobalBounds()) && (entityB as Esqueleto).GetCurrentFrame() == 5 && !(entityB as Esqueleto).attackcontact)
                {
                    if ((entityB as Esqueleto).GetCurrentFrame() == 5 && !(entityB as Esqueleto).attackcontact)
                    {
                        entityA.GetHit((entityB as Esqueleto).GetDm());
                        (entityB as Esqueleto).attackcontact = true;
                    }

                }
            }
                                   
        }

        


        public void Update()
        {
            for (int i = 0; i < mobs.Count; i++)
            {

                MobAttack(jugador, mobs[i]);

                //Chequeo cuando jugador entra en area de esqueleto

                //Chequeo cuando oprimo letra E
                if (Keyboard.IsKeyPressed(Keyboard.Key.E))
                {
                    IsHit(jugador, mobs[i]);                 
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                {
                    IsHitLong(jugador, mobs[i]);
                }


            }
                
        }
    }
}

