using UnityEngine;
using System.Collections;

public class PalletTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter (Collider other)
	{
		Material palletMaterial;
		Debug.Log ("I (" + this.name + ") have been touched by " + other.name);
		palletMaterial = this.GetComponent<Renderer> ().material;
		other.gameObject.transform.parent.SendMessage ("PalletChange", palletMaterial);
		//other.SendMessage("HelloFrom", this.name);   // Both methods work.
	}
}
