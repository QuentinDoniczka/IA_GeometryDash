using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Définition de l'espace de noms du projet
namespace Project1.Entities
{
    // Classe représentant un joueur dans le jeu
    internal class Player
    {
        // Constante définissant la taille du joueur
        private const int PlayerSize = 50;

        // Propriétés du joueur
        public Vector2 Position { get; set; } // Position du joueur
        private readonly Texture2D _texture; // Texture graphique du joueur
        public Vector2 Velocity { get; set; } // Vitesse du joueur
        public float Rotation { get; set; } // Rotation du joueur
        public bool IsJumping { get; set; } // Indique si le joueur est en train de sauter
        public Rectangle SourceRectangle => new Rectangle(0, 0, PlayerSize, PlayerSize); // Rectangle source de la texture

        // L'origine du joueur, définie au centre du carré
        public Vector2 Origin => new Vector2(PlayerSize / 2f, PlayerSize / 2f);

        // Limites du joueur utilisées pour les collisions
        public Rectangle Bounds => new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), PlayerSize, PlayerSize);

        // Constructeur du joueur
        public Player(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
        }

        // Méthode pour dessiner le joueur à l'écran
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, SourceRectangle, Color.White, Rotation, Origin, 1f, SpriteEffects.None, 0f);
        }

        // Méthode pour mettre à jour l'état du joueur à chaque frame
        public void Update(GameTime gameTime)
        {
            if (IsJumping)
            {
                // Appliquer une rotation progressive pendant le saut
                float jumpProgress = MathHelper.Clamp(-Velocity.Y / 6000f, -1f, 1f);
                Rotation = MathHelper.Lerp(MathHelper.Pi, -MathHelper.Pi, jumpProgress);
            }
        }
    }
}