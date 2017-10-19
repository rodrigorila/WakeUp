using UnityEngine;
using System.Collections;

public class HourMarkShooter : MonoBehaviour {

	public float MinSecondsPerShoot = 1.0f;

	public Object FireHourMark;

	private float m_minTimeBeforeNextShot;

	void UpdateMinTimeBeforeNextShot() {
		m_minTimeBeforeNextShot = Time.time + MinSecondsPerShoot;
	}

	void Start() {
		UpdateMinTimeBeforeNextShot ();
	}


	void Update () {
		if (!Input.GetButtonDown("Fire1"))
			return;

		if (Time.time < m_minTimeBeforeNextShot)
			return;

		Instantiate (FireHourMark, transform.position, transform.rotation);

		UpdateMinTimeBeforeNextShot ();
	}
}
