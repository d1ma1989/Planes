using UnityEngine;

using System;

public class EnemyBulletCollisionChecker : MonoBehaviour
{
	public event EventHandler Collided;

	private void OnTriggerEnter2D(Collider2D coll)
	{
		const string playerTag = "Player";
		if (coll.tag != playerTag)
		{
			return;
		}

		if (Collided != null)
		{
			Collided(this, EventArgs.Empty);
		}
	}
}
