using UnityEngine;

using System;
using System.Collections.Generic;

public class GUIFactory
{
	private readonly Dictionary<Type, string> _mediatorsPrefabsMap = new Dictionary<Type, string>
	{
		{ typeof (StartWindowMediator), "StartWindow" },
		{ typeof (GameOverWindowMediator), "GameOverWindow" },
		{ typeof (PlayerHUDMediator), "PlayerHUDWindow" }
	};

	public T Create<T>()
	{
		string prefabName = _mediatorsPrefabsMap[typeof (T)];
		GameObject prefab = Resources.Load<GameObject>(prefabName);
		GameObject windowGO = NGUITools.AddChild(UICamera.first.gameObject, prefab);
		return windowGO.GetComponent<T>();
	}
}
