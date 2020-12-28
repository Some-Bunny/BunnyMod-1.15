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
				ETGModConsole.Log("<color=#ff0000ff>curse_2.0</color> -Turned Off By Default, Toggles Curse 2.0.", false);
				ETGModConsole.Log("<color=#ff0000ff>random_artifact_on_load</color> -Turned Off By Default, Gives the Player a random artifact on loading a new run.", false);
				ETGModConsole.Log("<color=#ff0000ff>deicide_on_load</color> -Turned Off By Default, Gives the Player ALL artifacts on loading a new run. Good fucking luck mate.", false);
				ETGModConsole.Log("<color=#ff0000ff>'Name Of Artifact'_on_load</color> -Turned Off By Default, Gives the Player a specific artifact on loading a new run. This can be stacked for mulitple specific ones.", false);
				ETGModConsole.Log("<color=#ff0000ff>'custom_on_load</color> -Turned Off By Default, Gives the Player their current custom loadout on loading a new run.", false);
				ETGModConsole.Log("<color=#ff0000ff>'clear_custom_loadout</color> -Clears the Custom Artifact Loadout pool.", false);

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
				bool flag = JammedSquire.NoHarderLotJ;
				if (flag)
				{
					JammedSquire.NoHarderLotJ = false;
					ETGModConsole.Log("Curse 2.0 Enabled.", false);
				}
				else
				{
					JammedSquire.NoHarderLotJ = true;
					ETGModConsole.Log("Curse 2.0 Disabled.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("random_artifact_on_load", delegate (string[] args)
			{
				bool flag = ArtifactMonger.RandomArtifactMode;
				if (flag)
				{
					ArtifactMonger.RandomArtifactMode = false;
					ETGModConsole.Log("Random Artifacts Disabled.", false);
				}
				else
				{
					Commands.CustomLoadoutArtifactsEnabled = false;
					ArtifactMonger.RandomArtifactMode = true;
					DeicideShrine.AllArtifactMode = false;
					ETGModConsole.Log("Random Artifacts Enabled.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("deicide_on_load", delegate (string[] args)
			{
				bool flag = ArtifactMonger.RandomArtifactMode;
				if (flag)
				{
					DeicideShrine.AllArtifactMode = false;
					ETGModConsole.Log("Deicide Mode Disabled.", false);
				}
				else
				{
					Commands.CustomLoadoutArtifactsEnabled = false;
					DeicideShrine.AllArtifactMode = true;
					ArtifactMonger.RandomArtifactMode = false;
					ETGModConsole.Log("Deicide Mode Enabled.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("custom_on_load", delegate (string[] args)
			{
				bool flag = ArtifactMonger.RandomArtifactMode;
				if (flag)
				{
					Commands.CustomLoadoutArtifactsEnabled = false;
					ETGModConsole.Log("Custom Mode Disabled.", false);
				}
				else
				{
					Commands.CustomLoadoutArtifactsEnabled = true;
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					ETGModConsole.Log("Custom Mode Enabled.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("clear_custom_loadout", delegate (string[] args)
			{
				{
					//Commands.CustomLoadoutArtifactsEnabled = true;
					//DeicideShrine.AllArtifactMode = false;
					//ArtifactMonger.RandomArtifactMode = false;
					Commands.AttractionEnabled = false;
					Commands.AvariceEnabled = false;
					Commands.BolsterEnabled = false;
					Commands.DazeEnabled = false;
					Commands.FodderEnabled = false;
					Commands.FrailtyEnabled = false;
					Commands.GlassEnabled = false;
					Commands.MegalomaniaEnabled = false;
					Commands.PreyEnabled = false;
					Commands.RevengeEnabled = false;
					Commands.SacrificeEnabled = false;

					ETGModConsole.Log("Custom Loadout Cleared.", false);
				}
			});
			/*

			AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA



			*/
			ETGModConsole.Commands.GetGroup("bny").AddUnit("attraction_on_load", delegate (string[] args)
			{
				bool flag = Commands.AttractionEnabled;
				if (flag)
				{
					Commands.AttractionEnabled = false;
					ETGModConsole.Log("Attraction Removed from Loadout.", false);
				}
				else
				{
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					Commands.CustomLoadoutArtifactsEnabled = true;
					Commands.AttractionEnabled = true;
					ETGModConsole.Log("Attraction Added to Loadout.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("avarice_on_load", delegate (string[] args)
			{
				bool flag = Commands.AvariceEnabled;
				if (flag)
				{
					Commands.AvariceEnabled = false;
					ETGModConsole.Log("Avarice Removed from Loadout.", false);
				}
				else
				{
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					Commands.CustomLoadoutArtifactsEnabled = true;
					Commands.AvariceEnabled = true;
					ETGModConsole.Log("Avarice Added to Loadout.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("bolster_on_load", delegate (string[] args)
			{
				bool flag = Commands.BolsterEnabled;
				if (flag)
				{
					Commands.BolsterEnabled = false;
					ETGModConsole.Log("Bolster Removed from Loadout.", false);
				}
				else
				{
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					Commands.CustomLoadoutArtifactsEnabled = true;
					Commands.BolsterEnabled = true;
					ETGModConsole.Log("Bolster Added to Loadout.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("daze_on_load", delegate (string[] args)
			{
				bool flag = Commands.DazeEnabled;
				if (flag)
				{
					Commands.DazeEnabled = false;
					ETGModConsole.Log("Daze Removed from Loadout.", false);
				}
				else
				{
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					Commands.CustomLoadoutArtifactsEnabled = true;
					Commands.DazeEnabled = true;
					ETGModConsole.Log("Daze Added to Loadout.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("fodder_on_load", delegate (string[] args)
			{
				bool flag = Commands.FodderEnabled;
				if (flag)
				{
					Commands.FodderEnabled = false;
					ETGModConsole.Log("Fodder Removed from Loadout.", false);
				}
				else
				{
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					Commands.CustomLoadoutArtifactsEnabled = true;
					Commands.FodderEnabled = true;
					ETGModConsole.Log("Fodder Added to Loadout.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("frailty_on_load", delegate (string[] args)
			{
				bool flag = Commands.FrailtyEnabled;
				if (flag)
				{
					Commands.FrailtyEnabled = false;
					ETGModConsole.Log("Frailty Removed from Loadout.", false);
				}
				else
				{
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					Commands.CustomLoadoutArtifactsEnabled = true;
					Commands.FrailtyEnabled = true;
					ETGModConsole.Log("Frailty Added to Loadout.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("glass_on_load", delegate (string[] args)
			{
				bool flag = Commands.GlassEnabled;
				if (flag)
				{
					Commands.GlassEnabled = false;
					ETGModConsole.Log("Glass Removed from Loadout.", false);
				}
				else
				{
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					Commands.CustomLoadoutArtifactsEnabled = true;
					Commands.GlassEnabled = true;
					ETGModConsole.Log("Glass Added to Loadout.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("megalomania_on_load", delegate (string[] args)
			{
				bool flag = Commands.MegalomaniaEnabled;
				if (flag)
				{
					Commands.MegalomaniaEnabled = false;
					ETGModConsole.Log("Megalomania Removed from Loadout.", false);
				}
				else
				{
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					Commands.CustomLoadoutArtifactsEnabled = true;
					Commands.MegalomaniaEnabled = true;
					ETGModConsole.Log("Megalomania Added to Loadout.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("prey_on_load", delegate (string[] args)
			{
				bool flag = Commands.PreyEnabled;
				if (flag)
				{
					Commands.PreyEnabled = false;
					ETGModConsole.Log("Prey Removed from Loadout.", false);
				}
				else
				{
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					Commands.CustomLoadoutArtifactsEnabled = true;
					Commands.PreyEnabled = true;
					ETGModConsole.Log("Prey Added to Loadout.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("revenge_on_load", delegate (string[] args)
			{
				bool flag = Commands.RevengeEnabled;
				if (flag)
				{
					Commands.RevengeEnabled = false;
					ETGModConsole.Log("Revenge Removed from Loadout.", false);
				}
				else
				{
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					Commands.CustomLoadoutArtifactsEnabled = true;
					Commands.RevengeEnabled = true;
					ETGModConsole.Log("Revenge Added to Loadout.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("sacrifice_on_load", delegate (string[] args)
			{
				bool flag = Commands.SacrificeEnabled;
				if (flag)
				{
					Commands.SacrificeEnabled = false;
					ETGModConsole.Log("Sacrifice Removed from Loadout.", false);
				}
				else
				{
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					Commands.CustomLoadoutArtifactsEnabled = true;
					Commands.SacrificeEnabled = true;
					ETGModConsole.Log("Sacrifice Added to Loadout.", false);
				}
			});
			ETGModConsole.Commands.GetGroup("bny").AddUnit("enigma_on_load", delegate (string[] args)
			{
				bool flag = Commands.SacrificeEnabled;
				if (flag)
				{
					Commands.EnigmaEnabled = false;
					ETGModConsole.Log("Enigma Removed from Loadout.", false);
				}
				else
				{
					DeicideShrine.AllArtifactMode = false;
					ArtifactMonger.RandomArtifactMode = false;
					Commands.CustomLoadoutArtifactsEnabled = true;
					Commands.EnigmaEnabled = true;
					ETGModConsole.Log("Enigma Added to Loadout.", false);
				}
			});
		}
		public static bool ModularDoesntAutoScrapGunsInBlessed = true;
		public static bool NoHarderLotJ = true;

		public static bool AttractionEnabled = false;
		public static bool AvariceEnabled = false;
		public static bool BolsterEnabled = false;
		public static bool DazeEnabled = false;
		public static bool FodderEnabled = false;
		public static bool FrailtyEnabled = false;
		public static bool GlassEnabled = false;
		public static bool MegalomaniaEnabled = false;
		public static bool PreyEnabled = false;
		public static bool RevengeEnabled = false;
		public static bool SacrificeEnabled = false;
		public static bool EnigmaEnabled = false;


		public static bool CustomLoadoutArtifactsEnabled = false;


	}
}