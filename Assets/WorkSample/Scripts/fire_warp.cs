using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_warp : MonoBehaviour {
	public ParticleSystem par;
	Vector3 startSizeMax;

	public bool dessFlag = false;

	public InputManager im;
	// Use this for initialization
	void Start () {
		//dpar = GetComponent<ParticleSystem> ();
		startSizeMax = par.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnParticleCollision(GameObject obj)
	{
		if (!dessFlag) {
			
			if (par.transform.localScale.x > 0.1f) {
				par.transform.localScale -= par.transform.localScale / 50;

			} else {
				Debug.LogError (par.transform.localScale);
				dessFlag = true;
				im.warp_kirikae (gameObject);
			}
		}
	}


}
