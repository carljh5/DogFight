using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccellerometerParallax : MonoBehaviour {
	Vector3 origPos;

	void Start() {
		origPos = transform.position;
	}
		
	Vector3 velocity = Vector3.zero;

	void Update () {
		Vector3 targetPosition = origPos + ((Quaternion.AngleAxis(45, Vector3.right) * Input.acceleration)* 50f); 
		transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, 0.3f);
	}
}
