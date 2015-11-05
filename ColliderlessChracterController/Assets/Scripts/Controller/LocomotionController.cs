using UnityEngine;
using System.Collections;

[RequireComponent ( typeof( BroadcastUpdate ) )]
public class LocomotionController : MonoBehaviour {

	public delegate void LocomotionEvent( string eventID );
	public LocomotionEvent OnLocomotionEvent{ get; set; }

	[SerializeField]
	private float radius = 0.5f;
	public float Radius{ get{ return radius; } }

	[SerializeField]
	private LayerMask walkable;
	public LayerMask Walkable{ get{ return walkable; } }

	public Vector3 Up{ get{ return transform.up; } }
	public Vector3 Down{ get{ return -transform.up; } }

	private GroundController groundController;

	private Vector3 moveDirection = Vector3.zero;
	private Vector3 previousPosition;

	public bool contact;

	void Awake()
	{
		Time.timeScale = 1f;
		groundController = new GroundController( this, Walkable );
	}
	
	public void OnUpdate()
	{
		recursivePushback();
		transform.position += moveDirection * Time.deltaTime;
	}

	public void AddGravity()
	{
		moveDirection += Down * WorldProperties.Gravity * Time.deltaTime;
	}

	private void recursivePushback()
	{
		contact = false;
		foreach( Collider c in Physics.OverlapSphere( transform.position, radius, walkable ) )
		{
			contact = true;

			if( OnLocomotionEvent != null )
				OnLocomotionEvent( LocomotionEvents.ENTER_GROUND );

			Vector3 contactPoint = c.ClosestPointOnBounds( transform.position );

			Vector3 v = transform.position - contactPoint;

			transform.position += Vector3.ClampMagnitude( v, Mathf.Clamp( radius - v.magnitude, 0, radius ) );

			Vector3 movementThisStep = transform.position - previousPosition;
			float movementSqrMagnitude = movementThisStep.sqrMagnitude;
			float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);

			RaycastHit hit;
			if (Physics.Raycast( previousPosition, movementThisStep, out hit, movementMagnitude, walkable ) )
			{
				if (hit.collider) Debug.Log( "Passed Through" );

				Vector3 pt = transform.position - hit.point;
				transform.position = hit.point - (movementThisStep / movementMagnitude) * radius;
				recursivePushback();
			}

			previousPosition = transform.position;

			DebugDraw.DrawMarker( contactPoint, 0.5f, Color.red, 0.0f, false );
			
		}
	}

#region Ground Checking

	public bool MaintainingGround()
	{
		return groundController.IsGrounded( transform.position );
	}

#endregion

}

public static class LocomotionEvents
{
	public static string ENTER_GROUND = "ENTER_GROUND";
}
