using Project1.Entities;
using Project1.NeuralNetwork;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

internal class SimpleNeurone
{
    public List<SimpleDetector> Detectors { get; set; }
}

internal class SimpleDetector
{
    public Detector.DetectorType Type { get; set; }
    public Vector2 PositionAbso { get; set; }
}

internal class GameResult
{
    public float Score { get; set; }
    public List<SimpleNeurone> Neurones { get; set; }
    public GameResult() { }
    public GameResult(List<Neurone> neurones, float score)
    {
        Score = score;
        Neurones = new List<SimpleNeurone>();

        foreach (Neurone originalNeurone in neurones)
        {
            SimpleNeurone copiedNeurone = new SimpleNeurone
            {
                Detectors = new List<SimpleDetector>()
            };

            foreach (Detector originalDetector in originalNeurone.GetDetector)
            {
                SimpleDetector copiedDetector = new SimpleDetector
                {
                    Type = originalDetector.Type,
                    PositionAbso = originalDetector.PositionAbso
                };

                copiedNeurone.Detectors.Add(copiedDetector);
            }

            Neurones.Add(copiedNeurone);
        }
    }
}
