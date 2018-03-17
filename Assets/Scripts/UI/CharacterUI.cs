﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour 
{
	public Text characterInfo;
	public Image characterPanel;
	public Character character;
	public Slider HealthBar;
	float HealthMax;
	Text characterName;

	void Start () 
	{
		characterInfo = gameObject.GetComponentsInChildren<Text> ()[0];
		characterName = gameObject.GetComponentsInChildren<Text> ()[1];
		characterPanel = gameObject.GetComponentsInChildren<Image> ()[1];
		HealthBar = gameObject.GetComponentInChildren<Slider> ();
		characterInfo.enabled = false;
		characterPanel.enabled = false;
		//characterName.text = character.Name;
		//HealthBar.maxValue = (float) character.maxStamina;
		//HealthBar.value = (float) character.Stamina;
	}

	public void Init(Character characterToSet)
	{
		characterInfo = gameObject.GetComponentsInChildren<Text> ()[0];
		characterName = gameObject.GetComponentsInChildren<Text> ()[1];
		characterPanel = gameObject.GetComponentsInChildren<Image> ()[1];
		HealthBar = gameObject.GetComponentInChildren<Slider> ();
		characterInfo.enabled = false;
		characterPanel.enabled = false;
		character = characterToSet;
		characterName.text = character.Name;
		HealthBar.maxValue = (float)character.maxStamina;
		HealthBar.value = (float)character.Stamina;
	}

	void Update()
	{
		characterInfo.text = character.Name 
			+ "\nStamina: "+ character.Stamina + " / " + character.maxStamina
			+ "\nBalls: " + character.heldBalls
			+ "\n\nSkills:" 
			+ "\n\t" + character.GetActionName(4)
			+ "\n\t" + character.GetActionName(5)
			+ "\n\t" + character.GetActionName(6);

		HealthBar.value = (float) character.Stamina;
	}

	void OnMouseEnter()
	{
		characterInfo.enabled = true;
		characterPanel.enabled = true;
	}

	void OnMouseExit()
	{
		characterInfo.enabled = false;
		characterPanel.enabled = false;
	}
		
}
