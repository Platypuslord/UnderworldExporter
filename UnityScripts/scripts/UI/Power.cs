﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Power Gem update
/// </summary>
public class Power : GuiBase {

	public string CurrentPower;
	private float PreviousCharge=-1.0f;
	private int RepeatCounter=0;
	private int PreviousIndex=-1;
	private RawImage uiText;

		public override void Start()
		{
			base.Start();
			if (uiText==null)
			{
				uiText= this.gameObject.GetComponent<RawImage>();
			}
		}



	void Update () {


		if ((PreviousCharge!=GameWorldController.instance.playerUW.PlayerCombat.Charge)||(GameWorldController.instance.playerUW.PlayerCombat.AttackCharging==true))
		{
				PreviousCharge=GameWorldController.instance.playerUW.PlayerCombat.Charge;
			 
			int index= (int)GameWorldController.instance.playerUW.PlayerCombat.Charge/10;
	
			if (index==10)
			{
				if (!IsInvoking("UpdateMaxCharge"))
				{
					InvokeRepeating("UpdateMaxCharge",0.0f,0.1f);
				}
			}
			else
			{
				if (index!=PreviousIndex)
				{
					RepeatCounter=0;
					uiText.texture=Resources.Load <Texture2D> ("UW1/HUD/Power/Power_"+ index.ToString("0000"));
					CurrentPower="HUD/Power/Power_"+ index.ToString("0000");
				}
			}
		}
	else
		{
			if (IsInvoking("UpdateMaxCharge"))
			{
				CancelInvoke("UpdateMaxCharge");
				uiText.texture=Resources.Load <Texture2D> ("UW1/HUD/Power/Power_"+ 0.ToString("0000"));
			}
			RepeatCounter=0;
		}
	}

	void UpdateMaxCharge()
	{
		uiText.texture=Resources.Load <Texture2D> ("UW1/HUD/Power/Power_"+ (10+RepeatCounter).ToString("0000"));
		CurrentPower="HUD/Power/Power_"+ (10+RepeatCounter).ToString("0000");
		RepeatCounter++;
		if (RepeatCounter>3)
		{
			RepeatCounter=0;
		}
	}
}
