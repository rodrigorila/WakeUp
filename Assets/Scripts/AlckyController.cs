using UnityEngine;
using System.Collections;

public class AlckyController : MonoBehaviour
{

	public GameObject HourHand;
	public GameObject MinuteHand;

//	public ZSpawner ZSpawnerObject;

	// Fraction of the distance between Alcky and the Z Spawner wher the Z's will have zero size.
	// 0.2 would mean that if Alcky is closer than 20% of the initial distance then the Z's will become invisible
	//public float ZeroSizeDistanceFactor = 0.5f;

	public float HorizontalSpeed = 10.0f;
    public float JumpForce;

	public float TimeBetweenMinutes;

	public int Hour;
	public int Minute;


	private float m_nextMinuteTime;

    private bool m_jump = false;
    //private bool m_canJump = true;
    //private bool m_jumpButtonReleased = true;

    private Rigidbody m_rigidBody;
    //private float m_initialDistanceToZSpawner;
    private float m_horizontalMovement;
	private int m_playerLayer;
	private int m_platformLayer;


    private void UpdateNextMinuteTime () {
		m_nextMinuteTime = Time.time + TimeBetweenMinutes;
	}

	// Use this for initialization
	void Awake ()
	{
		m_rigidBody = GetComponent<Rigidbody> ();
		//m_initialDistanceToZSpawner = ZSpawnerObject.transform.position.x - transform.position.x;

		UpdateNextMinuteTime ();

		// determine the player's specified layer
		m_playerLayer = this.gameObject.layer;

		// determine the platform's specified layer
		m_platformLayer = LayerMask.NameToLayer("Platforms");

	}

	private void SetTime (int hour, int minute)
	{
		MinuteHand.transform.rotation = Quaternion.Euler (0.0f, 0.0f, 90.0f - minute * 6.0f);
		HourHand.transform.rotation = Quaternion.Euler (0.0f, 0.0f, 90.0f - hour * 30.0f - minute * 30.0f / 60.0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.gm != null && !GameManager.gm.Playing)
            return;

		m_horizontalMovement = Input.GetAxisRaw ("Horizontal");

		//Debug.Log (m_rigidBody.velocity.y);
		// when alcky is on top of a platform the absolute value of the velocity is greater than float.Epsilon (1e-7f aprox)
        if (Input.GetButtonDown ("Jump") && Mathf.Abs(m_rigidBody.velocity.y) < 1e-5f) {
            m_jump = true;
        }

//        if (Mathf.Abs(m_rigidBody.velocity.y) <= float.Epsilon)
//            m_canJump = true;
//
//        if (m_canJump && m_jumpButtonReleased && Input.GetButtonDown ("Jump")) {
//            m_jump = true;
//            m_jumpButtonReleased = false;
//            m_canJump = false;
//        } else if (!m_jumpButtonReleased && Input.GetButtonUp ("Jump")) {
//            m_jump = false;
//            m_jumpButtonReleased = true;
//        }

//        if (m_jumping && Mathf.Abs(m_rigidBody.velocity.y) > maxYVelocity) 
//            m_jumping = false;


		SetTime (Hour, Minute);

		if (Time.time > m_nextMinuteTime) {
			Minute += 1;

			if (Minute >= 60) {
				Minute = 0;
				Hour++;

				if (Hour >= 18)
					GameManager.gm.EndGame (false);
			}

			UpdateNextMinuteTime();
		}
			
		// if moving up then don't collide with platform layer
		// this allows the player to jump up through things on the platform layer
		// NOTE: requires the platforms to be on a layer named "Platform"

		Physics.IgnoreLayerCollision (m_playerLayer, m_platformLayer, m_rigidBody.velocity.y > 0.0f); 
	}

	void FixedUpdate ()
	{
		if (GameManager.gm != null && !GameManager.gm.Playing)
            return;

        // Alcky movement

//        if (Mathf.Abs (m_horizontalMovement) > float.Epsilon)
//            m_rigidBody.AddForce (
//                new Vector3 (m_horizontalMovement * HorizontalSpeed, 0f, 0f));

        float vx = m_horizontalMovement * HorizontalSpeed;

        float vy = m_rigidBody.velocity.y;
        if (m_jump) {
            vy = JumpForce;
            m_jump = false;
        }

        m_rigidBody.velocity = new Vector3(vx, vy, 0f);

		// Scale all the Z's respect to Alcky's proximity
        /*
		float zeroSpanSize = m_initialDistanceToZSpawner * ZeroSizeDistanceFactor;
		float d = m_initialDistanceToZSpawner - zeroSpanSize;
		float a = ZSpawnerObject.transform.position.x - transform.position.x - zeroSpanSize;

		float newScale = Mathf.Min (1.0f, Mathf.Max (0.0f, a / d));

		if (Mathf.Abs(ZSpawnerObject.ZScale - newScale) > float.Epsilon)
			ZSpawnerObject.ZScale = newScale;
   */         

	}

}
