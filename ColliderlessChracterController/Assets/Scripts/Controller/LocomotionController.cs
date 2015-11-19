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

	public bool groundedLastFrame = false;

	//Debug
	public Vector3 dp = Vector3.zero;

	void Awake()
	{
		Time.timeScale = 1f;
		groundController = new GroundController( this, Walkable );
	}
	
	public void OnUpdate()
	{
		
		//Check Ground
		bool grounded = groundController.IsGrounded();
		if( grounded )
		{
			if( groundedLastFrame )
				maintainGround();
			else
				aquireGround();
		}

		//Ground Returned
			//Was grounded
				//MaintainGround
			//Not Grounded
				//AquireGround
			//Recursive Pushback
		//Ground not returned
			//Fall

		transform.position += moveDirection * Time.deltaTime;

	}

	public void AddGravity()
	{
		//moveDirection += Down * WorldProperties.Gravity * Time.deltaTime;
	}

	private void maintainGround()
	{
		Debug.Log("Maintaining Ground");
	}

	private void aquireGround()
	{
		Debug.Log("Aquired Ground");
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
}
