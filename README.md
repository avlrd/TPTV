# TP - 1

## Introduction

Les seules choses à tester sont les constrcuteurs de chaque classe de jeu, ainsi que les méthodes verifVictoire et verifEgalite.

Pour modifier ceci, il faudrait refactorer énormément de choses et selon moi, il serait plus simple et moins couteux de totalement recommencer, pour un projet de cette taille.

## Commentaires

- L'usage de GOTO est déconseillé (clareté du code)
- Classe Morpion et Puissance 4 peuvent implémenter une classe abstraite Game
- Les méthodes de vérification sont très mal développées et ne fonctionnent pas entièrement, dans tous les cas.
- Le tour d'un joueur peut être intégré à une class Joueur qui permet également d'implémenter plus facilement et proprement un joueur "Ordinateur".
- Des classes permettant de gérer la grille de jeu seraient un plus non négligeable afin de garantir le typage et permettre des tests sur les valeurs des cellules. Cela simplifierait aussi le code et le rendrait plus clair.

## Refactoring

Voici les pistes à emprunter dans le cas où l'on souhaite garder ce qui fonctionne du code de base :

- Utiliser un système similaire aux scènes en développement de jeu vidéo :

On pourrait utiliser une classe Game abstraite qui est étendue par Morpion et Puissance4 et laisse la possibilité d'ajouter d'autres jeux dans le même genre dans le futur. L'intérêt de cette classe serait de rendre commun les champs utiles aux différentes fonctions du jeu comme par exemple les deux booléens évoqués précédemment et des méthodes comme tourDeJeu.

```cs

public abstract class Game
{
	private Grid Grid {get;}
	private List<Player> Players {get;}

	public abstract int TourDeJeu();
}

```

- Créer un classe joueur :

Cette classe permettrait, accompagnée d'un type CellValue pour les valeurs de chaque case, de rendre le tour du joueur réalisé par cette classe uniquement et donc de pouvoir créer une classe IA ou Ordi dont le comportement diffèrerait pour permettre de jouer contre l'ordinateur.

```cs

public class Player
{
	private string Name {get;}
	private CellValue Symbol {get;}

	public Player(string name, CellValue symbol)
	{
		Name = name;
		Symbol = symbol;
	}
}

```

Cette classe gèrerait le tour du joueur ce qui permet de réaliser les actions de façon globalisée dans la boucle de jeu car on accède à la lettre du joueur définie à la construction de l'objet.

- Créer une classe Grid, Cell et un enum CellValue

Ces classes permettent la gestion simplifiée et modulable de la grille de jeu, dont une instance est intégrée à la classe de jeu à sa construction.

```cs

public enum CellValue
{
	Empty = ' ',
	X = 'X',
	O = 'O'
}

public class Cell
{
	private int X {get;}
	private int Y {get;}
	private CellValue CellValue {get;}

	public Cell(int x, int y)
	{
		X = x;
		Y = y;
		CellValue = CellValue.Empty;
	}

	public int SetCellValue(CellValue value)
	{
		if(CellValue !== CellValue.Empty)
		{
			return -1;
		}
		else
		{
			CellValue = value;
			return 0;
		}
	}
}

public class Grid
{
	private List<List<Cell>> Cells {get;}
	private int RowsNumber {get;}
	private int ColumnsNumber {get;}

	public Grid(int rowsNumber, int columnsNumber)
	{
		RowsNumber = rowsNumber;
		ColumnsNumber = columnsNumber;

		CreateCells();
	}

	private void CreateCells()
	{
		Cells = new List<List<Cells>>();

		for(int i = 0; i < RowsNumber; i++)
		{
			List<Cell> list = new List<Cell>();
			for(int j = 0; j < ColumnsNumber; j++)
			{
				Cell cell = new Cell();
				list.Add(cell);
			}
			Cells.Add(list);
		}
	}
}

```

- Revoir le fonctionnement du main :

Avec ces modifications, on peut modifier la méthode main pour qu'elle adopte un comportement simple et généralisé en appelant premièrement une méthode de "setup" qui agit comme un menu pour demander combien de joueurs humain joueront, le jeu souhaité et gérer la rejouabilité. En instanciant un Game, et effectuant la boucle de jeu, le programme agit sur des objets dont les types varient en fonction des données renseignées par l'utilisateur dans la méthode de setup.

```cs
public static void Main(string[] args)
{
	Game game = Setup(); //retourne un new Morpion ou Puissance4

	while(replay)
	{
		game.Play();
	}
}

private void Play()
{
	while (!game.IsGameOver())
	{
		foreach(Player player in game.Players)
		{
			(int, int) move = player.GetMove();

			// Mise à jour de grille et vérif victoire
			game.Update(move, player.CellValue);

			game.Grid.Display();
		}
	}
	game.Result();
}
```

Le plus important selon moi est d'utiliser des champs et méthodes de vérification d'état de la partie afin de garantir le bon fonctionnement et tester les états et retours des méthodes à tout moment de la partie.

## Tests et validation

Ces modifications permettraient de tester l'ensemble des valeurs à tout moment de la vie du programme.

A tout moment une fois une partie lancée, on peut tester les getters des différents objets comme par exemple la valeur d'une cellule.

```cs

private void TestCellValueAfterPlayerMove()
{
	Game game = new Morpion();
	// ...
	Assert.True(game.grid.cell[x,y].CellValue === CellValue.X);
}

```

### Commentaire

Je n'ai pas réussi à organiser le refactoring comme souhaité et me suis emmêlé dans mes développements. J'ai donc préféré laisser le code comme tel et développer les commentaires concernant le refactoring et les exemples de code en espérant que vous verrez le travail de réflexion fourni.
