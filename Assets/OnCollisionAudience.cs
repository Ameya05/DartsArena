using UnityEngine;
using System.Collections;

public class OnCollisionAudience : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("enter called");
		collision.collider.GetComponent<Rigidbody>().velocity = Vector3.zero ;
		collision.collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		collision.collider.GetComponent<Rigidbody>().isKinematic = true;
		this.transform.parent.gameObject.GetComponent<Animation> ().Play ("celebration3");
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

