using UnityEngine;

public class GUIMediatorBase : MonoBehaviour
{
	public void Close()
	{
		Destroy(gameObject);
	}
}
