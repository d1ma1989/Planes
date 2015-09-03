using UnityEngine;

using System.Collections.Generic;

public class PoolManager
{
	private static Transform _container;

	private static readonly Dictionary<string, Queue<GameObject>> _pool = new Dictionary<string, Queue<GameObject>>();
	private static readonly Dictionary<string,GameObject> _loadedPrefabs = new Dictionary<string, GameObject>(); 

	public static PoolManager Create(Transform container)
	{
		PoolManager manager = new PoolManager();
		_container = container;
		return manager;
	}

	public Transform Spawn(string prefabName, Transform parent, Vector3 position)
	{
		Transform instanceTransform;
		
		if (_pool.ContainsKey(prefabName))
		{
			if (_pool[prefabName].Count > 0)
			{
				GameObject go = _pool[prefabName].Dequeue();
				instanceTransform = go.transform;
			}
			else
			{
				instanceTransform = GameObject.Instantiate(_loadedPrefabs[prefabName]).transform;
				instanceTransform.name = prefabName;
			}
		}
		else
		{
			GameObject prefab = Resources.Load<GameObject>(prefabName);
			instanceTransform = GameObject.Instantiate(prefab).transform;
			instanceTransform.name = prefabName;
			_pool[prefabName] = new Queue<GameObject>();
			_loadedPrefabs[prefabName] = prefab;
		}
		
		instanceTransform.parent = parent;
		instanceTransform.localPosition = Vector3.zero;
		instanceTransform.localScale = Vector3.one;
		instanceTransform.localPosition = position;
		instanceTransform.gameObject.SetActive(true);

		return instanceTransform;
	}

	public void Despawn(Transform gObj)
	{
		gObj.gameObject.SetActive(false);
		gObj.parent = _container;
		_pool[gObj.name].Enqueue(gObj.gameObject);
	}
}
