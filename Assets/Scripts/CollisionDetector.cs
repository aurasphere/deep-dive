using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CollisionDetector : MonoBehaviour {

	public AudioClip explosion;

	public int sleepSeconds;

	public void Start (){

	}

	// Resets the game on collision.
	void OnCollisionEnter(Collision col){
		Debug.Log ("COLLISION");
		AudioSource.PlayClipAtPoint (explosion, col.transform.position);
		StartCoroutine(sleep ());
		SceneManager.LoadScene("Dive");
	}

	IEnumerator sleep(){
		yield return new WaitForSeconds (sleepSeconds);
	}
}

