﻿using UnityEngine;
using System.Collections;

public class trap_base : object_base {


	public string TriggerObject;//Next in the chain

	public virtual void ExecuteTrap(int triggerX, int triggerY, int State)
	{
		//Do whatever
		Debug.Log ("ExecuteTrap " + this.name);
	}



	public virtual bool Activate(int triggerX, int triggerY, int State)
	{	//triggerX aka quality, triggerY aka owner
		CheckReferences();
		//Do what it needs to do.
		ExecuteTrap(triggerX,triggerY, State);

		//Trigger the next in the chain
		TriggerNext (triggerX, triggerY, State);

		//Stuff to happen after the trap has fired.
		PostActivate();
		return true;
	}

	public virtual void TriggerNext(int triggerX, int triggerY, int State)
	{

		GameObject triggerObj= GameObject.Find (TriggerObject);
		if (triggerObj!=null)
		{
			trigger_base trig= triggerObj.GetComponent<trigger_base>();
			if (trig!=null)
			{
				trig.Activate();
			}
			else
			{
				//try and find a trap instead
				trap_base trap = triggerObj.GetComponent<trap_base>();
				if (trap!=null)
				{
					trap.Activate(triggerX,triggerY,State);
					//return true;
				}
				else
				{
					Debug.Log ("no trigger or trap found on this object");
					//return false;
				}
			}
		}
	}

	public virtual void PostActivate()
	{
		//Destruction of traps is probably controlled by the trigger.
		int TriggerRepeat = (objInt.flags>>1) & 0x1;
		if (TriggerRepeat==0)
		{
			Destroy (this.gameObject);
		}
	}
}