﻿using UnityEngine;
using System.Collections;

public class a_delete_object_trap : trap_base {

	/*
Per Uw-formats.txt
   018b  a_delete object trap
         deletes an object when set off. "owner" and "quality" of the trap
         determines tile the object is to be found, "sp_link" points to the
         object.

Examples of usage
Level 3 removal of TMAP object when searching for the switch leading to the sword hilt.

	 */

	public override bool Activate (int triggerX, int triggerY, int State)
	{
		//Do what it needs to do.
		ExecuteTrap(triggerX,triggerY, State);

		//Delete object traps are always the end of the line so no more activations.

		PostActivate();
		return true;
	}


	public override void ExecuteTrap (int triggerX, int triggerY, int State)
	{
		GameObject triggerObj = GameObject.Find (TriggerObject);
		if (TriggerObject!=null)
		{
			Destroy(triggerObj);
		}
	}
}