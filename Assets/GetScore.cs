using UnityEngine;
using System.Collections;

public class GetScore : MonoBehaviour {

	public GameObject audience1;
	public GameObject audience2;
	public GameObject audience3;
	public GameObject audience4;
	public GameObject audience5;
	public GameObject audience6;
	public GameObject audience7;
	public GameObject audience8;
	//public GameObject audience9;
	//public GameObject audience10;

	private GameObject[] audience;

	private float origin_x = 250;
	private float origin_y = 1.58f;
	private float point_x = -20;
	private float point_y = -20;
	private float inner_b_r = 0.00625f;
	private float outer_b_r = 0.016f;
	private float inner_single_r = 0.1f;
	private float triple_r = 0.10825f;
	private float outer_single_r = 0.1635f;
	private float double_r = 0.172f;
	private float t_radius = 0.225f;
	private int score = 0;
	//private int[] val_scores = {6, 13, 4, 18, 1, 20, 5, 12, 9, 14, 11, 8, 16, 7, 19, 3, 17, 2, 15, 10};
	private int[] val_scores = {11, 14, 9, 12, 5, 20, 1, 18, 4, 13, 6, 10, 15, 2, 17, 3, 19, 7, 16, 8};
	//private const_angle_division = 18;
	public static GameObject scoreboard;

	void OnCollisionEnter(Collision collision)
	{
		
		audience = new GameObject[]{audience1, audience2, audience3, audience4, audience5, audience6, audience7, audience8 };//, audience9, audience10}
		score = 0;
		ContactPoint contact = collision.contacts[0];
		Vector3 pos = contact.point;
		point_x = pos.x;
		point_y = pos.y;
		//TODO assign Points x,y and Origin x,y
		float dist = Mathf.Sqrt( ((point_y - origin_y) * (point_y - origin_y)) + ((point_x - origin_x) * (point_x - origin_x)) );
		if (dist <= outer_b_r) {
			if (dist <= inner_b_r) {
				score = 50;
			} else {
				score = 25;
			}
		} else {
			float angle = (Mathf.Atan2 ((point_y - origin_y), (point_x - origin_x)) * 180) / Mathf.PI;
			if ((point_y < origin_y))
				angle += 360;
			angle += 9;
			int index = ((int)(angle/18)) % 20;
			int section = val_scores[index];
			Debug.Log ("ANGLE IS -------- : " + angle);

			if (dist <= inner_single_r) {
				score = section; 
			} else if (dist <= triple_r) {
				score = 3 * section;
			} else if (dist <= outer_single_r) {
				score = section;
			} else if (dist <= double_r) {
				score = 2 * section;
			} else {
				score = 0;
			}
		}
		if (dist <= t_radius) {
			
			collision.collider.GetComponent<Rigidbody>().velocity = Vector3.zero ;
			collision.collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			collision.collider.GetComponent<Rigidbody>().isKinematic = true;
			this.gameObject.GetComponent<AudioSource> ().Play ();
		}
		//scoreboard.GetComponent<SumScore>().Add(score);
		SumScore.IncrementDartCount ();
		if (SumScore.Score - score > 0) {
			SumScore.Subtract (score);
			if(score!=0){
				foreach(GameObject aud in audience){
					aud.GetComponent<Animation> ().Play ("applause");
				}
			}

		} else if (SumScore.Score - score == 0) {
			SumScore.Subtract (score);
			SumScore.SaveHighScore (SumScore.DartScore);
			SumScore.Reset();
			SumScore.Add(101);
			foreach(GameObject aud in audience){
				aud.GetComponent<Animation> ().Play ("celebration");
			}
		} else {
			SumScore.Subtract (0);
			Debug.Log ("Last dart score must be equal to remaining score");
		}
	}

	void OnCollisionStay(Collision collision)
	{
		//Debug.Log ("stay called");
	}

	void OnCollisionExit(Collision collision)
	{
		//Debug.Log ("exit called");	
	}

}
