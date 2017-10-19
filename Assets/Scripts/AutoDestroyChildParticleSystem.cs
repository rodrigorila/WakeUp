using UnityEngine;
using System.Collections;

public class AutoDestroyChildParticleSystem : MonoBehaviour {

    private ParticleSystem _particles;

	// Use this for initialization
	void Start () {
        _particles = GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (!_particles.IsAlive ())
            Destroy (gameObject);
	}
}
