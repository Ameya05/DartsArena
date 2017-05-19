// Shoot3.cs
// Create and shoot into the air a new cannonball instance when "Fire1" input is triggered
// Split the shooting operation into a separate method ("Shoot"), which can be called from other scripts
// Now add a new material that the Cannon will temporarily have when fired.

// There's a bug here, plus an inefficiency -- see Shoot4.cs

using UnityEngine;
using System.Collections;

public class Shoot3 : MonoBehaviour {
	public GameObject projectilePrefab;	// Copies of this prefab will be the ammo (e.g. "cannonball")
	public Material fireMaterial;		// The temporary material used when Cannon is fired
	private Material normMaterial;		// Store the normal material of the Cannon for restoration
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

		// Temporarily change the Cannon's material
		normMaterial = this.GetComponent<Renderer>().material;
		this.GetComponent<Renderer>().material = fireMaterial;
		Invoke("RestoreMaterial", 0.25f);
	}

	void RestoreMaterial() {
		this.GetComponent<Renderer>().material = normMaterial;
	}
}