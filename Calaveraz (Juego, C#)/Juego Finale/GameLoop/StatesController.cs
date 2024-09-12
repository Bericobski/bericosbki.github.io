using SFML.Graphics;
using System;
using System.Text;

using System.Collections.Generic;
using SFML.System;
using SFML.Window;

namespace Juego_Finale
{
    public class StatesController
    {
        private RenderWindow window;
        private Menu MenuState;
        private GameLoop gameplay;
        private View view;

        private bool isrunning;
        private bool IsPauseButtonPressed;
        private bool Ispaused;

        

        enum programStates
        {
            playagain, 
            gameover
        }

        private programStates currentstate;

        private void setView()
        {           
            view.Center = new Vector2f(window.Size.X/2, window.Size.Y/2);
            window.SetView(view);
        }

        public StatesController (RenderWindow window)
        {
            this.window = window;
            this.view = new View(window.GetView()); 
            MenuState = new Menu(window);
            gameplay = new GameLoop(window);

            MenuState.OnPlayPressed += StartGame;
            MenuState.OnRestartPressed += RestartApplication;
            MenuState.OnExitPressed += QuitApplication;
            //MenuState.OnCreditsPressed += RestartApplication;
            //MenuState.OnControlPressed += RestartApplication;

        }

        ~StatesController ()
        {
            MenuState.OnPlayPressed -= StartGame;
            MenuState.OnRestartPressed -= RestartApplication;
            MenuState.OnExitPressed -= QuitApplication;

        }

        public void QuitApplication()
        {
            if(MenuState.currentmenu == Menu.menus.first)
            {
                MenuState.FinishMenu();
                window.Close();
                return;
            }


            if (!Ispaused)
                return;
                                     
            isrunning = false;
            gameplay.Finish();
            MenuState.FinishMenu();

            currentstate = programStates.gameover;

            
        }
        public void RestartApplication()
        {
            
            Console.WriteLine(Ispaused);
            if (!Ispaused)
                return;
            gameplay.Finish();
            gameplay = new GameLoop(window);
            isrunning = false;
            currentstate = programStates.playagain;          
        }

        public void StartGame()
        {           
            gameplay.Play();
            MenuState.currentmenu = Menu.menus.main;
            isrunning = true;
            Ispaused = false;
            LoopGameOn();
        }

        public void LoopGameOn()
        {                                            
            while (isrunning)
            {
                // Obtenemos la cantidad de tiempo transcurrido entre este frame y el anterior.             

                window.DispatchEvents();
                if (!isrunning) //Probando un posible bug con un loop complicado
                    break;

                if (Keyboard.IsKeyPressed(Keyboard.Key.P) && !IsPauseButtonPressed && MenuState.currentmenu != Menu.menus.end)
                {
                    Ispaused = Ispaused ? false : true;
                    //Console.WriteLine(Ispaused);
                    IsPauseButtonPressed = true;
                    MenuState.currentmenu = Menu.menus.main;
                    setView();
                }
                else if (!Keyboard.IsKeyPressed(Keyboard.Key.P))
                {
                    IsPauseButtonPressed = false;
                }

                
                if (Ispaused)
                {                                      
                    
                    MenuState.UpdatePos(gameplay.jugador.GetXPos());

                    //gameplay.DrawBG();

                    MenuState.Draw(gameplay.jugador.IsAlive);
                    
                    continue;
                }
                

                //actualizar entidades
                gameplay.OnLoop();

                
                //procesar eventos

                if (!gameplay.jugador.IsAlive || gameplay.jugador.Score >= gameplay.victoryScore || gameplay.jugador.GetXPos() >= gameplay.victoryX)
                {
                    MenuState.TheEndMenu();
                    Ispaused = true;
                    setView();
                }                
            }

            switch (currentstate)
            {
                case programStates.gameover:
                    window.Close();
                    break;

                case programStates.playagain:                    
                    StartGame();
                    break;
            }
        }

        public void StartApplication ()
        {
            MenuState.PLayMenuMusic();
            while (MenuState.currentmenu == Menu.menus.first)
            {                           
                MenuState.Draw(true);                              
                window.DispatchEvents();
            }
            
        }

        
    }
        
}
