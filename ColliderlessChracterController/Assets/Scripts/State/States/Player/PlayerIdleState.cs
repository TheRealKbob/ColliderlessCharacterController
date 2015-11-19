using UnityEngine;
using System.Collections;

public class PlayerIdleState : PlayerState {

	public PlayerIdleState( PlayerStateMachine machine ) : base( machine ){}

	public override void DoEnterState()
	{
		Debug.Log( "Entering Idle State" );
	}

	public override void DoUpdate()
	{
		if( !machine.Controller.MaintainGround() )
		{
			machine.CurrentState = PlayerStateMachine.PlayerStateType.FALLING;
		}
	}

	public override void DoExitState()
	{
		Debug.Log( "Exiting Idle State" );
	}

	public override void HandleLocomotionEvent( string eventID )
	{
		if( eventID == LocomotionEvents.EXIT_GROUND )
		{
			machine.CurrentState = PlayerStateMachine.PlayerStateType.FALLING;
			Debug.Log("falling?");
		}
	}

}
