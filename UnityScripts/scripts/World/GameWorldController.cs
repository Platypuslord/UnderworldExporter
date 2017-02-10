﻿using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Game world controller for controlling references and various global activities
/// </summary>

public class GameWorldController : UWEBase {

		/// <summary>
		/// Enables texture animation effects
		/// </summary>
	public bool EnableTextureAnimation;


		/// <summary>
		/// The level model parent object
		/// </summary>
	public GameObject LevelModel;
	
		/// <summary>
		/// The instance of this class
		/// </summary>
	public static GameWorldController instance;

		/// <summary>
		/// What level number we are currently on.
		/// </summary>	
	public int LevelNo;

		/// <summary>
		/// What level the player starts on in a quick start
		/// </summary>
		public int startLevel=0;
		/// <summary>
		/// What start position for the player.
		/// </summary>
		public Vector3 StartPos=new Vector3(38f, 4f, 2.7f);


	/// <summary>
	/// Array of cycled game palettes for animation effects.
	/// </summary>
	public Texture2D[] paletteArray= new Texture2D[8];

	/// <summary>
	/// The index of the palette currently in use
	/// </summary>
	public int paletteIndex=0;

	/// <summary>
	/// The palette index when going in reverse.
	/// </summary>
	public int paletteIndexReverse=0;

	/// <summary>
	/// The Variables for the check/set variable traps
	/// </summary>
	public int[] variables = new int[127];
	
	/// <summary>
	/// The tilemap class for the game
	/// </summary>
	public TileMap[] Tilemaps = new TileMap[9];

	/// <summary>
	/// The player character.
	/// </summary>
	[SerializeField]
	private UWCharacter _playerUW;
	public UWCharacter playerUW {
		get { return _playerUW; }
		set { _playerUW=value; }
		}


	/// <summary>
	/// The music controller for the game
	/// </summary>
	private MusicController mus;


	/// <summary>
	/// The game object that picked up items are parented to.
	/// </summary>
	public GameObject InventoryMarker;

	/// <summary>
	/// The game name.
	/// </summary>
	/// Value is passed to UWEBase and used in all resource file loads
	public string game;

	//public string UI_Name;
	/// <summary>
	/// The object master class for storing and reading object properties in an external file
	/// </summary>
	public ObjectMasters objectMaster;

	/// <summary>
	/// The critter properties from objects.dat
	/// </summary>
	public Critters critter;

	/// <summary>
	/// Common Object Properties
	/// </summary>
	public CommonObjProps commobj;

	/// <summary>
	/// Weapon properties.
	/// </summary>
	public WeaponProps weaponprops;

	/// <summary>
	/// The grey scale shader. Reference to allow loading of a hidden shader.
	/// </summary>
	public Shader greyScale;

	/// <summary>
	/// The vortex effect shader.  Reference to allow loading of a hidden shader.
	/// </summary>
	public Shader vortex;

	/// <summary>
	/// Is the game at the main menu or should it start at the mainmenu.
	/// </summary>
	public bool AtMainMenu;

	/// <summary>
	/// Path to lev.ark file to load
	/// </summary>
	public string Lev_Ark_File;

	/// <summary>
	/// The graves file for associating grave textures with grave objects
	/// </summary>
	public string Graves_File;	
	
	/// <summary>
	/// The material master list for matching the texture list to materials.
	/// </summary>
	public Material[] MaterialMasterList=new Material[260];

		/// <summary>
		/// The material for doors
		/// </summary>
		public Material[] MaterialDoors=new Material[13];

	/// <summary>
	/// Gameobject to load the objects at
	/// </summary>
	public GameObject ObjectMarker;

		/// <summary>
		/// The object lists for each level.
		/// </summary>
	public ObjectLoader[] objectList= new ObjectLoader[9];

	public RAIN.Navigation.NavMesh.NavMeshRig NavRigLand;
	public RAIN.Navigation.NavMesh.NavMeshRig NavRigWater;//To implement for create npc


	/// <summary>
	/// Shared palettes for artwork
	/// </summary>
	public PaletteLoader palLoader;

	/// <summary>
	/// The bytloader for bty files
	/// </summary>
	public BytLoader bytloader;
		/// <summary>
		/// The tex loader for textures
		/// </summary>
	public TextureLoader texLoader;
		/// <summary>
		/// The spell icons gr loader
		/// </summary>
	public GRLoader SpellIcons;
		/// <summary>
		/// The object art gr loader
		/// </summary>
	public GRLoader ObjectArt;

		/// <summary>
		/// The door art.
		/// </summary>
	public GRLoader DoorArt;

	/// <summary>
	/// The tm object art.
	/// </summary>
	public GRLoader TmObjArt;

	/// <summary>
	/// The tm flat art.
	/// </summary>
	public GRLoader TmFlatArt;

	/// <summary>
	/// Small animations art.
	/// </summary>
	public GRLoader TmAnimo;

	/// <summary>
	/// The lev ark file data.
	/// </summary>
	private char[] lev_ark_file_data;

	/// <summary>
	/// The female armor
	/// </summary>
	public GRLoader armor_f;

	/// <summary>
	/// The male armor.
	/// </summary>
	public GRLoader armor_m;

	/// <summary>
	/// The cursors art
	/// </summary>
	public GRLoader grCursors;

	/// <summary>
	/// The health & mana flasks.
	/// </summary>
	public GRLoader grFlasks;

	void Awake()
	{
		instance=this;
		UWEBase._RES = game;
		Loader._RES=game;
		objectMaster=new ObjectMasters();
		objectMaster.Load(Application.dataPath + "//..//" + UWEBase._RES + "_object_config.txt");
		critter = new Critters();
		critter.Load(Application.dataPath + "//..//" + UWEBase._RES + "_critters.txt");
		commobj=new CommonObjProps();
		commobj.Load(Application.dataPath + "//..//" + UWEBase._RES + "_comobj.txt");
		weaponprops =new WeaponProps();
		weaponprops.Load(Application.dataPath + "//..//" + UWEBase._RES + "_weapons.txt");

		palLoader = new PaletteLoader();
		palLoader.Path=Loader.BasePath + "data\\pals.dat";
		palLoader.LoadPalettes();
		bytloader=new BytLoader();

		texLoader=new TextureLoader();
		ObjectArt=new GRLoader(GRLoader.OBJECTS_GR);
		SpellIcons = new GRLoader(GRLoader.SPELLS_GR);
		DoorArt=new GRLoader(GRLoader.DOORS_GR);
		TmObjArt=new GRLoader(GRLoader.TMOBJ_GR);
		TmFlatArt=new GRLoader(GRLoader.TMFLAT_GR);
		TmAnimo=new GRLoader(GRLoader.ANIMO_GR);
		armor_f=new GRLoader(GRLoader.ARMOR_F_GR);
		armor_m=new GRLoader(GRLoader.ARMOR_M_GR);
		grCursors = new GRLoader(GRLoader.CURSORS_GR);
		grFlasks=new GRLoader(GRLoader.FLASKS_GR);
	}

	void Start () {

		instance=this;

		//Load up my map materials
		for (int i =0; i<=MaterialMasterList.GetUpperBound(0);i++)
		{
			MaterialMasterList[i]=(Material)Resources.Load(_RES+"/Materials/textures/" + _RES + "_" + i.ToString("d3"));
			MaterialMasterList[i].mainTexture= texLoader.LoadImageAt(i);
		}

		//Load up my door texture
		for (int i =0; i<=MaterialDoors.GetUpperBound(0);i++)
		{
			MaterialDoors[i]= (Material)Resources.Load(_RES + "/Materials/doors/doors_" +i.ToString("d2") +"_material");	
			MaterialDoors[i].mainTexture= DoorArt.LoadImageAt(i);
		}

		//Load up my tile maps
		//First read in my lev_ark file in it's entirey
		if (!DataLoader.ReadStreamFile(Loader.BasePath + Lev_Ark_File, out lev_ark_file_data))
				{
						Debug.Log(Lev_Ark_File + "File not loaded");
						Application.Quit();
				}




		if (EnableTextureAnimation==true)
		{
			UWHUD.instance.CutsceneFullPanel.SetActive(false);
			InvokeRepeating("UpdateAnimation",0.2f,0.2f);
		}

		if ((AtMainMenu) && (!LevelSerializer.IsDeserializing))
		{
			SwitchLevel(-1);//Turn off all level maps
			UWHUD.instance.CutsceneFullPanel.SetActive(true);
			UWHUD.instance.mainmenu.gameObject.SetActive(true);
			//Freeze player movement and put them at a set location
			playerUW.playerController.enabled=false;
			playerUW.playerMotor.enabled=false;
			playerUW.transform.position=Vector3.zero;

			getMus().InIntro=true;
		}
		else
		{
			AtMainMenu=false;
			UWHUD.instance.CutsceneFullPanel.SetActive(false);	
			UWHUD.instance.mainmenu.gameObject.SetActive(false);


		}
		InvokeRepeating("PositionDetect",0.0f,0.02f);
		return;
	}

		/// <summary>
		/// Gets the current level model.
		/// </summary>
		/// <returns>The current level model gameobject</returns>
	public GameObject getCurrentLevelModel()
	{
		//return GameWorldController.instance.WorldModel[LevelNo].transform.FindChild("Level" + LevelNo + "_model").gameObject;
		return LevelModel;
	}

	/// <summary>
	/// Updates the global shader parameter for the colorpalette shaders at set intervals. To enable texture animation
	/// </summary>
	void UpdateAnimation()
	{
		Shader.SetGlobalTexture ("_ColorPaletteIn",paletteArray[paletteIndex]);

		if (paletteIndex<paletteArray.GetUpperBound(0))
		{
			paletteIndex++;
		}
		else
		{
			paletteIndex=0;
		}

		//In Reverse

		Shader.SetGlobalTexture ("_ColorPaletteInReverse",paletteArray[paletteIndexReverse]);
		
		if (paletteIndexReverse>0)
		{
			paletteIndexReverse--;
		}
		else
		{
			paletteIndexReverse=paletteArray.GetUpperBound(0);
		}
		return;
	}

	/// <summary>
	/// inds a door in the tile pointed to by the two coordinates.
	/// </summary>
	/// <returns>The door.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static GameObject findDoor(int x, int y)
	{
		return GameObject.Find ("door_" +x .ToString ("D3") + "_" + y.ToString ("D3"));
	}

	/// <summary>
	/// Finds the tile or wall at the specified coordinates.
	/// </summary>
	/// <returns>The tile.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="surface">Surface.</param>
	public static GameObject FindTile(int x, int y, int surface)
	{
		string tileName = GetTileName (x,y,surface);
		return instance.getCurrentLevelModel().transform.FindChild (tileName).gameObject;
	}
	
		/// <summary>
		/// Gets the gameobject name for the specified tile x,y and surface. Eg Wall_02_03, Tile_22_23
		/// </summary>
		/// <returns>The tile name.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="surface">Surface.</param>
		/// Surfaces are 
	public static string GetTileName(int x, int y, int surface)
	{//Assumes we'll only ever need to deal with open/solid tiles with floors and ceilings.
		string tileName;
		string X; string Y;
		X=x.ToString ("D2");
		Y=y.ToString ("D2");
		switch (surface)
		{
		case TileMap.SURFACE_WALL:  //SURFACE_WALL:
		{
			tileName= "Wall_" + X + "_" + Y;
			break;
		}
		case TileMap.SURFACE_CEIL: //SURFACE_CEIL:
		{
			tileName="Ceiling_" + X + "_" + Y;
			break;
		}
		case TileMap.SURFACE_FLOOR:
		case TileMap.SURFACE_SLOPE:
		default:
		{
			tileName="Tile_" + X  + "_" + Y;
			break;
		}
		}
		return tileName;
	}
	
	/// <summary>
	/// Finds a tile in the current level by name
	/// </summary>
	/// <returns>The tile by name.</returns>
	/// <param name="tileName">Tile name.</param>
	public static GameObject FindTileByName(string tileName)
	{
		return instance.getCurrentLevelModel().transform.FindChild (tileName).gameObject;
	}
	
	/// <summary>
	/// Returns the transform of the levels object marker. So objects will remain on that level
	/// </summary>
	/// <returns>The marker.</returns>
	public Transform LevelMarker()
	{
		return ObjectMarker.transform;
		//return LevelObjects[LevelNo].transform;
	}

	/// <summary>
	/// Switches the level to another one. Disables the map and level objects of the old one.
	/// </summary>
	/// <param name="newLevelNo">New level no.</param>
		/// 
		public void SwitchLevel(int newLevelNo)
		{
			if (newLevelNo!=-1)
			{

					//Check loading
					if (Tilemaps[newLevelNo]==null)
					{//Data has not been loaded for this level
						Tilemaps[newLevelNo]=new TileMap();
						Tilemaps[newLevelNo].thisLevelNo=newLevelNo;
						Tilemaps[newLevelNo].BuildTileMapUW(lev_ark_file_data,1,newLevelNo);
						objectList[newLevelNo]=new ObjectLoader();
						objectList[newLevelNo].LoadObjectList( Tilemaps[newLevelNo],lev_ark_file_data,1);
						//Tilemaps[i].setRooms();
						//Tilemaps[i].MergeWaterRegions();
						//Tilemaps[i].MergeLavaRegions();
						Tilemaps[newLevelNo].CleanUp(1);//I can reduce the tile map complexity after I know about what tiles change due to objects	
					}


				if(LevelNo!=-1)
				{//Changing from a level that has already loaded
					//Update the positions of all object interactions in the level
					//UpdatePositions();

					//Store the state of the object list with just the objects in objects transform for when I re
					ObjectLoader.UpdateObjectList();	
				}


				//Get my object info into the tile map.
				LevelNo=newLevelNo;
				TileMapRenderer.GenerateLevelFromTileMap(LevelModel,1,Tilemaps[newLevelNo],objectList[newLevelNo]);
				ObjectLoader.RenderObjectList(objectList[newLevelNo],Tilemaps[newLevelNo],LevelMarker().gameObject);
				GenerateNavmesh(NavRigLand);
				GenerateNavmesh(NavRigWater);
						//TODO Lava
			}

		}

		/// <summary>
		/// Switchs the level and puts the player at the floor level of the new level
		/// </summary>
		/// <param name="newLevelNo">New level no.</param>
		/// <param name="newTileX">New tile x.</param>
		/// <param name="newTileY">New tile y.</param>
		public void SwitchLevel(int newLevelNo, int newTileX, int newTileY)
		{
				SwitchLevel(newLevelNo);
				float targetX=(float)newTileX*1.2f + 0.6f;
				float targetY= (float)newTileY*1.2f + 0.6f;
				float Height = ((float)(GameWorldController.instance.Tilemaps[newLevelNo].GetFloorHeight(newTileX,newTileY)))*0.15f;
				GameWorldController.instance.playerUW.transform.position=new Vector3(targetX,Height+0.1f,targetY);
		}

		// This will regenerate the navigation mesh when called
		void GenerateNavmesh(RAIN.Navigation.NavMesh.NavMeshRig NavRig)
		{//From Legacy.rivaltheory.com/forums/topics/runtime-navmesh-generation-and-path-finding-tutorial
				int _threadcount=4;
				// Unregister any navigation mesh we may already have (probably none if you are using this)
				NavRig.NavMesh.UnregisterNavigationGraph();
				NavRig.NavMesh.Size = 20;
				//float startTime = Time.time;
				NavRig.NavMesh.StartCreatingContours(_threadcount);
				NavRig.NavMesh.CreateAllContours();
				//float endTime = Time.time;
				//Debug.Log("NavMesh generated in " + (endTime - startTime) + "s");
				NavRig.NavMesh.RegisterNavigationGraph();
				NavRig.Awake();

		}


	/*
	public void SwitchLevel(int newLevelNo)
	{
		for (int i=0; i <=WorldModel.GetUpperBound(0);i++)
		{
			if(WorldModel[i]==null)
			{
				WorldModel[i]=GameObject.Find("_Level" + i);
			}
			WorldModel[i].SetActive(i==newLevelNo);
			LevelObjects[i].SetActive(i==newLevelNo);
		}	
		LevelNo=newLevelNo;
	}*/

	/// <summary>
	/// Freezes the movement of the specified object if it has a rigid body attached.
	/// </summary>
	/// <param name="myObj">My object.</param>
	public static void FreezeMovement(GameObject myObj)
	{//Stop objects which can move in the 3d world from moving when they are in the inventory or containers.
			Rigidbody rg = myObj.GetComponent<Rigidbody>();
			if (rg!=null)
			{
					rg.useGravity=false;
					rg.constraints = 
							RigidbodyConstraints.FreezeRotationX 
							| RigidbodyConstraints.FreezeRotationY 
							| RigidbodyConstraints.FreezeRotationZ 
							| RigidbodyConstraints.FreezePositionX 
							| RigidbodyConstraints.FreezePositionY 
							| RigidbodyConstraints.FreezePositionZ;
			}
	}

		/// <summary>
		/// Unfreeze the movement of the specified object if it has a rigid body attached.
		/// </summary>
		/// <param name="myObj">My object.</param>
		public static void UnFreezeMovement(GameObject myObj)
		{//Allow objects which can move in the 3d world to moving when they are released.
				Rigidbody rg = myObj.GetComponent<Rigidbody>();
				if (rg!=null)
				{
						rg.useGravity=true;
						rg.constraints = 
								RigidbodyConstraints.FreezeRotationX 
								| RigidbodyConstraints.FreezeRotationY 
								| RigidbodyConstraints.FreezeRotationZ;

				}
		}

		public MusicController getMus()
		{
			if (mus==null)	
			{
				mus=GameObject.Find("_MusicController").GetComponent<MusicController>();
			}
			return mus;
		}


		public TileMap currentTileMap()
		{
			return Tilemaps[LevelNo];	
		}

		/// <summary>
		/// Detects where the player currently is an updates their swimming state and auto map as needed.
		/// </summary>
		public void PositionDetect()
		{
				if (AtMainMenu==true)
				{
						return;
				}
				TileMap.visitTileX =(int)(playerUW.transform.position.x/1.2f);
				TileMap.visitTileY =(int)(playerUW.transform.position.z/1.2f);
				currentTileMap().SetTileVisited(TileMap.visitTileX,TileMap.visitTileY);
				GameWorldController.instance.playerUW.isSwimming=((TileMap.OnWater) && (!GameWorldController.instance.playerUW.isWaterWalking)) ;

		}

		public ObjectLoader CurrentObjectList()
		{
				return objectList[LevelNo];
		}

		/// <summary>
		/// Moves the object to the game world where it will be managed by the objectloader list
		/// </summary>
		/// <param name="obj">Object.</param>
		public static void MoveToWorld(GameObject obj)
		{
				//Debug.Log(obj.name + "is moved to world");
				MoveToWorld(obj.GetComponent<ObjectInteraction>());
		}

		public static void MoveToWorld(ObjectInteraction obj)
		{
			//Add item to a free slot on the item list and point the instance back to this.
				ObjectLoader.AssignObjectToList(ref obj);

				//Not needed???
		}

		/// <summary>
		/// Moves to inventory where it will no longer be managed by the objectloader list.
		/// </summary>
		/// <param name="obj">Object.</param>
		public static void MoveToInventory(GameObject obj)
		{
			MoveToInventory(obj.GetComponent<ObjectInteraction>());
		}

		public static void MoveToInventory(ObjectInteraction obj)
		{//Break the instance back to the object list
			obj.objectloaderinfo.InUseFlag=0;//This frees up the slot to be replaced with another item.
		}

		public void UpdatePositions()
		{
			foreach (Transform t in GameWorldController.instance.ObjectMarker.transform) 
			{
				if (t.gameObject.GetComponent<ObjectInteraction>()!=null)
				{
					t.gameObject.GetComponent<ObjectInteraction>().UpdatePosition();	
				}
			}
		}

}