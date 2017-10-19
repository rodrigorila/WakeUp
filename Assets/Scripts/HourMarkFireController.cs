using UnityEngine;
using System.Collections;

public class HourMarkFireController : MonoBehaviour {

	public float Speed; // m/s because using VelocityChange (no mass involved)

	// Use this for initialization
	void Start () {
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * Speed, ForceMode.VelocityChange);
	}
	
}
