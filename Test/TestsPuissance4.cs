using MorpionApp;

public class TestsPuissance4
{
	[Fact]
	public void TestConstructor()
	{
		PuissanceQuatre p4 = new PuissanceQuatre();

		Assert.NotNull(p4.grille);
	}

	[Fact]
	public void TestVerifEgalite()
	{
		PuissanceQuatre p4 = new PuissanceQuatre();

		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 7; j++)
			{
				p4.grille[i, j] = 'X';
			}
		}

		bool result = p4.verifEgalite();

		Assert.True(result);
	}
}