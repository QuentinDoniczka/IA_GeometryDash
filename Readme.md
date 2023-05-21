# Projet de Jeu avec Intelligence Artificielle bas�e sur des Neurones

## Description

Ce projet explore la mise en �uvre d'une forme d'intelligence artificielle (IA) pour un jeu, bas�e sur un algorithme de neurones con�u sur mesure. Dans ce syst�me, chaque IA du jeu est mod�lis�e comme une collection de neurones et de d�tecteurs. Ces �l�ments sont utilis�s par l'IA pour prendre des d�cisions et interagir avec le monde du jeu. Au fur et � mesure que l'IA �volue et apprend de ses exp�riences dans le jeu, des d�tecteurs peuvent �tre ajout�s ou retir�s � chaque neurone.

Au fil du temps, le 'cerveau' de l'IA change gr�ce � un processus de modification qui tend � ajouter de nouveaux d�tecteurs et neurones. Cela entra�ne des comportements de plus en plus sophistiqu�s et diversifi�s. Dans certains cas, un neurone ou un d�tecteur peut �tre retir� al�atoirement, introduisant ainsi une variabilit� suppl�mentaire et pr�servant la dynamique de l'�volution.

## Axes d'Am�lioration et Liste de T�ches

Bien que le projet soit d�j� fonctionnel, il reste encore plusieurs aspects � am�liorer :

1. **Optimisation :** Le code actuel contient une boucle de complexit� O(n^3) qui pourrait �tre simplifi�e en une constante en utilisant une table de hachage.

2. **Gestion de la m�moire :** Lors de tests de performance par lots de 1000, toute variable inutilement appel�e ou stock�e peut l'�tre 10 000 fois. Une attention particuli�re doit donc �tre port�e � la gestion de la m�moire pour �viter d'�ventuelles fuites.

3. **D�veloppement Front-End :** Le projet s'est principalement concentr� sur le d�veloppement Back-End. Le Front-End pourrait �tre am�lior� pour rendre le jeu plus attractif et convivial pour les utilisateurs.

4. **Am�lioration continue de l'IA :** L'IA pourrait b�n�ficier d'am�liorations suppl�mentaires et de tests suppl�mentaires pour am�liorer la jouabilit� et le r�alisme du jeu.

5. **R�partition des Classes et Modularit� :** La classe principale actuelle est assez volumineuse, avec plus de 500 lignes de code. Certaines de ses m�thodes, comme 'update', pourraient �tre d�plac�es vers des classes distinctes. De plus, les variables d'environnement, actuellement trop nombreuses dans la classe principale, pourraient �tre r�parties de mani�re plus efficace.

## Utilisation Future

� l'avenir, cette IA pourrait �tre utilis�e pour des applications comme le d�veloppement de niveaux, en testant si un niveau est finissable avant de le rendre disponible pour les joueurs.

## Contact

Pour toute question ou suggestion concernant le projet, n'h�sitez pas � me contacter � doniczka.quentin67@gmail.com.
