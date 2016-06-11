﻿using UnityEngine;
using System.Collections;

public class SpellProp_OrtJux : SpellProp {
	//Properties for the Ort Jux spell.
	//public string ProjectileSprite = "Sprites/objects_023";
	//public float force=200.0f;
	//public int BaseDamage=3;
	SpellEffectPoison spelleffect;

	public override void init ()
	{
		base.init ();
		ProjectileSprite = "Sprites/objects_023";
		Force=200.0f;
		BaseDamage=3;
		impactFrameStart=46;
		impactFrameEnd=50;
		spread=0;
		noOfCasts=1;

	}

	public override void onHit (ObjectInteraction objInt)
	{//Sample enchantment poisons the target.
	if (objInt.ItemType==ObjectInteraction.NPC_TYPE)
		{
			playerUW.PlayerMagic.CastEnchantment(playerUW.gameObject,objInt.gameObject,SpellEffect.UW1_Spell_Effect_Poison,Magic.SpellRule_TargetOther);				
		}		
	}
}