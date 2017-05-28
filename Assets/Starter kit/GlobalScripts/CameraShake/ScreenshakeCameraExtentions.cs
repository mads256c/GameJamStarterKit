//Copyright (c) 2016 Kai Clavier [kaiclavier.com] Do Not Distribute
using UnityEngine;
using System.Collections;
//Allows you to call t.GetComponent<Camera>().Screenshake();
//instead of t.GetComponent<Screenshake>().Shake();
//because why not????????
public static class ScreenshakeCameraExtentions {
	public static void Shake (this Camera cam) {
		Screenshake shake = cam.transform.GetComponent<Screenshake>();
		if(shake != null){
			shake.Shake();
		}else{
			Debug.Log("Camera doesn't have Screenshake component, adding one!");
			cam.transform.gameObject.AddComponent<Screenshake>().Shake(); //add and shake
		}
	}
	public static void Shake (this Camera cam, float multi) {
		Screenshake shake = cam.transform.GetComponent<Screenshake>();
		if(shake != null){
			shake.Shake(multi);
		}else{
			Debug.Log("Camera doesn't have Screenshake component, adding one!");
			cam.transform.gameObject.AddComponent<Screenshake>().Shake(multi); //add and shake
		}
	}
	
	public static void Kick (this Camera cam, Vector3 dir) {
		Kickback kick = cam.transform.GetComponent<Kickback>();
		if(kick != null){
			kick.Kick(dir);
		}else{
			Debug.Log("Camera doesn't have Kickback component, adding one!");
			cam.transform.gameObject.AddComponent<Kickback>().Kick(dir); //add and shake
		}
	}
	public static void Kick (this Camera cam, Vector3 dir, float multi) {
		Kickback kick = cam.transform.GetComponent<Kickback>();
		if(kick != null){
			kick.Kick(dir, multi);
		}else{
			Debug.Log("Camera doesn't have Kickback component, adding one!");
			cam.transform.gameObject.AddComponent<Kickback>().Kick(dir, multi); //add and shake
		}
	}
	public static void Freeze (this Camera cam){
		ImpactFreeze freeze = cam.transform.GetComponent<ImpactFreeze>();
		if(freeze != null){
			freeze.Freeze();
		}else{
			Debug.Log("Camera doesn't have ImpactFreeze component, adding one!");
			cam.transform.gameObject.AddComponent<ImpactFreeze>().Freeze(); //add and freeze
		}
	}
	public static void Freeze (this Camera cam, float time){
		ImpactFreeze freeze = cam.transform.GetComponent<ImpactFreeze>();
		if(freeze != null){
			freeze.Freeze(time);
		}else{
			Debug.Log("Camera doesn't have ImpactFreeze component, adding one!");
			cam.transform.gameObject.AddComponent<ImpactFreeze>().Freeze(time); //add and freeze
		}
	}
}