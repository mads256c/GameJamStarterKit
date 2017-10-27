//Copyright (c) 2016 Kai Clavier [kaiclavier.com] Do Not Distribute
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Kickback))]
[CanEditMultipleObjects] //why not
public class KickbackEditor : Editor{
	override public void OnInspectorGUI(){
		serializedObject.Update(); //for onvalidate stuff!
		SerializedProperty curve = serializedObject.FindProperty("curve");
		SerializedProperty multiplier = serializedObject.FindProperty("multiplier");
		SerializedProperty time = serializedObject.FindProperty("time");
		SerializedProperty roundDecimals = serializedObject.FindProperty("roundDecimals");
		SerializedProperty relativeToCam = serializedObject.FindProperty("relativeToCam");
		EditorGUILayout.PropertyField(curve);
		EditorGUILayout.PropertyField(relativeToCam);
		EditorGUILayout.PropertyField(multiplier);
		EditorGUILayout.PropertyField(time);
		EditorGUILayout.PropertyField(roundDecimals);
		serializedObject.ApplyModifiedProperties();
	}
}
#endif
[AddComponentMenu("Utility/Kickback", 1)]
public class Kickback : MonoBehaviour {
	private Transform t;
	private Coroutine c;
	private Vector3 lastMovement;
	
	public AnimationCurve curve = new AnimationCurve(new Keyframe(0f,1f,-1f,-1f), new Keyframe(1f,0f,-1f,-1f));
	public bool relativeToCam = false;
	public float multiplier = 0.5f; //default multiplier
	public float time = 0.1f;
	[Range(0,7)]
	public int roundDecimals = 7;
	
	void Awake(){
		t = this.transform;
	}
	void OnValidate(){
		if(time < 0f){time = 0f;}
	}
	public void Kick(Vector3 dir){ //call default 
		Kick(dir,multiplier);
	}
	public void Kick(Vector3 dir, float multi){ //call default 
		if(c != null){
			StopCoroutine(c);
		}
		c = StartCoroutine(DoKick(dir, multi));
	}
	IEnumerator DoKick (Vector3 dir, float multi) {
		float timer = 0f;
		while(timer < time){
			t.localPosition -= lastMovement; //move back
			float myStrength = curve.Evaluate(timer / time) * multi;
			if(relativeToCam){
				lastMovement = t.localRotation * dir.normalized * myStrength;//relative to camera's rotation
			}else{
				lastMovement = dir.normalized * myStrength;
			}
			if(roundDecimals != 7){
				lastMovement.x = (float)System.Math.Round(lastMovement.x, roundDecimals);
				lastMovement.y = (float)System.Math.Round(lastMovement.y, roundDecimals);
				lastMovement.z = (float)System.Math.Round(lastMovement.z, roundDecimals);
			}
			
			t.localPosition += lastMovement;
			
			timer += Time.unscaledDeltaTime; //count up using unscaled time.
			yield return null;
		}
		t.localPosition -= lastMovement; //move back
		lastMovement = Vector3.zero;
	}
}