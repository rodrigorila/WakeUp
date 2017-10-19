using UnityEngine;
using System.Collections;

public class ZChangeToCollected : MonoBehaviour {

	public Material NewMaterial;

	// Use this for initialization
	void Start () {
		StartCoroutine (RotateAndChangeMaterial (NewMaterial));
	}

	IEnumerator RotateAndChangeMaterial (Material m) {
		bool materialChanged = false;

		for (float angle = 0f; angle <= 360f; angle += 18f) {

			transform.rotation = Quaternion.Euler (angle, 0f, 0f);

			if (!materialChanged && angle >= 180f) {
				Renderer[] renderers = GetComponentsInChildren<Renderer> ();

				foreach (Renderer r in renderers)
					r.material = NewMaterial;

				materialChanged = true;
			}

			yield return new WaitForSeconds (0.000001f);
		}

		transform.rotation = Quaternion.Euler (0f, 0f, 0f);

		yield return null;

	}
}
