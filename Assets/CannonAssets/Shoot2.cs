// Shoot2.cs
// Create and shoot into the air a new cannonball instance when "Fire1" input is triggered
// Split the shooting operation into a separate method ("Shoot"), which can be called from other scripts

using UnityEngine;
using System.Collections;

public class Shoot2 : MonoBehaviour {
	public GameObject projectilePrefab;	// Copies of this prefab will be the ammo (e.g. "cannonball")
	private float shootSpeed = 30.0f;	// A speed to give the newly created projectiles

	// Update routine now just watches for the "Fire1" button
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Shoot ();
		}
	}

	void Shoot() {
		GameObject newBall;		// A variable to store the newly created projectile

		//Debug.Log ("Fire");

		// Create a new cannonball, name it, give it an initial velocity pointed in the direction
		//   of our "cannon", and then set it to self-destruct after 15 seconds.
		newBall = (GameObject)Instantiate (projectilePrefab, transform.position, transform.rotation);
		newBall.name = "CannonBall";
		newBall.GetComponent<Rigidbody>().velocity = transform.up * shootSpeed;
		Destroy (newBall.gameObject, 15);
	}
}
