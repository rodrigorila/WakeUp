using UnityEngine;
using System.Collections;

public class DogTailController : MonoBehaviour {

    private enum State 
    {
        Idle,
        MovingToLeft,
        MovingToRight,
        MovingToIdle
    }


    public Vector3 LeftAngles;
    public Vector3 RightAngles;


    [Range(0f, 1f)]
    public float MoveProbability = 0.1f;

    public float Speed;

    private State _state = State.Idle;
    private float _movement;
    private Quaternion _idlePosition;
    private Quaternion _leftPosition;
    private Quaternion _rightPosition;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        switch (_state) {
        case State.Idle:
            if (Random.Range (0f, 1f) < MoveProbability) {
                _state = State.MovingToRight;
                _movement = 0;
                _idlePosition = transform.rotation;
                _rightPosition = Quaternion.Euler (RightAngles);
            }
            break;
        case State.MovingToRight:
            _movement += Time.deltaTime / Speed;

            if (_movement >= 1f) {
                transform.rotation = _rightPosition;
                _movement = 0f;
                _state = State.MovingToLeft;
                _leftPosition = Quaternion.Euler (LeftAngles);
            }
            else
                transform.rotation = Quaternion.Slerp (_idlePosition, _rightPosition, _movement);
            break;
        case State.MovingToLeft:
            _movement += Time.deltaTime / Speed;

            if (_movement >= 1f) {
                transform.rotation = _leftPosition;
                _movement = 0f;
                _state = State.MovingToIdle;
            }
            else
                transform.rotation = Quaternion.Slerp (_rightPosition, _leftPosition, _movement);
            break;
        case State.MovingToIdle:
            _movement += Time.deltaTime / Speed;

            if (_movement >= 1f) {
                _state = State.Idle;
                transform.rotation = _idlePosition;
            }
            else
                transform.rotation = Quaternion.Slerp (_leftPosition, _idlePosition, _movement);
            break;
        default:
            throw new System.ArgumentException("Unknown state");
        }
	}
}
