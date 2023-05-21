# Projet de Jeu avec Intelligence Artificielle basée sur des Neurones

## Description

Ce projet explore la mise en œuvre d'une forme d'intelligence artificielle (IA) pour un jeu, basée sur un algorithme de neurones conçu sur mesure. Dans ce système, chaque IA du jeu est modélisée comme une collection de neurones et de détecteurs. Ces éléments sont utilisés par l'IA pour prendre des décisions et interagir avec le monde du jeu. Au fur et à mesure que l'IA évolue et apprend de ses expériences dans le jeu, des détecteurs peuvent être ajoutés ou retirés à chaque neurone.

Au fil du temps, le 'cerveau' de l'IA change grâce à un processus de modification qui tend à ajouter de nouveaux détecteurs et neurones. Cela entraîne des comportements de plus en plus sophistiqués et diversifiés. Dans certains cas, un neurone ou un détecteur peut être retiré aléatoirement, introduisant ainsi une variabilité supplémentaire et préservant la dynamique de l'évolution.

## Axes d'Amélioration et Liste de Tâches

Bien que le projet soit déjà fonctionnel, il reste encore plusieurs aspects à améliorer :

1. **Optimisation :** Le code actuel contient une boucle de complexité O(n^3) qui pourrait être simplifiée en une constante en utilisant une table de hachage.

2. **Gestion de la mémoire :** Lors de tests de performance par lots de 1000, toute variable inutilement appelée ou stockée peut l'être 10 000 fois. Une attention particulière doit donc être portée à la gestion de la mémoire pour éviter d'éventuelles fuites.

3. **Développement Front-End :** Le projet s'est principalement concentré sur le développement Back-End. Le Front-End pourrait être amélioré pour rendre le jeu plus attractif et convivial pour les utilisateurs.

4. **Amélioration continue de l'IA :** L'IA pourrait bénéficier d'améliorations supplémentaires et de tests supplémentaires pour améliorer la jouabilité et le réalisme du jeu.

5. **Répartition des Classes et Modularité :** La classe principale actuelle est assez volumineuse, avec plus de 500 lignes de code. Certaines de ses méthodes, comme 'update', pourraient être déplacées vers des classes distinctes. De plus, les variables d'environnement, actuellement trop nombreuses dans la classe principale, pourraient être réparties de manière plus efficace.

## Utilisation Future

À l'avenir, cette IA pourrait être utilisée pour des applications comme le développement de niveaux, en testant si un niveau est finissable avant de le rendre disponible pour les joueurs.

## Contact

Pour toute question ou suggestion concernant le projet, n'hésitez pas à me contacter à doniczka.quentin67@gmail.com.
