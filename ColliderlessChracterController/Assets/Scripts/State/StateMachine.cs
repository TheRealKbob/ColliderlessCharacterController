using UnityEngine;
using System;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour {

	private Dictionary<Enum, IState> stateMap = new Dictionary<Enum, IState>();

	protected IState state = new State();

	public Enum CurrentState
	{
		get
		{
			return state.ID;
		}
		set
		{
			if( state.ID == value )
				return;

			configureNewState( value );
		}
	}

	public Enum LastState;

	void Awake()
	{
		
	}
	
	private void configureNewState( Enum newState )
	{
		state.DoExitState();
		LastState = state.ID;
		state = getState( newState );
		state.ID = newState;
		state.DoEnterState();
	}

	protected void addState( Enum stateID, IState state )
	{
		stateMap.Add( stateID, state );
	}

	protected void removeState( Enum stateID )
	{
		stateMap.Remove( stateID );
	}

	protected IState getState( Enum stateID )
	{
		if( stateMap.ContainsKey( stateID ) )
		{	
			IState s = stateMap[stateID];
			return s;
		}
		return null;
	}

	public void OnUpdate()
	{
		PreUpdate();
		if( state != null ) state.DoUpdate();
		PostUpdate();
	}

	protected virtual void PreUpdate(){}
	protected virtual void PostUpdate(){}

}

public interface IState
{
	Enum ID{ get; set; }
	void DoEnterState();
	void DoUpdate();
	void DoExitState();
}

public class State : IState
{

	public Enum ID{ get; set; }
	public virtual void DoEnterState(){}
	public virtual void DoUpdate(){}
	public virtual void DoExitState(){}

}
