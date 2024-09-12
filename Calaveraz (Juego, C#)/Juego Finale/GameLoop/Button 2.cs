using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Juego_Finale
{
    public class Button
    {
        private RenderWindow window;
        private Font font;
        private Text text;
        //private Texture texture;
        //private Sprite sprite;

        public event Action OnPressed;
        private const float ScreenMargins = 30f;
        private const int FontSize = 50;
        private const float OutlineThickness = 3f;

        public SFML.Graphics.RectangleShape rectangle;

        

        public Button(RenderWindow window, Vector2f position, string buttonText)
        {
            this.window = window;
            font = new Font("Extras/Sketch Gothic School.ttf");
            //texture = new Texture(imagePath);
            text = new Text(buttonText, font);
            //sprite = new Sprite(texture);

            //FloatRect spriteRect = sprite.GetLocalBounds();

            //sprite.Origin = new Vector2f(spriteRect.Width / 2f, spriteRect.Height / 2f);

            SetText(buttonText);
            SetPosition(position);



            FloatRect bounds = text.GetGlobalBounds();

            var rectangle = new SFML.Graphics.RectangleShape(new Vector2f(bounds.Width, bounds.Height)) //Segundo parametro son las puntas
            {
                Position = new Vector2f(bounds.Left, bounds.Top),
                FillColor = SFML.Graphics.Color.Transparent,
                OutlineColor = SFML.Graphics.Color.White,
                OutlineThickness = 2.5f
            };

            this.rectangle = rectangle;




            window.MouseButtonReleased += OnReleaseMouseButton;
        }

        ~Button()
        {
            window.MouseButtonReleased -= OnReleaseMouseButton;
        }

        private void OnReleaseMouseButton(object sender, MouseButtonEventArgs eventArgs)
        {
            //cambie el sprite por text
            FloatRect bounds = text.GetGlobalBounds();            
                      

            if (bounds.Contains(eventArgs.X, eventArgs.Y))
                OnPressed?.Invoke();
        }

        public void SetText(string newText)
        {
            text.DisplayedString = newText;

            //text.CharacterSize = 35;

            FloatRect textRect = text.GetLocalBounds();

            text.Origin = new Vector2f(textRect.Width / 2f, textRect.Height / 2f);

            text.CharacterSize = FontSize;
            
            text.OutlineThickness = OutlineThickness;
           
            text.FillColor = Color.White;
            
            text.OutlineColor = Color.Black;
            

        }

        public void SetColor(Color color)
        {
            //sprite.Color = color;
        }

        public void FormatText(Color fillColor, Color outlineColor, uint size, bool outline, float outlineThickness)
        {
            text.FillColor = fillColor;
            text.CharacterSize = size;

            if (outline)
            {
                text.OutlineColor = outlineColor;
                text.OutlineThickness = outlineThickness;
            }
            else
            {
                text.OutlineColor = Color.Transparent;
                text.OutlineThickness = 0f;
            }
        }

        public void SetPosition(Vector2f position)
        {
            text.Position = position;
            //sprite.Position = position;

            //window.MouseButtonReleased += OnReleaseMouseButton;

            FloatRect bounds = text.GetGlobalBounds();

            var rectangle = new SFML.Graphics.RectangleShape(new Vector2f(bounds.Width, bounds.Height)) //Segundo parametro son las puntas
            {
                Position = new Vector2f(bounds.Left, bounds.Top),
                FillColor = SFML.Graphics.Color.Transparent,
                OutlineColor = SFML.Graphics.Color.White,
                OutlineThickness = 2.5f
            };

            this.rectangle = rectangle;
        }

        public void Draw()
        {
            //window.Draw(sprite);
            window.Draw(rectangle);
            window.Draw(text);
        }
    }
}
