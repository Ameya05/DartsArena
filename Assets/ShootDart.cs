using UnityEngine;
using System.Collections;

public class ShootDart : MonoBehaviour {
	public GameObject projectilePrefab;	

	private bool shoot = true;
	private float shootSpeed = 5.0f;

	private SteamVR_TrackedController device;
	void Start () {

		device = GetComponent<SteamVR_TrackedController>();
		device.TriggerClicked += TriggerClicked;
		shoot = false;
	}

	// Update routine now just watches for the "Fire1" button
	void Update () {
		if (shoot) {

			GameObject newDart;
			newDart = (GameObject)Instantiate (projectilePrefab, transform.position, transform.rotation);
			newDart.name = "Dart";
			newDart.GetComponent<Rigidbody>().velocity = transform.forward * shootSpeed ;
			newDart.GetComponent<Rigidbody> ().freezeRotation = true;

			newDart.transform.eulerAngles = new Vector3(
				this.transform.eulerAngles.x-90,
				this.transform.eulerAngles.y,
				this.transform.eulerAngles.z
			);

			shoot = false;
			Destroy (newDart.gameObject, 2); 

		}
	}

	void TriggerClicked (object sender, ClickedEventArgs e)
	{
		Debug.Log ("Trigger clicked ... Shooting object");
		shoot = true;
	}
}
