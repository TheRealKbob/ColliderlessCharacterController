using UnityEngine;
using System.Collections;

public class GroundController {

	private LocomotionController controller;
	private LayerMask walkable;

	public GroundController( LocomotionController controller, LayerMask walkable )
	{
		this.controller = controller;
		this.walkable = walkable;
	}

	public bool IsGrounded( Vector3 origin )
	{
		return false;
	}

	public void Probe()
	{
		
	}

}
