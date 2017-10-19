using UnityEngine;
using System.Collections;

public class ThiefController : MonoBehaviour {

	private enum State {
		Idle,
		Watching,
		Hiding
	}

 	private Vector3 m_initialPosition;
	private float m_time;
	private State m_state = State.Idle;

	public float MoveSpeed;
	public float MoveBackSpeed;
	public Transform MoveToPosition;
	public float MinIdleTime;



	void Start () {
		m_initialPosition = transform.position;
	}
	
	void Update () {
		switch (m_state) {
		case State.Idle:
			m_time += Time.deltaTime;

			if (m_time >= MinIdleTime){
				if (Random.Range (0f, 1f) < 0.5f)
					m_state = State.Watching;
				m_time = 0f;
			}
			break;
		case State.Watching: 
			m_time += Time.deltaTime / MoveSpeed;
			transform.position = Vector3.Lerp (m_initialPosition, MoveToPosition.position, m_time);
			if (m_time >= 1f) {
				m_state = State.Hiding;
				m_time = 0f;
			}
			break;
		case State.Hiding:
			m_time += Time.deltaTime / MoveBackSpeed;
			transform.position = Vector3.Lerp (MoveToPosition.position, m_initialPosition, m_time);
			if (m_time >= 1f) {
				m_state = State.Idle;
				m_time = 0f;
			}
			break;
		default:
			throw new System.ArgumentException ("Unknownn state");
		}


	}
}
