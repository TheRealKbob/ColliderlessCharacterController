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

	private bool contact;

	void Awake()
	{
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
