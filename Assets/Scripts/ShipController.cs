using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {
    private Rigidbody rb;
    private Vector3 UpDir;
    private bool UpPressed = false;
    private bool LeftPressed = false;
    private bool RightPressed = false;
    public GameObject Detonation;
	public bool alive = true;
	private int score = 0;
    public AudioClip CapsuleSound;
    public AudioClip DetonationSound;
    private AudioSource Engine;
    private AudioSource Effects;
    private GameObject flame;

    void Start()  {
        AudioSource[] sources = this.GetComponents<AudioSource>();
        Effects = sources[0];
        Engine = sources[1];
        flame = this.transform.FindChild("Flame").gameObject;
        rb = this.GetComponent<Rigidbody>();
    }

	void OnTriggerEnter(Collider col) {
		if (alive) {
			if (col.GetComponent<Collider>().gameObject.GetComponent<CapsuleController> ())  {
                Effects.clip = CapsuleSound;
                Effects.Play();
                GameObject.Destroy (col.GetComponent<Collider>().gameObject);
				score++;
			}
		}
	}

	void OnCollisionEnter(Collision col) {
		if (alive) {
			if (col.collider.gameObject.name != "Floor") {
				GameObject detonation = Instantiate (Detonation);
                Effects.clip = DetonationSound;
                Effects.Play();
				detonation.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
				foreach (Renderer renderer in this.GetComponentsInChildren<Renderer>()) {
					renderer.enabled = false;
				}
				alive = false;
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
        UpPressed = false;
        if (Input.GetKey("w"))
        {
            UpPressed = true;
        }
        LeftPressed = false;
        RightPressed = false;
        if (Input.GetKey("a"))
        {
            LeftPressed = true;
        }
        else if (Input.GetKey("d"))
        {
            RightPressed = true;
        }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);

        GameObject.Find("Canvas/Score").GetComponent<Text>().text = "Score: " + score.ToString();
    }

    void FixedUpdate()
    {
        if (UpPressed)
        {
            rb.AddForce(this.transform.forward * 10f);
            flame.GetComponent<ParticleSystem>().enableEmission = true;
            if(!Engine.isPlaying) Engine.Play();
        } else
        {
            Engine.Stop();
            flame.GetComponent<ParticleSystem>().enableEmission = false;
        }
        if (LeftPressed)
        {
            //rb.AddRelativeTorque(new Vector3(0f, -1f, 0f));
            this.transform.Rotate(new Vector3(0f, -100f * Time.deltaTime, 0f));
        }
        else if (RightPressed)
        {
            //rb.AddRelativeTorque(new Vector3(0f, 1f, 0f));
            this.transform.Rotate(new Vector3(0f, 100f * Time.deltaTime, 0f));
        }
    }
}
