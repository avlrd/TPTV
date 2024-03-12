using MorpionApp;

public class TestsMorpion
{
	[Fact]
	public void TestConstructor()
	{
		Morpion morpion = new Morpion();

		Assert.NotNull(morpion.grille);
	}

	[Fact]
	public void TestVerifEgalite()
	{
		Morpion morpion = new Morpion();

		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				morpion.grille[i, j] = 'X';
			}
		}

		bool result = morpion.verifEgalite();

		Assert.True(result);
	}
}