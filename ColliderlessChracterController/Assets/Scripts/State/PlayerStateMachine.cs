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

		addState( PlayerStateType.IDLE, new PlayerIdleState( this ) );
		addState( PlayerStateType.MOVING, new PlayerMoveState( this ) );
		addState( PlayerStateType.FALLING, new PlayerFallState( this ) );
		addState( PlayerStateType.JUMPING, new PlayerJumpState( this ) );

		CurrentState = PlayerStateType.IDLE;
	}

}

public class PlayerState : State
{

	protected PlayerStateMachine machine;

	public PlayerState( PlayerStateMachine machine )
	{
		this.machine = machine;
	}
}
