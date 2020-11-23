using System;
using System.Collections.Generic;
using System.Reflection;

namespace BunnyMod
{
	// Token: 0x0200001A RID: 26
	public class AdvancedStringDB
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000096F8 File Offset: 0x000078F8
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00009714 File Offset: 0x00007914
		public StringTableManager.GungeonSupportedLanguages CurrentLanguage
		{
			get
			{
				return GameManager.Options.CurrentLanguage;
			}
			set
			{
				StringTableManager.SetNewLanguage(value, true);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00009720 File Offset: 0x00007920
		public AdvancedStringDB()
		{
			StringDB strings = ETGMod.Databases.Strings;
			strings.OnLanguageChanged = (Action<StringTableManager.GungeonSupportedLanguages>)Delegate.Combine(strings.OnLanguageChanged, new Action<StringTableManager.GungeonSupportedLanguages>(this.LanguageChanged));
			this.Core = new AdvancedStringDBTable(() => StringTableManager.CoreTable);
			this.Items = new AdvancedStringDBTable(() => StringTableManager.ItemTable);
			this.Enemies = new AdvancedStringDBTable(() => StringTableManager.EnemyTable);
			this.Intro = new AdvancedStringDBTable(() => StringTableManager.IntroTable);
			this.Synergies = new AdvancedStringDBTable(() => AdvancedStringDB.SynergyTable);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00009830 File Offset: 0x00007A30
		public void LanguageChanged(StringTableManager.GungeonSupportedLanguages newLang)
		{
			this.Core.LanguageChanged();
			this.Items.LanguageChanged();
			this.Enemies.LanguageChanged();
			this.Intro.LanguageChanged();
			this.Synergies.LanguageChanged();
			Action<StringTableManager.GungeonSupportedLanguages> onLanguageChanged = this.OnLanguageChanged;
			bool flag = onLanguageChanged == null;
			if (!flag)
			{
				onLanguageChanged(newLang);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00009894 File Offset: 0x00007A94
		public static Dictionary<string, StringTableManager.StringCollection> SynergyTable
		{
			get
			{
				StringTableManager.GetSynergyString("ThisExistsOnlyToLoadTables", -1);
				return (Dictionary<string, StringTableManager.StringCollection>)AdvancedStringDB.m_synergyTable.GetValue(null);
			}
		}

		// Token: 0x04000084 RID: 132
		public readonly AdvancedStringDBTable Core;

		// Token: 0x04000085 RID: 133
		public readonly AdvancedStringDBTable Items;

		// Token: 0x04000086 RID: 134
		public readonly AdvancedStringDBTable Enemies;

		// Token: 0x04000087 RID: 135
		public readonly AdvancedStringDBTable Intro;

		// Token: 0x04000088 RID: 136
		public readonly AdvancedStringDBTable Synergies;

		// Token: 0x04000089 RID: 137
		public static FieldInfo m_synergyTable = typeof(StringTableManager).GetField("m_synergyTable", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x0400008A RID: 138
		public Action<StringTableManager.GungeonSupportedLanguages> OnLanguageChanged;
	}
}
