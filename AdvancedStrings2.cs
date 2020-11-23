using System;
using System.Collections.Generic;

namespace BunnyMod
{
	// Token: 0x0200001B RID: 27
	public sealed class AdvancedStringDBTable
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000098E0 File Offset: 0x00007AE0
		public Dictionary<string, StringTableManager.StringCollection> Table
		{
			get
			{
				Dictionary<string, StringTableManager.StringCollection> result;
				bool flag = (result = this._CachedTable) == null;
				if (flag)
				{
					result = (this._CachedTable = this._GetTable());
				}
				return result;
			}
		}

		// Token: 0x17000009 RID: 9
		public StringTableManager.StringCollection this[string key]
		{
			get
			{
				return this.Table[key];
			}
			set
			{
				this.Table[key] = value;
				int num = this._ChangeKeys.IndexOf(key);
				bool flag = num > 0;
				if (flag)
				{
					this._ChangeValues[num] = value;
				}
				else
				{
					this._ChangeKeys.Add(key);
					this._ChangeValues.Add(value);
				}
				JournalEntry.ReloadDataSemaphore++;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000099A8 File Offset: 0x00007BA8
		internal AdvancedStringDBTable(Func<Dictionary<string, StringTableManager.StringCollection>> _getTable)
		{
			this._ChangeKeys = new List<string>();
			this._ChangeValues = new List<StringTableManager.StringCollection>();
			this._GetTable = _getTable;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000099D0 File Offset: 0x00007BD0
		public bool ContainsKey(string key)
		{
			return this.Table.ContainsKey(key);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000099F0 File Offset: 0x00007BF0
		public void Set(string key, string value)
		{
			StringTableManager.StringCollection stringCollection = new StringTableManager.SimpleStringCollection();
			stringCollection.AddString(value, 1f);
			bool flag = this.Table.ContainsKey(key);
			if (flag)
			{
				this.Table[key] = stringCollection;
			}
			else
			{
				this.Table.Add(key, stringCollection);
			}
			int num = this._ChangeKeys.IndexOf(key);
			bool flag2 = num > 0;
			if (flag2)
			{
				this._ChangeValues[num] = stringCollection;
			}
			else
			{
				this._ChangeKeys.Add(key);
				this._ChangeValues.Add(stringCollection);
			}
			JournalEntry.ReloadDataSemaphore++;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00009A94 File Offset: 0x00007C94
		public void SetComplex(string key, List<string> values, List<float> weights)
		{
			StringTableManager.StringCollection stringCollection = new StringTableManager.ComplexStringCollection();
			for (int i = 0; i < values.Count; i++)
			{
				string value = values[i];
				float weight = weights[i];
				stringCollection.AddString(value, weight);
			}
			this.Table[key] = stringCollection;
			int num = this._ChangeKeys.IndexOf(key);
			bool flag = num > 0;
			if (flag)
			{
				this._ChangeValues[num] = stringCollection;
			}
			else
			{
				this._ChangeKeys.Add(key);
				this._ChangeValues.Add(stringCollection);
			}
			JournalEntry.ReloadDataSemaphore++;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00009B3C File Offset: 0x00007D3C
		public string Get(string key)
		{
			return StringTableManager.GetString(key);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00009B54 File Offset: 0x00007D54
		public void LanguageChanged()
		{
			this._CachedTable = null;
			Dictionary<string, StringTableManager.StringCollection> table = this.Table;
			for (int i = 0; i < this._ChangeKeys.Count; i++)
			{
				table[this._ChangeKeys[i]] = this._ChangeValues[i];
			}
		}

		// Token: 0x0400008B RID: 139
		private readonly Func<Dictionary<string, StringTableManager.StringCollection>> _GetTable;

		// Token: 0x0400008C RID: 140
		private Dictionary<string, StringTableManager.StringCollection> _CachedTable;

		// Token: 0x0400008D RID: 141
		private readonly List<string> _ChangeKeys;

		// Token: 0x0400008E RID: 142
		private readonly List<StringTableManager.StringCollection> _ChangeValues;
	}
}
