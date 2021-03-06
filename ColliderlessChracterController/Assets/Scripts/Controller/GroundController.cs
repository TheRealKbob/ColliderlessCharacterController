﻿using UnityEngine;
using System.Collections;

public class GroundController {

	private LocomotionController controller;
	private LayerMask walkable;

	public GroundHit Ground;

	public GroundController( LocomotionController controller, LayerMask walkable )
	{
		this.controller = controller;
		this.walkable = walkable;
	}

	public bool IsGrounded()
	{
		Vector3 o = controller.Position + new Vector3(0, 3, 0);

		RaycastHit hit;
		if( Physics.SphereCast(o, controller.Radius, controller.Down, out hit, Mathf.Infinity, walkable) )
		{
			DebugDraw.DrawMarker(o, controller.Radius, Color.green, 0f, false);
			DebugDraw.DrawMarker(hit.point, controller.Radius, Color.red, 0f, false);
			Vector3 p = Vector3.MoveTowards(hit.point, o, controller.Radius);

			controller.dp = p;

			if( controller.Position.y <= p.y )
			{
				Ground = new GroundHit( p );
				return true;
			}
			else
			{
				clearGround();
				return false;
			}
		}
		controller.dp = Vector3.zero;
		clearGround();
		return false;

	}

	public void Probe()
	{
		
	}

	private void clearGround()
	{
		Ground = null;
	}

}

public class GroundHit
{

	private Vector3 point;
	public Vector3 Point{ get{ return point; } }

	public GroundHit( Vector3 point )
	{
		this.point = point;
	}
}