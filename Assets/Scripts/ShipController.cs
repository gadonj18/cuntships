using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {
	public GameObject Detonation;
	public bool alive = true;
	private int score = 0;

	void OnTriggerEnter(Collider col) {
		if (alive) {
			if (col.GetComponent<Collider>().gameObject.GetComponent<CapsuleController> ()) {
				GameObject.Destroy (col.GetComponent<Collider>().gameObject);
				score++;
			}
		}
	}

	void OnCollisionEnter(Collision col) {
		if (alive) {
			if (col.collider.gameObject.name != "Floor") {
				GameObject detonation = Instantiate (Detonation);
				this.GetComponent<AudioSource> ().Play ();
				detonation.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
				foreach (Renderer renderer in this.GetComponentsInChildren<Renderer>()) {
					renderer.enabled = false;
				}
				alive = false;
			}
		}
	}

	void Update() {
		GameObject.Find ("Canvas/Score").GetComponent<Text> ().text = "Score: " + score.ToString ();
	}
}
