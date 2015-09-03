public class PlayerModel
{
	public ExtendedInteger Score;
	public ExtendedInteger HighScore;
	public ExtendedInteger Lifes;

	public PlayerModel()
	{
		Score = new ExtendedInteger();
		HighScore = new ExtendedInteger();
		Lifes = new ExtendedInteger();
	}
}
