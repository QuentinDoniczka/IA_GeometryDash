using Microsoft.Xna.Framework.Graphics;
using Project1.Entities;
using Project1.NeuralNetwork;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace Project1.IA
{
    internal class GenerateBrain
    {
        private List<Neurone> _neurones;
        private List<Neurone> _bestNeurones; // Nouvelle variable pour stocker les meilleurs neurones
        private int detectorX = -25;
        private int detectorY = -25;

        private int minX = 0;
        //private int maxX = 350;
        private int minY = -200;
        //private int maxY = 150;

        private Texture2D neuroneTexture;
        private Texture2D detectorTexture;
        private Vector2 playerPosition;

        private Random rand = new Random();
        public GenerateBrain(Vector2 playerPosition, Texture2D neuroneTexture, Texture2D detectorTexture)
        {
            this.neuroneTexture = neuroneTexture;
            this.detectorTexture = detectorTexture;
            this.playerPosition = playerPosition;

            _neurones = new List<Neurone>();
            _bestNeurones = new List<Neurone>();
            GenerateFromZero();
        }
        private void RandomDetector(Neurone neurone)
        {
            int randomX = rand.Next(0, 70) * 5;
            int randomY = rand.Next(0, 70) * 5;
            Detector.DetectorType[] values = (Detector.DetectorType[])Enum.GetValues(typeof(Detector.DetectorType));
            Detector.DetectorType randomDetector = values[rand.Next(values.Length)];



            neurone.AddDetector(new Detector(playerPosition, new Vector2(detectorX + randomX + minX, detectorY + randomY + minY), detectorTexture, randomDetector));
        }

        private void GenerateFromZero()
        {
            double bias = 2.0; // Augmentez cette valeur pour rendre les nombres plus grands encore plus rares.

            // Génère un nombre aléatoire entre 0 et 1 avec une distribution biaisée.
            double randDouble = Math.Pow(rand.NextDouble(), bias);

            // Maintenant, nous devons adapter notre nombre aléatoire à notre plage souhaitée, qui est de 0 à 9.
            int nNeurone = (int)(randDouble * 6) + 3; // 10 parce que nous avons besoin d'une plage de 0 à 9.
            int nDetector;
            for (int i = 0; i < nNeurone; i++)
            {
                Neurone newNeurone = new Neurone(playerPosition, neuroneTexture);
                randDouble = Math.Pow(rand.NextDouble(), bias);
                nDetector = (int)(randDouble * 4) + 1;
                for (int j = 0; j < nDetector; j++)
                {
                    RandomDetector(newNeurone);
                }
                _neurones.Add(newNeurone);
            }
        }
        public void RegenerateBrain()
        {
            // Vider la liste des neurones
            _neurones.Clear();

            // Générer de nouveaux neurones et détecteurs
            GenerateFromZero();
        }
        public void FirstBrain(GenerationResult generationResult)
        {
            // Récupérer le meilleur GameResult
            GameResult bestGameResult = generationResult.BestGameResult;

            // Vider la liste des neurones
            _neurones.Clear();
            _bestNeurones.Clear(); // Vider aussi la liste des meilleurs neurones

            // Générer de nouveaux neurones à partir du meilleur GameResult
            foreach (SimpleNeurone simpleNeurone in bestGameResult.Neurones)
            {
                Neurone neurone = new Neurone(playerPosition, neuroneTexture);
                foreach (SimpleDetector simpleDetector in simpleNeurone.Detectors)
                {
                    Detector detector = new Detector(playerPosition, simpleDetector.PositionAbso, detectorTexture, simpleDetector.Type);
                    neurone.GetDetector.Add(detector);
                }
                Neurone neuroneCopy = neurone.Copy();  // Créer une copie du neurone pour _neurones et _bestNeurones
                _neurones.Add(neuroneCopy);
                _bestNeurones.Add(neuroneCopy);
            }
        }


        public void BrainFromFirst()
        {
            // Effacer la liste des neurones actuels
            _neurones.Clear();
            foreach (Neurone bestNeurone in _bestNeurones)
            {
                _neurones.Add(bestNeurone.Copy());
            }
            double bias = 2.0; // Augmentez cette valeur pour rendre les nombres plus grands encore plus rares.
            double randDouble = Math.Pow(rand.NextDouble(), bias);
            int nModif = (int)(randDouble * 19) + 1; // 10 parce que nous avons besoin d'une plage de 0 à 9.
            Random random = new Random();
            for (int i = 0; i < nModif; i++)
            {
                int randomNum = random.Next(0, 3);
                switch (randomNum) {
                    case 0:
                        if (_neurones.Count > 0) // Vérifie qu'il y a des neurones à modifier
                        {
                            int randomNeuronIndex = random.Next(0, _neurones.Count); // Choisit un neurone au hasard
                            Neurone neuron = _neurones[randomNeuronIndex];

                            if (neuron.GetDetector.Count > 0) // Vérifie qu'il y a des détecteurs à supprimer
                            {
                                int randomDetectorIndex = random.Next(0, neuron.GetDetector.Count); // Choisit un détecteur au hasard
                                neuron.GetDetector.RemoveAt(randomDetectorIndex); // Supprime le détecteur

                                if (neuron.GetDetector.Count == 0) // Si le neurone n'a plus de détecteurs
                                {
                                    _neurones.RemoveAt(randomNeuronIndex); // Supprime le neurone
                                }
                            }
                        }
                        break;
                    case 1:
                        if (_neurones.Count > 0) // Vérifie qu'il y a des neurones à modifier
                        {
                            int randomNeuronIndex = random.Next(0, _neurones.Count); // Choisit un neurone au hasard
                            Neurone neuron = _neurones[randomNeuronIndex];

                            // Ajoute un nouveau détecteur au neurone
                            RandomDetector(neuron);
                        }
                        break;
                    case 2:
                        Neurone newNeurone = new Neurone(playerPosition, neuroneTexture);
                        randDouble = Math.Pow(rand.NextDouble(), bias);
                        int nDetector = (int)(randDouble * 4) + 1;
                        for (int j = 0; j < nDetector; j++)
                        {
                            RandomDetector(newNeurone);
                        }
                        _neurones.Add(newNeurone);
                        break;
                    default:
                        break;
                }
            }

        }
        public List<Neurone> GetNeurones()
        {
            return _neurones;
        }
    }
}
/*
            Neurone newNeurone;
            newNeurone = new Neurone(playerPosition, neuroneTexture);
            newNeurone.AddDetector(new Detector(playerPosition, new Vector2(detectorX + 50, detectorY + 50), detectorTexture, Detector.DetectorType.Pick));
            _neurones.Add(newNeurone);
            newNeurone = new Neurone(playerPosition, neuroneTexture);
            newNeurone.AddDetector(new Detector(playerPosition, new Vector2(detectorX + 75, detectorY + 0), detectorTexture, Detector.DetectorType.Pick));
            _neurones.Add(newNeurone);
            newNeurone = new Neurone(playerPosition, neuroneTexture);
            newNeurone.AddDetector(new Detector(playerPosition, new Vector2(detectorX + 100, detectorY + 0), detectorTexture, Detector.DetectorType.Block));
            newNeurone.AddDetector(new Detector(playerPosition, new Vector2(detectorX + 0, detectorY - 50), detectorTexture, Detector.DetectorType.NonPick));
            _neurones.Add(newNeurone);
            newNeurone = new Neurone(playerPosition, neuroneTexture);
            newNeurone.AddDetector(new Detector(playerPosition, new Vector2(detectorX + 100, detectorY + 50), detectorTexture, Detector.DetectorType.Empty));
            _neurones.Add(newNeurone);

            newNeurone = new Neurone(playerPosition, neuroneTexture);
            newNeurone.AddDetector(new Detector(playerPosition, new Vector2(detectorX + 200, detectorY + 0), detectorTexture, Detector.DetectorType.Block));
            _neurones.Add(newNeurone);
            */

