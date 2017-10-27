//Copyright (c) 2017 Kai Clavier [kaiclavier.com] Do Not Distribute
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Utility/Impact Freeze", 1)]
public class ImpactFreeze : MonoBehaviour {
	private Transform t;
	private Coroutine c;
	public float time = 0.1f;
	public float timeScale = 0f;
	private float oldTimescale;
	void OnValidate(){
		if(time < 0f){time = 0f;}
		if(timeScale < 0f){time = 0f;}
	}
	public void Freeze () {
		Freeze(time);
	}
	public void Freeze (float customTime) {
		if(c != null){
			Time.timeScale = oldTimescale;
			StopCoroutine(c);
		}
		c = StartCoroutine(DoFreeze(customTime));
	}
	IEnumerator DoFreeze(float customTime){
		oldTimescale = Time.timeScale;
		Time.timeScale = timeScale;
		float timer = 0f;
		while(timer < customTime){
			timer += Time.unscaledDeltaTime;
			yield return null;
		}
		Time.timeScale = oldTimescale;
	}
}
