using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject ship;
	
	// Update is called once per frame
	void Update () {
		if (ship.GetComponent<ShipController> ().alive) {
			this.transform.position = new Vector3 (ship.transform.position.x, ship.transform.position.y + 1.5f, ship.transform.position.z - 20f);
		}
	}
}
