using UnityEngine;

public class EnemyFactory
{
	private const int _enemyHeight = 52;
	private const int _halfEnemyWidth = 32;

	private static Vector3 _spawnPos;

	public void Create()
	{
		float maxXPos = AppManager.GUI.MaxXPos - _halfEnemyWidth;
		float xPos = Random.Range(-maxXPos, maxXPos);
		float yPos = AppManager.GUI.MaxYPos + _enemyHeight;

		_spawnPos.Set(xPos, yPos, 0f);

		const string enemyPrefabName = "Enemy";
		AppManager.Pool.Spawn(enemyPrefabName, AppManager.Game.GameObjectsContainer, _spawnPos);
	}
}
