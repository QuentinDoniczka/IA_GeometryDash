using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Entities
{
    // Classe représentant un détecteur dans le jeu
    internal class Detector
    {
        // Constantes pour la taille du détecteur, la largeur de la bordure et le centre
        public const int DetectorSize = 50;
        private const int BorderWidth = 4;
        private const int center = 10;

        // Propriétés du détecteur
        public Vector2 Position { get; set; } // Position relative du détecteur
        public Vector2 PositionAbso { get; set; } // Position absolue du détecteur
        public Color Color { get; set; } // Couleur du détecteur
        public Texture2D BlankTexture { get; private set; } // Texture graphique du détecteur
        public bool Activated { get; set; } // Indique si le détecteur est activé
        public DetectorType Type { get; private set; } // Type du détecteur

        // Enumération des différents types de détecteurs
        public enum DetectorType
        {
            Block,
            Empty,
            Pick,
            NonPick,
            NonEmpty,
            NonBlock
        }

        // Constructeur du détecteur
        public Detector(Vector2 position, Vector2 positionAbso, Texture2D blankTexture, DetectorType type)
        {
            Position = position + positionAbso;
            PositionAbso = positionAbso;
            Color = Color.Red;
            BlankTexture = blankTexture;
            Activated = false;
            Type = type;
        }

        // Méthode pour créer une copie de ce détecteur
        public Detector Copy()
        {
            Detector copy = new Detector(Position, PositionAbso, BlankTexture, Type);
            // Copie des propriétés pertinentes
            copy.Activated = this.Activated;
            copy.Color = this.Color;

            return copy;
        }

        // Limites du détecteur utilisées pour les collisions
        public Rectangle Bounds => new Rectangle((int)Position.X + center, (int)Position.Y + center, DetectorSize - center * 2, DetectorSize - center * 2);

        // Méthode pour dessiner le détecteur à l'écran
        public void Draw(SpriteBatch spriteBatch)
        {
            Color color;

            // Choix de la couleur en fonction du type du détecteur
            switch (Type)
            {
                case DetectorType.Block:
                case DetectorType.NonBlock: color = Activated ? Color.Green : Color.Aqua; break;
                case DetectorType.Pick:
                case DetectorType.NonPick: color = Activated ? Color.Green : Color.Gray; break;
                case DetectorType.Empty:
                case DetectorType.NonEmpty: color = Activated ? Color.Green : Color.White; break;
                default: return;
            }

            // Dessin des bordures du détecteur
            // Bordure supérieure
            spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X, (int)Position.Y, DetectorSize, BorderWidth), color);
            // Bordure gauche
            spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X, (int)Position.Y, BorderWidth, DetectorSize), color);
            // Bordure inférieure
            spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X, (int)Position.Y + DetectorSize - BorderWidth, DetectorSize, BorderWidth), color);
            // Bordure droite
            spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X + DetectorSize - BorderWidth, (int)Position.Y, BorderWidth, DetectorSize), color);

            // Dessin de la bordure centrale
            // Bordure supérieure
            if (Type == DetectorType.Block || Type == DetectorType.Pick || Type == DetectorType.Empty)
            {
                spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X + center, (int)Position.Y + center, DetectorSize - center * 2, BorderWidth), color);
                // Bordure gauche
                spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X + center, (int)Position.Y + center, BorderWidth, DetectorSize - center * 2), color);
                // Bordure inférieure
                spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X + center, (int)Position.Y + DetectorSize - BorderWidth - center, DetectorSize - center * 2, BorderWidth), color);
                // Bordure droite
                spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X + DetectorSize - BorderWidth - center, (int)Position.Y + center, BorderWidth, DetectorSize - center * 2), color);
            }

            // Dessin de la petite bordure
            // Bordure supérieure
            spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X, (int)Position.Y, DetectorSize, 1), Color.Black);
            // Bordure gauche
            spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X, (int)Position.Y, 1, DetectorSize), Color.Black);
            // Bordure inférieure
            spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X, (int)Position.Y + DetectorSize - 1, DetectorSize, 1), Color.Black);
            // Bordure droite
            spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X + DetectorSize - 1, (int)Position.Y, 1, DetectorSize), Color.Black);
        }
    }
}
