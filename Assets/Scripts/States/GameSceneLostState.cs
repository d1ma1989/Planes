using UnityEngine;

public class GameSceneLostState : BaseState<GameController>
{
	private GameOverWindowMediator _gameOverWindow;

	public GameSceneLostState(GameController context) : base(context) { }

	public override void Enter()
	{
		_gameOverWindow = AppManager.GUI.ShowWindow<GameOverWindowMediator>();
		NextState = GameSceneStatesMap.Play;
		AppManager.Audio.PlayAmbientMusic(AudioClipsNames.GameOverMusic);
	}

	public override bool Execute()
	{
		return Input.GetKeyUp(KeyCode.Space);
	}

	public override void Exit()
	{
		_gameOverWindow.Close();
	}
}
