using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCapture : MonoBehaviour {
	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Beacon") {
			Destroy(other.gameObject.transform.root.gameObject);
		}
	}
}
