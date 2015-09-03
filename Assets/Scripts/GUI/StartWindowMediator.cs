using UnityEngine;

using System.Collections;

public class StartWindowMediator : GUIMediatorBase
{
	[SerializeField] private UILabel _label;

	private IEnumerator Start()
	{
		const float blinkDelayTime = 0.5f;
		while (true)
		{
			yield return new WaitForSeconds(blinkDelayTime);
			_label.enabled = !_label.enabled;
		}
	}
}
