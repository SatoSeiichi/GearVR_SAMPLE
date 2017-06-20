using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour {
	public ParticleSystem par;
	public bool dessFlag = false;
	public GameObject koge;
	// Use this for initialization
	void Start () {
		koge.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnParticleCollision(GameObject obj)
	{
		//Debug.LogError (obj.name);
		if (par.startSize > 0) 
		{
			par.startSize -= par.startSize / 10;
		}

		if (!dessFlag) {

			if (par.transform.localScale.x > 0.1f) {
				par.transform.localScale -= par.transform.localScale / 50;

			} else {
				koge.SetActive (true);
				dessFlag = true;
			}
		}
	}
}
