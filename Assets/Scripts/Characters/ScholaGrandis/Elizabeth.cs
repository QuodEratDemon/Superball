﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elizabeth : Character {
	
    public bool Transform = false;
	// this is used to keep track of stamina for Skill1
	public int lastStamina;
    // Use this for initialization
    
    new void Start() {
        Name = "Elizabeth";
        Stamina = maxStamina;
		
        Role = "Catcher";

		actionNames = new string[]{ "None", "Throw", "Catch", "Gather", "Preen", "Royal Touch", "Skill3", "Skill4" };
		actionDescription = new string[]{ "Wait", "Throw ball at target enemy", "Attempt to catch any incoming balls", "Gather balls", 
										  "Become staggered until attacked, then become steady", 	
										  "Become buffed", 
										  "Strong attack that may stun against one enemy", "" };
		actionTypes = new string[]{ "None", "Offense", "Defense", "Utility", "Utility", "Offense", "Utility", "Utility" };
		defaultTargetingTypes = new int[]{ 0, 1, 0, 0, 0, 0, 1, 0 };
		alternateTargetingTypes = new int[]{ 0, 1, 0, 0, 0, 0, 1, 0 };
		actionCosts = new int[]{ 0, 1, 0, 0, 0, 2, 0, 0 };

		base.Start ();

		lastStamina = Stamina;
    }

    // Update is called once per frame
    new void Update() {
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

    //Unfocused/Attack: Elizabeth is now unsteady, taking more dmg until she is attacked. At end of turn if she was attacked she switches to steady (less dmg);
    //this one is kinda weird since its more of a passive to be checked at the beginning of the execute phase, like the other abilities that occur post planning (change target to me, all attack me, etc.) 
	public override int Skill1() {
		// adds unsteady status for a period no player should be able to reach
		addStatusEffect ("unsteady", 100);
		return 0;
    }

	//   Preen (Unfocused): stacking self damage buff to stamina dmg.
	// cant do stacking buffs yet    //
	public override int Skill2(){
		return 0;
	}
	// Royal Touch: Hard Hitting Attack that may stun an enemy
	public override int Skill3() {
        float variance = UnityEngine.Random.Range(.7f, 1.3f);
		int damage = (int)((this.Damage + 35) * variance * attackMultiplier * Target [0].defenseMultiplier);
		Target[0].loseStamina(damage);
        if (UnityEngine.Random.Range(0, 5) > 4) 
		{
           	Target[0].addStatusEffect("stun", 2);
        }
        actionCooldowns[5] = 3;
		return damage;
    }
		
	public override int Skill4() { return 0;}

	public override void cleanUp()
	{
		base.cleanUp ();

		// This checks if Elizabeth was attacked this turn, isn't transformed, and is unsteady
		if(this.lastStamina < Stamina && !Transform && findStatus("unsteady") != -1)
		{
			this.Transform = true;
			addStatusEffect ("buff", 1);
			addStatusEffect ("steady", 1);
			print (statusEffects [findStatus ("buff")].name + ": " + statusEffects [findStatus ("buff")].duration);

		}

		lastStamina = Stamina;
	}

}
