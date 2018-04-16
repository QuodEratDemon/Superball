﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mei : Character {

    // Use this for initialization
    void Start() {
        Name = "Mei";
<<<<<<< HEAD
        Damage = 10;
=======
>>>>>>> a5eac5b2088e1a2b65d64de2a08342e83936a8f7
        Catch = 100;
        Gather = 1;
        Stamina = maxStamina;
        heldBalls = 0;
        Capacity = 4;
        Role = "Supporter";

		actions = new string[]{ "None", "Throw", "Catch", "Gather", "Skill1", "Skill2", "Skill3", "Skill4" };
		actionNames = new string[]{ "None", "Throw", "Catch", "Gather", "Silver Platter", "Clean Up", "Cup of Tea", "Skill4" };
		actionDescription = new string[]{ "Wait", "Throw ball at target enemy", "Attempt to catch any incoming balls", "Gather balls from the ground", "Gives all of your balls to allies", "Gather an ammount of balls equal to ammount of balls used last turn", "Heals an ally and returns them to their calm state", "" };
		actionTypes = new string[]{ "None", "Offense", "Defense", "Utility", "Utility", "Utility", "Utility", "Utility" };
		defaultTargetingTypes = new int[]{ 0, 2, 0, 0, 0, 0, 1, 0 };
		alternateTargetingTypes = new int[]{ 0, 1, 0, 0, 0, 0, 2, 0 };
		actionCosts = new int[]{ 0, 1, 0, 0, 0, 0, 0, 0 };

		base.Start ();
    }

    // Update is called once per frame
    void Update () {
		/*
        if (allegiance == 1) {
            this.targetingTypes = alternateTargetingTypes;
            allies = combat.Player;
            enemies = combat.Enemy;
        } else {
            this.targetingTypes = defaultTargetingTypes;
            allies = combat.Enemy;
            enemies = combat.Player;
        }
        */
		base.Update ();
    }
		
	/*
	//PassOff here isnt an ability that can be selected, but rather a helper function to SilverPlatter 
	private void PassOff(Character target, int gift) {
		int diff = target.Capacity - target.heldBalls;
		while (diff > 0 && this.heldBalls > 0) {
			target.heldBalls++;
			diff = Target[0].Capacity - Target[0].heldBalls;
		}
	}
	*/

	// Silver Platter: Mei gives half her balls to each of her allies
    public override bool Skill1() {
		int gift = heldBalls;
		for(int i = 0; i < 3; i++)
		{
			if(allies[i] != this)
			{
				// if this is the first time balls are passed off give half of gift and recaluculate int gift
				if(gift == heldBalls)
				{
					allies [i].heldBalls += (int)gift/2;
					gift -= (int)gift / 2;
				}	
				// else just pass off the remaining balls
				else
				{
					allies [i].heldBalls += (int)gift;
				}

				// Check for overflow in target's ball cap and correct
				if(allies[i].heldBalls > allies[i].Capacity)
				{
					allies [i].heldBalls = allies [i].Capacity;
				}
			}
			heldBalls = 0;
		}
		return true;
    }
		
	//We currently have no way to check the past turn. This could be implemented easily though and would be very useful

	//Method of doing so in involving setting copying the past turn into oldCombatQueue at the end of the current execute phase. Thus, when in the current planning and execute phase, you can reference last turn

	//oldCombatQueue = combatQueue
	// Clean-Up: Gather an amount of balls equal to half the balls spent on actions last turn(?)
	// 
	// Currently runs into a problem if there are multiple Mei using Skill2
    public override bool Skill2() {
		if(combat.currentCharacter != 5)
		{
			// The intent here is to move Mei to the end of queue DURING the execute phase, this may be buggy so look for other avenues
			Character swap = this;
			for(int i = combat.currentCharacter + 1; i < 6; i++)
			{
				// This if statement is a way to prevent an infinite loop of tow or more Mei both using Clean-Up
				if(combat.combatQueue[i].Name != "Mei")
				{
					swap = combat.combatQueue [i];
					combat.combatQueue [i] = this;
					combat.combatQueue [i - 1] = swap;
				}
			}
		}
		else
		{
			int cleanUp = 0;
			for(int i = 0; i < 5; i++)
			{
				for(int j = 0; j < combat.combatQueue[i].actionCosts.Length; j++)
				{
					if(combat.combatQueue[i].actions[j] == combat.combatQueue[i].action)
					{
						cleanUp += combat.combatQueue [i].GetActionCost (j);
					}
				}
			}
			heldBalls += cleanUp / 2;
			if(heldBalls > Capacity)
			{
				heldBalls = Capacity;
			}
		}
		return true;
    }

	// Cup of Tea: Switches Elizabeth or Victoria to her calm state. If already calm instead + 15 Stamina
    public override bool Skill3() {
		if ( Target[0].Name == "Elizabeth" ){
            if (Target[0].allegiance == this.allegiance) { //is Elizabeth on our team?
				Elizabeth haruna = (Elizabeth)Target[0];
				if(haruna.Transform)
				{
					// transform is a unique boolean to the Schola Grandis girls
					haruna.Transform = false;
				}
				else
				{
					haruna.gainStamina(15);
				}
            }
		} else if (Target[0].Name == "Victoria" ){
            if (Target[0].allegiance == this.allegiance) { //is Victoria on our team?
				Victoria chikako = (Victoria)Target[0];
				if(chikako.Transform)
				{
					// transform is a unique boolean to the Schola Grandis girls
					chikako.Transform = false;
				}
				else
				{
					chikako.gainStamina (15);
				}
            }
        }
		// This statement only runs if the target is neither Elizabeth or Victoria
		else
		{
			Target [0].gainStamina (15);
		}
		return true;
    }

    public override bool Skill4() {
		return true;
    }

}
