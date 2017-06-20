using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

	public GameObject WaterShower;
	ParticleSystem[]  WaterEffects;

	[Space(10)]
	public LineRenderer _line;
	public Transform startLine;
	Material _lineMaterial;
	RaycastHit hit;
	public Fader fader;
	public AudioSource waterSE;
	public GameObject Sphere100;

	public GameObject Sphere100_warp0;
	public GameObject Sphere100_warp1;
	// Use this for initialization
	void Start () {
		WaterEffects  = WaterShower.GetComponentsInChildren<ParticleSystem> ();
		foreach (ParticleSystem p in WaterEffects) {
			p.Stop ();
		}

		_lineMaterial = _line.GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
#if  UNITY_EDITOR
		if (Input.GetKey (KeyCode.Space)) {
#elif UNITY_ANDROID
		if (OVRInput.Get (OVRInput.Button.PrimaryIndexTrigger)) {
#else
		if (Input.GetKey (KeyCode.Space)) {
#endif
			foreach (ParticleSystem p in WaterEffects) {
				if(p.isStopped) p.Play ();
			}
			
			if(!waterSE.isPlaying)
			{
				waterSE.Play();
			}
		}
		else
		{
			foreach (ParticleSystem p in WaterEffects) {
				if(!p.isStopped) p.Stop ();
			}
			if(waterSE.isPlaying)
			{
				waterSE.Stop();
			}
		}

		_line.SetPosition(0,startLine.position);
		if (Physics.Raycast (startLine.position, -startLine.forward, out hit)) {
				_line.SetPosition(1,hit.point);
				if(hit.transform.gameObject.tag == "Fire")
				{
					_lineMaterial.color = Color.blue;
				}
				else
				{
					_lineMaterial.color = Color.white;
				}
			}else{
				_line.SetPosition (1, -startLine.forward*100);
				_lineMaterial.color = Color.white;
			}
		}

	public void warp_kirikae(GameObject par)
	{
		fader.StartFade (Fader.FADE_TYPE.FADE_OUT, 1f, () => {
			par.transform.localScale = Vector3.one;
			par.GetComponent<fire_warp>().dessFlag = false;
			StartCoroutine(waiteTime(1) );
		});

	}

	IEnumerator waiteTime(float time)
	{
		if (Sphere100_warp0.activeSelf) {
			Sphere100_warp0.SetActive (false);
			Sphere100_warp1.SetActive (true);
		} else {
			Sphere100_warp0.SetActive (true);
			Sphere100_warp1.SetActive (false);
		}
		yield return new WaitForSeconds (time);
		fader.StartFade (Fader.FADE_TYPE.FADE_IN, 1f, () => {
			//StartCoroutine(waiteTime(1) );
		});
	}
}

