using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Entities
{
    internal class Detector
    {
        public const int DetectorSize = 50;
        private const int BorderWidth = 4;
        private const int center = 10;

        public Vector2 Position { get; set; }
        public Vector2 PositionAbso { get; set; }
        public Color Color { get; set; }
        public Texture2D BlankTexture { get; private set; }
        public bool Activated { get; set; }
        public DetectorType Type { get; private set; }
        public enum DetectorType
        {
            Block,
            Empty,
            Pick,
            NonPick,
            NonEmpty,
            NonBlock
        }
        public Detector(Vector2 position, Vector2 positionAbso, Texture2D blankTexture, DetectorType type)
        {
            Position = position + positionAbso;
            PositionAbso = positionAbso;
            Color = Color.Red;
            BlankTexture = blankTexture;
            Activated = false;
            Type = type;
        }
        public Detector Copy()
        {
            Detector copy = new Detector(Position, PositionAbso, BlankTexture, Type);
            // Copiez ici toutes les autres propriétés que vous voulez conserver
            copy.Activated = this.Activated;
            copy.Color = this.Color;

            return copy;
        }
        public Rectangle Bounds => new Rectangle((int)Position.X + center, (int)Position.Y + center, DetectorSize - center * 2, DetectorSize - center * 2);


        public void Draw(SpriteBatch spriteBatch)
        {
            Color color;
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
            // Dessiner les bordures
            // Bordure supérieure
            spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X, (int)Position.Y, DetectorSize, BorderWidth), color);
            // Bordure gauche
            spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X, (int)Position.Y, BorderWidth, DetectorSize), color);
            // Bordure inférieure
            spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X, (int)Position.Y + DetectorSize - BorderWidth, DetectorSize, BorderWidth), color);
            // Bordure droite
            spriteBatch.Draw(BlankTexture, new Rectangle((int)Position.X + DetectorSize - BorderWidth, (int)Position.Y, BorderWidth, DetectorSize), color);

            // Bordure central
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


            // small border
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
