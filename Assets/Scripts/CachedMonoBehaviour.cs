using UnityEngine;

public abstract class CachedMonoBehaviour : MonoBehaviour
{
	private Transform _cachedTransform;
	private BoxCollider2D _cachedCollider;

	public Transform CachedTransform
	{
		get { return _cachedTransform != null ? _cachedTransform : _cachedTransform = transform; }
	}

	public BoxCollider2D CachedCollider
	{
		get { return _cachedCollider != null ? _cachedCollider : _cachedCollider = GetComponent<BoxCollider2D>(); }
	}
}
