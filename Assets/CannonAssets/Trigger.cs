// Trigger.cs
// When an in-game activation trigger is contacted, tell our object to "Shoot"

using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	public GameObject shooter;	// Object to which the "Shoot" message should be sent

	// OnTriggerEnter() is called when our object is first contacted by another collider
	void OnTriggerEnter(Collider other) {
		Debug.Log("It's touching me!");
		shooter.SendMessage ("Shoot");
	}
}
