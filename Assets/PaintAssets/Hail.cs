using UnityEngine;
using System.Collections;

public class Hail : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void HelloFrom (string message) {
		Debug.Log(this.name + " receiving message: " + message);
	}
}
