using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project1.NeuralNetwork
{
    internal class Neurone
    {
        public const int NeuroneSize = 30;
        public Vector2 Position { get; set; }
        public Vector2 PositionAbso { get; set; }
        public Color Color { get; set; }
        public Texture2D BlankTexture { get; private set; }
        public bool Activated { get; set; }
        private List<Detector> detector;

        public Neurone(Vector2 position, Texture2D blankTexture)
        {
            detector = new List<Detector>();
            Position = position;
            Color = Color.Gray;
            BlankTexture = blankTexture;
            Activated = false;
        }
        public void AddDetector(Detector newDetector)
        {
            detector.Add(newDetector);
            UpdatePosition();
        }
        public void UpdatePosition()
        {
            Vector2 sum = Vector2.Zero;

            foreach (var detector in detector)
            {
                sum += ((detector.PositionAbso)+new Vector2(10,10));
            }

            Vector2 averagePosition = sum / detector.Count;
            PositionAbso = new Vector2(averagePosition.X + NeuroneSize - NeuroneSize, averagePosition.Y + NeuroneSize - NeuroneSize);
        }
        public List<Detector> GetDetector { get {  return detector; } }
        public void Draw(SpriteBatch spriteBatch)
        {
            Color color = Activated ? Color.LightGreen : Color.LightGray;
            Rectangle neurone = new Rectangle((int)Position.X, (int)Position.Y, NeuroneSize, NeuroneSize);

            spriteBatch.Draw(BlankTexture, neurone, color);
        }
        public Neurone Copy()
        {
            Neurone copy = new Neurone(Position, BlankTexture);
            foreach (Detector detector in GetDetector)
            {
                copy.GetDetector.Add(detector.Copy()); // Assumer que vous avez également une méthode Copy() dans la classe Detector
            }
            return copy;
        }
    }
}
