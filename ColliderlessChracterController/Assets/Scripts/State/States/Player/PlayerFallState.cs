using UnityEngine;
using System.Collections;

public class PlayerFallState : PlayerState {

	public PlayerFallState( PlayerStateMachine machine ) : base( machine ){}

	public override void DoEnterState()
	{
		Debug.Log( "Entering Fall State" );
	}

	public override void DoUpdate()
	{

		if( machine.Controller.AquiredGround() )
		{
			machine.CurrentState = PlayerStateMachine.PlayerStateType.IDLE;
			return;
		}

		machine.Controller.AddGravity();
	}

	public override void DoExitState()
	{
		Debug.Log( "Exiting Fall State" );
	}

	public override void HandleLocomotionEvent( string eventID )
	{
		machine.CurrentState = PlayerStateMachine.PlayerStateType.IDLE;
	}

}
