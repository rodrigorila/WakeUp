using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	private Rigidbody m_rigidbody;

	private bool m_jumping = false;
	private bool m_canJump = true;
	private bool m_jumpButtonReleased = true;

	public ForceMode forceMode;
	public float forceMagnitude;
	public float maxYVelocity;

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody> ();
	}

	void Update () {
		if (Mathf.Abs(m_rigidbody.velocity.y) <= float.Epsilon)
			m_canJump = true;

		if (m_canJump && m_jumpButtonReleased && Input.GetButtonDown ("Jump")) {
			m_jumping = true;
			m_jumpButtonReleased = false;
			m_canJump = false;
		} else if (!m_jumpButtonReleased && Input.GetButtonUp ("Jump")) {
			m_jumping = false;
			m_jumpButtonReleased = true;
		}

		if (m_jumping && Mathf.Abs(m_rigidbody.velocity.y) > maxYVelocity) 
			m_jumping = false;


//		Debug.Log (Input.GetButton ("Jump"));
//
//		if (Input.GetButtonUp ("Jump"))
//			Debug.Log ("up");
//
//		if (Input.GetButtonDown ("Jump"))
//			Debug.Log ("down");
	}
	
	void FixedUpdate () {
		if (m_jumping) {
			m_rigidbody.AddForce (transform.up * forceMagnitude, forceMode);
		}
	}
}
