using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Window;
using SFML.Graphics;


namespace Juego_Finale
{
    public class Hitbox  
    {

        private readonly Texture texture; //readonly, este campo es solo de lectura, quiere decir que los parametros solo pueden ser definidos en el constructor
        public readonly Sprite sprite;

        public Sprite Sprite { get => sprite; } //propiedad que solo puede hacerse get, hacerselo a Sprite publico, nos devuelve el sprite privado
        

        public Vector2f Position { get => sprite.Position; set => sprite.Position = value; }

        public Hitbox(Vector2f position, float rotation, Vector2i size)
        {
            texture = new Texture("Extras/HitboxB.png");
            sprite = new Sprite(texture)
            {
                Position = position,
                Rotation = rotation //Rotacion, positivo, siguiendo las agujas del reloj
            };

            sprite.TextureRect = new IntRect()
            {
                //Los 4 lados del rectangulo
                Left = 0, //Lado izq
                Top = 0, //Lado superior
                Width = size.X, //Lado inferior
                Height = size.Y //Lado der
            };
        }

    }
}
