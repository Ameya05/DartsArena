using UnityEngine;
using System.Collections;

public class OnCollision : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		collision.collider.GetComponent<Rigidbody>().velocity = Vector3.zero ;
		collision.collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		collision.collider.GetComponent<Rigidbody>().isKinematic = true;
		collision.gameObject.GetComponent<AudioSource> ().Play ();
	}

	void OnCollisionStay(Collision collision)
	{
		Debug.Log ("stay called");
	}

	void OnCollisionExit(Collision collision)
	{
		Debug.Log ("exit called");	
	}
}

