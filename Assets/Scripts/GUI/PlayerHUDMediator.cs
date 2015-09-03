using UnityEngine;

using System;

public class PlayerHUDMediator : GUIMediatorBase
{
	[SerializeField] private UILabel _pauseButtonLabel;
	[SerializeField] private UILabel _highScoreLabel;
	[SerializeField] private UILabel _scoreLabel;
	[SerializeField] private UILabel _lifesLabel;

	public event EventHandler PauseButtonClicked;

	private void Awake()
	{
		SetHighScores(AppManager.PlayerData.HighScore);
		SetScore(AppManager.PlayerData.Score);
		SetLifes(AppManager.PlayerData.Lifes);

		AppManager.PlayerData.HighScore.Changed += (sender, args) => SetHighScores(args.NewValue);
		AppManager.PlayerData.Score.Changed += (sender, args) => SetScore(args.NewValue);
		AppManager.PlayerData.Lifes.Changed += (sender, args) => SetLifes(args.NewValue);
	}

	private void SetHighScores(int value)
	{
		_highScoreLabel.text = string.Format("High Scores: {0}", value);
	}

	private void SetScore(int value)
	{
		_scoreLabel.text = string.Format("Scores: {0}", value);
	}

	private void SetLifes(int value)
	{
		_lifesLabel.text = string.Format("Lifes: {0}", value);
	}

	private void OnPauseButtonClick()
	{
		if (PauseButtonClicked != null)
		{
			PauseButtonClicked(this, EventArgs.Empty);
		}

		_pauseButtonLabel.text = AppManager.Game.IsPaused ? "Resume" : "Pause";
	}
}
