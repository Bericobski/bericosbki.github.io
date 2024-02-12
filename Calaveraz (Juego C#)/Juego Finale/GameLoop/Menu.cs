using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace Juego_Finale
{
    class Menu
    {
        private Font font;
        private Font font2;

        private Text titulo;
        private Text company;
        private Text win;
        private Text failure;

        private SoundEffect buttonpress;
        private Music menumusic;

        private const int FontSize = 60;
        private const float OutlineThickness = 3f;

        private SecondMenu secondmenu;

        private Button play;
        private Button restart;
        private Button exit;
        private Button exit2;
        private Button credits;
        private Button controlls;

        private RenderWindow window;
        private View view;

        private const float Ypos = 200f;

        public event Action OnPlayPressed;
        public event Action OnRestartPressed;
        public event Action OnExitPressed;

        private Background menubg;
        private Background victorybg;
        private Background deathbg;
        private Background logo;


        private Vector2f pos = new Vector2f(0, 0);
       
        public enum menus
        {
            first,
            main, 
            credits, 
            controll,
            end
        }

        private const float X = 150;
        private const float X2 = 45;
        private const float Y = -60;
        private const float Y2 = 60;

        private float Xmain;
        private float Xmain2;
        private float Ymain;
        

        public menus currentmenu;

        
        public Menu(RenderWindow window)
        {
            this.window = window;
            view = this.window.GetView();

            currentmenu = menus.first;

            buttonpress = new SoundEffect("Extras/Sound/Button Press.wav");

            //Por que no se actualiza la posicion? Si no escribo lo siguiente, la primera que declare es la posicion que va a tener el boton
            Vector2f playpos = new Vector2f(view.Center.X - X, view.Center.Y - Y);
            Vector2f exitpos2 = new Vector2f(view.Center.X + X2, view.Center.Y - Y);

            Xmain = view.Center.X - X;
            Xmain2 = view.Center.X + X2;
            Ymain = view.Center.Y - Y;

            Vector2f restartpos = new Vector2f(Xmain, Ymain);
            Vector2f exitpos = new Vector2f(Xmain2, Ymain);           
            Vector2f creditpos = new Vector2f(Xmain2, Ymain + Y2);
            Vector2f controlpos = new Vector2f(Xmain, Ymain + Y2);

            play = new Button(window, playpos, "Play"); 
            restart = new Button(window, restartpos, "Restart");
            exit = new Button(window, exitpos, "Exit");
            exit2 = new Button(window, exitpos2, "Exit");
            credits = new Button(window, creditpos, "Credits");
            controlls = new Button(window, controlpos, "Controlls");
            font = new Font("Extras/Sketch Gothic School.ttf");
            font2 = new Font("Extras/PIXEL3.ttf");

            secondmenu = new SecondMenu(window, font2);

            win = new Text("¡Victory!", font);
            failure = new Text("Failure", font);
            titulo = new Text("CalaVeraz", font);
            company = new Text("Middle Points presents", font);

            play.OnPressed += OnPressPlay;
            restart.OnPressed += OnPressRestart;
            exit.OnPressed += OnPressExit;
            exit2.OnPressed += OnPressExit;
            credits.OnPressed += OnPressCredits;
            controlls.OnPressed += OnPressControlls;

            secondmenu.OnBackPressed += OnPressBack;

            Vector2f fposition = new Vector2f(0, 0);
            Vector2f fposition4 = new Vector2f(590, 45);

            Vector2i fsize1 = new Vector2i(600, 338); //Tamaño de personaje
            Vector2i fsize2 = new Vector2i(1024, 566); //Tamaño de personaje
            Vector2i fsize3 = new Vector2i(602, 383); //Tamaño de personaje
            Vector2i fsize4 = new Vector2i(15, 15); //Tamaño de personaje
            float frotation = 0f;
            
            string fimagePath1 = "Extras/Menu.jpg";
            string fimagePath2 = "Extras/victory.jpg";
            string fimagePath3 = "Extras/Death.jpg";
            string fimagePath4 = "Extras/Logo.png";


            //Vector2f fscale1 = new Vector2f(1.2f, 1.5f);
            Vector2f fscale1 = new Vector2f(0.8f, 0.6f);
            Vector2f fscale2 = new Vector2f(1f, 1f);
            Vector2f fscale3 = new Vector2f(1.6f, 1.4f);
            Vector2f fscale4 = new Vector2f(2f, 2f);

            menubg = new Background(fposition, fsize1, frotation, fimagePath1, fscale1);
            victorybg = new Background(fposition, fsize2, frotation, fimagePath2, fscale2);
            deathbg = new Background(fposition, fsize3, frotation, fimagePath3, fscale3);
            logo = new Background(fposition4, fsize4, frotation, fimagePath4, fscale4);
        }

        public void PLayMenuMusic()
        {
            menumusic = new Music("Extras/Sound/Menu Music Castlevania Forest.wav");
            menumusic.Loop = true;
            menumusic.Volume = 30f;
            menumusic.Play();
        }
        private void OnPressPlay()
        {
            if(currentmenu == menus.first)
            {                
                currentmenu = menus.main;               
                buttonpress.Play();
                menumusic.Stop();
                OnPlayPressed?.Invoke();
            }
            
        }
        private void OnPressRestart() {
            if(currentmenu == menus.main || currentmenu == menus.end)
            {
                buttonpress.Play();
                Console.WriteLine("Aprete restart");
                OnRestartPressed?.Invoke();
               
                
            }
                
        }
        
            
        private void OnPressExit()
        {
            if(currentmenu == menus.main || currentmenu == menus.first || currentmenu == menus.end)
            {
                buttonpress.Play();
                OnExitPressed?.Invoke();
                if (menumusic.Status == SoundStatus.Playing)
                {
                    menumusic.Stop();
                }
            }
                
        }
            
            
        private void OnPressCredits()
        {
            if (currentmenu == menus.main)
            {
                secondmenu.currentype = SecondMenu.Type.credits;
                currentmenu = menus.credits;
                buttonpress.Play();
                              
            }                          
        }
        
        private void OnPressControlls()
        {
            if (currentmenu == menus.main)
            {
                
                secondmenu.currentype = SecondMenu.Type.controlls;
                currentmenu = menus.controll;
                buttonpress.Play();
            }
            
        }

        private void OnPressBack()
        {
            if (currentmenu == menus.controll || currentmenu == menus.credits)
            {
                buttonpress.Play();
                currentmenu = menus.main;
            }
            
        }

        public void TheEndMenu()
        {
            
            currentmenu = menus.end;
        }
        
        public void UpdatePos(float RightX)
        {

            //Console.WriteLine("El menu actual es en menu = " + currentmenu);


            //Lo necesito, pero, no puedo actualizar la posicion del menu
            view = window.GetView();




            Vector2f restartpos = new Vector2f(Xmain, Ymain);
            Vector2f exitpos = new Vector2f(Xmain2, Ymain);
            Vector2f creditpos = new Vector2f(Xmain2, Ymain + Y2);
            Vector2f controlpos = new Vector2f(Xmain, Ymain + Y2);
            restart.SetPosition(restartpos);
            exit.SetPosition(exitpos);
            credits.SetPosition(creditpos);
            controlls.SetPosition(controlpos);
            secondmenu.UpdatePos();


        }
        

        

        public void Draw(bool goodoutcome)
        {
            window.Clear();
            switch (currentmenu)
            {
                case menus.first:
                    window.Draw(menubg.Sprite);
                    window.Draw(logo.Sprite);
                    DrawFisrtMenu();
                    play.Draw();
                    exit2.Draw();
                    break;

                case menus.end:
                    DrawEnding(goodoutcome);
                    break;               

                case menus.credits:
                    window.Draw(menubg.Sprite);
                    secondmenu.Draw();
                    break;

                case menus.main:
                    window.Draw(menubg.Sprite);
                    restart.Draw();
                    exit.Draw();
                    credits.Draw();
                    controlls.Draw();
                    break;

                case menus.controll:
                    window.Draw(menubg.Sprite);
                    secondmenu.Draw();
                    break;


            }
            window.Display();
        }

        public void DrawFisrtMenu()
        {
            Vector2f correction = new Vector2f(280f, 150f);
            Vector2f correction2 = new Vector2f(110f, 230f);
            company.Position = view.Center - correction2;
            company.CharacterSize = 25;
            company.OutlineThickness = OutlineThickness;
            window.Draw(company);

            
            titulo.Position = view.Center - correction;
            titulo.CharacterSize = 140;
            titulo.OutlineThickness = OutlineThickness;
            window.Draw(titulo);            
        }
        public void DrawEnding(bool goodoutcome)
        {
            view = window.GetView();

            /*
            Vector2f restartpos = new Vector2f(view.Center.X - 100f, view.Center.Y + 150);
            Vector2f exitpos = new Vector2f(view.Center.X + 100f, view.Center.Y + 150);
            */

            Vector2f restartpos = new Vector2f(Xmain + 40, Ymain);
            Vector2f exitpos = new Vector2f(Xmain2 + 40, Ymain);
            
            restart.SetPosition(restartpos);
            exit.SetPosition(exitpos);

            

            Vector2f correction = new Vector2f(80f, 140f);

            if (goodoutcome)
            {
                window.Draw(victorybg.Sprite);
                win.Position = view.Center - correction;
                win.CharacterSize = FontSize;
                win.OutlineThickness = OutlineThickness;
                window.Draw(win);
            }
            else
            {
                window.Draw(deathbg.Sprite);
                failure.Position = view.Center - correction;
                failure.CharacterSize = FontSize;
                failure.OutlineThickness = OutlineThickness;
                window.Draw(failure);
            }
            restart.Draw();
            exit.Draw();

        }

        public void FinishMenu()
        {
            play.OnPressed -= OnPressPlay;
            restart.OnPressed -= OnPressRestart;
            exit.OnPressed -= OnPressExit;
            exit2.OnPressed -= OnPressExit;
            credits.OnPressed -= OnPressCredits;
            controlls.OnPressed -= OnPressControlls;
            secondmenu.OnBackPressed -= OnPressBack;
        }
    }
}
