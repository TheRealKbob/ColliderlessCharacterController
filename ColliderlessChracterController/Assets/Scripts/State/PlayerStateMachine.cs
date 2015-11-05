using UnityEngine;
using System.Collections;

public class PlayerStateMachine : StateMachine {

	public LocomotionController Controller{ get; private set; }

	public enum PlayerStateType
	{
		IDLE,
		MOVING,
		FALLING,
		JUMPING
	}

	void Start()
	{

		Controller = gameObject.GetComponent<LocomotionController>();

		Controller.OnLocomotionEvent += HandleLocomotionEvent;

		addState( PlayerStateType.IDLE, new PlayerIdleState( this ) );
		addState( PlayerStateType.MOVING, new PlayerMoveState( this ) );
		addState( PlayerStateType.FALLING, new PlayerFallState( this ) );
		addState( PlayerStateType.JUMPING, new PlayerJumpState( this ) );

		CurrentState = PlayerStateType.IDLE;
	}

	private void HandleLocomotionEvent( string eventID )
	{
		(state as PlayerState).HandleLocomotionEvent( eventID );
	}

}

public class PlayerState : State
{

	protected PlayerStateMachine machine;

	public PlayerState( PlayerStateMachine machine )
	{
		this.machine = machine;
	}

	public virtual void HandleLocomotionEvent( string eventID ){}

}
