using System;

public class ExtendedInteger
{
	public EventHandler<ExtendedIntegerEventArgs> Changed;

	private int _currentValue;

	public int Value
	{
		get
		{
			return _currentValue;
		}

		set
		{
			int oldValue = _currentValue;
			_currentValue = value;
			if (Changed != null)
			{
				Changed(this, new ExtendedIntegerEventArgs(oldValue, _currentValue));
			}
		}
	}

	public ExtendedInteger() : this(0) { }

	public ExtendedInteger(int value)
	{
		Value = value;
	}

	public static implicit operator int(ExtendedInteger currentInstance)
	{
		return currentInstance.Value;
	}

	public static ExtendedInteger operator +(ExtendedInteger currentInstance, int value)
	{
		currentInstance.Value += value;
		return currentInstance;
	}

	public static bool operator >=(ExtendedInteger first, ExtendedInteger second)
	{
		return first.Value >= second.Value;
	}

	public static bool operator <=(ExtendedInteger first, ExtendedInteger second)
	{
		return first.Value <= second.Value;
	}

	public class ExtendedIntegerEventArgs : EventArgs
	{
		public int OldValue { get; private set; }
		public int NewValue { get; private set; }

		public ExtendedIntegerEventArgs(int oldValue, int newValue)
		{
			OldValue = oldValue;
			NewValue = newValue;
		}
	}
}