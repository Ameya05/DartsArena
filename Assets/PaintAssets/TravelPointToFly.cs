using UnityEngine;
using System.Collections;

public class TravelPointToFly : MonoBehaviour {

	public GameObject mover;	// Object which is moved (should be [CameraRig]
	public bool travelling = false;
	private Vector3 speed = Vector3.forward * 0.03F;

	// When game is initialized, find the TrackedController component, and
	//    add Trigger() method to it's operations
	private SteamVR_TrackedController device;
	void Start () {
		device = GetComponent<SteamVR_TrackedController>();
		device.MenuButtonClicked += MenuButtonClicked;
		device.MenuButtonUnclicked += MenuButtonUnclicked;
		device.Gripped += Gripped;
	}

	void Update() {
		if (travelling) {
			//mover.transform.Translate (.1F, 0F, 0F);
			mover.transform.Translate(this.transform.rotation * speed);
		}
	}

	void MenuButtonClicked (object sender, ClickedEventArgs e)
	{
		Debug.Log ("Menu button has been pressed");	
		travelling = true;
	}
	void MenuButtonUnclicked (object sender, ClickedEventArgs e)
	{
		Debug.Log ("Menu button has been released");	
		travelling = false;
	}
	void Gripped (object sender, ClickedEventArgs e)
	{
		Debug.Log ("Grip button has been pressed");	
		mover.transform.position = Vector3.zero;
	}
}
