﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greg: Character
{
	Character Trevor;

    void Start()
    {
        Name = "Greg";
		Damage = 1;
        Catch = 50;
        Capacity = 8;
        Gather = 2;
        Stamina = 14;
        heldBalls = 0;
        Role = "Support";

		actionNames = new string[]{ "None", "Throw", "Catch", "Gather", "Terrapin", "Pass Off", "Skill3", "Rest" };
		actionDescription = new string[]{ "Wait", "Throw ball at target enemy", "Attempt to catch any incoming balls", "Gather balls from the ground", "Immune to damage, any balls thrown at you will be given to Trevor", "Pass off all balls to target ally", "", "" };
		actionTypes = new string[]{ "None", "Offense", "Defense", "Defense", "Utility", "Utility", "Utility" };
		defaultTargetingTypes = new int[]{ 0, 2, 0, 0, 0, 1, 0, 0 };
		alternateTargetingTypes = new int[]{ 0, 1, 0, 0, 0, 2, 0, 0 };
		actionCosts = new int[]{ 0, 1, 0, 0, 0, 0, 0, 0 };

		GameObject[] P = GameObject.FindGameObjectsWithTag ("Player");
		GameObject[] E = GameObject.FindGameObjectsWithTag ("Enemy");
		for(int i = 0; i < P.Length; i ++)
		{
			if(P[i].GetComponent<Character>().Name == "Trevor")
			{
				Trevor = P [i].GetComponent<Trevor> ();
			}
		}
		for(int i = 0; i < E.Length; i ++)
		{
			if(P[i].GetComponent<Character>().Name == "Trevor")
			{
				Trevor = E [i].GetComponent<Trevor> ();
			}
		}
    }

	void Update() {
        if (combat == null) {
            combat = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        }else{
            if (allegiance == 1) { //this is unique for Shiro, Clemence and Theodore as they are defaultly under player control
                this.targetingTypes = alternateTargetingTypes;
                allies = combat.Player;
                enemies = combat.Enemy;
            } else {
                this.targetingTypes = defaultTargetingTypes;
                allies = combat.Enemy;
                enemies = combat.Player;
            }
        }
    }

	// This skill is Greg's Terrapin skill
	// If a hit is successful against Greg and terrapin has been used it rebounds into Trevor's ball pool
	// Is there a cost for this?
	public override void Skill1()
    {
        //recall this is a defense skill so it is called to see if you get hit, ignoring what the enemie's ability is. If they throw multiple balls, then Terrapin happens multiple times
        if (Trevor.heldBalls < Trevor.Capacity) Trevor.heldBalls++;
        actionCooldowns[4] = 3;
        heldBalls--;
    }

    public override void Skill2() {
       int diff = Target.Capacity - Target.heldBalls;
        while (diff > 0 && this.heldBalls > 0) {
            Target.heldBalls++;
            diff = Target.Capacity - Target.heldBalls;
        }
    }
}
