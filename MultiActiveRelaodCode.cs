using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;

namespace BunnyMod
{
	// Token: 0x02000042 RID: 66
	public class MultiActiveReload
	{
		// Token: 0x040000C0 RID: 192
		public dfSprite sprite;

		// Token: 0x040000C1 RID: 193
		public dfSprite celebrationSprite;

		// Token: 0x040000C2 RID: 194
		public int startValue;

		// Token: 0x040000C3 RID: 195
		public int endValue;

		// Token: 0x040000C4 RID: 196
		public bool stopsReload;

		// Token: 0x040000C5 RID: 197
		public bool canAttemptActiveReloadAfterwards;

		// Token: 0x040000C6 RID: 198
		public ActiveReloadData reloadData;

		// Token: 0x040000C7 RID: 199
		public bool usesActiveReloadData;

		// Token: 0x040000C8 RID: 200
		public string Name;
	}
}

namespace BunnyMod
{
	// Token: 0x02000044 RID: 68
	internal class MultiActiveReloadController : AdvancedGunBehaviour
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		public virtual void OnActiveReloadSuccess(MultiActiveReload reload)
		{
			bool flag = reload == null || reload.stopsReload;
			if (flag)
			{
				MultiActiveReloadController.info.Invoke(this.gun, new object[]
				{
					true,
					false,
					false
				});
			}
			float num = 1f;
			bool flag2 = Gun.ActiveReloadActivated && base.PickedUpByPlayer && base.Player.IsPrimaryPlayer;
			if (flag2)
			{
				num *= CogOfBattleItem.ACTIVE_RELOAD_DAMAGE_MULTIPLIER;
			}
			bool flag3 = Gun.ActiveReloadActivatedPlayerTwo && base.PickedUpByPlayer && !base.Player.IsPrimaryPlayer;
			if (flag3)
			{
				num *= CogOfBattleItem.ACTIVE_RELOAD_DAMAGE_MULTIPLIER;
			}
			bool flag4 = reload == null || reload.usesActiveReloadData;
			if (flag4)
			{
				bool flag5 = this.gun.LocalActiveReload && (reload == null || reload.reloadData == null);
				if (flag5)
				{
					num *= Mathf.Pow(this.gun.activeReloadData.damageMultiply, (float)((int)MultiActiveReloadController.info2.GetValue(this.gun) + 1));
				}
				else
				{
					bool flag6 = reload != null && reload.reloadData != null;
					if (flag6)
					{
						num *= Mathf.Pow(reload.reloadData.damageMultiply, reload.reloadData.ActiveReloadStacks ? ((float)((int)MultiActiveReloadController.info2.GetValue(this.gun) + 1)) : 1f);
					}
				}
			}
			this.damageMult = num;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00011C22 File Offset: 0x0000FE22
		public override void PostProcessProjectile(Projectile projectile)
		{
			base.PostProcessProjectile(projectile);
			projectile.baseData.damage *= this.damageMult;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00011C45 File Offset: 0x0000FE45
		public virtual void OnActiveReloadFailure(MultiActiveReload reload)
		{
			this.damageMult = 1f;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00011C53 File Offset: 0x0000FE53
		public override void MidGameDeserialize(List<object> data, ref int dataIndex)
		{
			base.MidGameDeserialize(data, ref dataIndex);
			this.reloads = (List<MultiActiveReloadData>)data[dataIndex];
			this.activeReloadEnabled = (bool)data[dataIndex + 1];
			dataIndex += 2;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00011C8D File Offset: 0x0000FE8D
		public override void MidGameSerialize(List<object> data, int dataIndex)
		{
			base.MidGameSerialize(data, dataIndex);
			data.Add(this.reloads);
			data.Add(this.activeReloadEnabled);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00011CB8 File Offset: 0x0000FEB8
		public override void InheritData(Gun source)
		{
			base.InheritData(source);
			MultiActiveReloadController component = source.GetComponent<MultiActiveReloadController>();
			bool flag = component;
			if (flag)
			{
				this.reloads = component.reloads;
				this.activeReloadEnabled = component.activeReloadEnabled;
			}
		}

		// Token: 0x040000D3 RID: 211
		public static MethodInfo info = typeof(Gun).GetMethod("FinishReload", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040000D4 RID: 212
		public static FieldInfo info2 = typeof(Gun).GetField("SequentialActiveReloads", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040000D5 RID: 213
		public List<MultiActiveReloadData> reloads = new List<MultiActiveReloadData>();

		// Token: 0x040000D6 RID: 214
		public bool canAttemptActiveReload;

		// Token: 0x040000D7 RID: 215
		public bool activeReloadEnabled;

		// Token: 0x040000D8 RID: 216
		public float damageMult = 1f;
	}
}

namespace BunnyMod
{
	// Token: 0x02000043 RID: 67
	public struct MultiActiveReloadData
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x00011A48 File Offset: 0x0000FC48
		public MultiActiveReloadData(float activeReloadStartPercentage, int startValue, int endValue, int pixelWidth, int activeReloadLastTime, bool stopsReload, bool canAttemptActiveReloadAfterwards, ActiveReloadData reloadData, bool usesActiveReloadData, string Name)
		{
			this.activeReloadStartPercentage = activeReloadStartPercentage;
			this.startValue = startValue;
			this.endValue = endValue;
			this.pixelWidth = pixelWidth;
			this.activeReloadLastTime = activeReloadLastTime;
			this.stopsReload = stopsReload;
			this.canAttemptActiveReloadAfterwards = canAttemptActiveReloadAfterwards;
			this.reloadData = reloadData;
			this.usesActiveReloadData = usesActiveReloadData;
			this.Name = Name;
		}

		// Token: 0x040000C9 RID: 201
		public float activeReloadStartPercentage;

		// Token: 0x040000CA RID: 202
		public int startValue;

		// Token: 0x040000CB RID: 203
		public int endValue;

		// Token: 0x040000CC RID: 204
		public int pixelWidth;

		// Token: 0x040000CD RID: 205
		public int activeReloadLastTime;

		// Token: 0x040000CE RID: 206
		public bool stopsReload;

		// Token: 0x040000CF RID: 207
		public bool canAttemptActiveReloadAfterwards;

		// Token: 0x040000D0 RID: 208
		public ActiveReloadData reloadData;

		// Token: 0x040000D1 RID: 209
		public bool usesActiveReloadData;

		// Token: 0x040000D2 RID: 210
		public string Name;
	}
}


namespace BunnyMod
{
	// Token: 0x02000041 RID: 65
	public static class MultiActiveReloadManager
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		public static void SetupHooks()
		{
			Hook hook = new Hook(typeof(GameUIReloadBarController).GetMethod("TriggerReload", BindingFlags.Instance | BindingFlags.Public), typeof(MultiActiveReloadManager).GetMethod("TriggerReloadHook"));
			Hook hook2 = new Hook(typeof(Gun).GetMethod("OnActiveReloadPressed", BindingFlags.Instance | BindingFlags.NonPublic), typeof(MultiActiveReloadManager).GetMethod("OnActiveReloadPressedHook"));
			Hook hook3 = new Hook(typeof(Gun).GetMethod("Reload", BindingFlags.Instance | BindingFlags.Public), typeof(MultiActiveReloadManager).GetMethod("ReloadHook"));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00011064 File Offset: 0x0000F264
		public static bool ReloadHook(Func<Gun, bool> orig, Gun self)
		{
			bool flag = orig(self);
			bool flag2 = flag && self.GetComponent<MultiActiveReloadController>() != null;
			if (flag2)
			{
				self.GetComponent<MultiActiveReloadController>().canAttemptActiveReload = true;
				self.GetComponent<MultiActiveReloadController>().damageMult = 1f;
			}
			return flag;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000110B4 File Offset: 0x0000F2B4
		public static void TriggerReloadHook(MultiActiveReloadManager.Action<GameUIReloadBarController, PlayerController, Vector3, float, float, int> orig, GameUIReloadBarController self, PlayerController attachParent, Vector3 offset, float duration, float activeReloadStartPercent, int pixelWidth)
		{
			bool flag = MultiActiveReloadManager.tempraryActiveReloads.ContainsKey(self);
			if (flag)
			{
				foreach (MultiActiveReload multiActiveReload in MultiActiveReloadManager.tempraryActiveReloads[self])
				{
					bool flag2 = multiActiveReload.sprite != null && multiActiveReload.sprite.gameObject != null;
					if (flag2)
					{
						UnityEngine.Object.Destroy(multiActiveReload.sprite.gameObject);
					}
					bool flag3 = multiActiveReload.celebrationSprite != null && multiActiveReload.celebrationSprite.gameObject != null;
					if (flag3)
					{
						UnityEngine.Object.Destroy(multiActiveReload.celebrationSprite.gameObject);
					}
				}
				MultiActiveReloadManager.tempraryActiveReloads[self].Clear();
			}
			orig(self, attachParent, offset, duration, activeReloadStartPercent, pixelWidth);
			bool flag4 = attachParent != null && attachParent.CurrentGun != null && attachParent.CurrentGun.GetComponent<MultiActiveReloadController>() != null;
			if (flag4)
			{
				foreach (MultiActiveReloadData multiActiveReloadData in attachParent.CurrentGun.GetComponent<MultiActiveReloadController>().reloads)
				{
					dfSprite dfSprite = UnityEngine.Object.Instantiate<dfSprite>(self.activeReloadSprite);
					self.activeReloadSprite.Parent.AddControl(dfSprite);
					dfSprite.enabled = true;
					float width = self.progressSlider.Width;
					float maxValue = self.progressSlider.MaxValue;
					float num = (float)multiActiveReloadData.startValue / maxValue * width;
					float num2 = (float)multiActiveReloadData.endValue / maxValue * width;
					float x = num + (num2 - num) * multiActiveReloadData.activeReloadStartPercentage;
					float width2 = (float)pixelWidth * Pixelator.Instance.CurrentTileScale;
					dfSprite.RelativePosition = self.activeReloadSprite.RelativePosition;
					dfSprite.RelativePosition = GameUIUtility.QuantizeUIPosition(dfSprite.RelativePosition.WithX(x));
					dfSprite.Width = width2;
					dfSprite.IsVisible = true;
					dfSprite dfSprite2 = UnityEngine.Object.Instantiate<dfSprite>(self.celebrationSprite);
					self.activeReloadSprite.Parent.AddControl(dfSprite2);
					dfSprite2.enabled = true;
					dfSpriteAnimation component = dfSprite2.GetComponent<dfSpriteAnimation>();
					component.Stop();
					component.SetFrameExternal(0);
					dfSprite2.enabled = false;
					dfSprite2.RelativePosition = dfSprite.RelativePosition + new Vector3(Pixelator.Instance.CurrentTileScale * -1f, Pixelator.Instance.CurrentTileScale * -2f, 0f);
					int num3 = Mathf.RoundToInt((float)(multiActiveReloadData.endValue - multiActiveReloadData.startValue) * multiActiveReloadData.activeReloadStartPercentage) + multiActiveReloadData.startValue - multiActiveReloadData.activeReloadLastTime / 2;
					MultiActiveReload item = new MultiActiveReload
					{
						sprite = dfSprite,
						celebrationSprite = dfSprite2,
						startValue = num3,
						endValue = num3 + multiActiveReloadData.activeReloadLastTime,
						stopsReload = multiActiveReloadData.stopsReload,
						canAttemptActiveReloadAfterwards = multiActiveReloadData.canAttemptActiveReloadAfterwards,
						reloadData = multiActiveReloadData.reloadData,
						usesActiveReloadData = multiActiveReloadData.usesActiveReloadData,
						Name = multiActiveReloadData.Name
					};
					bool flag5 = MultiActiveReloadManager.tempraryActiveReloads.ContainsKey(self);
					if (flag5)
					{
						MultiActiveReloadManager.tempraryActiveReloads[self].Add(item);
					}
					else
					{
						MultiActiveReloadManager.tempraryActiveReloads.Add(self, new List<MultiActiveReload>
						{
							item
						});
					}
				}
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00011494 File Offset: 0x0000F694
		public static bool AttemptActiveReloadHook(Func<GameUIReloadBarController, bool> orig, GameUIReloadBarController self)
		{
			bool flag = !self.ReloadIsActive;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = MultiActiveReloadManager.tempraryActiveReloads.ContainsKey(self);
				if (flag2)
				{
					foreach (MultiActiveReload multiActiveReload in MultiActiveReloadManager.tempraryActiveReloads[self])
					{
						bool flag3 = self.progressSlider.Value >= (float)multiActiveReload.startValue && self.progressSlider.Value <= (float)multiActiveReload.endValue;
						if (flag3)
						{
							self.progressSlider.Color = Color.green;
							AkSoundEngine.PostEvent("Play_WPN_active_reload_01", self.gameObject);
							multiActiveReload.celebrationSprite.enabled = true;
							multiActiveReload.sprite.enabled = false;
							bool stopsReload = multiActiveReload.stopsReload;
							if (stopsReload)
							{
								self.progressSlider.Thumb.enabled = false;
								MultiActiveReloadManager.info.SetValue(self, false);
							}
							multiActiveReload.celebrationSprite.GetComponent<dfSpriteAnimation>().Play();
							return true;
						}
					}
				}
				bool flag4 = orig(self);
				result = flag4;
			}
			return result;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000115F0 File Offset: 0x0000F7F0
		public static bool AttemptActiveReloadOnlyMultireload(this GameUIRoot self, PlayerController targetPlayer)
		{
			int index = (!targetPlayer.IsPrimaryPlayer) ? 1 : 0;
			return ((List<GameUIReloadBarController>)MultiActiveReloadManager.info3.GetValue(self))[index].AttemptActiveReloadOnlyMultireload();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0001162C File Offset: 0x0000F82C
		public static bool AttemptActiveReloadOnlyMultireload(this GameUIReloadBarController self)
		{
			bool flag = !self.ReloadIsActive;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = MultiActiveReloadManager.tempraryActiveReloads.ContainsKey(self);
				if (flag2)
				{
					foreach (MultiActiveReload multiActiveReload in MultiActiveReloadManager.tempraryActiveReloads[self])
					{
						bool flag3 = self.progressSlider.Value >= (float)multiActiveReload.startValue && self.progressSlider.Value <= (float)multiActiveReload.endValue;
						if (flag3)
						{
							self.progressSlider.Color = Color.green;
							AkSoundEngine.PostEvent("Play_WPN_active_reload_01", self.gameObject);
							multiActiveReload.celebrationSprite.enabled = true;
							multiActiveReload.sprite.enabled = false;
							bool stopsReload = multiActiveReload.stopsReload;
							if (stopsReload)
							{
								self.progressSlider.Thumb.enabled = false;
								MultiActiveReloadManager.info.SetValue(self, false);
							}
							multiActiveReload.celebrationSprite.GetComponent<dfSpriteAnimation>().Play();
							return true;
						}
					}
				}
				self.progressSlider.Color = Color.red;
				result = false;
			}
			return result;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00011794 File Offset: 0x0000F994
		public static void OnActiveReloadPressedHook(Action<Gun, PlayerController, Gun, bool> orig, Gun self, PlayerController p, Gun g, bool actualPress)
		{
			orig(self, p, g, actualPress);
			bool flag = self.IsReloading || self.reloadTime < 0f;
			if (flag)
			{
				PlayerController exists = self.CurrentOwner as PlayerController;
				bool flag2 = exists && (actualPress || true);
				if (flag2)
				{
					MultiActiveReloadController component = self.GetComponent<MultiActiveReloadController>();
					bool flag3 = component != null && component.activeReloadEnabled && component.canAttemptActiveReload && !GameUIRoot.Instance.GetReloadBarForPlayer(self.CurrentOwner as PlayerController).IsActiveReloadGracePeriod();
					if (flag3)
					{
						bool flag4 = GameUIRoot.Instance.AttemptActiveReloadOnlyMultireload(self.CurrentOwner as PlayerController);
						MultiActiveReload multiActiveReloadForController = GameUIRoot.Instance.GetReloadBarForPlayer(self.CurrentOwner as PlayerController).GetMultiActiveReloadForController();
						bool flag5 = flag4;
						if (flag5)
						{
							component.OnActiveReloadSuccess(multiActiveReloadForController);
							GunFormeSynergyProcessor component2 = self.GetComponent<GunFormeSynergyProcessor>();
							bool flag6 = component2;
							if (flag6)
							{
								component2.JustActiveReloaded = true;
							}
							ChamberGunProcessor component3 = self.GetComponent<ChamberGunProcessor>();
							bool flag7 = component3;
							if (flag7)
							{
								component3.JustActiveReloaded = true;
							}
						}
						else
						{
							component.OnActiveReloadFailure(multiActiveReloadForController);
						}
						bool flag8 = multiActiveReloadForController == null || !multiActiveReloadForController.canAttemptActiveReloadAfterwards;
						if (flag8)
						{
							ETGModConsole.Log("yes", false);
							component.canAttemptActiveReload = false;
							Action<PlayerController, Gun, bool> value = (Action<PlayerController, Gun, bool>)MultiActiveReloadManager.info2.CreateDelegate<Action<PlayerController, Gun, bool>>();
							self.OnReloadPressed = (Action<PlayerController, Gun, bool>)Delegate.Remove(self.OnReloadPressed, value);
						}
					}
				}
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00011928 File Offset: 0x0000FB28
		public static MultiActiveReload GetMultiActiveReloadForController(this GameUIReloadBarController controller)
		{
			MultiActiveReload result = null;
			bool flag = MultiActiveReloadManager.tempraryActiveReloads.ContainsKey(controller);
			if (flag)
			{
				foreach (MultiActiveReload multiActiveReload in MultiActiveReloadManager.tempraryActiveReloads[controller])
				{
					bool flag2 = controller.progressSlider.Value >= (float)multiActiveReload.startValue && controller.progressSlider.Value <= (float)multiActiveReload.endValue;
					if (flag2)
					{
						result = multiActiveReload;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x040000BC RID: 188
		public static Dictionary<GameUIReloadBarController, List<MultiActiveReload>> tempraryActiveReloads = new Dictionary<GameUIReloadBarController, List<MultiActiveReload>>();

		// Token: 0x040000BD RID: 189
		public static FieldInfo info = typeof(GameUIReloadBarController).GetField("m_reloadIsActive", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040000BE RID: 190
		public static MethodInfo info2 = typeof(Gun).GetMethod("OnActiveReloadPressed", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040000BF RID: 191
		public static FieldInfo info3 = typeof(GameUIRoot).GetField("m_extantReloadBars", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x020000F1 RID: 241
		// (Invoke) Token: 0x0600054F RID: 1359
		public delegate void Action<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
	}
}
