using System;
using System.Collections.Generic;

public class StateMachine
{
	private readonly Dictionary<string, Type> _statesMap;
	private readonly object _context;

	public IState CurrentState { get; private set; }

	public StateMachine(Dictionary<string, Type> statesMap, string startState, object context)
	{
		_statesMap = statesMap;
		_context = context;
		ChangeState(startState);
	}

	public void Update()
	{
		bool stateFinished = CurrentState.Execute();
		if (!stateFinished)
		{
			return;
		}

		CurrentState.Exit();
		ChangeState(CurrentState.NextState);
	}

	private void ChangeState(string nextState)
	{
		Type stateType = _statesMap[nextState];
		CurrentState = (IState)Activator.CreateInstance(stateType, _context);
		CurrentState.Enter();
	}
}
