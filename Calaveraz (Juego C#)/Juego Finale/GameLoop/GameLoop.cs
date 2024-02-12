using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Juego_Finale
{
    class GameLoop 
    {
        private RenderWindow window;
        public bool isrunning;

        private Background fondo;

        public float MinX = 0;
        public int GroundHeight = 72;
        public float WallsHeight = 128;
        public int MaxX = 20000;

        private HUD hud;

        private Music theme;

        Random rand = new Random();
        
        public Jugador jugador;

        private Boss jefe;

        private List<Esqueleto> esqueleto = new List<Esqueleto>();
        private List<Skull> calaveras = new List<Skull>();

        private List<Plat> platforms = new List<Plat>();
        private float lastPlatX;

        private FinishLine finishline;
        private Piso floor;

        private Support ronald;
        
        private CollisionsHandler collisionhandler;
        private DamageHandler damagehandler;

        private View view; //Vista de window                     

        public float victoryX = 6500f;
        public float victoryScore = 100f;
       

        private Clock clock;
        private Clock clock2;
       

        public GameLoop (RenderWindow window)
        {
            this.window = window;
            collisionhandler = new CollisionsHandler();
            damagehandler = new DamageHandler();
            view = new View(window.GetView());
            
           
        }

        

        private void OnResize(object sender, SizeEventArgs sizeArgs)
        {
            Console.WriteLine(sizeArgs.Width + ", " + sizeArgs.Height);
        }

        //Actualizo las entidades del juego en esta seccion
        private void Update(float deltatime, Clock time2)
        {
            float halfWindowWidth = window.Size.X / 2f;
            float minViewPositionX = MinX + halfWindowWidth;
            float maxViewPositionX = MaxX - halfWindowWidth;
            float playerPositionX = jugador.Position.X + jugador.Sprite.TextureRect.Width / 2;
            float viewPositionX = Math.Clamp(playerPositionX, minViewPositionX, maxViewPositionX);
            view.Center = new Vector2f(viewPositionX, view.Center.Y);
            window.SetView(view);

            if (time2.ElapsedTime.AsSeconds() >= 2f)
            {
                CreatePlatform();
                CreateSkull();
                CreateMob();
                time2.Restart();
            }                        

            //Console.WriteLine("Tiempo entre frame: " + deltatime);
            foreach (var plat in platforms)
            {
                plat.Update(deltatime);
            }

            
            
            floor.Update(deltatime);
                       

            jugador.Update(deltatime);            

            for (int i = esqueleto.Count - 1; i >= 0; i--)
            {                                           
                esqueleto[i].Update(deltatime);
                if (!esqueleto[i].IsAlive)
                {
                    jugador.Score += esqueleto[i].points;
                    collisionhandler.RemoveEntity(esqueleto[i]);
                    damagehandler.RemoveMob(esqueleto[i]);
                    esqueleto.RemoveAt(i);                 
                }
                                           
            }

            for (int i = calaveras.Count - 1; i >= 0; i--)
            {
                calaveras[i].Update(deltatime);
                if (calaveras[i].Position.X <= view.Center.X - window.Size.X - 70f || !calaveras[i].IsAlive)
                { //No puedo modificar listas si las recorro en orden, debo empezar desde la ultima
                    if(!calaveras[i].IsAlive)
                        jugador.Score += calaveras[i].points;
                    collisionhandler.RemoveEntity(calaveras[i]);
                    damagehandler.RemoveMob(calaveras[i]);
                    calaveras.RemoveAt(i);
                    
                }
            }

            jefe.SetPosition(new Vector2f(view.Center.X + 300f, 10f));
            jefe.Update(deltatime);

            ronald.Update(deltatime);

            collisionhandler.Update();
            damagehandler.Update();
            hud.Update();
        }


        //Dibujo a las entidades, en update solo cambio los parametros o actualizo los estados
        private void Draw()
        {
            window.Clear();

            window.Draw(fondo.Sprite);

            window.Draw(finishline.Sprite);

            
            
            foreach (var plat in platforms)
            {
                window.Draw(plat.Sprite);               
            }

            
            
            window.Draw(floor.Sprite);
                       
            
            foreach (var mob in esqueleto)
            {
                window.Draw(mob.Sprite);

                //window.Draw(mob.Attack.Sprite);                           
            }

            window.Draw(ronald.Sprite);
            window.Draw(jugador.Sprite);
            //CreateFigures(); //LLamo a las figuras a dibujar

            foreach (var cal in calaveras)
            {
                window.Draw(cal.Sprite);
            }
            window.Draw(jefe.Sprite);

            hud.Draw();
            
            window.Display(); //Llamo a la pantalla
           
        }
       
        private void CreatePlatform()
        {
            Vector2i size = new Vector2i(3, 80); //Tamaño de personaje
            float mobrotation = 0f;
            string imagePath = "Extras/Plataforma.png";

            int Y = rand.Next(150, 300);

            float X = window.Size.X / 3 + view.Center.X + 200;
            
            if(X <= lastPlatX + 200)
            {
                return;
            }
            else
            {
                lastPlatX = X;
            }

            if (60 <= rand.Next(0, 100) && ronald.GetXPos() <= (view.Center.X - window.Size.X / 2 - 150))
            {
                Support(X, Y - 195);
            }
            
            Vector2f pposition = new Vector2f(X, Y);

            platforms.Add(new Plat(pposition, size, mobrotation, imagePath));

            int last = platforms.Count;

            platforms[last -1].IsCharacter = false;
            platforms[last -1].IsStatic = true;

            collisionhandler.AddEntity(platforms[last -1]);
        }
        private void Platform()
        {
            
            /*
            Vector2i size = new Vector2i(3, 80); //Tamaño de personaje
            float mobrotation = 0f;
            string imagePath = "Extras/Plataforma.png";

            Vector2f pposition = new Vector2f(window.Size.X / 3, window.Size.Y / 3 + 80);

            platforms.Add(new Plat(pposition, size, mobrotation, imagePath));

            Vector2f pposition2 = new Vector2f(window.Size.X / 3 + 600, window.Size.Y / 3 + 80);          
            */

            foreach (var plat in platforms)
            {
                plat.IsCharacter = false;
                plat.IsStatic = true;

                collisionhandler.AddEntity(plat);
            }


            Vector2i fsize = new Vector2i(15, 287); //Tamaño de personaje
            float frotation = 0f;
            string fimagePath = "Extras/Piso A.png";

            Vector2f floorposition = new Vector2f(0, 510);
            
            floor = new Piso(floorposition, fsize, frotation, fimagePath);
            floor.Sprite.TextureRect = new IntRect(0, 0, MaxX, 510);
            floor.Sprite.Texture.Repeated = true;
           
            
            floor.IsCharacter = false;
            floor.IsStatic = true;
                
            collisionhandler.AddEntity(floor);
            


        }
        private void Support(float X, float Y)
        {                      

            Vector2f playerposition = new Vector2f(X, Y);
            
            float ronaldspeed = 300f;
            Vector2i size = new Vector2i(64, 64); //Tamaño de personaje
            float ronaldrotation = 0f;
            string imagePath = "Extras/Ronald McDonald.png";
            ronald = new Support(playerposition, size, ronaldrotation, ronaldspeed, imagePath);

            ronald.IsCharacter = true;
            ronald.IsStatic = false;
            ronald.Mass = 9f;

            collisionhandler.AddEntity(ronald);
        }        
        private void CreateSkull()
        {
            jefe.SetInteraction();
            int Y = rand.Next(20, 350); 
            


            Vector2f mobposition3 = new Vector2f(window.Size.X/3 + view.Center.X + 200, Y);
            Vector2i size2 = new Vector2i(51, 55); //Tamaño de personaje
            float skullspeed = 50;
            string skullImagepath = "Extras/Skull.png";
            float horizontalspeed = 50f;
            float amplitude = 400f;
            float frequency = 0.1f;


            calaveras.Add(new Skull(mobposition3, size2, 0, skullspeed, skullImagepath, horizontalspeed, amplitude, frequency));

            foreach (var cal in calaveras)
            {
                cal.IsCharacter = false;
                cal.IsStatic = false;
                cal.Mass = 10f;
                cal.IsAlive = true;
                collisionhandler.AddEntity(cal);
                damagehandler.AddMob(cal);
            }
        }

        private void CreateMob()
        {                      
            float mobspeed = 50f;
            Vector2i size = new Vector2i(32, 32); //Tamaño de personaje
            float mobrotation = 0f;
            string imagePath = "Extras/Skeleton/Skeleton SPRITE.png";

            Vector2f mobposition = new Vector2f(window.Size.X / 3 + view.Center.X + 500, -200);
            

            esqueleto.Add(new Esqueleto(mobposition, size, mobrotation, mobspeed, imagePath));
            

            foreach (var mob in esqueleto)
            {
                mob.IsCharacter = true;
                mob.IsStatic = false;
                mob.Mass = 10f;
                mob.IsAlive = true;
                collisionhandler.AddEntity(mob);
                damagehandler.AddMob(mob);
            }

        }
        private void Mobs()
        {
            
            Vector2i sizeboss = new Vector2i(192, 192); //Tamaño de personaje            
            string bossimagePath = "Extras/Boss/Demon.png";

            Vector2f bossposition = new Vector2f(window.Size.X - 200, 20f);

            jefe = new Boss(bossposition,sizeboss, 0f, 0f, bossimagePath);
            jefe.SetPosition(new Vector2f(view.Center.X + 300f, 10f));

            /*
            float mobspeed = 50f;
            Vector2i size = new Vector2i(32, 32); //Tamaño de personaje
            float mobrotation = 0f;
            string imagePath = "Extras/Skeleton/Skeleton SPRITE.png";

            Vector2f mobposition = new Vector2f(window.Size.X / 3, window.Size.Y / 3);
            Vector2f mobposition2 = new Vector2f(window.Size.X * 0.6f, window.Size.Y / 3);

            esqueleto.Add(new Esqueleto(mobposition, size, mobrotation, mobspeed, imagePath));
            esqueleto.Add(new Esqueleto(mobposition2, size, mobrotation, mobspeed, imagePath));  
            

            foreach (var mob in esqueleto)
            {
                mob.IsCharacter = true;
                mob.IsStatic = false;
                mob.Mass = 10f;
                mob.IsAlive = true;
                collisionhandler.AddEntity(mob);
                damagehandler.AddMob(mob);
            }
            */
            
        }
        private void Background()
        {
            Vector2f fposition = new Vector2f(0, 0);           
            Vector2i fsize = new Vector2i(3, 80); //Tamaño de personaje
            float frotation = 0f;
            string fimagePath = "Extras/BG-sky.png";
            Vector2f fscale = new Vector2f(2.5f, 2.5f);

            fondo = new Background(fposition, fsize, frotation, fimagePath, fscale);
            fondo.Sprite.TextureRect = new IntRect(0, 0, MaxX, (int)window.Size.Y);
            fondo.Sprite.Texture.Repeated = true;

            Vector2f endposition = new Vector2f(victoryX - 500, -20);
            Vector2i endsize = new Vector2i(276, 133); //Tamaño de personaje
            
            string endimagePath = "Extras/Finish Line.png";

            finishline = new FinishLine(endposition, endsize, frotation, endimagePath);

        }        
        private void Start()
        {
            isrunning = true;
            window.Closed += OnCloseWindow; //El boton de cierre, se suscribe al cierre del programa
            window.Resized += OnResize;

            Background();

            //window.MouseMoved += OnMouseMove; Mover mouse, mover personaje

            Vector2f playerposition = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
            float jugadorspeed = 400f;
            float jugadorjumpspeed = 1000f;
            Vector2i size = new Vector2i(64, 64); //Tamaño de personaje
            float jugadorrotation = 0f;
            string imagePath = "Extras/Moves Knight Sprite.png";

            jugador = new Jugador(playerposition, size, jugadorrotation, jugadorspeed, jugadorjumpspeed, imagePath);
            jugador.IsCharacter = true;
            jugador.IsStatic = false;
            jugador.Mass = 10f;

            collisionhandler.AddEntity(jugador); //Si el character esta al final del collission handler, se cae
            damagehandler.AssignJugador(jugador);

            hud = new HUD(window, jugador, "Extras/Sketch Gothic School.ttf");

            Mobs();
            Support(-300, -300);
            Platform();

            theme = new Music("Extras/Sound/Pandora Palace.wav");
            theme.Loop = true;
            theme.Volume = 30f;
            theme.Play();


            // Inicio los turnos
            
        }        

        public void Finish()
        {
            window.Closed -= OnCloseWindow; //Buena practica desuscribirse despues de usarlo
            window.Resized -= OnResize;
            collisionhandler.RemoveEntity(ronald);
            collisionhandler.RemoveEntity(jugador);

            
            
            collisionhandler.RemoveEntity(floor);
            

            foreach (var esq in esqueleto)
            {
                collisionhandler.RemoveEntity(esq);
            }

            foreach (var plat in platforms)
            {
                collisionhandler.RemoveEntity(plat);
            }

            foreach (var cal in calaveras)
            {
                collisionhandler.RemoveEntity(cal);
            }

            theme.Stop();
            
        }

        private void OnCloseWindow(Object sender, EventArgs e)
        {
            isrunning = false;
            Finish();
            window?.Close();
        }


        public void OnLoop()
        {                             
            // Obtenemos la cantidad de tiempo transcurrido entre este frame y el anterior.
            Time deltatime = clock.Restart();                                      

            if (Keyboard.IsKeyPressed(Keyboard.Key.L))
            {
                jugador.hp = 0;
            }           
            //actualizar entidades
            Update(deltatime.AsSeconds(), clock2);
            Draw();                  
        }


        public void Play()
        {
            clock = new Clock();
            clock2 = new Clock();
            
            Start();
            
            
                       
        }

    }
}

