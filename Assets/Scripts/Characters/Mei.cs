﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mei : Character {

    // Use this for initialization
    void Start() {
        Name = "default";
        Damage = 1;
        Catch = 100;
        Gather = 1;
        Stamina = 10;
        maxStamina = 10;
        heldBalls = 0;
        Capacity = 4;
        Role = "Supporter";

		actions = new string[]{ "None", "Throw", "Catch", "Gather", "Skill1", "Skill2", "Skill3", "Skill4" };
		actionNames = new string[]{ "None", "Throw", "Catch", "Gather", "Silver Platter", "Clean Up", "Cup of Teas", "Skill4" };
		actionDescription = new string[]{ "Wait", "Throw ball at target enemy", "Attempt to catch any incoming balls", "Gather balls from the ground", "Gives all of your balls to allies", "Gather an ammount of balls equal to ammount of balls used last turn", "Heals an ally and returns them to their calm state", "" };
		actionTypes = new string[]{ "None", "Offense", "Defense", "Utility", "Utility", "Utility", "Utility", "Utility" };
		defaultTargetingTypes = new int[]{ 0, 2, 0, 0, 0, 0, 1, 0 };
		alternateTargetingTypes = new int[]{ 0, 1, 0, 0, 0, 0, 2, 0 };
		actionCosts = new int[]{ 0, 1, 0, 0, 0, 0, 2, 0 };
    }

    // Update is called once per frame
    void Update () {
        if (combat == null) {
            combat = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        } else {
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

    public override void Skill1() {
        int gift1 = this.heldBalls / 2;
        int gift2 = this.heldBalls / 2;

        if (heldBalls % 2 == 1) {
            PassOff(allies[0], gift1 + 1);
            PassOff(allies[1], gift2);
        } else {
            PassOff(allies[0], gift1);
            PassOff(allies[1], gift2);
        }
    }

    //PassOff here isnt an ability that can be selected, but rather a helper function to SilverPlatter 
    private void PassOff(Character target, int gift) {
        int diff = target.Capacity - target.heldBalls;
        while (diff > 0 && this.heldBalls > 0) {
            target.heldBalls++;
            diff = Target.Capacity - target.heldBalls;
        }
    }

    public override void Skill2() {
        //We currently have no way to check the past turn. This could be implemented easily though and would be very useful

        //Method of doing so in involving setting copying the past turn into oldCombatQueue at the end of the current execute phase. Thus, when in the current planning and execute phase, you can reference last turn

        //oldCombatQueue = combatQueue
    }

    public override void Skill3() {
        if ( Target == GameObject.Find("Elizabeth").GetComponent<Elizabeth>()){
            if (Target.allegiance == this.allegiance) { //is Haruna on our team?
                Elizabeth elizabeth = GameObject.Find("Elizabeth").GetComponent<Elizabeth>();
                elizabeth.Stamina += 15;
                elizabeth.Transform = false; //recall transform is a unique boolean to the Schola Grandis girls
            }
        } else if (Target == GameObject.Find("Victoria").GetComponent<Victoria>() ){
            if (Target.allegiance == this.allegiance) { //is Chikako on our team?
               Victoria victoria = GameObject.Find("Victoria").GetComponent<Victoria>();
                victoria.Stamina += 15;
                victoria.Transform = false; //recall transform is a unique boolean to the Schola Grandis girls
            }
        }
    }

    public override void Skill4() {

    }

}
