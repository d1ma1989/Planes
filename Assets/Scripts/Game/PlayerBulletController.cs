using UnityEngine;

public class PlayerBulletController : CachedMonoBehaviour
{
	// Speed and moving direction of bullet
	private static readonly Vector3 _moveVector = new Vector3(0f, 25f);

	private void Update()
	{
		CachedTransform.localPosition += _moveVector;

		if (CachedTransform.localPosition.y > AppManager.GUI.MaxYPos)
		{
			AppManager.Pool.Despawn(CachedTransform);
		}
	}
}
