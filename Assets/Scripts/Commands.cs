using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Commands : MonoBehaviour {

	public int manovrability;

	private Rigidbody body;

	public bool debug;

	public bool accellerometer;

	public int forwardSpeed;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		body = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		body.AddForce (forwardSpeed * Vector3.forward);
		if (debug) {
			pcTestCommands ();
		} else {
			if (accellerometer) {
				accellerometerCommands ();
			} else {
				androidTouchCommands ();
			}
		}

	}

	void androidTouchCommands(){
		if (Input.touchCount > 0 && 
			Input.GetTouch(0).phase == TouchPhase.Moved) {


			// Get movement of the finger since last frame
			Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;

			Vector3 movement = new Vector3(deltaPosition.x * manovrability * Time.deltaTime, deltaPosition.y * manovrability * Time.deltaTime, 0);
			body.AddForce (movement);
		
		}
	}

	void pcTestCommands(){
		// I can only go down on Y-axis (pressing W).
		if (Input.GetKey (KeyCode.W)) {
			//transform.position += new Vector3 (0, speed * Time.deltaTime * -1, 0);
			//Vector3 movementVec= new Vector3 (0, manovrability * Time.deltaTime * -1, 0);
			body.AddForce (new Vector3 (0, manovrability * Time.deltaTime * -1, 0));

		//	body.MoveRotation(m_Rigidbody.rotation * deltaRotation);
		}
		if (Input.GetKey (KeyCode.S)) {
			//transform.position += new Vector3 (0, speed * Time.deltaTime, 0);
			body.AddForce (new Vector3 (0, (manovrability / 2) * Time.deltaTime, 0));
		}
		// I can go both left and right on X-axis (pressing A or D).
		if (Input.GetKey (KeyCode.A)) {
			//transform.position += new Vector3 ( speed * Time.deltaTime * -1, 0, 0);
			body.AddForce (new Vector3 (manovrability * Time.deltaTime * -1, 0, 0));
		}
		if (Input.GetKey (KeyCode.D)) {
			//transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
			body.AddForce (new Vector3 (manovrability * Time.deltaTime, 0, 0));
		}
	}

	void accellerometerCommands(){
		Vector3 dir = Vector3.zero;
	
		// we assume that the device is held parallel to the ground
		// and the Home button is in the right hand

		// remap the device acceleration axis to game coordinates:
		// 1) XY plane of the device is mapped onto XZ plane
		// 2) rotated 90 degrees around Y axis
		dir.x = Input.acceleration.x;
		dir.y = Input.acceleration.z;

		// clamp acceleration vector to the unit sphere
//		if (dir.sqrMagnitude > 1)
//			dir.Normalize ();

		// Make it move 10 meters per second instead of 10 meters per frame...
		dir *= Time.deltaTime;

		// Move object
		body.transform.Translate(dir * 20);
	}
}