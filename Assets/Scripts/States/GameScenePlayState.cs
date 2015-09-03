using UnityEngine;

public class GameScenePlayState : BaseState<GameController>
{
	private float _timer;

	public GameScenePlayState(GameController context) : base(context) { }

	public override void Enter()
	{
		NextState = GameSceneStatesMap.Lost;
		Context.ResetPlayerData();
		Context.ShowHUD(true);
		Context.ShowPlayer(true);
		AppManager.Audio.PlayAmbientMusic(AudioClipsNames.MainTheme);
		ResetTimer();
	}

	private void ResetTimer()
	{
		_timer = 2f - Context.CurrentDifficulty * 0.1f;
	}

	public override bool Execute()
	{
		_timer -= Time.deltaTime;
		if (_timer <= 0)
		{
			Context.GenerateEnemy();
			ResetTimer();
		}

		return AppManager.PlayerData.Lifes == 0;
	}

	public override void Exit()
	{
		Context.ShowPlayer(false);
		Context.ShowHUD(false);
	}
}
