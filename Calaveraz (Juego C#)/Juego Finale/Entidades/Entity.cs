using SFML.System;
using SFML.Graphics;

namespace Juego_Finale
{
    public abstract class Entity
    {
        /*Texture: Imagen de la entidad, con toda la animacion
          Sprite: Lo que se muestra en pantalla, una fraccion de la textura, o un solo frame de animacion*/

        private readonly Texture texture; //readonly, este campo es solo de lectura, quiere decir que los parametros solo pueden ser definidos en el constructor
        public readonly Sprite sprite;

        public float Mass { get; set; }
        public bool IsStatic { get; set; } //No se debe mover
        public bool IsCharacter { get; set; } //Es personaje
        public bool IsFalling { get; set; } //Esta tocando piso o plataforma
        public bool PassFloor { get; set; } //Debe traspasar el piso?

        public bool IsAlive { get; set; } //Debe traspasar el piso?

        public Sprite Sprite { get => sprite; } //propiedad que solo puede hacerse get, hacerselo a Sprite publico, nos devuelve el sprite privado
        
        //Devuelve la posicion del sprite, y con el set, la podemos cambiar para hacer la animacion
        public Vector2f Position { get => sprite.Position; set => sprite.Position = value; } 

        public Entity(Vector2f position, float rotation, string imagePath)
        {
            texture = new Texture(imagePath);
            sprite = new Sprite(texture)
            {
                Position = position,
                Rotation = rotation //Rotacion, positivo, siguiendo las agujas del reloj
            };
        }


        /*Cada entidad se actualizara a su manera, por eso es abstracta esta clase */
        public abstract void Update(float deltaTime);

        public bool IsCollidingWith(Entity entity)
        {
            return sprite.GetGlobalBounds().Intersects(entity.sprite.GetGlobalBounds());
        }
        public void Translate(Vector2f movement)
        {
            Sprite.Position += movement;
        }
    }
}

