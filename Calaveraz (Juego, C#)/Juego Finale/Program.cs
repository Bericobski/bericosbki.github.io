using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

/* Capturar input
 * actualizar entidades logicas
 * dibujar las entidades en pantalla
 */


/*
 * Agarrar power ups
 * Plataforma
 * Spawn de enemigos
 * Menu interactivo cada turno
 * Sistema turno
 * Ataques boss
 * Ataque de esqueletos
 * Colision con esqueletos
 * Final de juego
 */

namespace Juego_Finale
{
    class Program
    {
        static void Main()
        {
            //RenderWindow window = new RenderWindow(new VideoMode(800, 600), "Juego Finale");

            VideoMode videoMode = new VideoMode()
            {
                Width = VideoMode.DesktopMode.Width /2,
                Height = VideoMode.DesktopMode.Height /2
            };

            string title = "CalaVeraz";

            RenderWindow window = new RenderWindow(videoMode, title);

            /*
            GameLoop loop = new GameLoop(window);

            loop.Play();
            */
            
            StatesController statesController = new StatesController(window);

            statesController.StartApplication();


        }

    }
}
