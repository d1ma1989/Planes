using UnityEngine;

public class GameSceneStartState : BaseState<GameController>
{
	private StartWindowMediator _startWindow;

	public GameSceneStartState(GameController context) : base(context) { }

	public override void Enter()
	{
		_startWindow = AppManager.GUI.ShowWindow<StartWindowMediator>();
		NextState = GameSceneStatesMap.Play;
		AppManager.Audio.PlayAmbientMusic(AudioClipsNames.StartMusic);
	}

	public override bool Execute()
	{
		return Input.GetKeyUp(KeyCode.Space);
	}

	public override void Exit()
	{
		_startWindow.Close();
	}
}
