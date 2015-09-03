public abstract class BaseState<TContext> : IState
{
	protected readonly TContext Context;

	protected BaseState(TContext context)
	{
		Context = context;
	}

	public string NextState { get; protected set; }

	public abstract void Enter();

	public abstract bool Execute();

	public abstract void Exit();
}

public interface IState
{
	void Enter();

	bool Execute();

	void Exit();

	string NextState { get; }
}
