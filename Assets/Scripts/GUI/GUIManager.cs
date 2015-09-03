using UnityEngine;

public class GUIManager : MonoBehaviour
{
	private static UIRoot _uiRoot;
	private static GUIFactory _guiFactory;

	private int _screenHeight;
	private int _screenWidth;

	// Максимальные координаты X и Y в зависимости от размера экрана
	public float MaxYPos { get; private set; }
	public float MaxXPos { get; private set; }

	public static GUIManager Create(UIRoot uiRoot)
	{
		_uiRoot = uiRoot;
		_guiFactory = new GUIFactory();
		GUIManager manager = new GameObject("GUIManager").AddComponent<GUIManager>();
		manager.CheckResolution();
		return manager;
	}

	private void CheckResolution()
	{
		if (_screenHeight != Screen.height || _screenWidth != Screen.width)
		{
			_screenHeight = Screen.height;
			_screenWidth = Screen.width;
			MaxYPos = _uiRoot.activeHeight * 0.5f;
			MaxXPos = _uiRoot.GetPixelSizeAdjustment(Screen.height) * Screen.width * 0.5f;
		}
	}

	public T ShowWindow<T>()
	{
		return _guiFactory.Create<T>();
	}

	private void Update()
	{
		CheckResolution();
	}
}
