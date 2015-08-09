using UnityEngine;

public abstract class AbstractState<T>
{
	public T parent { set; get; }

	public abstract void OnEnter();
	public abstract void OnUpdate();
	public abstract void OnExit();
}

public abstract class AbstractStateAbstractOnEnter<T> : AbstractState<T>
{
	public override void OnUpdate()
	{
	}

	public override void OnExit()
	{
	}
}

public class StateMachine<T>
{
	T _parent;
	AbstractState<T> _stateGlobal;
	AbstractState<T> _stateCurrent;
	AbstractState<T> _statePrevious;

	public StateMachine(T parent, AbstractState<T> state)
	{
		_parent = parent;

		ChangeState(state);
	}

	public void OnUpdate()
	{
		if (_stateGlobal != null)
			_stateGlobal.OnUpdate();

		_stateCurrent.OnUpdate();
	}

	public void ChangeState(AbstractState<T> state)
	{
		if (_statePrevious != null)
			_statePrevious.OnExit();

		if (_stateCurrent != null)
			_stateCurrent.OnExit();

		_statePrevious = _stateCurrent;
		_stateCurrent = state;
		_stateCurrent.parent = _parent;

		state.OnEnter();
	}

	public void RevertState()
	{
		Debug.Assert(_statePrevious != null);

		_statePrevious.OnExit();
		_statePrevious.OnEnter();
		_stateCurrent = _statePrevious;
	}

	public void SetGlobalState(AbstractState<T> state)
	{
		_stateGlobal = state;
		_stateGlobal.parent = _parent;
	}
}
