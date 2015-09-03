using UnityEngine;

using System.Collections;

public class PlayerController : CachedMonoBehaviour
{
	[SerializeField] private UISprite _sprite;

	private Vector3 _targetPos;

	private float _halfPlaneWidth;
	private float _halfPlaneHeight;

	public static PlayerController Create()
	{
		const string playerPrefabName = "Player";
		GameObject playerPrefab = Resources.Load<GameObject>(playerPrefabName);
		PlayerController player = NGUITools.AddChild(AppManager.Game.GameObjectsContainer.gameObject, playerPrefab).GetComponent<PlayerController>();
		player.gameObject.SetActive(false);
		return player;
	}

	private void Awake()
	{
		_halfPlaneHeight = _sprite.height * 0.5f;
		_halfPlaneWidth = _sprite.width * 0.5f;
	}

	private void OnEnable()
	{
		MakeImmortal();
	}

	// Makes plane immortal for 3 seconds
	private void MakeImmortal()
	{
		CachedCollider.enabled = false;
		StartCoroutine(EnableColliderRoutine());
		StartCoroutine(PlaneBlinkingRoutine());
	}

	private IEnumerator EnableColliderRoutine()
	{
		const float immortalTime = 3f;
		yield return new WaitForSeconds(immortalTime);
		CachedCollider.enabled = true;
		_sprite.enabled = true;
		StopAllCoroutines();
	}

	// Immortal visual effect
	private IEnumerator PlaneBlinkingRoutine()
	{
		const float blinkDelay = 0.25f;

		while (!CachedCollider.enabled)
		{
			_sprite.enabled = !_sprite.enabled;
			yield return new WaitForSeconds(blinkDelay);
		}
	}

	private void Update()
	{
		// Firing bullets
		if (Input.GetKeyUp(KeyCode.Space))
		{
			const string bulletPrefabName = "PlayerBullet";
			AppManager.Audio.PlaySoundEffect(AudioClipsNames.Shot);
			AppManager.Pool.Spawn(bulletPrefabName, AppManager.Game.GameObjectsContainer, CachedTransform.localPosition + Vector3.up * _halfPlaneHeight);
		}

		// Moving plane
		_targetPos = CachedTransform.localPosition;

		const float playerSpeed = 10f;

		float maxXPos = AppManager.GUI.MaxXPos - _halfPlaneWidth;
		float maxYPos = AppManager.GUI.MaxYPos - _halfPlaneHeight;

		if (Input.GetKey(KeyCode.DownArrow))
		{
			_targetPos.y -= playerSpeed;
			_targetPos.y = Mathf.Max(_targetPos.y, -maxYPos);
		}
		else if (Input.GetKey(KeyCode.UpArrow))
		{
			_targetPos.y += playerSpeed;
			_targetPos.y = Mathf.Min(_targetPos.y, maxYPos);
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			_targetPos.x += playerSpeed;
			_targetPos.x = Mathf.Min(_targetPos.x, maxXPos);
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			_targetPos.x -= playerSpeed;
			_targetPos.x = Mathf.Max(_targetPos.x, -maxXPos);
		}

		CachedTransform.localPosition = _targetPos;
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		const string enemyTag = "Enemy";
		if (coll.tag != enemyTag)
		{
			return;
		}

		AppManager.Pool.Despawn(coll.transform);
		AppManager.Audio.PlaySoundEffect(AudioClipsNames.Explosion);
		Died();
	}

	public void Died()
	{
		AppManager.PlayerData.Lifes.Value--;
		MakeImmortal();
	}
}
