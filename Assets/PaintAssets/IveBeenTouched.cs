// IveBeenTouched.cs
using UnityEngine;
using System.Collections;

public class IveBeenTouched : MonoBehaviour {
	//public GameObject shooter;	// Object to which the "Shoot" message should be sent

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("I (" + this.name + ") have been touched by " + other.name);
		other.gameObject.SendMessage("HelloFrom", "Hello From " + this.name);
		//other.SendMessage("HelloFrom", this.name);   // Both methods work.
	}

}
