﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

	public static Transition blackScreen;
	void Awake () {
		if (blackScreen == null) {
			DontDestroyOnLoad (this);
			blackScreen = this;
		} else if (blackScreen != this) {
			Destroy (this);
		}
	}

	RectTransform canvas;
	RectTransform cover;
	public float speed;

	void Start()
	{
		cover = gameObject.GetComponent<RectTransform>();
		canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
		speed = -10f;
		var coverSize = cover.transform as RectTransform;
		coverSize.sizeDelta = new Vector2 (canvas.rect.width + 50, canvas.rect.height + 50);
	}

	//void Update () {
		/*if (done == 0) {
			transform.Translate (0f, speed, 0f);
			if (button.position.y < -button.rect.height)
				transform.position = new Vector3 (startingPosition.x, canvas.rect.height + button.rect.height, startingPosition.z);
		} else if (done == 1) {
			transform.Translate (0f, -1f * speed, 0f);
			if (button.position.y > (canvas.rect.height + button.rect.height/2))
				transform.position = new Vector3 (startingPosition.x, -(canvas.rect.height + button.rect.height), startingPosition.z);
		} else {
			transform.Translate (0f, 0f, 0f);
		}*/
		/*if (Input.GetKeyDown (KeyCode.D)) {
			//done = 0;
			StartCoroutine("uncoverScreen");
		}
		if (Input.GetKeyDown (KeyCode.U)) {
			//done = 1;
			StartCoroutine("coverScreen");
		}*/
	//}

	IEnumerator uncoverScreen () {
		bool comp = false;
		while (!comp) {
			if (cover.position.y < -canvas.rect.height){
				comp = true;
			} else {
				transform.Translate (0f, speed, 0f);
				//button.rect.y += speed;
			}
			yield return new WaitForEndOfFrame ();
		}
	}

	IEnumerator coverScreen () {
		bool comp = false;
		while (!comp) {
			if (cover.position.y > (canvas.rect.height - (cover.rect.height/2))) {
				comp = true;
			} else {
				transform.Translate (0f, -1f * speed, 0f);
			}
			yield return new WaitForEndOfFrame ();
		}
	}
}
