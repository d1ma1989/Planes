using UnityEngine;

public class EnemyController : CachedMonoBehaviour
{
	[SerializeField] private UISprite _sprite;

	private const float _speed = 2f;

	private bool _fired;

	private void Awake()
	{
		_sprite.depth++;
	}

	private void OnEnable()
	{
		_fired = false;
	}

	private void Update()
	{
		CachedTransform.Translate(Vector3.up * _speed * Time.deltaTime, Space.Self);

		// Make shot at player after passing 25% of screen
		if (!_fired && CachedTransform.localPosition.y < AppManager.GUI.MaxYPos * 0.5f + _sprite.height)
		{
			_fired = true;

			const string bulletPrefabName = "EnemyBullet";
			AppManager.Pool.Spawn(bulletPrefabName, AppManager.Game.GameObjectsContainer, CachedTransform.localPosition - Vector3.up * _sprite.height);
		}

		// Add enemy to pool after passing screen
		if (CachedTransform.localPosition.y < -AppManager.GUI.MaxYPos)
		{
			AppManager.Pool.Despawn(CachedTransform);
		}
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		const string playerbulletTag = "PlayerBullet";
		if (coll.tag != playerbulletTag)
		{
			return;
		}

		AppManager.Audio.PlaySoundEffect(AudioClipsNames.Explosion);
		AppManager.PlayerData.Score += GameSettings.DestroyingEnemyPoints;
		AppManager.Pool.Despawn(coll.transform);
		AppManager.Pool.Despawn(CachedTransform);
	}
}
