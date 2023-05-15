using System;

namespace Project1
{
    // Classe Program contenant la méthode d'entrée principale de l'application
    public static class Program
    {
        // Attribut STAThread nécessaire pour utiliser certaines fonctionnalités Windows, telles que les boîtes de dialogue
        [STAThread]
        // Méthode d'entrée principale de l'application
        static void Main()
        {
            // Créer une nouvelle instance du jeu et l'exécuter
            using (var game = new Game1())
                game.Run();
        }
    }
}
