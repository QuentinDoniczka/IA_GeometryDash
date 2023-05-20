using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Entities;
using Project1.Camera;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Project1.NeuralNetwork;
using static Project1.Entities.Detector;
using Project1.IA;
using System;
using System.Diagnostics;

namespace Project1
{
    public class Game1 : Game
    {
        // Stopwatch
        Stopwatch stopwatch = new Stopwatch();

        // Game Settings
        private const float PlayerSpeed = 500f;
        private const float Gravity = 5000f;
        private const float JumpSpeed = 1300f;
        private const int playerX = 325;
        private const int playerY = 175;
        private float _updateInterval = 60f;
        private int _updateMultiplier = 1;

        // Game State
        private bool _isPlayerOnGround = true;
        private bool _isGameStarted = false;

        private bool _isTrainingMode = false;

        private bool _isPlayerFrozen = false;
        private int nombreGame = 0;
        private int Generation = 0;
        private int maxGeneration = 30;
        private int nGamePerGeneration = 1000;

        // Game Time
        private float _elapsedTime = 0f;
        private float gamepause = 0f;
        private float _elapsedUpdateTime = 0f;
        private float _freezeTime = 0f;

        // Game Score
        private float _score;

        // Game Elements
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private List<Block> _blocks;
        private Camera2D _camera;
        private SpriteFont _font;
        private List<Pick> _pick;

        // AI Elements
        private List<Neurone> _neurones;
        private GenerateBrain _generateBrain;
        private List<GameResult> _gameResults = new List<GameResult>();
        GenerationResult generationResult;


        public Game1()
        {
            // afficher la souris
            this.IsMouseVisible = true;
            stopwatch.Start();
            if (_isTrainingMode)
            {
                _updateMultiplier = 100000;
            }
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1400;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            _camera = new Camera2D(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        }
        private bool IsPlayerOnBlock()
        {
            Rectangle playerBounds = _player.Bounds;
            playerBounds.Offset(0, 1); // Ajoutez une marge pour éviter que le joueur ne soit considéré comme sur un bloc lorsqu'il touche à peine le bord

            foreach (Block block in _blocks)
            {
                if (block.Bounds.Intersects(playerBounds))
                {
                    return true;
                }
            }

            return false;
        }
        protected override void Initialize()
        {
            base.Initialize();
            // Positionner la caméra plus à droite et en haut lors de l'initialisation
            float initialCameraOffsetX = 600f;
            float initialCameraOffsetY = 150f;
            _camera.Move(new Vector2(1, 0), initialCameraOffsetX);
            _camera.Move(new Vector2(0, -1), initialCameraOffsetY);
        }

        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("ScoreFont");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D playerTexture = new Texture2D(GraphicsDevice, 1, 1);
            playerTexture.SetData(new[] { Color.Red });

            Texture2D blockTexture = new Texture2D(GraphicsDevice, 1, 1);
            blockTexture.SetData(new[] { Color.White });

            Texture2D pickTexture = new Texture2D(GraphicsDevice, 1, 1);
            pickTexture.SetData(new[] { Color.White });

            Texture2D detectorTexture = new Texture2D(GraphicsDevice, 1, 1);
            detectorTexture.SetData(new[] { Color.White });

            Texture2D neuroneTexture = new Texture2D(GraphicsDevice, 1, 1);
            neuroneTexture.SetData(new[] { Color.White });


            _blocks = new List<Block>();
            _pick = new List<Pick>();

            _player = new Player(playerTexture, new Vector2(playerX, playerY));

            _neurones = new List<Neurone>();
            if(_isTrainingMode) { 
                _generateBrain = new GenerateBrain(_player.Position, neuroneTexture, detectorTexture);
                _neurones = _generateBrain.GetNeurones();
            }
            else
            {
                string jsonPath = Path.Combine("Generation", $"BestGameResult_{maxGeneration}.json");
                string json = File.ReadAllText(jsonPath);

                GameResult gameResultFromJson = JsonConvert.DeserializeObject<GameResult>(json);

                _neurones = new List<Neurone>();

                foreach (SimpleNeurone simpleNeurone in gameResultFromJson.Neurones)
                {
                    Neurone neurone = new Neurone(_player.Position, neuroneTexture);
                    foreach (SimpleDetector simpleDetector in simpleNeurone.Detectors)
                    {
                        Detector detector = new Detector(neurone.Position, simpleDetector.PositionAbso, detectorTexture, simpleDetector.Type);
                        neurone.GetDetector.Add(detector);
                    }
                    neurone.UpdatePosition(); // met à jour la position du neurone
                    _neurones.Add(neurone);
                }
            }






            // Utilisez la méthode LoadLevel pour charger le niveau à partir du fichier JSON
            string levelPath = "level.json";
            LoadLevel(blockTexture, pickTexture, levelPath);
            foreach (Neurone neurone in _neurones)
            {
                foreach (Detector detector in neurone.GetDetector)
                {
                    detector.Position = new Vector2(_player.Position.X + detector.PositionAbso.X, _player.Position.Y + detector.PositionAbso.Y);
                }
                neurone.Position = new Vector2(_player.Position.X + neurone.PositionAbso.X, _player.Position.Y + neurone.PositionAbso.Y);
            }
        }
        private Vector2 StringToVector2(string input)
        {
            string[] inputs = input.Split(',');
            return new Vector2(float.Parse(inputs[0]), float.Parse(inputs[1]));
        }

        private void CreateBlocks(int startX, int startY, int nombreLine, int nombreCol, Texture2D blankTexture)
        {
            int blockSize = 50;
            for (int i = 0; i < nombreLine; i++)
            {
                for (int j = 0; j < nombreCol; j++)
                {
                    int x = startX + (blockSize * j);
                    int y = startY + (blockSize * i);

                    _blocks.Add(new Block(new Vector2(x, y), blankTexture));
                }
            }
        }
        private void LoadLevel(Texture2D blockTexture, Texture2D pickTexture, string levelPath)
        {
            string json = File.ReadAllText(levelPath);
            dynamic levelData = JsonConvert.DeserializeObject(json);

            int startX = -100;
            int startY = -100;
            int blockSize = 50;

            int rowIndex = 0;
            int playerX, playerY;
            foreach (string row in levelData.rows)
            {
                for (int colIndex = 0; colIndex < row.Length; colIndex++)
                {
                    char cell = row[colIndex];

                    int x = startX + (colIndex * blockSize);
                    int y = startY + (rowIndex * blockSize);

                    if (cell == 'b')
                    {
                        CreateBlocks(x, y, 1, 1, blockTexture);
                    }
                    else if (cell == 'v')
                    {
                        _pick.Add(new Pick(new Vector2(x, y), pickTexture));
                    }
                    else if (cell == 'x')
                    {
                        playerX = x;
                        playerY = y;
                    }
                }
                rowIndex++;
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _elapsedUpdateTime += _updateInterval;

            if (_elapsedUpdateTime >= 60)
            {
                
                for (int i = 0; i < _updateMultiplier; i++)
                {
                    if (!_isGameStarted)
                    {
                        _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (_elapsedTime >= gamepause)
                        {
                            _isGameStarted = true;
                        }
                        return;
                    }
                    else
                    {
                        // Si le joueur est gelé, attendre 1 seconde avant de redémarrer le jeu
                        if (_isPlayerFrozen)
                        {
                            _freezeTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (_freezeTime >= gamepause)
                            {
                                RestartGame();
                            }
                        }
                        else
                        {
                            UpdatePlayer(gameTime);
                            UpdateCamera(gameTime);
                            UpdateNeurone(gameTime);
                            _player.Update(gameTime);
                        }
                    }
                }

                _elapsedUpdateTime = 0f; // Réinitialiser le temps écoulé après chaque mise à jour
            }



            base.Update(gameTime);
        }
        private void UpdateNeurone(GameTime gameTime)
        {
            foreach (Neurone neurone in _neurones)
            {
                foreach (Detector detector in neurone.GetDetector)
                {
                    detector.Position = new Vector2(_player.Position.X + detector.PositionAbso.X, _player.Position.Y + detector.PositionAbso.Y);
                    bool isActivated;
                    switch (detector.Type)
                    {
                        case Detector.DetectorType.Block:
                            isActivated = false;
                            foreach (Block block in _blocks)
                            {
                                if (detector.Bounds.Intersects(block.Bounds))
                                {
                                    isActivated = true;
                                    break;
                                }
                            }
                            detector.Activated = isActivated;
                            break;
                        case DetectorType.NonBlock:
                            isActivated = true;
                            foreach (Block block in _blocks)
                            {
                                if (detector.Bounds.Intersects(block.Bounds))
                                {
                                    isActivated = false;
                                    break;
                                }
                            }
                            detector.Activated = isActivated;
                            break;
                        case DetectorType.Pick:
                            isActivated = false;
                            foreach (Pick pick in _pick)
                            {
                                if (detector.Bounds.Intersects(pick.Bounds))
                                {
                                    isActivated = true;
                                    break;
                                }
                            }
                            detector.Activated = isActivated;
                            break;
                        case DetectorType.NonPick:
                            isActivated = true;
                            foreach (Pick pick in _pick)
                            {
                                if (detector.Bounds.Intersects(pick.Bounds))
                                {
                                    isActivated = false;
                                    break;
                                }
                            }
                            detector.Activated = isActivated;
                            break;
                        case DetectorType.Empty:
                            isActivated = true;
                            foreach (Block block in _blocks)
                            {
                                if (detector.Bounds.Intersects(block.Bounds))
                                {
                                    isActivated = false;
                                    break;
                                }
                            }
                            if (isActivated)
                            {
                                foreach (Pick pick in _pick)
                                {
                                    if (detector.Bounds.Intersects(pick.Bounds))
                                    {
                                        isActivated = false;
                                        break;
                                    }
                                }
                            }
                            detector.Activated = isActivated;
                            break;
                        case DetectorType.NonEmpty:
                            isActivated = false;
                            foreach (Block block in _blocks)
                            {
                                if (detector.Bounds.Intersects(block.Bounds))
                                {
                                    isActivated = true;
                                    break;
                                }
                            }
                            if (!isActivated)
                            {
                                foreach (Pick pick in _pick)
                                {
                                    if (detector.Bounds.Intersects(pick.Bounds))
                                    {
                                        isActivated = true;
                                        break;
                                    }
                                }
                            }
                            detector.Activated = isActivated;
                            break;
                        default: return;
                    }
                }
                neurone.Position = new Vector2(_player.Position.X + neurone.PositionAbso.X, _player.Position.Y + neurone.PositionAbso.Y);
                neurone.Activated = true;
                foreach (Detector detector in neurone.GetDetector)
                {
                    if (!detector.Activated)
                    {
                        neurone.Activated = false;
                    }
                }
                if(neurone.Activated && _isPlayerOnGround)
                {
                    _player.Velocity = new Vector2(_player.Velocity.X, -JumpSpeed);
                    _isPlayerOnGround = false;
                }
            }
        }
        private void UpdateCamera(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float distanceToMove = PlayerSpeed * deltaTime;

            _camera.Move(new Vector2(1, 0), distanceToMove);
        }
        private void UpdatePlayer(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float distanceToMove = PlayerSpeed * deltaTime;
            _score += distanceToMove / 50f;

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space) && _isPlayerOnGround)
            {
                _player.Velocity = new Vector2(_player.Velocity.X, -JumpSpeed);
                _isPlayerOnGround = false;
            }

            _player.Position += new Vector2(distanceToMove, _player.Velocity.Y * deltaTime);

            // Appliquer la gravité et mettre à jour la rotation du joueur
            if (!_isPlayerOnGround)
            {
                _player.Velocity += new Vector2(0, Gravity * deltaTime);
                if (!_player.IsJumping)
                {
                    _player.IsJumping = true;
                    _player.Rotation += MathHelper.Pi;
                }
            }
            else
            {
                _player.Rotation = 0;
                _player.IsJumping = false;
            }


            // Vérifier les collisions avec les blocs
            foreach (Block block in _blocks)
            {
                if (_player.Bounds.Intersects(block.Bounds))
                {
                    // Vérifiez si le joueur est à gauche du bloc
                    if (_player.Position.X < block.Position.X)
                    {
                        _isPlayerFrozen = true;
                        return;
                    }
                    else
                    {
                        // Si le joueur ne touche pas le côté gauche, ajustez la position en fonction de la collision verticale
                        _player.Position = new Vector2(_player.Position.X, block.Position.Y - _player.Bounds.Height + _player.Origin.Y);
                        _player.Velocity = new Vector2(_player.Velocity.X, 0);
                        _isPlayerOnGround = true;
                    }
                }
            }


            // Vérifier les collisions avec les picks
            foreach (Pick pick in _pick)
            {
                if (_player.Bounds.Intersects(pick.Bounds))
                {
                    _isPlayerFrozen = true;
                    return;
                }
            }


            _isPlayerOnGround = IsPlayerOnBlock();
        }
        private void RestartGame()
        {
            // Réinitialiser les variables
            if (!_isTrainingMode)
            {
                System.Threading.Thread.Sleep(2000);
                Environment.Exit(0);
            }
            else { 
                nombreGame++;
                _gameResults.Add(new GameResult(_generateBrain.GetNeurones(), _score));

                // Créer une nouvelle génération avec l'ID et les résultats de la partie
                if (nombreGame > nGamePerGeneration)
                {
                    generationResult = new GenerationResult(Generation, _gameResults);
                    string directoryName = "Generation";
                    string json = JsonConvert.SerializeObject(generationResult.BestGameResult, Formatting.Indented);

                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    File.WriteAllText(Path.Combine(directoryName, $"BestGameResult_{Generation}.json"), json);
                    Generation++;
                    nombreGame = 0;
                    _gameResults.Clear();
                }
                if (Generation > maxGeneration)
                {
                    stopwatch.Stop();
                    Debug.WriteLine($"Temps écoulé: {stopwatch.ElapsedMilliseconds} ms");
                    Environment.Exit(0);
                }
                _isPlayerFrozen = false;
                _freezeTime = 0f;
                _score = 0f;
                _elapsedTime = 0f;
                _isGameStarted = false;
                _player.Position = new Vector2(325, 125);
                _player.Velocity = Vector2.Zero;
                _player.Rotation = 0f; // Ajouter cette ligne pour réinitialiser la rotation du joueur

                // Créer un nouveau cerveau
                if (Generation == 0)
                {
                    _generateBrain.RegenerateBrain();
                }
                else
                {
                    if (nombreGame == 0)
                    {
                        _generateBrain.FirstBrain(generationResult);
                    }
                    else
                    {
                        _generateBrain.BrainFromFirst();
                    }
                }
                foreach (Neurone neurone in _neurones)
                {
                    foreach (Detector detector in neurone.GetDetector)
                    {
                        detector.Position = new Vector2(_player.Position.X + detector.PositionAbso.X, _player.Position.Y + detector.PositionAbso.Y);
                    }
                    neurone.Position = new Vector2(_player.Position.X + neurone.PositionAbso.X, _player.Position.Y + neurone.PositionAbso.Y);
                }
                _camera.SetPosition(Vector2.Zero);
                float initialCameraOffsetX = 600f;
                float initialCameraOffsetY = 150f;
                _camera.Move(new Vector2(1, 0), initialCameraOffsetX);
                _camera.Move(new Vector2(0, -1), initialCameraOffsetY);
            }
        }

        // La méthode Draw est appelée à chaque frame pour dessiner les éléments du jeu à l'écran
        protected override void Draw(GameTime gameTime)
        {
            if(_isTrainingMode)
            {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(_font, $"Score: {nombreGame:F1}", new Vector2(10, 10), Color.Black);
                GraphicsDevice.Clear(Color.CornflowerBlue);



                _spriteBatch.End();

                base.Draw(gameTime);
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                _spriteBatch.Begin();
                _spriteBatch.DrawString(_font, $"Score: {_score:F1}", new Vector2(10, 10), Color.Black);
                _spriteBatch.End();

                _spriteBatch.Begin(transformMatrix: _camera.Transform);
                _player.Draw(_spriteBatch);
                
                foreach (Block block in _blocks)
                {
                    block.Draw(_spriteBatch);
                }
                foreach (Pick pick in _pick)
                {
                    pick.Draw(_spriteBatch);
                }
                foreach (Neurone neurone in _neurones)
                {
                    foreach (Detector detector in neurone.GetDetector)
                    {
                        detector.Draw(_spriteBatch);
                    }
                    neurone.Draw(_spriteBatch);
                }
                _spriteBatch.End();

                base.Draw(gameTime);
            }
        }
    }
}
