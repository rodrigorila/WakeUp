using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZSpawner : MonoBehaviour {

	public Object Z;
	public float SecondsPerSpawn;
	public float MaxInitialHorizontalImpulse;
	public int MaxSpawnCount = 0; // zero means no limit


	private List<ZController> m_zs = new List<ZController> ();

	private float m_zScale = 1.0f;

	public float ZScale {
		get{ 
			return m_zScale;
		}
		set {
			if (Mathf.Abs(m_zScale - value) < float.Epsilon)
				return;

			m_zScale = value;
		}
	}

	private float m_nextSpawnTime;
	private int m_spawnCount;

	private void SetNextSpawnTime () {
		m_nextSpawnTime = Time.time + SecondsPerSpawn;
	}

	// Use this for initialization
	void Start () {
		SetNextSpawnTime ();
	}
	
	// Update is called once per frame
	void Update () {
		if (MaxSpawnCount > 0 && m_spawnCount >= MaxSpawnCount)
			return;

		if (Time.time < m_nextSpawnTime)
			return;

		GameObject z = Instantiate (Z, transform.position, transform.rotation) as GameObject;

		ZController zController = z.GetComponent<ZController> ();
		zController.CounterGravityFloatFactor = 1.009f;
		zController.MaxInitialHorizontalImpulse = MaxInitialHorizontalImpulse;
		zController.InitialScale = 0.001f;
        zController.FinalScale = 0.02f;
		zController.ScaleRate = 0.005f;

		m_zs.Add (zController);

		SetNextSpawnTime ();

		m_spawnCount++;
	}
}
