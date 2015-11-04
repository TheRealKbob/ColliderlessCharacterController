using UnityEngine;

public class BroadcastUpdate : MonoBehaviour {

	private const string UPDATE_CALLER = "OnUpdate";

	public enum UpdateType
	{
		NORMAL,
		FIXED,
		LATE
	}

	public UpdateType UpdateOn = UpdateType.NORMAL;
	
	void Update (){ if( UpdateOn == UpdateType.NORMAL ) broadcastUpdate(); }
	void FixedUpdate (){ if( UpdateOn == UpdateType.FIXED ) broadcastUpdate(); }
	void LateUpdate (){ if( UpdateOn == UpdateType.LATE ) broadcastUpdate(); }

	private void broadcastUpdate()
	{
		gameObject.SendMessage( UPDATE_CALLER );
	}
}
