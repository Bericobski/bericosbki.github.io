using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Juego_Finale
{
    public class SecondMenu
    {
        RenderWindow window;
        View view;

        private const int FontSize = 30;
        private const float OutlineThickness = 3f;
        public enum Type
        {
            controlls, 
            credits
        }

        public Type currentype;

        private List<Text> credits = new List<Text>();
        private List<Text> controlls = new List<Text>();

        public Button back;

        private Vector2f firstline;
        private Vector2f backline;
        private Vector2f spacebetween = new Vector2f(0, 0);

        public event Action OnBackPressed;

        public SecondMenu(RenderWindow window, Font font)
        {
            this.window = window;
            view = this.window.GetView();
            firstline = new Vector2f(100f, 50f);

            backline = new Vector2f(130f, 350f);

            back = new Button(window, backline, "Back");

            credits.Add(new Text("CREDITS", font));
            credits.Add(new Text("Desarrollador: Bruno Bevilacqua", font));
            credits.Add(new Text("Musica: Toby Fox", font));
            credits.Add(new Text("Middle Point Studio", font));

            foreach (var line in credits)
            {
                line.Position = firstline;
                line.CharacterSize = FontSize;
                line.OutlineThickness = OutlineThickness;
                line.Position = line.Position + spacebetween;
                spacebetween += new Vector2f(0, 50f);
            }

            spacebetween = new Vector2f(0, 0);

            controlls.Add(new Text("CONTROLLS", font));
            controlls.Add(new Text("Jump : SPACE", font));
            controlls.Add(new Text("Right : D/RIGHT", font));
            controlls.Add(new Text("Left : A/LEFT", font));
            controlls.Add(new Text("Attack : E", font));
            controlls.Add(new Text("Sorpresa : L", font));

            foreach (var line in controlls)
            {
                line.Position = firstline;
                line.CharacterSize = FontSize;
                line.OutlineThickness = OutlineThickness;
                line.Position = line.Position + spacebetween;
                spacebetween += new Vector2f(0, 50f);
            }

            back.OnPressed += OnPressBack;

            
        }

        private void OnPressBack() => OnBackPressed?.Invoke();

        public void UpdatePos()
        {
            view = window.GetView();
            
            foreach (var line in credits)
            {
                line.Position = new Vector2f(view.Center.X - window.Size.X / 2 + 100f, line.Position.Y);                                
            }

            foreach (var line in controlls)
            {
                line.Position = new Vector2f(view.Center.X - window.Size.X / 2 + 100f, line.Position.Y);
            }

            back.SetPosition(new Vector2f(view.Center.X - window.Size.X / 2 + 130f, 350f));
        }

        public void Draw()
        {
            switch (currentype)
            {
                case Type.credits:
                    foreach (var line in credits)
                    {
                        window.Draw(line);
                    }
                    break;

                

                case Type.controlls:
                    foreach (var line in controlls)
                    {
                        window.Draw(line);
                    }
                    break;                    
            }
            

            back.Draw();
        }
    }
}
