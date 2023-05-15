using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Entities
{
    // Classe représentant un bloc dans le jeu
    internal class Pick
    {
        // Constantes pour les dimensions et les bordures
        private const int BlockSize = 50;
        private const int BorderWidth = 4;
        private const int LowBorderWidth = BorderWidth - 2;

        // Propriétés pour accéder à la position, à la couleur et à la texture du bloc
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public Texture2D BlankTexture { get; private set; }

        // Constructeur pour créer un nouveau bloc avec une position spécifiée
        public Pick(Vector2 position, Texture2D blankTexture)
        {
            Position = position;
            Color = Color.Gray;
            BlankTexture = blankTexture;
        }

        // Propriété pour accéder aux limites du bloc (utilisée pour les collisions)
        public Rectangle Bounds => new Rectangle((int)Position.X + 5, (int)Position.Y + 5, BlockSize-10, BlockSize-10);

        // Méthode pour dessiner le bloc à l'écran en utilisant un SpriteBatch
        public void Draw(SpriteBatch spriteBatch)
        {
            // Dessiner le bloc
            Rectangle blockRect = new Rectangle((int)Position.X, (int)Position.Y, BlockSize, BlockSize);
            spriteBatch.Draw(BlankTexture, blockRect, Color);

            // Dessiner les bordures
            // Bordure inférieure (noire)
            spriteBatch.Draw(BlankTexture, new Rectangle(blockRect.Left, blockRect.Bottom - LowBorderWidth, BlockSize, LowBorderWidth), Color.Black);
            // Bordure droite (noire)
            spriteBatch.Draw(BlankTexture, new Rectangle(blockRect.Right - LowBorderWidth, blockRect.Top, LowBorderWidth, BlockSize), Color.Black);
            // Bordure supérieure (blanche)
            spriteBatch.Draw(BlankTexture, new Rectangle(blockRect.Left, blockRect.Top, BlockSize, BorderWidth), Color.White);
            // Bordure gauche (blanche)
            spriteBatch.Draw(BlankTexture, new Rectangle(blockRect.Left, blockRect.Top, BorderWidth, BlockSize), Color.White);
        }
    }
}
