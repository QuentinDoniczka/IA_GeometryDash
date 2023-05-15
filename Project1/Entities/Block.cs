using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Entities
{
    // Classe représentant un bloc dans le jeu
    internal class Block
    {
        // Constantes pour les dimensions et les bordures
        private const int BorderWidth = 4;
        private const int Shadow = BorderWidth - 2;
        private const int BlockSize = 50;
        private const int BlockShadow = BlockSize - Shadow*2;
        private const int BlockBody = BlockSize - BorderWidth*2;

        // Propriétés pour accéder à la position, à la couleur et à la texture du bloc
        public Vector2 Position { get; set; }
        public Texture2D BlankTexture { get; private set; }

        // Constructeur pour créer un nouveau bloc avec une position spécifiée
        public Block(Vector2 position, Texture2D blankTexture)
        {
            Position = position;
            BlankTexture = blankTexture;
        }

        // Propriété pour accéder aux limites du bloc (utilisée pour les collisions)
        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, BlockSize, BlockSize);

        // Méthode pour dessiner le bloc à l'écran en utilisant un SpriteBatch
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle blockBorder = new Rectangle((int)Position.X, (int)Position.Y, BlockSize, BlockSize);
            Rectangle blockShadow = new Rectangle((int)Position.X + BorderWidth, (int)Position.Y + BorderWidth, BlockShadow, BlockShadow);
            Rectangle blockBody = new Rectangle((int)Position.X + BorderWidth, (int)Position.Y + BorderWidth, BlockBody + Shadow, BlockBody + Shadow);

            spriteBatch.Draw(BlankTexture, blockBorder, Color.White);
            spriteBatch.Draw(BlankTexture, blockShadow, Color.Black);
            spriteBatch.Draw(BlankTexture, blockBody, Color.Aqua);

            /*
            // Dessiner le bloc
            Rectangle blockRect = new Rectangle((int)Position.X, (int)Position.Y, BlockSize, BlockSize);
            spriteBatch.Draw(BlankTexture, blockRect, Color);

            // Dessiner les bordures
            // Bordure inférieure (noire)
            spriteBatch.Draw(BlankTexture, new Rectangle(blockRect.Left, blockRect.Bottom - Shadow, BlockSize, Shadow), Color.Black);
            // Bordure droite (noire)
            spriteBatch.Draw(BlankTexture, new Rectangle(blockRect.Right - Shadow, blockRect.Top, Shadow, BlockSize), Color.Black);
            // Bordure supérieure (blanche)
            spriteBatch.Draw(BlankTexture, new Rectangle(blockRect.Left, blockRect.Top, BlockSize, BorderWidth), Color.White);
            // Bordure gauche (blanche)
            spriteBatch.Draw(BlankTexture, new Rectangle(blockRect.Left, blockRect.Top, BorderWidth, BlockSize), Color.White);
            */
        }
    }
}
