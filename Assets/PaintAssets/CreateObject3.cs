// CreateObject3.cs
// When the Vive hand-held controller's trigger is pulled, continuously create new objects in the world
// Turn given HUD object on or off when trigger is pressed (disabled for now)
// Newly created objects are added immediately to the root of the hierarchy
// Attach this script to a Vive Controller



using UnityEngine;
using System.Collections;

public class CreateObject3 : MonoBehaviour {
	//public GameObject myHUD;		// Object to turn on and off
	public GameObject createMe;		// Prefab to create
	public GameObject brush;		// The brush object (so it can reflect the color)
	private Material currentMaterial;
	private GameObject newObject;	// The object created by pressing the trigger
	private int objectCount = 0;	// How many objects have been created thus far

	// When game is initialized, find the TrackedController component, and
	//    add Trigger() method to it's operations
	private SteamVR_TrackedController device;
	void Start () {
		//myHUD.SetActive (false);

		currentMaterial = createMe.GetComponent<Renderer> ().sharedMaterial;

		device = GetComponent<SteamVR_TrackedController>();
		//device.TriggerClicked += Trigger;
		device.TriggerClicked += TriggerClicked;
		device.TriggerUnclicked += TriggerUnclicked;
	}

	// When a Vive controller Trigger is pressed, turn object on
	void TriggerClicked (object sender, ClickedEventArgs e)
	{
		Debug.Log ("Trigger was clicked");	
		//myHUD.SetActive (true);

		// Now create a new object
		objectCount++;
		newObject = (GameObject)Instantiate (createMe, transform.position, transform.rotation);
		newObject.transform.parent = null;
		newObject.name = "blob-" + objectCount.ToString ();
		newObject.GetComponent<Renderer> ().material = currentMaterial;
		//Debug.Log(this.gameObject.GetType());
		//Debug.Log("Created object number " + objectCount.ToString());
	}

	// When a Vive controller Trigger is unpressed, turn object off
	void TriggerUnclicked (object sender, ClickedEventArgs e)
	{
		Debug.Log ("Trigger was released");	
		//myHUD.SetActive (false);
		//newObject.transform.parent = null;
	}

	void PalletChange (Material newMaterial) {
		Debug.Log(this.name + " new material " + newMaterial.name);
		currentMaterial = newMaterial;
		brush.GetComponent<Renderer> ().material = newMaterial;
	}
}
