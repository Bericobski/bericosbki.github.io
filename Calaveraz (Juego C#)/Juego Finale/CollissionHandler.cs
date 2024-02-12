using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Text;
using SFML.Audio;


namespace Juego_Finale
{  
    public class CollisionsHandler
    {

        private bool PassPlatOn = false; //Las plataformas no se pisan entre si el passfloor

        private readonly List<Entity> entities = new List<Entity>();

        private readonly List<Entity> charactersOffground = new List<Entity>();
        
        private float gravity = 0.5f;

        private void SolveCollision(Entity entityA, Entity entityB)
        {
           

            

            IsOffGrounded(entityA, entityB);

            TouchDamage(entityA, entityB);

            RonaldPowerUp(entityA, entityB);


        }

        

        public void AddEntity(Entity entity)
        {
            if (!entities.Contains(entity))
                entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            if (entities.Contains(entity))
                entities.Remove(entity);
        }

        public void IsOffGrounded (Entity entityA, Entity entityB) //Chequeo si la entidad esta en el suelo, hago una caida, lo hago 2 veces por el tipo de recorrido que hago, si A es piso y B personaje, mi recorrido no vuelve a juntarlos
        {
            if (entityA.IsCharacter && entityB.IsStatic && charactersOffground.Contains(entityA))
            {
                charactersOffground.Remove(entityA);
                entityA.IsFalling = false;
                if(entityA is Jugador)
                    PassPlatOn = false;
            }
            else if (entityB.IsCharacter && entityA.IsStatic && charactersOffground.Contains(entityB))
            {
                charactersOffground.Remove(entityB);
                entityB.IsFalling = false;
                if (entityB is Jugador)
                    PassPlatOn = false;
            }
        }

        public void SideCollisionPlat(Entity entityA, Entity entityB) //Chequeo si la entidad esta en el suelo, hago una caida, lo hago 2 veces por el tipo de recorrido que hago, si A es piso y B personaje, mi recorrido no vuelve a juntarlos
        {
            //IsFalling Esta tocando algun estatico
            //PassFloor Pasar atravez de lo estatico
            //PlasFloorPlat Indica a jugador que esta teniendo evento platlateral
            //PlasPlatOn Indica a las otras plataformas que una es teniendo el evento de platlateral


            //Tratar de hacer 2 hitbox de plat, laterales internas, y una interna grande.
            if(entityA is Jugador && entityB is Plat)
            {         
                if (entityA.sprite.GetGlobalBounds().Intersects((entityB as Plat).LeftLimit.sprite.GetGlobalBounds()) && entityA.IsFalling){
                    entityA.PassFloor = true;
                    (entityA as Jugador).PassFloorPlat = true;
                    PassPlatOn = true;
                }
                else if (entityA.sprite.GetGlobalBounds().Intersects((entityB as Plat).RightLimit.sprite.GetGlobalBounds()) && entityA.IsFalling)
                {
                    entityA.PassFloor = true;
                    (entityA as Jugador).PassFloorPlat = true; //PassFloorPat le indica a jugador que esta entrando en el evento PassFloorPat
                    PassPlatOn = true;
                }
                else if (entityA.IsFalling && !PassPlatOn) //Las otras plataformas que no son tocadas por el jugador, desactivaran el pasaje, udo PassPlatOn
                    (entityA as Jugador).PassFloorPlat = false;

                
                else if (!entityA.sprite.GetGlobalBounds().Intersects((entityB as Plat).RightLimit.sprite.GetGlobalBounds()) && !entityA.sprite.GetGlobalBounds().Intersects((entityB as Plat).LeftLimit.sprite.GetGlobalBounds()) && entityA.IsFalling && PassPlatOn)
                {
                    PassPlatOn = false; //Este es un caso especial, el jugador esta yendose de la plat, el sistema piensa que esta entrando al evento especial de traspar plat, pero se esta yendo. Este chequeo se asegura que el jugador no esta entrando en esta excepcion
                }                                
                    
            }

            if (entityA is Esqueleto && entityB is Plat)
            {                  
                if (entityA.sprite.GetGlobalBounds().Intersects((entityB as Plat).LeftLimit.sprite.GetGlobalBounds()))
                {
                    (entityA as Esqueleto).Side = "Right";
                    (entityA as Esqueleto).state = Esqueleto.States.Walk;

                }
                else if (entityA.sprite.GetGlobalBounds().Intersects((entityB as Plat).RightLimit.sprite.GetGlobalBounds()))
                {
                    (entityA as Esqueleto).Side = "Left";
                    (entityA as Esqueleto).state = Esqueleto.States.Walk;
                }
                
            }
            
            

            
        }

        public void SideMobVision(Entity entityA, Entity entityB)
        {
            if (entityA is Jugador && entityB is Esqueleto)
            {
                if (entityA.sprite.GetGlobalBounds().Intersects((entityB as Esqueleto).LeftVision.sprite.GetGlobalBounds()))
                {
                    (entityB as Esqueleto).state = Esqueleto.States.AttackLeft;

                }
                else if (entityA.sprite.GetGlobalBounds().Intersects((entityB as Esqueleto).RightVision.sprite.GetGlobalBounds()))
                {
                    (entityB as Esqueleto).state = Esqueleto.States.AttackRight;
                }

            }
        }

        public void RonaldPowerUp(Entity entityA, Entity entityB)
        {
            if (entityA is Jugador && entityB is Support)
            {
                if ((entityA as Jugador).hitboxhp.sprite.GetGlobalBounds().Intersects((entityB as Support).sprite.GetGlobalBounds()) && (entityB as Support).state == Support.States.Powerup)
                {
                    (entityB as Support).state = Support.States.Magic;
                    (entityA as Jugador).GetHeal(20f);

                }       
            }
        }
        public void Fall(Entity character) //Funcion de caida
        {
            Vector2f fall = new Vector2f(0f, gravity);
            character.Translate(fall);
        }

        public void TouchDamage(Entity entityA, Entity entityB)
        {
            if(entityA is Jugador && entityB is Skull)
            {
                if(entityB.sprite.GetGlobalBounds().Intersects((entityA as Jugador).hitboxhp.sprite.GetGlobalBounds()))
                    (entityA as Jugador).GetHit((entityB as Skull).GetDm()); 
            }
        }
        
        public void Update()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].IsCharacter)
                {
                    charactersOffground.Add(entities[i]);
                    entities[i].IsFalling = true;
                }
                    
                for (int j = i + 1; j < entities.Count; j++)
                {
                    
                    if (entities[i].IsCollidingWith(entities[j]))
                        SolveCollision(entities[i], entities[j]);

                    SideCollisionPlat(entities[i], entities[j]);
                    SideMobVision(entities[i], entities[j]);

                    //Cheque de esqueletos
                }

                
                
                   


                if (charactersOffground.Contains(entities[i]) || entities[i].PassFloor) //Los personajes que esten en la lista estan en el aire, caeran
                    Fall(entities[i]); //Caeran las entidades

            }
            charactersOffground.Clear();
        }
    }
}