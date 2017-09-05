﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Quest variables.
/// </summary>
/// 	 * per uw-specs.txt
/// 7.8.1 UW1 quest flags
/// 
/// flag   description
/// 
/// 0    Dr. Owl's assistant Murgo freed
/// 1    talked to Hagbard
/// 2    met Dr. Owl?
/// 3    permission to speak to king Ketchaval
/// 4    Goldthirst's quest to kill the gazer (1: gazer killed)
/// 5    Garamon, find talismans and throw into lava
/// 6    friend of Lizardman folk
/// 7    ?? (conv #24, Murgo)
/// 8    book from Bronus for Morlock
/// 9    "find Gurstang" quest
/// 10    where to find Zak, for Delanrey
/// 11    Rodrick killed
/// 
/// 32    status of "Knight of the Crux" quest
/// 0: no knight
/// 1: seek out Dorna Ironfist
/// 2: started quest to search the "writ of Lorne"
/// 3: found writ
/// 4: door to armoury opened
/// 
/// 
/// 
/// UW2 Quest flags
/// 0: PT related - The troll is released
/// 1: PT related
/// 2: PT related - Borne wants you to kill bishop
/// 3: PT related - Bishop is dead
/// 
/// 6: Helena asks you to speak to praetor loth
/// 7: Set after freeing praetor loth/characters now know about this object
/// 8:  Kintara tells you about Javra
/// 9:  Lobar tells you about the "virtues"
/// 10: PT related
/// 11: Listener under the castle is dead.
/// 12: used in Ice caverns to say the avatar can banish the guardians presence. Wand of altara?
/// 13: Mystral wants you to spy on altara
/// 
/// 15: Altara tells you about the listener
/// 
/// 18: You learn that the Trikili can talk.
/// 19: You know Relk has found the black jewel (josie tells you)
/// 20: You've met Mokpo
/// 22: Blog is now your friend(?)
/// 26: You know about the magic scepter (for the Zoranthus)
/// 27: You know about the sigil of binding(by bringing the scepter to zorantus)
/// 28: Took Krilner as a slave
/// 29: You know Dorstag has the gem(?)
/// 30: Dorstag refused your challenge(?)
/// 32: Met a Talorid
/// 35: The vortz are attacking!
/// 43: Patterson has gone postal
/// 45: Janar has been met and befriended
/// 
/// 47: You have recieve the quest from the triliki
/// 50: The keep is going to crash.
/// 
/// 54: Checked by Mors Gotha? related to keep crashing
/// 
/// 58: Set when meeting bishop
/// 
/// 61: You've brought water to Josie
/// 
/// 63: Used by Rawstag (you know blog?)
/// 107: Set after freeing praetor loth
/// 109: Set to 1 after first LB conversation. All castle occupants check this on first talk.
/// 110: Checked when talking to LB. The guardians forces are attacking
/// 112: checked when talking to LB. You have been fighting with the others
/// 114: checked when talking to LB. The servants are restless
/// 115: checked when talking to LB. The servants are on strike
/// 116: The strike has been called off.
/// 
/// 118: The wisps tell you about the triliki
/// 119: Fizzit the thief surrenders
/// 120: checked by miranda?
/// 121: You have defeated Dorstag
/// 123: Relk is dead
/// 128: 0-128 bit field of where the lines of power have been broken. NEed to fix string replacement to implement.
/// 129: Pits related
/// 131: You are told that you are in the prison tower =1
/// 	You are told you are in kilhorn keep =3
/// 	You are told you are in scintilus = 19
/// 132: Set to 2 during Kintara conversation
/// 
/// 134: PT related
/// 135: Checked by goblin in sewers
/// 143: Set to 33 after first LB conversation
public class Quest : UWEBase {


		/// <summary>
		/// Item ID for the sword of justice
		/// </summary>
		public const int TalismanSword=10;
		/// <summary>
		/// Item ID for the shield of valor
		/// </summary>
		public const int TalismanShield=55;
		/// <summary>
		/// Item id for the Taper of sacrifice
		/// </summary>
		public const int TalismanTaper=147;
		/// <summary>
		/// Item Id for the cup of wonder.
		/// </summary>
		public const int TalismanCup =174;
		/// <summary>
		/// Item ID for the book of honesty
		/// </summary>
		public const int TalismanBook=310;
		/// <summary>
		/// Item Id for the wine of compassion.
		/// </summary>
		public const int TalismanWine=191;
		/// <summary>
		/// Item ID for the ring of humility
		/// </summary>
		public const int TalismanRing=54;
		/// <summary>
		/// Item ID for the standard of honour.
		/// </summary>
		public const int TalismanHonour=287;

		/// <summary>
		/// The quest variable integers
		/// </summary>
	public int[] QuestVariables=new int[256];

	/// <summary>
	/// The no of talismans to still be cast into abyss in order to complete the game.
	/// </summary>
	public int TalismansRemaining; //= new bool[8];

		/// <summary>
		/// Tracks which garamon dream we are at.
		/// </summary>
	public int GaramonDream;//The next dreams to play
		/// <summary>
		/// Tracks which incense dream we are at
		/// </summary>
	public int IncenseDream;

	/// <summary>
	/// Tracks the last day that there was a garamon dream.
	/// </summary>
	public int DayGaramonDream=-1;

		/// <summary>
		/// Is tybal dead.
		/// </summary>
	//public bool isTybalDead;

		/// <summary>
		/// Is the orb on tybals level destroyed.
		/// </summary>
		public bool isOrbDestroyed;

		/// <summary>
		/// Has Garamon been buried. If so talismans can now be sacrificed.
		/// </summary>
		public bool isGaramonBuried;


		/// <summary>
		/// Is the cup of wonder found.
		/// </summary>
		public bool isCupFound;

	/// <summary>
	/// The x clocks Does a thing. Not sure what it is yet but used in conversations to track events. (Progress through game?)
	/// </summary>
	/// Possibly related to game variables
	/// Some known values
	/// 0=Staff strike 
	/// 1=Miranda conversations & related events in the castle
		/// 1 - Nystrul is curious about exploration.Set after entering lvl 1 from the route downwards. (set variable traps 52 may be related)
		/// 2 - After you visited another world.  (variable 17 is set to 16), dupre is tempted
		/// 3 - servants getting restless
		/// 4 - concerned about water, dupre is annoyed by patterson
		/// 5 - Dupre is bored / dupre is fetching water
		/// 7 - Tory is killed
		/// 9 - Charles finds a key
		/// 11 - Go see Nelson
		/// 13 - Patterson is dead
		/// 14 - Gem is weak
		/// 15 - Nystrul wants to see you again re endgame
		/// 16 - Nystrul questions have been answered Mars Gotha comes
	/// 2=Nystrul and blackrock gems treated
	/// 
	/// 15=Used in multiple convos. Possibly tells the game to process a change
	public int[] x_clocks=new int[32];


	void Start()
	{
		switch (_RES)
		{
		case GAME_UW2:
			QuestVariables=new int[200];//TODO:Figure out how many variables UW2 needs
			break;
		default:					
			QuestVariables=new int[36];
			break;
		}
	}


	/// <summary>
	/// Gets the next incense dream
	/// </summary>
	/// <returns>The incense dream.</returns>
	public int getIncenseDream()
	{
		if (IncenseDream>=3)
		{//Loop around
			IncenseDream=0;
		}
		return IncenseDream++;
	}

}
