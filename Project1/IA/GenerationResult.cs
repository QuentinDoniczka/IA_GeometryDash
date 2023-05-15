using System.Collections.Generic;

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
            foreach (var gameResult in GameResults)
            {
                if (gameResult.Score > bestScore)
                {
                    bestScore = gameResult.Score;
                    bestResult = gameResult;
                }
            }
            return bestResult;
        }
    }
}
