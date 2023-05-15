using Microsoft.Xna.Framework;

namespace Project1.Camera
{
    // Classe représentant une caméra 2D pour un jeu
    internal class Camera2D
    {
        // Déclaration des variables privées
        private Vector2 _position;
        private readonly int _screenWidth;
        private readonly int _screenHeight;
        private const float Zoom = 1f;

        // Propriété pour accéder à la matrice de transformation de la caméra
        public Matrix Transform { get; private set; }

        // Constructeur de la classe Camera2D
        public Camera2D(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            // Initialiser la position de la caméra à (0, 0)
            _position = Vector2.Zero;
            UpdateTransform();
        }

        // Propriété pour accéder à la position de la caméra
        public Vector2 Position
        {
            get => _position;
            private set
            {
                _position = value;
                UpdateTransform();
            }
        }

        // Méthode pour mettre à jour la matrice de transformation de la caméra
        private void UpdateTransform()
        {
            Transform = Matrix.CreateTranslation(-_position.X + _screenWidth / 2f, -_position.Y + _screenHeight / 2f, 0) * Matrix.CreateScale(Zoom);
        }

        // Méthode pour déplacer la caméra
        public void Move(Vector2 direction, float amount)
        {
            Position += direction * amount;
        }

        // Méthode pour définir la position de la caméra
        public void SetPosition(Vector2 position)
        {
            Position = position;
        }
    }
}
