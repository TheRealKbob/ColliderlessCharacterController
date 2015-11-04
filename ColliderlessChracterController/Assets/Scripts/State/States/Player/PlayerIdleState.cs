using UnityEngine;
using System.Collections;

public class PlayerIdleState : PlayerState {

	public PlayerIdleState( PlayerStateMachine machine ) : base( machine ){}

	public override void DoUpdate()
	{
		if( !machine.Controller.MaintainingGround() )
		{
			Debug.Log("switching to fall");
			machine.CurrentState = PlayerStateMachine.PlayerStateType.FALLING;
		}
	}

}
