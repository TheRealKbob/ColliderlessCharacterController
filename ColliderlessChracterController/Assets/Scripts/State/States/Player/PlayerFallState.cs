using UnityEngine;
using System.Collections;

public class PlayerFallState : PlayerState {

	public PlayerFallState( PlayerStateMachine machine ) : base( machine ){}

	public override void DoUpdate()
	{
		Debug.Log("falling?");
		machine.Controller.AddGravity();
	}

}
