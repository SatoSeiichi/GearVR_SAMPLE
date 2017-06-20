using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughCamera : MonoBehaviour {
	public GameObject webcamPlane;
	public GameObject sphere100; 
	// Use this for initialization
	void Start () {
		WebCamTexture webcamTexture = new WebCamTexture(1280,720,25);
		webcamPlane.GetComponent<Renderer>().material.mainTexture = webcamTexture;
		webcamTexture.Play();
	}

	// Update is called once per frame
	void Update () {
		#if  UNITY_EDITOR
		if (Input.GetKeyDown (KeyCode.Space)) {
			
		#elif UNITY_ANDROID
		if (OVRInput.GetDown (OVRInput.Button.PrimaryIndexTrigger)) {
		#else
		if (Input.GetKeyDown (KeyCode.Space)) {
		#endif
			if(sphere100.activeSelf == false)
			{
				sphere100.SetActive(true);
				webcamPlane.SetActive(false);
			}
			else
			{
				sphere100.SetActive(false);
				webcamPlane.SetActive(true);
			}
		}
	}
}
