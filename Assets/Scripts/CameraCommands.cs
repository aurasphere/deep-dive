using UnityEngine;
using System.Collections;

public class CameraCommands : MonoBehaviour {

	// Use this for initialization
	void Start (){
	
	}
	public float smooth = 2.0F;
	public float tiltAngle = 30.0F;
	void Update() {
		keyboardCommands ();
		
	}
	void accellerometerCommands(){
		Vector3 dir = Vector3.zero;

		float tiltAroundZ = Input.acceleration.z * tiltAngle;
		float tiltAroundX = Input.acceleration.x * tiltAngle;
		Quaternion target;
		target = Quaternion.Euler (tiltAroundX, 0, tiltAroundZ);
		transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
		}

	void keyboardCommands(){
		float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
		float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
		Quaternion target;

		if (Input.GetKey(KeyCode.S)) {
			target = Quaternion.Euler (tiltAroundX, 0, tiltAroundZ);
			transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
		}
		if (Input.GetKey(KeyCode.D)) {
			target = Quaternion.Euler (tiltAroundX, 0, tiltAroundZ);
			transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
		}
		if (Input.GetKey(KeyCode.W)) {
			target = Quaternion.Euler (tiltAroundX, 0, tiltAroundZ);
			transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
		}
		if (Input.GetKey(KeyCode.A)) {
			target = Quaternion.Euler (tiltAroundX, 0, tiltAroundZ);
			transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
		}

	}
}
