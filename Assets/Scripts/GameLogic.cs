using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {
	public GameObject ship;
	private Rigidbody rb;
	private Vector3 UpDir;
	private bool UpPressed = false;
	private bool LeftPressed = false;
	private bool RightPressed = false;

	void Start() {
		rb = ship.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		UpPressed = false;
		if(Input.GetKey("w")) {
			UpPressed = true;
		}
		LeftPressed = false;
		RightPressed = false;
		if(Input.GetKey ("a")) {
			LeftPressed = true;
		} else if(Input.GetKey ("d")) {
			RightPressed = true;
		}
		ship.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y, 0f);
	}

	void FixedUpdate () {
		if(UpPressed) {
			rb.AddForce(ship.transform.forward * 10f);
		}
		if(LeftPressed) {
			//rb.AddRelativeTorque(new Vector3(0f, -1f, 0f));
			ship.transform.Rotate(new Vector3(0f, -100f * Time.deltaTime, 0f));
		} else if(RightPressed) {
			//rb.AddRelativeTorque(new Vector3(0f, 1f, 0f));
			ship.transform.Rotate(new Vector3(0f, 100f * Time.deltaTime, 0f));
		}
	}
}