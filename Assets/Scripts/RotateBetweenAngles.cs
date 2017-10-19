using UnityEngine;
using System.Collections;

public class RotateBetweenAngles : MonoBehaviour {

    public enum RotationAxis {
        X,
        Y,
        Z};

    public RotationAxis RotateAboutAxis;
    public float StartAngle;
    public float EndAngle;
    public float Speed; //degrees s^-1

    private Quaternion _start;
    private Quaternion _end;
    private float _time; 

    private Quaternion GetQuaternion (float angle) {
        switch (RotateAboutAxis) {
        case RotationAxis.X:
            return Quaternion.Euler (new Vector3 (angle, 0f, 0f));
        case RotationAxis.Y:
            return Quaternion.Euler (new Vector3 (0f, angle, 0f));
        case RotationAxis.Z:
            return Quaternion.Euler (new Vector3 (0f, 0f, angle));
        default:
            return Quaternion.identity;
        }
    }

	// Use this for initialization
	void Start () {
        _start = GetQuaternion (StartAngle);
        _end = GetQuaternion (EndAngle);
        transform.rotation = _start;
        _time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        _time += Speed * Time.deltaTime;

        transform.rotation =  Quaternion.Slerp (_start, _end, _time);

        if (_time >= Speed) {
            Quaternion aux = _start;
            _start = _end;
            _end = aux;
            _time = 0f;
        }
	}
}
