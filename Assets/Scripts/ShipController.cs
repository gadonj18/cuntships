using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {
	public GameObject Detonation;
	public bool alive = true;

	void OnCollisionEnter(Collision col) {
		if (alive && col.collider.gameObject.name != "Floor") {
			GameObject detonation = Instantiate(Detonation);
			this.GetComponent<AudioSource>().Play();
			detonation.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
			foreach(Renderer renderer in this.GetComponentsInChildren<Renderer>()) {
				renderer.enabled = false;
			}
			alive = false;
		}
	}
}
