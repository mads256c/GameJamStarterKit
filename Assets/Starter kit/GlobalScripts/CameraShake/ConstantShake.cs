//Copyright (c) 2016 Kai Clavier [kaiclavier.com] Do Not Distribute
using UnityEngine;
using System.Collections;
[AddComponentMenu("Utility/Constant Shake")]
public class ConstantShake : MonoBehaviour {
	private Transform t;
	private Vector3 lastShake;
	public Vector3 strength = Vector2.one;
	public float multiplier = 0.1f;
	void Awake () {
		t = this.transform;
	}
	void Update () {
		t.localPosition -= lastShake;
		lastShake = new Vector3(Random.Range(-strength.x,strength.x) * multiplier, 
								Random.Range(-strength.y,strength.y) * multiplier, 
								Random.Range(-strength.z,strength.z) * multiplier);
		t.localPosition += lastShake;
	}
}
