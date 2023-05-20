using System.Collections.Generic;
using System.Linq;

namespace Project1.IA
{
    internal class GenerationResult
    {
        public int GenerationId { get; set; }
        public List<GameResult> GameResults { get; set; }
        public GameResult BestGameResult { get; set; }

        public GenerationResult(int generationId, List<GameResult> gameResults)
        {
            GenerationId = generationId;
            GameResults = gameResults;
            BestGameResult = FindBestGameResult();
        }

        private GameResult FindBestGameResult()
        {
            GameResult bestResult = null;
            float bestScore = float.MinValue;
            int leastDetectorCount = int.MaxValue;  // Initialise à la plus grande valeur possible pour int

            foreach (var gameResult in GameResults)
            {
                int totalDetectors = gameResult.Neurones.Sum(n => n.Detectors.Count); // Compte le nombre total de détecteurs

                if (gameResult.Score > bestScore)
                {
                    bestScore = gameResult.Score;
                    bestResult = gameResult;
                    leastDetectorCount = totalDetectors; // Mise à jour du nombre de détecteurs pour le meilleur score
                }
                else if (gameResult.Score == bestScore) // Si les scores sont égaux
                {
                    if (totalDetectors < leastDetectorCount) // Compare le nombre de détecteurs
                    {
                        leastDetectorCount = totalDetectors;
                        bestResult = gameResult; // Met à jour le meilleur résultat si le nombre de détecteurs est inférieur
                    }
                }
            }
            return bestResult;
        }
    }
}
