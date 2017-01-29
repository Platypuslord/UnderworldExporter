﻿using UnityEngine;
using System.Collections;

public class a_change_terrain_trap : trap_base {
/*
A change terrain trap changes a tile from one type to another. It works over a range of tiles that are set by the traps
x,y values. In Underworld these are set by the the relative position of the trap within the tile.
The tile it acts on is controlled by the trigger.

In this implemntation the change terrain tiles are already created off map. When the trap is executed it copies the tiles to
their destination and removes the existing tile at that location. This allows the trap to be repeatable and support overlapping
traps changing terrain (eg the maze on the ice level of UW2)

Examples of it's usage
The Moonstone room on Level2
The path to the sword hilt on Level3

*/
	//public int TileX;//The tile from which the change terrain trap begins.
	//public int TileY;
	public int X;//The range of tiles to be changed.
	public int Y;
	public int NewFloorHeight;


	public override void ExecuteTrap (int triggerX, int triggerY, int State)
	{
		Vector3 Dist = new Vector3(-64*1.2f,0,0);
		for (int i = 0; i<=X;i++)
		{
			for (int j=0; j<=Y;j++)
			{
				//Find the tile at the location.
				GameObject ExistingTile = GameWorldController.FindTile(triggerX+i,triggerY+j,TileMap.SURFACE_FLOOR);
				//Find the tile that becomes the tile at that location.
				GameObject CTTile =GameWorldController.FindTileByName(this.name + "_" + (i).ToString ("D2") + "_" + (j).ToString ("D2"));   
				GameObject ReplacementTile =Instantiate(CTTile,CTTile.transform.position,CTTile.transform.rotation) as GameObject;
				ReplacementTile.transform.parent=CTTile.transform.parent;
				ReplacementTile.name = ExistingTile.name;
				if (ExistingTile != null)
				{					
					Destroy(ExistingTile);
				}
				if (ReplacementTile!=null)
				{
					Vector3 StartPos = ReplacementTile.transform.position;
					Vector3 EndPos = StartPos + Dist;
					ReplacementTile.transform.position = Vector3.Lerp (StartPos,EndPos,1.0f);
					//Change the tile type for the automap
					GameWorldController.instance.currentTileMap().Tiles[triggerX+i,triggerY+j].tileType=objInt().Quality & 0x1;
					GameWorldController.instance.currentTileMap().Tiles[triggerX+i,triggerY+j].Render=1;
					GameWorldController.instance.currentTileMap().Tiles[triggerX+i,triggerY+j].floorHeight=NewFloorHeight;
					switch (LayerMask.LayerToName(ReplacementTile.layer).ToUpper() )
					{
						case "WATER":
							GameWorldController.instance.currentTileMap().Tiles[triggerX+i,triggerY+j].isWater=true;
							GameWorldController.instance.currentTileMap().Tiles[triggerX+i,triggerY+j].isLava=false;
							break;
						case "MAPMESH":
							GameWorldController.instance.currentTileMap().Tiles[triggerX+i,triggerY+j].isWater=false;
							GameWorldController.instance.currentTileMap().Tiles[triggerX+i,triggerY+j].isLava=false;												
							break;
						case "LAVA":
							GameWorldController.instance.currentTileMap().Tiles[triggerX+i,triggerY+j].isWater=false;
							GameWorldController.instance.currentTileMap().Tiles[triggerX+i,triggerY+j].isLava=true;
							break;
					}
				}
			}
		}
	}
}
