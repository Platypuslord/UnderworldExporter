﻿using UnityEngine;
using System.Collections;

public class Boots : Armour {
	/*public int Durability;
	public int Protection;

	public string EquipFemaleLowest; 
	public string EquipFemaleLow;
	public string EquipFemaleMedium;
	public string EquipFemaleBest;
	
	public string EquipMaleLowest; 
	public string EquipMaleLow;
	public string EquipMaleMedium;
	public string EquipMaleBest;
	
	public Texture2D EquipDisplay;
	private ObjectInteraction objInt;
	private int previousQuality;
	// Use this for initialization
	void Start () {
		previousQuality=-1;
		objInt=this.gameObject.GetComponent<ObjectInteraction>();
	}
	
	// Update is called once per frame
	void Update () {
		if (previousQuality!=objInt.Quality)
		{
			previousQuality =objInt.Quality;
			
			if ((objInt.Quality>0) && (objInt.Quality<=15))
			{
				//Return lowest quality 
				//setEquipTexture(0)
				SetEquipTexture(EquipFemaleLowest);
			}
			else if ((objInt.Quality>15) && (objInt.Quality<=30))
			{
				//Low quality
				SetEquipTexture(EquipFemaleLow);
			}
			else if ((objInt.Quality>30) && (objInt.Quality<=45))
			{
				//Medium
				SetEquipTexture(EquipFemaleMedium);
			}
			else if ((objInt.Quality>45) && (objInt.Quality<=63))
			{
				//Best
				SetEquipTexture(EquipFemaleBest);
			}
		}
		objInt.EquipDisplay=Sprite.Create(EquipDisplay,new Rect(0,0,EquipDisplay.width,EquipDisplay.height), new Vector2(0.5f, 0.5f));
	}
	
	void SetEquipTexture(string EquipTexture)
	{
		EquipDisplay = Resources.Load <Texture2D> (EquipTexture);
	}*/
}