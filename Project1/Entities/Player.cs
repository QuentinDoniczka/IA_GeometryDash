using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Entities
{
    internal class Player
    {
        // Constantes pour les dimensions
        private const int PlayerSize = 50;

        public Vector2 Position { get; set; }
        private readonly Texture2D _texture;
        public Vector2 Velocity { get; set; }
        public float Rotation { get; set; }
        public bool IsJumping { get; set; }
        public Rectangle SourceRectangle => new Rectangle(0, 0, PlayerSize, PlayerSize);


        public Vector2 Origin => new Vector2(PlayerSize / 2f, PlayerSize / 2f); // Ajoutez cette ligne pour définir l'origine au centre du carré
        public Rectangle Bounds => new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), PlayerSize, PlayerSize);

        public Player(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, SourceRectangle, Color.White, Rotation, Origin, 1f, SpriteEffects.None, 0f);
        }


        public void Update(GameTime gameTime)
        {
            if (IsJumping)
            {
                // Appliquer une rotation progressive pendant le saut
                float jumpProgress = MathHelper.Clamp(-Velocity.Y / 6000f, -1f, 1f); // La rotation atteindra un maximum de +/- 1 radian lorsque la vitesse verticale est inférieure à -700
                Rotation = MathHelper.Lerp(MathHelper.Pi, -MathHelper.Pi, jumpProgress); // La rotation est interpolée entre +Pi et -Pi en fonction de la progression du saut
            }
        }
    }
}
