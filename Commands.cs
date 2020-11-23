using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using Dungeonator;
using System.Reflection;
using Random = System.Random;
using FullSerializer;
using System.Collections;
using Gungeon;
using MonoMod.RuntimeDetour;
using MonoMod;



namespace BunnyMod
{
	// Token: 0x020000BA RID: 186
	public class Commands : ETGModule
	{
		// Token: 0x060003CA RID: 970 RVA: 0x00024798 File Offset: 0x00022998
		public override void Exit()
		{
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0002479B File Offset: 0x0002299B
		public override void Start()
		{
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000247A0 File Offset: 0x000229A0
		public override void Init()
		{
			ETGModConsole.Commands.AddGroup("bny", delegate (string[] args)
			{
				ETGModConsole.Log("<size=100><color=#ff0000ff>Please specify a command. Type 'bny help' for a list of commands.</color></size>", false);
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("help", delegate (string[] args)
			{
				ETGModConsole.Log("<size=100><color=#ff0000ff>List of Commands</color></size>", false);
				ETGModConsole.Log("<color=#ff0000ff>modular_scraps_while_blessed</color> -Turned Off By Default, Toggles whether the Modular can-auto scrap weapons in Blessed Mode.", false);
				ETGModConsole.Log("<color=#ff0000ff>curse_2.0</color> -Turned Off By Default, Toggles Harder LotJ and extra Curse benefits.", false);
			});

			ETGModConsole.Commands.GetGroup("bny").AddUnit("modular_scraps_while_blessed", delegate (string[] args)
			{
				bool flag = Commands.ModularDoesntAutoScrapGunsInBlessed;
				if (flag)
				{
					Commands.ModularDoesntAutoScrapGunsInBlessed = false;
					ETGModConsole.Log("Modular Will Now Auto-Scrap Guns in Blessed Mode.", false);
				}
				else
				{
					Commands.ModularDoesntAutoScrapGunsInBlessed = true;
					ETGModConsole.Log("Modular Will Now Not Auto-Scrap Guns in Blessed Mode.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("curse_2.0", delegate (string[] args)
			{
				bool flag = Commands.NoHarderLotJ;
				if (flag)
				{
					Commands.NoHarderLotJ = false;
					ETGModConsole.Log("Curse 2.0 Enabled.", false);
				}
				else
				{
					Commands.NoHarderLotJ = true;
					ETGModConsole.Log("Curse 2.0 Disabled.", false);
				}
			});

		}
		public static bool ModularDoesntAutoScrapGunsInBlessed = true;
		public static bool NoHarderLotJ = true;

		//public static bool NoSacrifice = true;
	}
}