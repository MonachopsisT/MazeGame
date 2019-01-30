using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour 
{
	public float delayTime;

	// Use this for initialization
	void Start ()
	{ 
		 
		StartCoroutine (Go());

	}

	void RepeatMyCoroutine()
	{
		StartCoroutine (Go ());
	}

	IEnumerator Go()
	{
		//GetComponent<Animation>().Play ();
		//	yield return new WaitForSeconds (delayTime); 
		if (delayTime > 0) {
			while (true) {
				GetComponent<Animation> ().Play ();
				yield return new WaitForSeconds (delayTime); 
				GetComponent<Animation> ().Stop ();
				RepeatMyCoroutine ();
				yield return null;
			}
		} 
		else 
		{
			while (true)
			{
				GetComponent<Animation> ().Play ();
				yield return new WaitForSeconds (delayTime); 
			}
		}
	}
}
