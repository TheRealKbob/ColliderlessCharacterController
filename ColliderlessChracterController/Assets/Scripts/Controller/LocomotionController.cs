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
	private float height = 1f;
	public float Height{ get{ return height; } }

	[SerializeField]
	private LayerMask walkable;
	public LayerMask Walkable{ get{ return walkable; } }

	public Vector3 Position{ get{ return transform.position; } }
	public Vector3 Up{ get{ return transform.up; } }
	public Vector3 Down{ get{ return -transform.up; } }

	private GroundController groundController;

	private Vector3 moveDirection = Vector3.zero;
	private Vector3 previousPosition;

	private bool grounded = false;
	public bool groundedLastFrame = false;

	public float Velocity = 0;

	//Debug
	public Vector3 dp = Vector3.zero;

	void Awake()
	{
		Time.timeScale = 1f;
		groundController = new GroundController( this, Walkable );
	}
	
	public void OnUpdate()
	{
		Vector3 prevPos = transform.position;
		transform.position += moveDirection * Time.deltaTime;
		Velocity = (( transform.position - prevPos ) / Time.deltaTime).magnitude;
		groundedLastFrame = grounded;
	}

	public void AddGravity()
	{
		moveDirection += Down * WorldProperties.Gravity * Time.deltaTime;
	}

	public bool MaintainGround()
	{
		grounded = groundController.IsGrounded();
		if( grounded )
		{

			return true;
		}
		return false;
		//Debug.Log("Maintaining Ground");
	}

	public bool AquiredGround()
	{
		grounded = groundController.IsGrounded();
		if( grounded )
		{
			moveDirection = Vector3.zero;
			transform.position = new Vector3( transform.position.x, groundController.Ground.Point.y, transform.position.z );
			groundedLastFrame = true;
			return true;
		}
		return false;
	}

	void OnDrawGizmos() {
		if(dp != Vector3.zero)
		{
			Gizmos.color = Color.yellow;
	        Gizmos.DrawWireSphere(dp, Radius);
	    }
	}

}

public static class LocomotionEvents
{
	public static string ENTER_GROUND = "ENTER_GROUND";
	public static string EXIT_GROUND = "EXIT_GROUND";
}
