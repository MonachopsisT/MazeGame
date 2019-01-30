using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public GameManager manager;
	
	public float moveSpeed;
	public GameObject deathParticles;
	public bool usesManager = true;

	private float maxSpeed = 5f;
	private Vector3 input;

	private Vector3 spawn;

	//Audio
	public AudioClip[] audioClip;
	int soundIndex;

	// Use this for initialization
	void Start () 
	{
		AudioListener.volume = 0.3f;

		spawn = transform.position;
		if(usesManager)
		{
			manager = manager.GetComponent<GameManager> ();

		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));

		if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
		{
			GetComponent<Rigidbody>().AddRelativeForce (input * moveSpeed);
		}

		if (transform.position.y < -2) 
		{
			Die ();
		}

		Physics.gravity = Physics.Raycast (transform.position, Vector3.down, .3f) ? Vector3.zero : new Vector3 (0, -25f, 0);
	}

	void OnCollisionEnter (Collision other)
	{
		//u hit red cube - U DIE
		if(other.transform.tag == "Enemy")
		{
			Die ();
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.transform.tag == "Enemy")
		{
			Die ();
		}
			
		if (other.transform.tag == "Token")
		{
			soundIndex = 0;
			if (usesManager) 
			{
				manager.tokenCount += 1;
			}
			PlaySound (soundIndex);
			Destroy (other.gameObject);
		}

		if (other.transform.tag == "Goal")
		{
			soundIndex = 1;
			PlaySound (soundIndex);
			Time.timeScale = 0f;
			manager.CompleteLevel ();
		}
	}

	void PlaySound(int clip)
	{
		GetComponent <AudioSource> ().clip = audioClip [clip];
		GetComponent <AudioSource>().Play ();
	}


	void Die ()
	{
		Instantiate (deathParticles, transform.position, Quaternion.Euler (270,0,0));
		transform.position = spawn;
	}


}
