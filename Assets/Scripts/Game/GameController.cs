using UnityEngine;

using System;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	private const string _highScorePrefsKey = "HighScore";

	private StateMachine _stateMachine;

	private PlayerHUDMediator _playerHud;

	private EnemyFactory _enemyFactory;

	public PlayerController Player { get; private set; }

	public Transform GameObjectsContainer { get; private set; }

	public int CurrentDifficulty { get; private set; }

	public bool IsPaused { get; private set; }

	public static GameController Create(Transform container)
	{
		GameController controller = new GameObject("GameController").AddComponent<GameController>(); 
		controller.GameObjectsContainer = container;
		return controller;
	}

	private void Start()
	{
		Player = PlayerController.Create();
		AppManager.PlayerData.Score.Changed += OnScoreChanged;
		CurrentDifficulty = 1;
		_enemyFactory = new EnemyFactory();
		_stateMachine = new StateMachine(new GameSceneStatesMap(), GameSceneStatesMap.Start, this);
	}

	private void OnScoreChanged(object sender, ExtendedInteger.ExtendedIntegerEventArgs args)
	{
		int difficulty = AppManager.PlayerData.Score.Value / GameSettings.DifficultyLevelRaisePoints + 1;
		if (difficulty != CurrentDifficulty)
		{
			AppManager.Audio.PlaySoundEffect(AudioClipsNames.DifficultyRaised);
			CurrentDifficulty = difficulty;
		}

		if (AppManager.PlayerData.HighScore >= AppManager.PlayerData.Score)
		{
			return;
		}

		AppManager.PlayerData.HighScore.Value = AppManager.PlayerData.Score.Value;
		PlayerPrefs.SetInt(_highScorePrefsKey, AppManager.PlayerData.HighScore);
	}

	private void Update()
	{
		_stateMachine.Update();
	}

	public void ShowPlayer(bool value)
	{
		Player.gameObject.SetActive(value);
	}

	public void GenerateEnemy()
	{
		_enemyFactory.Create();
	}

	public void ResetPlayerData()
	{
		AppManager.PlayerData.Score.Value = 0;
		AppManager.PlayerData.HighScore.Value = PlayerPrefs.GetInt(_highScorePrefsKey, 0);
		AppManager.PlayerData.Lifes.Value = GameSettings.StartLifes;
	}

	public void ShowHUD(bool value)
	{
		if (value)
		{
			_playerHud = AppManager.GUI.ShowWindow<PlayerHUDMediator>();
			_playerHud.PauseButtonClicked += OnPauseButtonClicked;
		}
		else
		{
			_playerHud.Close();
		}
	}

	private void OnPauseButtonClicked(object sender, EventArgs args)
	{
		IsPaused = !IsPaused;
		Time.timeScale = IsPaused ? 0 : 1;
	}
}

public class GameSceneStatesMap : Dictionary<string, Type>
{
	public const string Start = "Start";
	public const string Play = "Play";
	public const string Lost = "Lost";

	public GameSceneStatesMap()
	{
		Add(Start, typeof (GameSceneStartState));
		Add(Play, typeof (GameScenePlayState));
		Add(Lost, typeof (GameSceneLostState));
	}
}
