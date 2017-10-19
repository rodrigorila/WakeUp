using UnityEngine;
using System.Collections;

public class BatSwing : MonoBehaviour {

    private enum State
    {
        Idle,
        Swinging,
        SwingBack
    }

    public Vector3 SwingAngles = new Vector3(0f, 180f, 0f);

	// Percentage of the swing where the collider is on for hitting (1f all the time, 0f never turn on the collider, 0.2f only the final 20% of the swing han hit anything)
	[Range(0f, 1f)] 
	public float SwingHitRange = 0.2f; 

    // Speed of the swing in seconds. If Speed 0.5f it will take half second to swing and half second to swing back
	public float Speed = 1f;

	// Bat collider
	public GameObject Bat;

	private Collider _collider;
    private State _state;
    private Quaternion _initialRotation;
    private float _swing;
    private Quaternion _swingEnd;

    // Use this for initialization
    void Start () {
		if (Bat == null)
			Debug.LogError ("Bat must be assigned");

		_collider = Bat.GetComponent<Collider> ();

		if (_collider == null)
			Debug.LogError ("Bat must have a collider");

		_collider.enabled = false;

        _initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update () {

        switch (_state) {
		case State.Idle:
			if (_collider.enabled)
				_collider.enabled = false;

			if (Input.GetKeyDown (KeyCode.LeftControl) || Input.GetKeyDown (KeyCode.RightControl)) {
                _state = State.Swinging;
                _swing = 0f;
                _swingEnd = Quaternion.Euler (SwingAngles);
            }
            break;
        case State.Swinging:
            _swing += Time.deltaTime / Speed;

            if (_swing >= 1f) {
                _swing = 0f;
                _state = State.SwingBack;
				_collider.enabled = SwingHitRange > 0f;

                transform.rotation = _swingEnd;
            } else {
				_collider.enabled = _swing > 1f - SwingHitRange;

                transform.rotation = Quaternion.Slerp (_initialRotation, _swingEnd, _swing);
            }
            break;
        case State.SwingBack:
            _swing += Time.deltaTime / Speed;

            if (_swing >= 1f) {
                _swing = 0f;
                _state = State.Idle;
				_collider.enabled = false;

                transform.rotation = _initialRotation;
            } else {
				_collider.enabled = _swing < SwingHitRange;

                transform.rotation = Quaternion.Slerp (_swingEnd, _initialRotation, _swing);
            }
            break;
		default:
			throw new System.ArgumentException ("Undknown state");
        }

    }

}
