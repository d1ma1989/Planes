using UnityEngine;

public class EnemyBulletController : CachedMonoBehaviour
{
	[SerializeField] private EnemyBulletCollisionChecker _collisionChecker;

	private static readonly Vector3 _bulletSpriteTargetAngle = new Vector3(0, 180f);

	private void Awake()
	{
		_collisionChecker.Collided += (sender, args) =>
		{
			AppManager.Pool.Despawn(CachedTransform);
			AppManager.Audio.PlaySoundEffect(AudioClipsNames.Explosion);
			AppManager.Game.Player.Died();
		};
	}

	private void OnEnable()
	{
		CachedTransform.LookAt(AppManager.Game.Player.CachedTransform);
		_collisionChecker.transform.eulerAngles = _bulletSpriteTargetAngle;
	}

	private void Update()
	{
		CachedTransform.Translate(Vector3.forward * Time.deltaTime, Space.Self);

		Vector3 pos = CachedTransform.localPosition;

		// Checks if bullet is out of screen
		if (pos.y < -AppManager.GUI.MaxYPos || pos.y > AppManager.GUI.MaxYPos || pos.x > AppManager.GUI.MaxXPos || pos.x < -AppManager.GUI.MaxXPos)
		{
			AppManager.Pool.Despawn(CachedTransform);
		}
	}
}
