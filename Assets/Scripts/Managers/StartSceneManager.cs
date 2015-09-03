using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
	[SerializeField] private UIRoot _uiRoot;
	[SerializeField] private AudioManager _audioManager;
	[SerializeField] private Transform _poolContainer;
	[SerializeField] private Transform _gameObjectsContainer;

	// Application entry point
	private void Awake()
	{
		AppManager.GUI = GUIManager.Create(_uiRoot);
		AppManager.Audio = _audioManager;
		AppManager.Pool = PoolManager.Create(_poolContainer);
		AppManager.PlayerData = new PlayerModel();
		AppManager.Game = GameController.Create(_gameObjectsContainer);
		Destroy(gameObject);
	}
}
