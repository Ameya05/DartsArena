// Shoot1.cs
// Create and shoot into the air a new cannonball instance when "Fire1" input is triggered

using UnityEngine;
using System.Collections;

public class Shoot1 : MonoBehaviour {
	public GameObject projectilePrefab;	// Copies of this prefab will be the ammo (e.g. "cannonball")
	private float shootSpeed = 30.0f;	// A speed to give the newly created projectiles

	// Handle "Fire1" action whenever it happens.
	void Update () {
		GameObject newBall;		// A variable to store the newly created projectile

		// When the "Fire1" button is pressed, fire the cannon
		if (Input.GetButtonDown ("Fire1")) {
			Debug.Log ("Fire");			// Debug output so we know something happened

			// Create a new cannonball, name it, give it an initial velocity pointed in the direction
			//   of our "cannon", and then set it to self-destruct have 15 seconds.
			// NOTE: "transform" is the transformation of the object we're attached to -- e.g. a Cannon
			newBall = (GameObject)Instantiate (projectilePrefab, transform.position, transform.rotation);
			newBall.name = "CannonBall";
			newBall.GetComponent<Rigidbody>().velocity = transform.up * shootSpeed;
			Destroy (newBall.gameObject, 15);
		}
	}
}
