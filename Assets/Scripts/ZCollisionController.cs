using UnityEngine;
using System.Collections;

public class ZCollisionController : MonoBehaviour {

    public Object CollisionParticleSystem;

    void OnCollisionEnter (Collision collision)
    {
        if (CollisionParticleSystem) {
            // Instantiate an explosion effect at the gameObjects position and rotation
            Instantiate (CollisionParticleSystem, transform.position, transform.rotation);
        }

		if (GameManager.gm != null)
			GameManager.gm.IncrementZsCollected ();

		Destroy (gameObject);
    }
}
