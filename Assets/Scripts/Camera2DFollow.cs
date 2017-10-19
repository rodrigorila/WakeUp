using System;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public List<Transform> targets;

    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

	// private variables
    float m_OffsetZ;
    Vector3 m_LastTargetPosition;
    Vector3 m_CurrentVelocity;
    Vector3 m_LookAheadPos;

    // Use this for initialization
    private void Start()
    {
		m_LastTargetPosition = FindTargetsCenterPoint();
		m_OffsetZ = (transform.position - m_LastTargetPosition).z;
        transform.parent = null;

		if (targets==null || targets.Count == 0)
			Debug.LogError("Targets not set on Camera2DFollow.");
    }

	private Vector3 FindTargetsCenterPoint() {
		if (targets == null || targets.Count == 0)
			return Vector3.zero;

		if (targets.Count == 1)
			return targets[0].position;

		Bounds bounds = new Bounds(targets[0].position, Vector3.zero);

		for (var i = 1; i < targets.Count; i++)
			bounds.Encapsulate(targets[i].position); 
		
		return bounds.center;
	}

    // Update is called once per frame
	private void Update()
    {
		Vector3 center = FindTargetsCenterPoint();

        // only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (center - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
            m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
        else
			m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);

		Vector3 aheadTargetPos = center + m_LookAheadPos + Vector3.forward*m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        transform.position = newPos;

		m_LastTargetPosition = center;
    }
}
