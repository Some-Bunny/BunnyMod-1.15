using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using ItemAPI;
using Gungeon;
using FullInspector;
using Brave.BulletScript;
using System.Collections;

using System.Reflection;
using GungeonAPI;
using MonoMod.RuntimeDetour;
using Dungeonator;



/*
namespace BunnyMod
{
	public class TestOverrideBehavior : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "01972dee89fc4404a5c408d50007dad5"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.62f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(TestBulletScript)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class TestBulletScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(7.5f, SpeedType.Absolute), new TestBulletScript.Break());

				yield break;
			}
			public class Break : Bullet
			{
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (preventSpawningProjectiles)
					{
						return;
					}
					float num = base.RandomAngle();
					float num2 = 45f;
					for (int i = 0; i < 8; i++)
					{
						base.Fire(new Direction(num + num2 * (float)i, DirectionType.Absolute, -1f), new Speed(11f, SpeedType.Absolute), null);
						base.PostWwiseEvent("Play_WPN_smallrocket_impact_01", null);
					}
				}
			}
		}
	}
}
*/
/*
namespace BunnyMod
{
	public class TestOverrideBehavior : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "01972dee89fc4404a5c408d50007dad5"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.62f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(TestBulletScript)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class TestBulletScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(7.5f, SpeedType.Absolute), new TestBulletScript.Break());

				yield break;
			}
			public class Break : Bullet
			{
				protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
				{
					for (int i = 0; i < 300; i++)
                    {	
						float num = base.RandomAngle();
						float num2 = 45f;
						for (int i1 = 0; i1 < 8; i1++)
						{
							base.Fire(new Direction(num + num2 * (float)i1, DirectionType.Absolute, -1f), new Speed(9f, SpeedType.Absolute), null);
							base.PostWwiseEvent("Play_WPN_smallrocket_impact_01", null);
						}
						yield return base.Wait(20);
					}
					yield break;
				}
			}
		}
	}
}
*/
/*
namespace BunnyMod
{
	public class TestOverrideBehavior : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "01972dee89fc4404a5c408d50007dad5"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.62f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(TestBulletScript)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class TestBulletScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("ec6b674e0acd4553b47ee94493d66422").bulletBank.GetBullet("bigBullet"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
				}
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new HelixBullet(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new HelixBullet(true));
				//yield return base.Wait(10);
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet1(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet1(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet2(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet2(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet3(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet3(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet4(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet4(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet5(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet5(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet6(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new HelixChainBullet6(true));
				yield break;
			}
			public class HelixBullet : Bullet
			{
				public HelixBullet(bool reverse) : base("bigBullet", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet : Bullet
			{
				public HelixChainBullet(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(5);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet1 : Bullet
			{
				public HelixChainBullet1(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(10);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet2 : Bullet
			{
				public HelixChainBullet2(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(15);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet3 : Bullet
			{
				public HelixChainBullet3(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(20);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet4 : Bullet
			{
				public HelixChainBullet4(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(25);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet5 : Bullet
			{
				public HelixChainBullet5(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(30);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet6 : Bullet
			{
				public HelixChainBullet6(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(35);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}

			
			protected override IEnumerator Top()
			{
				this.ManualControl = true;
				Vector2 truePosition = this.Position;
				float startVal = 1;
				for (int i = 0; i < 360; i++)
				{
					float offsetMagnitude = Mathf.SmoothStep(-1f, 1f, Mathf.PingPong(startVal + (float)i / 75f * 3f, 1f));
					truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 60f);
					this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 180f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 180f, offsetMagnitude));
					yield return this.Wait(1);
				}
				this.Vanish(false);
				yield break;
			}
			

			private bool reverse;
			
		}
	}
}
*/
/*
namespace BunnyMod
{
	public class TestOverrideBehaviorA : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "128db2f0781141bcb505d8f00f9e4d47"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.62f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(TestBulletScript)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class TestBulletScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").bulletBank.GetBullet("dagger"));
				}
				base.Fire(new Direction(-30f, DirectionType.Aim, -1f), new Speed(7f, SpeedType.Absolute), new TestBulletScript.HardmodeBigBullet());
				base.Fire(new Direction(-30f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new TestBulletScript.Dagger());
				base.Fire(new Direction(-20f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), null);
				base.Fire(new Direction(-10f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), null);

				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(4f, SpeedType.Absolute), new TestBulletScript.HardmodeBigBullet());
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(5f, SpeedType.Absolute), new TestBulletScript.HardmodeBigBullet());
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(6f, SpeedType.Absolute), new TestBulletScript.HardmodeBigBullet());
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(7f, SpeedType.Absolute), new TestBulletScript.HardmodeBigBullet());
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new TestBulletScript.HardmodeBigBullet());
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new TestBulletScript.HardmodeBigBullet());
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(10f, SpeedType.Absolute), new TestBulletScript.HardmodeBigBullet());
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(11f, SpeedType.Absolute), new TestBulletScript.Dagger());


				base.Fire(new Direction(10f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), null);
				base.Fire(new Direction(20f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), null);
				base.Fire(new Direction(30f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new TestBulletScript.Dagger());
				base.Fire(new Direction(30f, DirectionType.Aim, -1f), new Speed(7f, SpeedType.Absolute), new TestBulletScript.HardmodeBigBullet());

				yield return base.Wait(30);

				yield break;
			}
			public class HardmodeBigBullet : Bullet
			{
				// Token: 0x06000007 RID: 7 RVA: 0x000021A2 File Offset: 0x000003A2
				public HardmodeBigBullet() : base("chain", false, false, false)
				{
				}
			}
			public class Dagger : Bullet
			{
				// Token: 0x06000007 RID: 7 RVA: 0x000021A2 File Offset: 0x000003A2
				public Dagger() : base("dagger", false, false, false)
				{
				}
			}
			//private SpeculativeRigidbody m_targetRigidbody;
		}
	}
}
*/
/*
namespace BunnyMod
{
	public class TestOverrideBehavior1 : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "88b6b6a93d4b4234a67844ef4728382c"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.62f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(TestBulletScript1)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class TestBulletScript1 : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top()
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").bulletBank.GetBullet("dagger"));
				}
				float aim = this.GetAimDirection(1f, 16f);
				this.Fire(new Direction(aim, DirectionType.Absolute, -1f), new Speed(6f, SpeedType.Absolute), new TestBulletScript1.FastHomingShot1());
				yield return this.Wait(20);
				this.Fire(new Direction(aim, DirectionType.Absolute, -1f), new Speed(9f, SpeedType.Absolute), new TestBulletScript1.FastHomingShot1());
				yield return this.Wait(20);
				this.Fire(new Direction(aim, DirectionType.Absolute, -1f), new Speed(12f, SpeedType.Absolute), new TestBulletScript1.FastHomingShot1());
				yield return this.Wait(20);
				this.Fire(new Direction(aim, DirectionType.Absolute, -1f), new Speed(15f, SpeedType.Absolute), new TestBulletScript1.FastHomingShot1());
				yield return this.Wait(20);
				yield break;
			}

			// Token: 0x020001E2 RID: 482
			public class FastHomingShot1 : Bullet
			{
				public FastHomingShot1() : base("dagger", false, false, false)
				{
				}
				protected override IEnumerator Top()
				{
					for (int i = 0; i < 60; i++)
					{
						float aim = this.GetAimDirection(1f, 16f);
						float delta = BraveMathCollege.ClampAngle180(aim - this.Direction);
						if (Mathf.Abs(delta) > 100f)
						{
							yield break;
						}
						this.Direction += Mathf.MoveTowards(0f, delta, 2f);
						yield return this.Wait(1);
					}
					yield break;
				}
			}
		}
	}
}
*/
/*
namespace BunnyMod
{
	public class TestOverrideBehavior2 : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "b54d89f9e802455cbb2b8a96a31e8259"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.62f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(TestBulletScript2)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class TestBulletScript2 : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top()
			{
				base.Fire(new Direction(30f, DirectionType.Aim, -1f), new Speed(5f, SpeedType.Absolute), null);
				base.Fire(new Direction(30f, DirectionType.Aim, -1f), new Speed(6f, SpeedType.Absolute), null);
				base.Fire(new Direction(30f, DirectionType.Aim, -1f), new Speed(7f, SpeedType.Absolute), null);
				base.Fire(new Direction(30f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), null);
				base.Fire(new Direction(30f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), null);
				base.Fire(new Direction(30f, DirectionType.Aim, -1f), new Speed(10f, SpeedType.Absolute), null);
				base.Fire(new Direction(30f, DirectionType.Aim, -1f), new Speed(11f, SpeedType.Absolute), null);
				base.Fire(new Direction(30f, DirectionType.Aim, -1f), new Speed(12f, SpeedType.Absolute), null);
				base.Fire(new Direction(-30f, DirectionType.Aim, -1f), new Speed(5f, SpeedType.Absolute), null);
				base.Fire(new Direction(-30f, DirectionType.Aim, -1f), new Speed(6f, SpeedType.Absolute), null);
				base.Fire(new Direction(-30f, DirectionType.Aim, -1f), new Speed(7f, SpeedType.Absolute), null);
				base.Fire(new Direction(-30f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), null);
				base.Fire(new Direction(-30f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), null);
				base.Fire(new Direction(-30f, DirectionType.Aim, -1f), new Speed(10f, SpeedType.Absolute), null);
				base.Fire(new Direction(-30f, DirectionType.Aim, -1f), new Speed(11f, SpeedType.Absolute), null);
				base.Fire(new Direction(-30f, DirectionType.Aim, -1f), new Speed(12f, SpeedType.Absolute), null);
				yield return this.Wait(20);
				float aim = this.GetAimDirection(1f, 16f);
				this.Fire(new Direction(aim, DirectionType.Absolute, -1f), new Speed(8f, SpeedType.Absolute), new TestBulletScript2.FastHomingShot1());
				this.Fire(new Direction(aim, DirectionType.Absolute, -1f), new Speed(12f, SpeedType.Absolute), new TestBulletScript2.FastHomingShot1());
				yield return this.Wait(10);

				yield break;
			}

			// Token: 0x020001E2 RID: 482
			public class FastHomingShot1 : Bullet
			{
				protected override IEnumerator Top()
				{
					for (int i = 0; i < 180; i++)
					{
						float aim = this.GetAimDirection(1f, 16f);
						float delta = BraveMathCollege.ClampAngle180(aim - this.Direction);
						if (Mathf.Abs(delta) > 100f)
						{
							yield break;
						}
						this.Direction += Mathf.MoveTowards(0f, delta, 3f);
						yield return this.Wait(1);
					}
					yield break;
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class TestOverrideBehaviora : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "db35531e66ce41cbb81d507a34366dfe"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.62f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(TestBulletScript2)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class TestBulletScript2 : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top()
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").bulletBank.GetBullet("dagger"));
				}

				yield return this.Wait(20);
				this.Fire(new Direction(60f, DirectionType.Aim, -1f), new Speed(1f, SpeedType.Absolute), new TestBulletScript2.Break());
				this.Fire(new Direction(180f, DirectionType.Aim, -1f), new Speed(1f, SpeedType.Absolute), new TestBulletScript2.Break());
				this.Fire(new Direction(300f, DirectionType.Aim, -1f), new Speed(1f, SpeedType.Absolute), new TestBulletScript2.Break());
				yield return this.Wait(300);

				yield break;
			}

			// Token: 0x020001E2 RID: 482
			public class Break : Bullet
			{
				public Break() : base("dagger", false, false, false)
				{
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					for (int i = 0; i < 190; i++)
					{
						base.ChangeSpeed(new Speed(Speed * 1.05f, SpeedType.Absolute), 0);
						yield return base.Wait(2);
					}
					yield return base.Wait(30);
				}
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (preventSpawningProjectiles)
					{
						return;
					}
					for (int i = 0; i < 20; i++)
					{
						this.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed((1 * i) + 5f, SpeedType.Absolute), new TestBulletScript2.Dagger());

						//base.PostWwiseEvent("Play_WPN_smallrocket_impact_01", null);
					}
				}
			}

			public class Dagger : Bullet
			{
				// Token: 0x06000007 RID: 7 RVA: 0x000021A2 File Offset: 0x000003A2
				public Dagger() : base("chain", false, false, false)
				{
				}
			}

		}

	}
}

namespace BunnyMod
{
	public class TestOverrideBehavior2 : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "b54d89f9e802455cbb2b8a96a31e8259"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.62f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(TestBulletScript2)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class TestBulletScript2 : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top()
			{
				for (int i = 0; i < 30; i++)
				{
					if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
					{
						//base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
						base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6c43fddfd401456c916089fdd1c99b1c").bulletBank.GetBullet("quickHoming"));
					}
					base.Fire(new Direction((12 * i), DirectionType.Relative, -1f), new Speed(6f, SpeedType.Absolute), new TestBulletScript2.FastHomingShot1());
					//base.Fire(new Direction((12 * i), DirectionType.Absolute, -1f), new Speed(6f, SpeedType.Absolute), new Bullet("homing", FastHomingShot1());

				}
				//yield return this.Wait(300);
				yield break;
			}

			// Token: 0x020001E2 RID: 482
			public class FastHomingShot1 : Bullet
			{
				public FastHomingShot1() : base("quickHoming", false, false, false)
				{
					//this.m_goalPos = goalPos;
				}
				protected override IEnumerator Top()
				{
					HealthHaver owner = base.BulletBank.healthHaver;
					for (int i = 0; i < 100; i++)
					{
						base.ChangeSpeed(new Speed(Speed * 0.94f, SpeedType.Absolute), 0);
						yield return base.Wait(2);
						if (!owner || owner.IsDead)
						{
							base.Vanish(false);
							yield break;
						}
					}
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					for (int i = 0; i < 1200; i++)
					{
						yield return base.Wait(3);
						if (!owner || owner.IsDead)
						{
							base.Vanish(false);
							yield break;
						}
					}
					base.Vanish(false);
					yield break;
				}
			}
		}
	}
}
*/
//Curse 2.0

namespace BunnyMod
{
	public class JammedBulletkin : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "01972dee89fc4404a5c408d50007dad5"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.80f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedBulletkinAttack)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class JammedBulletkinAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					//base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("37340393f97f41b2822bc02d14654172").bulletBank.GetBullet("quickHoming"));
				}
				PlayerController player = (GameManager.Instance.PrimaryPlayer);
				float num = 0f;
				num = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
				base.PostWwiseEvent("Play_WPN_magnum_shot_01", null);
				base.Fire(new Direction(-16f, DirectionType.Relative, -1f), new Speed(8f, SpeedType.Absolute), null);
				base.Fire(new Direction(-8f, DirectionType.Relative, -1f), new Speed(9f, SpeedType.Absolute), null);
				base.Fire(new Direction(0f, DirectionType.Relative, -1f), new Speed(10f, SpeedType.Absolute), new ShotgunCreecherUglyCircle1.CreecherBullet());
				base.Fire(new Direction(8f, DirectionType.Relative, -1f), new Speed(9f, SpeedType.Absolute), null);
				base.Fire(new Direction(16f, DirectionType.Relative, -1f), new Speed(8f, SpeedType.Absolute), null);
				yield return this.Wait(30);

				yield break;
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedRedShotgun : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "128db2f0781141bcb505d8f00f9e4d47"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.80f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedRedShotgunAttack)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class JammedRedShotgunAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
					//base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").bulletBank.GetBullet("dagger"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("ec6b674e0acd4553b47ee94493d66422").bulletBank.GetBullet("bigBullet"));
				}
				base.PostWwiseEvent("Play_WPN_shotgun_shot_01", null);
				base.Fire(new Direction(20f, DirectionType.Aim, -1f), new Speed(7.5f, SpeedType.Absolute), new JammedRedShotgunAttack.Chain());
				base.Fire(new Direction(12f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new JammedRedShotgunAttack.Chain());
				base.Fire(new Direction(4f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new JammedRedShotgunAttack.Chain());
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new JammedRedShotgunAttack.SplitBall());
				base.Fire(new Direction(-4f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new JammedRedShotgunAttack.Chain());
				base.Fire(new Direction(-12f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new JammedRedShotgunAttack.Chain());
				base.Fire(new Direction(-20f, DirectionType.Aim, -1f), new Speed(7.5f, SpeedType.Absolute), new JammedRedShotgunAttack.Chain());
				yield return base.Wait(120);

				yield break;
			}
			public class Chain : Bullet
			{
				// Token: 0x06000007 RID: 7 RVA: 0x000021A2 File Offset: 0x000003A2
				public Chain() : base("chain", false, false, false)
				{
				}
			}
			public class SplitBall : Bullet
			{
				// Token: 0x06000007 RID: 7 RVA: 0x000021A2 File Offset: 0x000003A2
				public SplitBall() : base("bigBullet", false, false, false)
				{
				}
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (!preventSpawningProjectiles)
					{

						float num = base.RandomAngle();
						float Amount = 20;
						float Angle = 360 / Amount;
						for (int i = 0; i < Amount; i++)
						{
							base.Fire(new Direction(num + Angle * (float)i, DirectionType.Absolute, -1f), new Speed(7.5f, SpeedType.Absolute), null);
						}
					}
				}
			}
			//private SpeculativeRigidbody m_targetRigidbody;
		}
	}
}

namespace BunnyMod
{
	public class JammedBlueShotgun : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "b54d89f9e802455cbb2b8a96a31e8259"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.80f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedBlueShotgunAttack)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class JammedBlueShotgunAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
					//base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").bulletBank.GetBullet("dagger"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("ec6b674e0acd4553b47ee94493d66422").bulletBank.GetBullet("bigBullet"));
				}
				base.PostWwiseEvent("Play_WPN_shotgun_shot_01", null);
				base.Fire(new Direction(32f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new JammedBlueShotgunAttack.SplitBall());
				base.Fire(new Direction(24f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(16f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(8f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(-8f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(-16f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(-24f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(-32f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new JammedBlueShotgunAttack.SplitBall());
				yield return base.Wait(30);
				base.PostWwiseEvent("Play_WPN_shotgun_shot_01", null);
				base.Fire(new Direction(24f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(16f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(8f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(8f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(-8f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(-16f, DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				base.Fire(new Direction(-24f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new JammedBlueShotgunAttack.Chain());
				yield break;
			}
			public class Chain : Bullet
			{
				// Token: 0x06000007 RID: 7 RVA: 0x000021A2 File Offset: 0x000003A2
				public Chain() : base("chain", false, false, false)
				{
				}
			}
			public class SplitBall : Bullet
			{
				// Token: 0x06000007 RID: 7 RVA: 0x000021A2 File Offset: 0x000003A2
				public SplitBall() : base("bigBullet", false, false, false)
				{
				}
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (!preventSpawningProjectiles)
					{

						float num = base.RandomAngle();
						float num2 = 30f;
						for (int i = 0; i < 12; i++)
						{
							base.Fire(new Direction(num + num2 * (float)i, DirectionType.Absolute, -1f), new Speed(8.5f, SpeedType.Absolute), null);
						}
					}
				}
			}
			//private SpeculativeRigidbody m_targetRigidbody;
		}
	}
}

namespace BunnyMod
{
	public class JammedBeadie: OverrideBehavior
	{
		public override string OverrideAIActorGUID => "7b0b1b6d9ce7405b86b75ce648025dd6"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.80f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(BlizzbulonBasicAttack2a)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class BlizzbulonBasicAttack2a : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				/*
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("022d7c822bc146b58fe3b0287568aaa2").bulletBank.GetBullet("icicle"));
				}
				*/
				for (int i = 0; i < 3; i++)
				{
					base.PostWwiseEvent("Play_WPN_colt1851_shot_01", null);
					base.Fire(new Direction(0 +(i*8), DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), null);
					yield return base.Wait(6);
					base.PostWwiseEvent("Play_WPN_colt1851_shot_01", null);
					base.Fire(new Direction(0+(i*5), DirectionType.Aim, -1f), new Speed(8.5f, SpeedType.Absolute), null);
				}

				yield break;
			}
			public class Pointy : Bullet
			{
				// Token: 0x06000007 RID: 7 RVA: 0x000021A2 File Offset: 0x000003A2
				public Pointy() : base("icicle", false, false, false)
				{
				}
			}
			//private SpeculativeRigidbody m_targetRigidbody;
		}
	}
}

namespace BunnyMod
{
	public class JammedFallenKin : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "5f3abc2d561b4b9c9e72b879c6f10c7e"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.80f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedFallenKinAttack)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class JammedFallenKinAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("ec6b674e0acd4553b47ee94493d66422").bulletBank.GetBullet("bigBullet"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
				}
				base.Fire(new Direction(-20f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new HelixChainBullet(false));
				base.Fire(new Direction(-20f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new HelixChainBullet(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new HelixChainBullet(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new HelixChainBullet(true));
				base.Fire(new Direction(20f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new HelixChainBullet(false));
				base.Fire(new Direction(20f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new HelixChainBullet(true));
				PlayerController player = (GameManager.Instance.PrimaryPlayer);
				float curse = 0f;
				curse = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
				for (int i = 0; i < 3; i++)
                {
					base.Fire(new Direction(-20f-curse, DirectionType.Aim, -1f), new Speed(3+(i*3f), SpeedType.Absolute), null);
					base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(4+(i*2f), SpeedType.Absolute), null);
					base.Fire(new Direction(20f+curse, DirectionType.Aim, -1f), new Speed(3+(i*3f), SpeedType.Absolute), null);

				}

				yield break;
			}
			public class HelixChainBullet : Bullet
			{
				public HelixChainBullet(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(9f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			//private SpeculativeRigidbody m_targetRigidbody;
		}
	}
}

namespace BunnyMod
{
	public class JammedSkullet : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "336190e29e8a4f75ab7486595b700d4a"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.80f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedSkulletAttack)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class JammedSkulletAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				int numBullets = 4;
				float sign = BraveUtility.RandomSign();
				bool skipInFirstWave = true;
				bool skipInSecondWave = true;
				int skip = UnityEngine.Random.Range(0, numBullets - 1);
				PlayerController player = (GameManager.Instance.PrimaryPlayer);
				float curse = 0f;
				curse = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
				for (int i = 0; i < numBullets - 1; i++)
				{
					if (i != skip || !skipInFirstWave)
					{
						base.Fire(new Direction(base.SubdivideArc(-sign * 10f, sign * 25f, numBullets, i, true), DirectionType.Aim, -1f), new Speed(8f+(i+1)+(curse/4), SpeedType.Absolute), null);
						base.Fire(new Direction(base.SubdivideArc(-sign * 25f, sign * 50f, numBullets, i, true), DirectionType.Aim, -1f), new Speed(7f+(i+(i/2)), SpeedType.Absolute), null);
					}
					yield return base.Wait(2);
				}
				yield return base.Wait(10);
				skip = UnityEngine.Random.Range(0, numBullets);
				for (int j = 0; j < numBullets; j++)
				{
					if (j != skip || !skipInSecondWave)
					{
						base.Fire(new Direction(base.SubdivideArc(sign * 40f, -sign * 60f, numBullets, j, false), DirectionType.Aim, -1f), new Speed(6f+ (j + 1), SpeedType.Absolute), null);
						base.Fire(new Direction(base.SubdivideArc(sign * 25f, -sign * 50f, numBullets, j, false), DirectionType.Aim, -1f), new Speed(7f+ (j + (j / 2) + (curse / 3)), SpeedType.Absolute), null);
					}
					yield return base.Wait(4);
				}
				yield break;
			}			
		}
	}
}

namespace BunnyMod
{
	public class JammedSkullmet : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "95ec774b5a75467a9ab05fa230c0c143"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.80f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedSkulletmAttack)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class JammedSkulletmAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				PlayerController player = (GameManager.Instance.PrimaryPlayer);
				float curse = 0f;
				curse = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
				int numBullets = 6;
				float sign = BraveUtility.RandomSign();
				bool skipInFirstWave = true;
				bool skipInSecondWave = true;
				int skip = UnityEngine.Random.Range(0, numBullets - 3);
				for (int i = 0; i < numBullets - 1; i++)
				{
					if (i != skip || !skipInFirstWave)
					{
						base.Fire(new Direction(base.SubdivideArc(-sign * 15f, sign * 10f, numBullets, i, true), DirectionType.Aim, -1f), new Speed(8.5f + (i + 1), SpeedType.Absolute), null);
						base.Fire(new Direction(base.SubdivideArc(-sign * 15f, sign * 35f, numBullets, i, true), DirectionType.Aim, -1f), new Speed(6.5f + (i + (i / 2))+(curse/5), SpeedType.Absolute), null);
					}
					yield return base.Wait(2);
				}
				yield return base.Wait(10);
				skip = UnityEngine.Random.Range(0, numBullets);
				for (int j = 0; j < numBullets; j++)
				{
					if (j != skip || !skipInSecondWave)
					{
						base.Fire(new Direction(base.SubdivideArc(sign * 45f, -sign * 65f, numBullets, j, false), DirectionType.Aim, -1f), new Speed(5f + (j * 1.25f) + (curse / 4), SpeedType.Absolute), null);
						base.Fire(new Direction(base.SubdivideArc(sign * 20f, -sign * 35f, numBullets, j, false), DirectionType.Aim, -1f), new Speed(8f + (j + (j / 1.5f)), SpeedType.Absolute), null);
					}
					yield return base.Wait(4);
				}
				yield break;
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedVeteranShotgn : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "2752019b770f473193b08b4005dc781f"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.80f;
			shootGunBehavior.LeadAmount = 1f;
			shootGunBehavior.RespectReload = true;
			shootGunBehavior.MagazineCapacity = 1;
			shootGunBehavior.ReloadSpeed = 7;
			shootGunBehavior.EmptiesClip = true;
			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedVeteranShotgnAttack)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class JammedVeteranShotgnAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				for (int i = 0; i < 3; i++)
                {
					base.PostWwiseEvent("Play_WPN_shotgun_shot_01", null);
					float aimDirection = base.GetAimDirection(1f, 9f);
					for (int a = -3; a <= 3; a++)
					{
						base.Fire(new Direction(aimDirection + (float)(a * (4.5f)), DirectionType.Absolute, -1f), new Speed(10f - (float)Mathf.Abs(a) * 0.5f, SpeedType.Absolute), null);
					}
					yield return base.Wait(20);

				}
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("880bbe4ce1014740ba6b4e2ea521e49d").bulletBank.GetBullet("grenade"));
				}
				float airTime = base.BulletBank.GetBullet("grenade").BulletObject.GetComponent<ArcProjectile>().GetTimeInFlight();
				Vector2 vector = this.BulletManager.PlayerPosition();
				Bullet bullet2 = new Bullet("grenade", false, false, false);
				float direction2 = (vector - base.Position).ToAngle();
				base.Fire(new Direction(direction2, DirectionType.Absolute, -1f), new Speed(1f, SpeedType.Absolute), bullet2);
				(bullet2.Projectile as ArcProjectile).AdjustSpeedToHit(vector);
				bullet2.Projectile.ImmuneToSustainedBlanks = true;
				yield return base.Wait(150);
				/*
				for (int i = 0; i < 4; i++)
				{
					yield return base.Wait(6);
					Vector2 targetVelocity = this.BulletManager.PlayerVelocity();
					float startAngle;
					float dist;
					if (targetVelocity != Vector2.zero && targetVelocity.magnitude > 0.5f)
					{
						startAngle = targetVelocity.ToAngle();
						dist = targetVelocity.magnitude * airTime;
					}
					else
					{
						startAngle = base.RandomAngle();
						dist = 5f * airTime;
					}
					float angle = base.SubdivideCircle(startAngle, 4, i, 1f, false);
					Vector2 targetPoint = this.BulletManager.PlayerPosition() + BraveMathCollege.DegreesToVector(angle, dist);
					float direction = (targetPoint - base.Position).ToAngle();
					if (i > 0)
					{
						direction += UnityEngine.Random.Range(-12.5f, 12.5f);
					}
					Bullet bullet = new Bullet("grenade", false, false, false);
					base.Fire(new Direction(direction, DirectionType.Absolute, -1f), new Speed(1f, SpeedType.Absolute), bullet);
					(bullet.Projectile as ArcProjectile).AdjustSpeedToHit(targetPoint);
					bullet.Projectile.ImmuneToSustainedBlanks = true;
				}
				*/
				yield break;
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedVeteranBullet : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "70216cae6c1346309d86d4a0b4603045"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.70f;
			shootGunBehavior.RespectReload = true;
			shootGunBehavior.MagazineCapacity = 3;
			shootGunBehavior.ReloadSpeed = 6;
			shootGunBehavior.Cooldown = 3;
			shootGunBehavior.EmptiesClip = true;
			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedVeteranbulleAttack)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class JammedVeteranbulleAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				for (int a = 0; a < 3; a++)
				{
					PlayerController player = (GameManager.Instance.PrimaryPlayer);
					float curse = 0f;
					curse = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
					int accuracy = UnityEngine.Random.Range(-20, 20);
					for (int i = -1; i < 2; i++)
					{
						base.PostWwiseEvent("Play_WPN_magnum_shot_01", null);
						base.Fire(new Direction(0 + (float)((i+(i/2)) * (9))+ accuracy, DirectionType.Aim, -1f), new Speed(10f - (float)Mathf.Abs(a) * 0.3f, SpeedType.Absolute), null);
					}
					yield return base.Wait(16);
					base.Fire(new Direction(12f, DirectionType.Aim, -1f), new Speed(9f + (float)Mathf.Abs(a) * 1.2f, SpeedType.Absolute), null);
					base.Fire(new Direction(-12f, DirectionType.Aim, -1f), new Speed(9f + (float)Mathf.Abs(a) * 1.2f, SpeedType.Absolute), null);
				}
				yield break;
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedSinper: OverrideBehavior
	{
		public override string OverrideAIActorGUID => "31a3ea0c54a745e182e22ea54844a82d"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

		ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
		     shootGunBehavior.LeadAmount = 0.70f;
			shootGunBehavior.RespectReload = true;
			shootGunBehavior.MagazineCapacity = 2;
			shootGunBehavior.ReloadSpeed = 4;
			shootGunBehavior.Cooldown = 1;
			shootGunBehavior.EmptiesClip = true;
			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedSinperAttack)); // Sets the bullet kin's bullet script to our custom bullet script.
																								//		ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedSinperAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("31a3ea0c54a745e182e22ea54844a82d").bulletBank.GetBullet("sniper"));
				}
				base.PostWwiseEvent("Play_WPN_sniperrifle_shot_01", null);
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(20f, SpeedType.Absolute), new JammedSinperAttack.HelixChainBullet());
				yield break;
			}
			public class HelixChainBullet : Bullet
			{
				public HelixChainBullet() : base("sniper", false, false, false)
				{
					//base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					for (int a = 0; a < 1000; a++)
                    {
						base.Fire(new Direction(-90f, DirectionType.Relative, -1f), new Speed(0f, SpeedType.Relative), new JammedSinperAttack.SpeedUp());
						base.Fire(new Direction(90f, DirectionType.Relative, -1f), new Speed(0f, SpeedType.Relative), new JammedSinperAttack.SpeedUp());
						yield return base.Wait(2);
					}
					yield break;
				}
			}
			public class SpeedUp : Bullet
			{
				public SpeedUp() : base("sniper", false, false, false)
				{
					//base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					PlayerController player = (GameManager.Instance.PrimaryPlayer);
					float curse = 0f;
					curse = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(60);
					base.ChangeSpeed(new Speed(.1f, SpeedType.Absolute), 0);
					for (int a = 0; a < 100; a++)
					{
						base.ChangeSpeed(new Speed(.075f * (a+(curse/20)), SpeedType.Absolute), 0);
						yield return base.Wait(2);

					}
					yield break;
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedSinperElite : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "c5b11bfc065d417b9c4d03a5e385fe2c"; 
																						  
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; 
			shootGunBehavior.LeadAmount = 0.70f;
			shootGunBehavior.RespectReload = true;
			shootGunBehavior.MagazineCapacity = 1;
			shootGunBehavior.ReloadSpeed = 5;
			shootGunBehavior.EmptiesClip = true;
			shootGunBehavior.WeaponType = WeaponType.BulletScript; 
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedEliteSinperAttack)); 
																									

		}
		public class JammedEliteSinperAttack : Script 
		{
			protected override IEnumerator Top() 
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("31a3ea0c54a745e182e22ea54844a82d").bulletBank.GetBullet("sniper"));
				}
				base.PostWwiseEvent("Play_WPN_sniperrifle_shot_01", null);
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(20f, SpeedType.Absolute), new JammedEliteSinperAttack.ElitePain());
				yield break;
			}

			public class ElitePain : Bullet
			{
				public ElitePain() : base("sniper", false, false, false)
				{
					//base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					for (int a = 0; a < 1000; a++)
					{
						base.Fire(new Direction(-90f, DirectionType.Relative, -1f), new Speed(0f, SpeedType.Relative), new JammedEliteSinperAttack.SpeedUp());
						base.Fire(new Direction(90f, DirectionType.Relative, -1f), new Speed(0f, SpeedType.Relative), new JammedEliteSinperAttack.SpeedUp());
						yield return base.Wait(2);
					}
					yield break;
				}
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (!preventSpawningProjectiles)
					{

						base.PostWwiseEvent("Play_WPN_shotgun_shot_01", null);
						base.Fire(new Direction(210f, DirectionType.Relative, -1f), new Speed(20f, SpeedType.Absolute), new JammedEliteSinperAttack.ElitePainSplits()); ;
						base.Fire(new Direction(150f, DirectionType.Relative, -1f), new Speed(20f, SpeedType.Absolute), new JammedEliteSinperAttack.ElitePainSplits()); ;
					}
				}
			}
			public class ElitePainSplits : Bullet
			{
				public ElitePainSplits() : base("sniper", false, false, false)
				{
					//base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					for (int a = 0; a < 1000; a++)
					{
						base.Fire(new Direction(-90-(a*9), DirectionType.Relative, -1f), new Speed(0f, SpeedType.Relative), new JammedEliteSinperAttack.SpeedUp());
						base.Fire(new Direction(90+(a*9), DirectionType.Relative, -1f), new Speed(0f, SpeedType.Relative), new JammedEliteSinperAttack.SpeedUp());
						yield return base.Wait(1);
					}
					yield break;
				}
			}
			public class SpeedUp : Bullet
			{
				public SpeedUp() : base("sniper", false, false, false)
				{
					//base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					PlayerController player = (GameManager.Instance.PrimaryPlayer);
					float curse = 0f;
					curse = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(60);
					base.ChangeSpeed(new Speed(.1f, SpeedType.Absolute), 0);
					for (int a = 0; a < 100; a++)
					{
						base.ChangeSpeed(new Speed(.1f * (a+(curse/25)), SpeedType.Absolute), 0);
						yield return base.Wait(2);

					}
					yield break;
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedBandanaKin : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "88b6b6a93d4b4234a67844ef4728382c"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 1f;
			shootGunBehavior.RespectReload = true;
			shootGunBehavior.MagazineCapacity = 2;
			shootGunBehavior.ReloadSpeed = 6;
			shootGunBehavior.Cooldown = 2;
			shootGunBehavior.EmptiesClip = true;
			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedBandanaKina)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class JammedBandanaKina : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top()
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").bulletBank.GetBullet("dagger"));
				}
				int shots = UnityEngine.Random.Range(9,16);
				for (int a = 0; a < shots; a++)
				{
					int accuracy = UnityEngine.Random.Range(-20, 20);
					base.PostWwiseEvent("Play_WPN_uzi_shot_01", null);
					base.Fire(new Direction(accuracy, DirectionType.Aim, -1f), new Speed(10f, SpeedType.Absolute), null);
					yield return base.Wait(120/shots);

				}
				base.PostWwiseEvent("Play_OBJ_spears_clank_01", null);
				yield return base.Wait(1200/shots);
				for (int a = -3; a < 2; a++)
				{
					base.Fire(new Direction(0f+(a*15), DirectionType.Aim, -1f), new Speed(13f, SpeedType.Absolute), new JammedBandanaKina.FastHomingShot1());

				}
				yield return base.Wait(20);
				{
					base.Fire(new Direction(-10f, DirectionType.Aim, -1f), new Speed(17f, SpeedType.Absolute), new JammedBandanaKina.FastHomingShot1());
					base.Fire(new Direction(10f, DirectionType.Aim, -1f), new Speed(17f, SpeedType.Absolute), new JammedBandanaKina.FastHomingShot1());
				}
				yield return base.Wait(300);
				yield break;
			}

			// Token: 0x020001E2 RID: 482
			public class FastHomingShot1 : Bullet
			{
				public FastHomingShot1() : base("dagger", false, false, false)
				{
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedAK47la : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "db35531e66ce41cbb81d507a34366dfe"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 1f;
			shootGunBehavior.RespectReload = true;
			shootGunBehavior.MagazineCapacity = 1;
			shootGunBehavior.ReloadSpeed = 9;
			shootGunBehavior.EmptiesClip = true;
			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedAK47laa)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class JammedAK47laa : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top()
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("880bbe4ce1014740ba6b4e2ea521e49d").bulletBank.GetBullet("grenade"));
				}
				float airTime = base.BulletBank.GetBullet("grenade").BulletObject.GetComponent<ArcProjectile>().GetTimeInFlight();
				Vector2 vector = this.BulletManager.PlayerPosition();
				Bullet bullet2 = new Bullet("grenade", false, false, false);
				float direction2 = (vector - base.Position).ToAngle();
				base.Fire(new Direction(direction2, DirectionType.Absolute, -1f), new Speed(1f, SpeedType.Absolute), bullet2);
				(bullet2.Projectile as ArcProjectile).AdjustSpeedToHit(vector);
				bullet2.Projectile.ImmuneToSustainedBlanks = true;
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").bulletBank.GetBullet("dagger"));
				}
				int shots = UnityEngine.Random.Range(11, 24);
				for (int a = 0; a < shots; a++)
				{
					int accuracy = UnityEngine.Random.Range(-5, 5);
					base.PostWwiseEvent("Play_WPN_uzi_shot_01", null);
					base.Fire(new Direction(accuracy, DirectionType.Aim, -1f), new Speed(10.5f, SpeedType.Absolute), null);
					yield return base.Wait(100 / shots);
				}
				base.PostWwiseEvent("Play_OBJ_spears_clank_01", null);
				yield return base.Wait(600 / shots);
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("880bbe4ce1014740ba6b4e2ea521e49d").bulletBank.GetBullet("grenade"));
				}
				Vector2 vectora = this.BulletManager.PlayerPosition();
				base.Fire(new Direction(direction2, DirectionType.Absolute, -1f), new Speed(1f, SpeedType.Absolute), bullet2);
				(bullet2.Projectile as ArcProjectile).AdjustSpeedToHit(vectora);
				bullet2.Projectile.ImmuneToSustainedBlanks = true;
				yield return base.Wait(300);
				yield break;
			}

			// Token: 0x020001E2 RID: 482
			public class FastHomingShot1 : Bullet
			{
				public FastHomingShot1() : base("dagger", false, false, false)
				{
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedWizbang: OverrideBehavior
	{
		public override string OverrideAIActorGUID => "43426a2e39584871b287ac31df04b544"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{
			//ToolsEnemy.DebugInformation(behaviorSpec);
			RemoteShootBehavior cast = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as RemoteShootBehavior;
			cast.remoteBulletScript = new CustomBulletScriptSelector(typeof(JammedWizbanga)); 

		}
		public class JammedWizbanga : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top()
			{

				int shots = UnityEngine.Random.Range(3, 5);
				int offsetangle = 360 / shots;
				for (int a = 0; a < shots; a++)
                {
					base.Fire(new Offset(0f, -2.5f, -20f - (float)a * offsetangle), new BulletManMagicAstral2.AstralBullet());
					yield return base.Wait((20*a)/2);
				}
				base.Fire(new BulletManMagicAstral2.AstralBullet());
				yield break;
			}			
		}
	}
}

namespace BunnyMod
{
	public class JammedMutantShotty: OverrideBehavior
	{
		public override string OverrideAIActorGUID => "7f665bd7151347e298e4d366f8818284"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.80f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedMutantShottytmAttack)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class JammedMutantShottytmAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				PlayerController player = (GameManager.Instance.PrimaryPlayer);
				float curse = 0f;
				curse = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
				base.PostWwiseEvent("Play_WPN_shotgun_shot_01", null);
				for (int a = -3; a <= 4; a++)
				{
					base.Fire(new Direction(0f + (float)(a * (7)), DirectionType.Aim, -1f), new Speed(9.5f - (float)Mathf.Abs(a) * 0.3f, SpeedType.Absolute), new ShotgrubManAttack1.GrubBullet());
				}
				yield return base.Wait(20);
				base.PostWwiseEvent("Play_WPN_shotgun_shot_01", null);
				for (int a = -3; a <= 4; a++)
				{
					base.Fire(new Direction(0f + (float)(a * (7)), DirectionType.Aim, -1f), new Speed(10.5f - (float)Mathf.Abs(a) * 0.3f, SpeedType.Absolute), new ShotgrubManAttack1.GrubBullet());
				}
				yield break;
			}
		}

	}
}

namespace BunnyMod
{
	public class JammedMutantBulletkin : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "d4a9836f8ab14f3fadd0f597438b1f1f"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.80f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedMutantBulletkinAttack)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class JammedMutantBulletkinAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					//base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("37340393f97f41b2822bc02d14654172").bulletBank.GetBullet("quickHoming"));
				}
				for (int a = 0; a <= 5; a++)
                {
					PlayerController player = (GameManager.Instance.PrimaryPlayer);
					float curse = 0f;
					curse = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
					int accuracy = UnityEngine.Random.Range(-12, 12);
					base.PostWwiseEvent("Play_WPN_magnum_shot_01", null);
					base.Fire(new Direction(accuracy, DirectionType.Aim, -1f), new Speed(11f, SpeedType.Absolute), new ShotgrubManAttack1.GrubBullet());
					yield return this.Wait(6);
				}
				yield break;
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedLilMushroom : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "f905765488874846b7ff257ff81d6d0c"; 
																						  
		public override void DoOverride()
		{

			ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootBehavior; 
			shootGunBehavior.LeadAmount = 0.80f;

			//shootGunBehavior.WeaponType = WeaponType.BulletScript; 
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedLilMushroomAttack)); 

		}
		public class JammedLilMushroomAttack : Script 
		{
			protected override IEnumerator Top()
			{
				int spurt1 = UnityEngine.Random.Range(30, 60);
				int spurt2 = UnityEngine.Random.Range(15, 35);
				for (int i = 0; i < spurt1; i++)
				{
					string bankName = (UnityEngine.Random.value > 0.33f) ? "spore2" : "spore1";
					base.Fire(new Direction(base.RandomAngle(), DirectionType.Absolute, -1f), new Speed(UnityEngine.Random.Range(1.2f, 6f), SpeedType.Absolute), new JammedLilMushroomAttack.WaftBullet1(bankName));
				}
				for (int j = 0; j < spurt2; j++)
				{
					string name = (UnityEngine.Random.value > 0.33f) ? "spore2" : "spore1";
					Bullet bullet = new SpeedChangingBullet(name, 9f, 75, 300, false);
					base.Fire(new Direction(base.RandomAngle(), DirectionType.Absolute, -1f), new Speed((float)UnityEngine.Random.Range(2, 16), SpeedType.Absolute), bullet);
					bullet.Projectile.spriteAnimator.Play();
				}
				return null;
			}

			// Token: 0x04000AF5 RID: 2805
			private const int NumWaftBullets = 200;

			// Token: 0x04000AF6 RID: 2806
			private const int NumFastBullets = 50;

			// Token: 0x04000AF7 RID: 2807
			private const float VerticalDriftVelocity = -0.5f;

			// Token: 0x04000AF8 RID: 2808
			private const float WaftXPeriod = 3f;

			// Token: 0x04000AF9 RID: 2809
			private const float WaftXMagnitude = 0.5f;

			// Token: 0x04000AFA RID: 2810
			private const float WaftYPeriod = 1f;

			// Token: 0x04000AFB RID: 2811
			private const float WaftYMagnitude = 0.125f;

			// Token: 0x04000AFC RID: 2812
			private const int WaftLifeTime = 300;

			// Token: 0x020002AF RID: 687
			public class WaftBullet1 : Bullet
			{
				// Token: 0x06000A86 RID: 2694 RVA: 0x00032CA0 File Offset: 0x00030EA0
				public WaftBullet1(string bankName) : base(bankName, false, false, false)
				{
				}

				// Token: 0x06000A87 RID: 2695 RVA: 0x00032CAC File Offset: 0x00030EAC
				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 120);
					yield return base.Wait(120);
					base.ManualControl = true;
					Vector2 truePosition = base.Position;
					float xOffset = UnityEngine.Random.Range(0f, 3f);
					float yOffset = UnityEngine.Random.Range(0f, 1f);
					truePosition -= new Vector2(Mathf.Sin(xOffset * 3.14159274f / 3f) * 0.5f, Mathf.Sin(yOffset * 3.14159274f / 1f) * 0.125f);
					for (int i = 0; i < 300; i++)
					{
						if (base.IsOwnerAlive && UnityEngine.Random.value < 0.005f)
						{
							this.Projectile.spriteAnimator.Play();
							yield return base.Wait(30);
							base.ManualControl = false;
							this.Direction = base.AimDirection;
							int speed = UnityEngine.Random.Range(15, 60);
							base.ChangeSpeed(new Speed(15f, SpeedType.Absolute), speed);
							yield break;
						}
						truePosition += new Vector2(0f, -0.008333334f);
						float t = (float)i / 60f;
						float waftXOffset = Mathf.Sin((t + xOffset) * 3.14159274f / 3f) * 0.5f;
						float waftYOffset = Mathf.Sin((t + yOffset) * 3.14159274f / 1f) * 0.125f;
						base.Position = truePosition + new Vector2(waftXOffset, waftYOffset);
						yield return base.Wait(1);
					}
					base.Vanish(false);
					yield break;
				}
				
			}
			public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
			{
				if (!preventSpawningProjectiles)
				{
					int numure = UnityEngine.Random.Range(0, 7);
					bool fuckye = numure == 0;
					if (fuckye)
					{
						int fuckyou = UnityEngine.Random.Range(4, 9);
						base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(fuckyou, SpeedType.Relative), new ShotgrubManAttack1.GrubBullet());
					}
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedCubulon : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "864ea5a6a9324efc95a0dd2407f42810";

		public override void DoOverride()
		{

			ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootBehavior;
			shootGunBehavior.LeadAmount = 0.80f;

			//shootGunBehavior.WeaponType = WeaponType.BulletScript; 
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedCubulonAttack));

		}
		public class JammedCubulonAttack : Script
		{
			protected override IEnumerator Top()
			{
				int rotation = UnityEngine.Random.Range(15, 60);
				for (int i = 0; i < 2; i++)
                {
					this.FireLine(45f+(45*i)+rotation);
					this.FireLine(135f+(45*i)+rotation);
					this.FireLine(225f+(45*i)+rotation);
					this.FireLine(315f+(45*i)+rotation);
				}
				return null;
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x000174FC File Offset: 0x000156FC
			private void FireLine(float startingAngle)
			{

				float num = 9f;
				for (int i = 0; i < 11; i++)
				{
					float num2 = Mathf.Atan((-45f + (float)i * num) / 45f) * 57.29578f;
					float num3 = Mathf.Cos(num2 * 0.0174532924f);
					float num4 = ((double)Mathf.Abs(num3) >= 0.0001) ? (1f / num3) : 1f;
					base.Fire(new Direction(num2 + startingAngle, DirectionType.Absolute, -1f), new Speed(num4 * 11f, SpeedType.Absolute), null);
				}
			}

			// Token: 0x040004A3 RID: 1187
			private const int NumBullets = 11;
		}
	}
}

namespace BunnyMod
{
	public class JammedCubulead : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "0b547ac6b6fc4d68876a241a88f5ca6a";	

		public override void DoOverride()
		{

			ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootBehavior;
			shootGunBehavior.LeadAmount = 0.80f;

			//shootGunBehavior.WeaponType = WeaponType.BulletScript; 
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedCubuleadAttack));

		}
		public class JammedCubuleadAttack : Script
		{
			protected override IEnumerator Top()
			{
				int rotation = UnityEngine.Random.Range(15, 45);
				for (int i = 0; i < 4; i++)
                {
					this.FireLine(45f + (22.5f * i)+rotation);
					this.FireLine(135f + (22.5f * i)+rotation);
					this.FireLine(225f + (22.5f * i)+rotation);
					this.FireLine(315f + (22.5f * i)+rotation);
					yield return this.Wait(20);
				}
				yield break;
			}

			// Token: 0x060004C2 RID: 1218 RVA: 0x000171A8 File Offset: 0x000153A8
			private void FireLine(float startingAngle)
			{
				float num = 4.5f;
				for (int i = 0; i < 21; i++)
				{
					float num2 = Mathf.Atan((-45f + (float)i * num) / 45f) * 57.29578f;
					float num3 = Mathf.Cos(num2 * 0.0174532924f);
					float num4 = ((double)Mathf.Abs(num3) >= 0.0001) ? (1f / num3) : 1f;
					base.Fire(new Direction(num2 + startingAngle, DirectionType.Absolute, -1f), new Speed(num4 * 10f, SpeedType.Absolute), new CubuleadSlam1.ReversingBullet());
				}
			}

			// Token: 0x0400049D RID: 1181
			private const int NumBullets = 11;

			// Token: 0x02000141 RID: 321
			public class ReversingBullet : Bullet
			{
				// Token: 0x060004C3 RID: 1219 RVA: 0x00017248 File Offset: 0x00015448
				public ReversingBullet() : base("reversible", false, false, false)
				{
				}

				// Token: 0x060004C4 RID: 1220 RVA: 0x00017258 File Offset: 0x00015458
				protected override IEnumerator Top()
				{
					if (base.BulletBank && base.BulletBank.healthHaver)
					{
						base.BulletBank.healthHaver.OnPreDeath += this.OnPreDeath;
					}
					float speed = this.Speed;
					yield return base.Wait(40);
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 20);
					yield return base.Wait(20);
					this.Direction += 180f;
					this.Projectile.spriteAnimator.Play();
					yield return base.Wait(60);
					base.ChangeSpeed(new Speed(speed, SpeedType.Absolute), 40);
					yield return base.Wait(70);
					base.Vanish(false);
					yield break;
				}

				// Token: 0x060004C5 RID: 1221 RVA: 0x00017274 File Offset: 0x00015474
				private void OnPreDeath(Vector2 deathDir)
				{
					base.Vanish(false);
				}

				// Token: 0x060004C6 RID: 1222 RVA: 0x00017280 File Offset: 0x00015480
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (base.BulletBank && base.BulletBank.healthHaver)
					{
						base.BulletBank.healthHaver.OnPreDeath -= this.OnPreDeath;
					}
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedMuzzleWisp : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "ffdc8680bdaa487f8f31995539f74265";

		public override void DoOverride()
		{
			ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootBehavior;
			shootGunBehavior.LeadAmount = 0.80f;

			//shootGunBehavior.WeaponType = WeaponType.BulletScript; 
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedMuzzleWispAttack));
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedMuzzleWispAttack : Script
		{
			protected override IEnumerator Top()
			{
				float num2 = 12f;
				for (int i = -6; i < 6; i++)
				{
					for (int a = 0; a < 4; a++)
                    {
						base.Fire(new Direction((0f + (float)i * num2)+(90*a), DirectionType.Aim, -1f), new Speed((11) - (float)Mathf.Abs(i) * 0.5f, SpeedType.Absolute), null);
					}
				}
				yield return null;
			}

			// Token: 0x04000CBA RID: 3258
			private const int NumBullets = 120;

			// Token: 0x02000302 RID: 770
			public class BurstBullet : Bullet
			{
				// Token: 0x06000BEC RID: 3052 RVA: 0x0003A418 File Offset: 0x00038618
				public BurstBullet() : base(null, false, false, false)
				{
				}

				// Token: 0x06000BED RID: 3053 RVA: 0x0003A424 File Offset: 0x00038624
				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(10f, SpeedType.Absolute), 70);
					return null;
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedMuzzleFlare : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "d8a445ea4d944cc1b55a40f22821ae69";

		public override void DoOverride()
		{
			ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootBehavior;
			shootGunBehavior.LeadAmount = 0.80f;

			//shootGunBehavior.WeaponType = WeaponType.BulletScript; 
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedMuzzleFlareAttack));
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedMuzzleFlareAttack : Script
		{
			protected override IEnumerator Top()
			{
				PlayerController player = (GameManager.Instance.PrimaryPlayer);
				float curse = 0f;
				curse = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
				float num2 = 10f;
				for (int i = -5; i < 5; i++)
				{
					for (int a = 0; a < 8; a++)
					{
						base.Fire(new Direction((0f + (float)i * num2) + (45 * a), DirectionType.Aim, -1f), new Speed((10) - (float)Mathf.Abs(i) * 0.6f, SpeedType.Absolute), null);
					}
				}
				yield return null;
			}

			// Token: 0x04000CBA RID: 3258
			private const int NumBullets = 120;

			// Token: 0x02000302 RID: 770
			public class BurstBullet : Bullet
			{
				// Token: 0x06000BEC RID: 3052 RVA: 0x0003A418 File Offset: 0x00038618
				public BurstBullet() : base(null, false, false, false)
				{
				}

				// Token: 0x06000BED RID: 3053 RVA: 0x0003A424 File Offset: 0x00038624
				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(10f, SpeedType.Absolute), 70);
					return null;
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedTanker : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "df7fb62405dc4697b7721862c7b6b3cd";

		public override void DoOverride()
		{
			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior; 

			shootGunBehavior.WeaponType = WeaponType.BulletScript; 
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedTankerAttack));
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedTankerAttack : Script
		{
			protected override IEnumerator Top()
			{
				for (int i = 0; i < 3; i++)
				{
					base.PostWwiseEvent("Play_WPN_uzi_shot_01", null);
					for (int a = -1; a <= 1; a++)
                    {
						base.Fire(new Direction(0f+(a*(30-(i*2))), DirectionType.Aim, -1f), new Speed(8.5f+(i/2), SpeedType.Absolute), null);
					}
					yield return this.Wait(5);
				}
				yield return this.Wait(20);
				yield return null;
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedShroomer : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "e5cffcfabfae489da61062ea20539887";

		public override void DoOverride()
		{
			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior;

			shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedShroomerAttack));
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedShroomerAttack : Script
		{
			protected override IEnumerator Top()
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("f905765488874846b7ff257ff81d6d0c").bulletBank.GetBullet("spore2"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("f905765488874846b7ff257ff81d6d0c").bulletBank.GetBullet("spore1"));
				}
				for (int i = 0; i < 7; i++)
				{
					base.PostWwiseEvent("Play_WPN_colt1851_shot_01", null);
					for (int a = 0; a <= 1; a++)
					{
						base.Fire(new Direction((-20f+(i*3f)) +(a*(40-(i*6))), DirectionType.Aim, -1f), new Speed(8.5f +(i/3), SpeedType.Absolute), null);
					}
					int ppfpfpft = UnityEngine.Random.Range(1, 2);
					for (int a = 0; a < ppfpfpft; a++)
					{

						string bankName = (UnityEngine.Random.value > 0.33f) ? "spore2" : "spore1";
						base.Fire(new Direction(base.RandomAngle(), DirectionType.Absolute, -1f), new Speed(UnityEngine.Random.Range(1.2f, 4f), SpeedType.Absolute), new MushroomGuySmallWaft1.WaftBullet(bankName));
					}
					yield return this.Wait(2);
				}
				yield return null;
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedKingBullat : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "1a4872dafdb34fd29fe8ac90bd2cea67";

		public override void DoOverride()
		{
			ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootBehavior;

			//shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedBullatAttack));
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedBullatAttack : Script
		{
			protected override IEnumerator Top()
			{
				float num = base.RandomAngle();
				float num2 = 10f;
				for (int u = 0; u < 2; u++)
                {
					for (int i = 0; i < 4; i++)
					{
						for (int a = 0; a < 5; a++)
						{
							base.Fire(new Direction((num + (float)i * num2) + (90 * a)+(u*45), DirectionType.Absolute, -1f), new Speed(10f, SpeedType.Absolute), null);
						}
					}
					yield return this.Wait(20);
				}
				yield return null;
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedAshenKin : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "1a78cfb776f54641b832e92c44021cf2";

		public override void DoOverride()
		{
			SequentialAttackBehaviorGroup g = (SequentialAttackBehaviorGroup)behaviorSpec.AttackBehaviors.Find((AttackBehaviorBase behav) => behav is SequentialAttackBehaviorGroup);
			ShootGunBehavior shootGunBehavior = g.AttackBehaviors[0] as ShootGunBehavior;
			shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedAshenKinAttack));
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedAshenKinAttack : Script
		{
			protected override IEnumerator Top()
			{
				for (int u = 0; u < 2; u++)
				{
					base.PostWwiseEvent("Play_WPN_magnum_shot_01", null);
					for (int i = -1; i < 1; i++)
					{
						base.Fire(new Direction(0+(10*i), DirectionType.Relative, -1f), new Speed(11f, SpeedType.Absolute), new JammedAshenKinAttack.BurstBullet());
					}
					yield return this.Wait(20);
				}
				yield return null;
			}
			public class BurstBullet : Bullet
			{
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (!preventSpawningProjectiles)
					{
						base.Fire(new Direction(180f, DirectionType.Relative, -1f), new Speed(8.5f, SpeedType.Absolute), null);
					}
				}
			}
		}
		
	}
}

namespace BunnyMod
{
	public class JammedAshenShotty : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "1bd8e49f93614e76b140077ff2e33f2b";

		public override void DoOverride()
		{
			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior;
			shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedAshenShottyAttack));
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedAshenShottyAttack : Script
		{
			protected override IEnumerator Top()
			{

				for (int u = 0; u < 1; u++)
				{
					base.PostWwiseEvent("Play_WPN_magnum_shot_01", null);
					for (int i = -3; i < 3; i++)
					{
						base.Fire(new Direction(0 + (6 * i), DirectionType.Relative, -1f), new Speed(12f, SpeedType.Absolute), new JammedAshenShottyAttack.BurstBullet());
					}
					yield return this.Wait(20);
				}
				yield return null;
			}
			public class BurstBullet : Bullet
			{
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (!preventSpawningProjectiles)
					{
						int numure = UnityEngine.Random.Range(0, 4);
						bool fuckye = numure == 0;
						if (fuckye)
						{
							for (int i = -1; i < 1; i++)
							{
								int numure1 = UnityEngine.Random.Range(1, 3);
								base.Fire(new Direction(180f + ((10* numure1) * i), DirectionType.Relative, -1f), new Speed(9f, SpeedType.Absolute), null);
							}
						}

					}
				}
			}
		}

	}
}

namespace BunnyMod
{
	public class JammedCreech : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "37340393f97f41b2822bc02d14654172";

		public override void DoOverride()
		{
			ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootBehavior;

			//shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedCreechAttack));
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedCreechAttack : Script
		{
			protected override IEnumerator Top()
			{
				for (int i = 0; i < 2; i++)
                {
					for (int k = 0; k < 7; k++)
					{
						int times = UnityEngine.Random.Range(1, 4);
						string transform = string.Format("shoot point {0}", i);
						for (int j = 0; j < times; j++)
						{
							base.Fire(new Offset(transform), new Direction(base.RandomAngle(), DirectionType.Absolute, -1f), new Speed((float)UnityEngine.Random.Range(8, 15), SpeedType.Absolute), new JammedCreechAttack.CreecherBullet());
						}
					}
					yield return this.Wait(10);
				}
				yield return null;
			}

			// Token: 0x04000C90 RID: 3216
			private const int NumBulletNodes = 7;

			// Token: 0x04000C91 RID: 3217
			private const int NumBulletsPerNode = 2;

			// Token: 0x020002F6 RID: 758
			public class CreecherBullet : Bullet
			{
				// Token: 0x06000BBE RID: 3006 RVA: 0x00039768 File Offset: 0x00037968
				public CreecherBullet() : base(null, false, false, false)
				{
				}

				// Token: 0x06000BBF RID: 3007 RVA: 0x00039774 File Offset: 0x00037974

			}
		}
	}
}

namespace BunnyMod
{
	public class JammedBerb: OverrideBehavior
	{
		public override string OverrideAIActorGUID => "ed37fa13e0fa4fcf8239643957c51293";

		public override void DoOverride()
		{
			ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootBehavior;

			//shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedBerbAttack));
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedBerbAttack : Script
		{
			protected override IEnumerator Top()
			{
				float num = BraveMathCollege.ClampAngle360(this.Direction);
				float direction = (float)((num <= 90f || num > 180f) ? 20 : 160);
				base.Fire(new Direction(direction, DirectionType.Absolute, -1f), new Speed(2f, SpeedType.Absolute), new JammedBerbAttack.EggBullet());

				return null;
			}

			// Token: 0x040000A7 RID: 167
			private const int ClusterBullets = 0;

			// Token: 0x040000A8 RID: 168
			private const float ClusterRotation = 150f;

			// Token: 0x040000A9 RID: 169
			private const float ClusterRadius = 0.5f;

			// Token: 0x040000AA RID: 170
			private const float ClusterRadiusSpeed = 2f;

			// Token: 0x040000AB RID: 171
			private const int InnerBullets = 12;

			// Token: 0x040000AC RID: 172
			private const int InnerBounceTime = 30;

			// Token: 0x0200002F RID: 47
			public class EggBullet : Bullet
			{
				// Token: 0x060000AB RID: 171 RVA: 0x000049B4 File Offset: 0x00002BB4
				public EggBullet() : base("egg", false, false, false)
				{
				}

				// Token: 0x060000AC RID: 172 RVA: 0x000049C4 File Offset: 0x00002BC4
				protected override IEnumerator Top()
				{
					this.Projectile.sprite.SetSprite("egg_projectile_001");
					float startRotation = (float)((this.Direction <= 90f || this.Direction >= 270f) ? 135 : -135);
					for (int i = 0; i < 45; i++)
					{
						Vector2 velocity = BraveMathCollege.DegreesToVector(this.Direction, this.Speed);
						velocity += new Vector2(0f, -7f) / 60f;
						this.Direction = velocity.ToAngle();
						this.Speed = velocity.magnitude;
						this.Projectile.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(startRotation, 0f, (float)i / 45f));
						yield return base.Wait(1);
					}
					this.Projectile.transform.rotation = Quaternion.identity;
					this.Speed = 0f;
					this.Projectile.spriteAnimator.Play();
					int animTime = Mathf.RoundToInt(this.Projectile.spriteAnimator.DefaultClip.BaseClipLength * 60f);
					yield return base.Wait(animTime / 2);
					if (!this.spawnedBursts)
					{
						this.SpawnBursts();
					}
					yield return base.Wait(animTime / 2);
					base.Vanish(false);
					yield break;
				}

				// Token: 0x060000AD RID: 173 RVA: 0x000049E0 File Offset: 0x00002BE0
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (!this.spawnedBursts && !preventSpawningProjectiles)
					{
						this.SpawnBursts();
					}
				}

				// Token: 0x060000AE RID: 174 RVA: 0x000049FC File Offset: 0x00002BFC
				private void SpawnBursts()
				{
					float num = BraveMathCollege.ClampAngle360(this.Direction);
					float direction = (float)((num <= 90f || num > 180f) ? 20 : 160);
					base.Fire(new Direction(direction+(45), DirectionType.Absolute, -1f), new Speed(4f, SpeedType.Absolute), new BirdEggVomit2.EggBullet());
					base.Fire(new Direction(direction-(45), DirectionType.Absolute, -1f), new Speed(4f, SpeedType.Absolute), new BirdEggVomit2.EggBullet());
					float positiveInfinity = float.PositiveInfinity;
					for (int i = 0; i < 0; i++)
					{
						base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(13f, SpeedType.Absolute), new BirdEggVomit2.ClusterBullet((float)i * positiveInfinity));
					}
					for (int j = 0; j < 16; j++)
					{
						base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(12f+(j/4), SpeedType.Absolute), new BirdEggVomit2.InnerBullet());
					}
					this.spawnedBursts = true;
				}

				// Token: 0x040000AD RID: 173
				private bool spawnedBursts;
			}

			// Token: 0x02000031 RID: 49
			public class ClusterBullet : Bullet
			{
				// Token: 0x060000B5 RID: 181 RVA: 0x00004D58 File Offset: 0x00002F58
				public ClusterBullet(float clusterAngle) : base(null, false, false, false)
				{
					this.clusterAngle = clusterAngle;
				}

				// Token: 0x060000B6 RID: 182 RVA: 0x00004D6C File Offset: 0x00002F6C
				protected override IEnumerator Top()
				{
					base.ManualControl = true;
					Vector2 centerPosition = base.Position;
					float radius = 0.5f;
					for (int i = 0; i < 180; i++)
					{
						base.UpdateVelocity();
						centerPosition += this.Velocity / 60f;
						radius += 0.0333333351f;
						this.clusterAngle += 2.5f;
						base.Position = centerPosition + BraveMathCollege.DegreesToVector(this.clusterAngle, radius);
						yield return base.Wait(1);
					}
					base.Vanish(false);
					yield break;
				}

				// Token: 0x040000B6 RID: 182
				private float clusterAngle;
			}

			// Token: 0x02000033 RID: 51
			public class InnerBullet : Bullet
			{
				// Token: 0x060000BD RID: 189 RVA: 0x00004F00 File Offset: 0x00003100
				public InnerBullet() : base(null, false, false, false)
				{
				}

				// Token: 0x060000BE RID: 190 RVA: 0x00004F0C File Offset: 0x0000310C
				protected override IEnumerator Top()
				{
					base.ManualControl = true;
					Vector2 centerPosition = base.Position;
					float radius = 0.5f;
					int bounceDelay = UnityEngine.Random.Range(0, 30);
					Vector2 startOffset = BraveMathCollege.DegreesToVector(base.RandomAngle(), UnityEngine.Random.Range(0f, radius));
					float goalAngle = base.RandomAngle();
					float goalRadiusPercent = UnityEngine.Random.value;
					for (int i = 0; i < 180; i++)
					{
						base.UpdateVelocity();
						centerPosition += this.Velocity / 60f;
						radius += 0.0333333351f;
						Vector2 goalOffset = BraveMathCollege.DegreesToVector(goalAngle, goalRadiusPercent * radius);
						if (bounceDelay == 0)
						{
							startOffset = goalOffset;
							goalAngle = base.RandomAngle();
							goalRadiusPercent = UnityEngine.Random.value;
							goalOffset = BraveMathCollege.DegreesToVector(goalAngle, goalRadiusPercent * radius);
							bounceDelay = 30;
							if (radius > 1f)
							{
								bounceDelay = Mathf.RoundToInt(radius * (float)bounceDelay);
							}
						}
						else
						{
							bounceDelay--;
						}
						base.Position = centerPosition + Vector2.Lerp(startOffset, goalOffset, 1f - (float)bounceDelay / 30f);
						yield return base.Wait(1);
					}
					base.Vanish(false);
					yield break;
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedPoopBoy : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "116d09c26e624bca8cca09fc69c714b3";

		public override void DoOverride()
		{
			SpinAttackBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as SpinAttackBehavior;
			//ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootBehavior;
			//SequentialAttackBehaviorGroup g = (SequentialAttackBehaviorGroup)behaviorSpec.AttackBehaviors.Find((AttackBehaviorBase behav) => behav is SequentialAttackBehaviorGroup);
			//ShootBehavior shootGunBehavior = g.AttackBehaviors[0] as ShootBehavior;
			//shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedPoopyAttack));
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedPoopyAttack : Script
		{
			protected override IEnumerator Top()
			{
				for (int i = 0; i < 50; i++)
				{
					float angle = base.RandomAngle();
					base.Fire(new Offset(0.75f, 0f, angle, string.Empty, DirectionType.Absolute), new Direction(angle, DirectionType.Absolute, -1f), new Speed(UnityEngine.Random.Range(6f, 10f), SpeedType.Absolute), new JammedPoopyAttack.RotatingBullet(base.Position));
					base.Fire(new Offset(0.75f, 0f, angle, string.Empty, DirectionType.Absolute), new Direction(angle, DirectionType.Absolute, -1f), new Speed(UnityEngine.Random.Range(6f, 10f), SpeedType.Absolute), new JammedPoopyAttack.RotatingBullet1(base.Position));
					yield return base.Wait(2);
				}
				yield break;
			}

			// Token: 0x04000B5D RID: 2909
			private const int NumBullets = 100;

			// Token: 0x020002C2 RID: 706
			public class RotatingBullet : Bullet
			{
				// Token: 0x06000AD4 RID: 2772 RVA: 0x00034000 File Offset: 0x00032200
				public RotatingBullet(Vector2 origin) : base(null, false, false, false)
				{
					this.m_origin = origin;
				}

				// Token: 0x06000AD5 RID: 2773 RVA: 0x00034014 File Offset: 0x00032214
				protected override IEnumerator Top()
				{
					Vector2 originToPos = base.Position - this.m_origin;
					float dist = originToPos.magnitude;
					float angle = originToPos.ToAngle();
					base.ManualControl = true;
					for (int i = 0; i < 300; i++)
					{
						angle -= 0.45f;
						dist += this.Speed / 60f;
						base.Position = this.m_origin + BraveMathCollege.DegreesToVector(angle, dist);
						yield return base.Wait(1);
					}
					base.Vanish(false);
					yield break;
				}

				// Token: 0x04000B5E RID: 2910
				private Vector2 m_origin;
			}
			public class RotatingBullet1 : Bullet
			{
				// Token: 0x06000AD4 RID: 2772 RVA: 0x00034000 File Offset: 0x00032200
				public RotatingBullet1(Vector2 origin) : base(null, false, false, false)
				{
					this.m_origin = origin;
				}

				// Token: 0x06000AD5 RID: 2773 RVA: 0x00034014 File Offset: 0x00032214
				protected override IEnumerator Top()
				{
					Vector2 originToPos = base.Position - this.m_origin;
					float dist = originToPos.magnitude;
					float angle = originToPos.ToAngle();
					base.ManualControl = true;
					for (int i = 0; i < 300; i++)
					{
						angle -= -0.45f;
						dist += this.Speed / 60f;
						base.Position = this.m_origin + BraveMathCollege.DegreesToVector(angle, dist);
						yield return base.Wait(1);
					}
					base.Vanish(false);
					yield break;
				}

				// Token: 0x04000B5E RID: 2910
				private Vector2 m_origin;
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedCardinal: OverrideBehavior
	{
		public override string OverrideAIActorGUID => "8bb5578fba374e8aae8e10b754e61d62";

		public override void DoOverride()
		{
			//SpinAttackBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as SpinAttackBehavior;
			//ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootBehavior;
			//SequentialAttackBehaviorGroup g = (SequentialAttackBehaviorGroup)behaviorSpec.AttackBehaviors.Find((AttackBehaviorBase behav) => behav is SequentialAttackBehaviorGroup);
		    ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootGunBehavior;
			//ShootBehavior shootBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootBehavior;
			shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedJammedCardinalAttack));
			//shootBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedJammedCardinalAttackHat));
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedJammedCardinalAttack : Script
		{
			protected override IEnumerator Top()
			{
				int times = UnityEngine.Random.Range(2, 6);
				for (int i = 0; i < times; i++)
				{
					base.PostWwiseEvent("Play_WPN_magnum_shot_01", null);
					float aim = this.GetAimDirection(1f, 16f);
					this.Fire(new Direction(aim, DirectionType.Absolute, -1f), new Speed(8+(i/2), SpeedType.Absolute), new JammedJammedCardinalAttack.FastHomingShot1());
					yield return base.Wait(60/times);

				}
				yield break;
			}

			public class FastHomingShot1 : Bullet
			{
				protected override IEnumerator Top()
				{
					for (int i = 0; i < 20; i++)
					{
						float aim = this.GetAimDirection(1f, 16f);
						float delta = BraveMathCollege.ClampAngle180(aim - this.Direction);
						if (Mathf.Abs(delta) > 100f)
						{
							yield break;
						}
						this.Direction += Mathf.MoveTowards(0f, delta, 2f);
						yield return this.Wait(1);
					}
					yield break;
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedCardinal1 : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "8bb5578fba374e8aae8e10b754e61d62";

		public override void DoOverride()
		{
			//SpinAttackBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as SpinAttackBehavior;
			//ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootBehavior;
			//SequentialAttackBehaviorGroup g = (SequentialAttackBehaviorGroup)behaviorSpec.AttackBehaviors.Find((AttackBehaviorBase behav) => behav is SequentialAttackBehaviorGroup);
			//ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootGunBehavior;
			ShootBehavior shootBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[1].Behavior as ShootBehavior;
			//shootGunBehavior.WeaponType = WeaponType.BulletScript;
			//shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedJammedCardinalAttack));
			shootBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedJammedCardinalAttackHat));
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedJammedCardinalAttack : Script
		{
			protected override IEnumerator Top()
			{
				int times = UnityEngine.Random.Range(3, 7);
				for (int i = 0; i < times; i++)
				{
					base.PostWwiseEvent("Play_WPN_magnum_shot_01", null);
					float aim = this.GetAimDirection(1f, 16f);
					this.Fire(new Direction(aim, DirectionType.Absolute, -1f), new Speed(8 + (i / 2), SpeedType.Absolute), new JammedJammedCardinalAttack.FastHomingShot1());
					yield return base.Wait(60 / times);

				}
				yield break;
			}

			public class FastHomingShot1 : Bullet
			{
				protected override IEnumerator Top()
				{
					for (int i = 0; i < 60; i++)
					{
						float aim = this.GetAimDirection(1f, 16f);
						float delta = BraveMathCollege.ClampAngle180(aim - this.Direction);
						if (Mathf.Abs(delta) > 100f)
						{
							yield break;
						}
						this.Direction += Mathf.MoveTowards(0f, delta, 2f);
						yield return this.Wait(1);
					}
					yield break;
				}
			}
		}
		public class JammedJammedCardinalAttackHat : Script
		{
			protected override IEnumerator Top()
			{
				this.FireSpinningLine(new Vector2(-0.3f, 0f), new Vector2(0.3f, 0f), 3);
				this.FireSpinningLine(new Vector2(0f, -0.3f), new Vector2(0f, 0.3f), 3);
				
				this.FireSpinningLine1(new Vector2(-0.06f, -0.24f), new Vector2(0.06f, 0.24f), 3);
				this.FireSpinningLine2(new Vector2(-0.12f, -0.18f), new Vector2(0.12f, 0.18f), 3);
				this.FireSpinningLine3(new Vector2(-0.18f, -0.12f), new Vector2(0.18f, 0.12f), 3);
				this.FireSpinningLine4(new Vector2(-0.24f, -0.06f), new Vector2(0.24f, 0.06f), 3);
				yield return base.Wait(60);
				yield break;
			}

			// Token: 0x0600033F RID: 831 RVA: 0x0001092C File Offset: 0x0000EB2C
			private void FireSpinningLine(Vector2 start, Vector2 end, int numBullets)
			{
				numBullets = 12;
				start *= 4f;
				end *= 4f;
				float direction = (this.BulletManager.PlayerPosition() - base.Position).ToAngle();
				for (int i = 0; i < numBullets; i++)
				{
					Vector2 b = Vector2.Lerp(start, end, (float)i / ((float)numBullets - 1f));
					base.Fire(new Direction(direction, DirectionType.Absolute, -1f), new BulletCardinalHat1.SpinningBullet(base.Position, base.Position + b));
				}
			}
			private void FireSpinningLine1(Vector2 start, Vector2 end, int numBullets)
			{
				numBullets = 14;
				start *= 4.6f;
				end *= 4.6f;
				float direction = (this.BulletManager.PlayerPosition() - base.Position).ToAngle();
				for (int i = 0; i < numBullets; i++)
				{
					Vector2 b = Vector2.Lerp(start, end, (float)i / ((float)numBullets - 1f));
					base.Fire(new Direction(direction, DirectionType.Absolute, -1f), new BulletCardinalHat1.SpinningBullet(base.Position, base.Position + b));
				}
			}
			private void FireSpinningLine2(Vector2 start, Vector2 end, int numBullets)
			{
				numBullets = 16;
				start *= 5.2f;
				end *= 5.2f;
				float direction = (this.BulletManager.PlayerPosition() - base.Position).ToAngle();
				for (int i = 0; i < numBullets; i++)
				{
					Vector2 b = Vector2.Lerp(start, end, (float)i / ((float)numBullets - 1f));
					base.Fire(new Direction(direction, DirectionType.Absolute, -1f), new BulletCardinalHat1.SpinningBullet(base.Position, base.Position + b));
				}
			}
			private void FireSpinningLine3(Vector2 start, Vector2 end, int numBullets)
			{
				numBullets = 16;
				start *= 5.2f;
				end *= 5.2f;
				float direction = (this.BulletManager.PlayerPosition() - base.Position).ToAngle();
				for (int i = 0; i < numBullets; i++)
				{
					Vector2 b = Vector2.Lerp(start, end, (float)i / ((float)numBullets - 1f));
					base.Fire(new Direction(direction, DirectionType.Absolute, -1f), new BulletCardinalHat1.SpinningBullet(base.Position, base.Position + b));
				}
			}
			private void FireSpinningLine4(Vector2 start, Vector2 end, int numBullets)
			{
				numBullets = 14;
				start *= 4.6f;
				end *= 4.6f;
				float direction = (this.BulletManager.PlayerPosition() - base.Position).ToAngle();
				for (int i = 0; i < numBullets; i++)
				{
					Vector2 b = Vector2.Lerp(start, end, (float)i / ((float)numBullets - 1f));
					base.Fire(new Direction(direction, DirectionType.Absolute, -1f), new BulletCardinalHat1.SpinningBullet(base.Position, base.Position + b));
				}
			}

			// Token: 0x020000D7 RID: 215
			public class SpinningBullet : Bullet
			{
				// Token: 0x06000340 RID: 832 RVA: 0x000109C0 File Offset: 0x0000EBC0
				public SpinningBullet(Vector2 origin, Vector2 startPos) : base("hat", false, false, false)
				{
					this.m_origin = origin;
					this.m_startPos = startPos;
					base.SuppressVfx = true;
				}

				// Token: 0x06000341 RID: 833 RVA: 0x000109E8 File Offset: 0x0000EBE8
				protected override IEnumerator Top()
				{
					base.ManualControl = true;
					Vector2 delta = (this.m_startPos - base.Position) / 45f;
					for (int i = 0; i < 45; i++)
					{
						base.Position += delta;
						yield return base.Wait(1);
					}
					this.Speed = 2f;
					float angle = 0f;
					Vector2 centerOfMass = this.m_origin;
					Vector2 centerOfMassOffset = this.m_origin - base.Position;
					for (int j = 0; j < 120; j++)
					{
						this.Direction = Mathf.MoveTowardsAngle(this.Direction, (this.BulletManager.PlayerPosition() - centerOfMass).ToAngle(), 1.5f);
						base.UpdateVelocity();
						centerOfMass += this.Velocity / 60f;
						angle += 2f;
						base.Position = centerOfMass + (Quaternion.Euler(0f, 0f, angle) * centerOfMassOffset).XY();
						yield return base.Wait(1);
					}
					base.Vanish(false);
					yield break;
				}

				// Token: 0x0400035E RID: 862
				private const float RotationSpeed = 6f;

				// Token: 0x0400035F RID: 863
				private Vector2 m_origin;

				// Token: 0x04000360 RID: 864
				private Vector2 m_startPos;
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedCoaler: OverrideBehavior
	{
		public override string OverrideAIActorGUID => "9d50684ce2c044e880878e86dbada919"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[1].Behavior as ShootBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			//shootGunBehavior.LeadAmount = 0.80f;

			//shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedFIREFIREAAA)); // Sets the bullet kin's bullet script to our custom bullet script.
			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedFIREFIREAAA : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("465da2bb086a4a88a803f79fe3a27677").bulletBank.GetBullet("default_novfx"));
				}

				int times = UnityEngine.Random.Range(3, 12);
				for (int i = 0; i < times; i++)
				{
					int speed = UnityEngine.Random.Range(4, 10);
					float aim = 360 / times;
					base.PostWwiseEvent("Play_BOSS_doormimic_flame_01", null);
					this.Fire(new Direction(0f+(aim*i), DirectionType.Absolute, -1f), new Speed(speed, SpeedType.Absolute), new JammedFIREFIREAAA.FireFromCoalie());
				}
				int waityounerd = UnityEngine.Random.Range(4, 16);
				yield return base.Wait(waityounerd);
				yield break;
			}	
			public class FireFromCoalie : Bullet
			{
				public FireFromCoalie() : base("default_novfx", false, false, false)
				{
				}

				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(16f, SpeedType.Absolute), 120);
					yield break;
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedSkullhead : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "af84951206324e349e1f13f9b7b60c1a"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list

			//shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(SKULLYNEARGH)); // Sets the bullet kin's bullet script to our custom bullet script.

		}
		public class SKULLYNEARGH : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("af84951206324e349e1f13f9b7b60c1a").bulletBank.GetBullet("homing"));
				}
				int accuracy = UnityEngine.Random.Range(-30, 30);
				for (int i = -2; i < 2; i++)
				{
					base.Fire(new Direction(0+(i*60), DirectionType.Aim, -1f), new Speed(12f, SpeedType.Absolute), new SKULLYNEARGH.FireFromCoalie());
				}
				yield return base.Wait(40);
				for (int a = 0; a <= 1; a++)
				{
					base.Fire(new Direction((-50f + (a * 2f)) + (a * (100 - (a * 4))), DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new SKULLYNEARGH.FireFromCoalie());
				}
				yield break;
			}
			public class FireFromCoalie : Bullet
			{
				public FireFromCoalie() : base("homing", false, false, false)
				{
					//base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
					{
						base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("465da2bb086a4a88a803f79fe3a27677").bulletBank.GetBullet("Breath"));
					}
					for (int i = 0; i < 2; i++)
					{
						int waittime = UnityEngine.Random.Range(40, 360);
						yield return base.Wait(waittime);
						this.Fire(new Direction(0f, DirectionType.Absolute, -1f), new Speed(5f, SpeedType.Absolute), new SKULLYNEARGH.Flames());

					}
					yield break;
				}
			}
			public class Flames: Bullet
			{
				public Flames() : base("Breath", false, false, false)
				{
					//base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{

					base.ChangeSpeed(new Speed(20f, SpeedType.Absolute), 100);
					yield break;
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedMiner: OverrideBehavior
	{
		public override string OverrideAIActorGUID => "3cadf10c489b461f9fb8814abc1a09c1";
																						 
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootGunBehavior; 
			shootGunBehavior.LeadAmount = 0.70f;
			shootGunBehavior.WeaponType = WeaponType.BulletScript; 
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedMinerAttack));
			
			TransformBehavior mine = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[1].Behavior as TransformBehavior;
			mine.inBulletScript = new CustomBulletScriptSelector(typeof(JammedMinerAttackBababoom)); 
		}
		public class JammedMinerAttack : Script 
		{
			protected override IEnumerator Top() 
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					//base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("37340393f97f41b2822bc02d14654172").bulletBank.GetBullet("quickHoming"));
				}
				base.PostWwiseEvent("Play_WPN_magnum_shot_01", null);
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(7f, SpeedType.Absolute), new JammedMinerAttack.Flamesa());

				yield return this.Wait(30);

				yield break;
			}
			public class Flamesa : Bullet
			{

				protected override IEnumerator Top()
				{
					yield return base.Wait(60);
					base.Vanish(false);
					yield break;
				}
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (!preventSpawningProjectiles)
					{
						for (int i = 0; i < 5; i++)
						{
							float aim = 72;
							this.Fire(new Direction(0f + (aim * i), DirectionType.Absolute, -1f), new Speed(9.5f, SpeedType.Absolute), null);
						}
					}
				}
			}
		}
		public class JammedMinerAttackBababoom : Script 
		{
			protected override IEnumerator Top() 
			{

				yield return base.Wait(60);
				int times = UnityEngine.Random.Range(2, 4);
				for (int a = 0; a < times; a++)
                {
					base.PostWwiseEvent("Play_BOSS_wall_slam_01", null);
					for (int i = 0; i < 20; i++)
					{
						float aim = 18;
						this.Fire(new Direction(0f + (aim * i)+(10*a), DirectionType.Absolute, -1f), new Speed(3f, SpeedType.Absolute), new JammedMinerAttackBababoom.Flames());

					}
					yield return base.Wait(30);
				}
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("880bbe4ce1014740ba6b4e2ea521e49d").bulletBank.GetBullet("grenade"));
				}
				float airTime = base.BulletBank.GetBullet("grenade").BulletObject.GetComponent<ArcProjectile>().GetTimeInFlight();
				Vector2 vector = this.BulletManager.PlayerPosition();
				Bullet bullet2 = new Bullet("grenade", false, false, false);
				float direction2 = (vector - base.Position).ToAngle();
				base.Fire(new Direction(direction2, DirectionType.Absolute, -1f), new Speed(1f, SpeedType.Absolute), bullet2);
				(bullet2.Projectile as ArcProjectile).AdjustSpeedToHit(vector);
				bullet2.Projectile.ImmuneToSustainedBlanks = true;
				yield break;
			}
			public class Flames : Bullet
			{
				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(17f, SpeedType.Absolute), 100);
					yield break;
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedGoost: OverrideBehavior
	{
		public override string OverrideAIActorGUID => "4db03291a12144d69fe940d5a01de376";

		public override void DoOverride()
		{
			//ToolsEnemy.DebugInformation(behaviorSpec);
			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootGunBehavior;
		    shootGunBehavior.LeadAmount = 0.70f;
			shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedGoosta));

			//TransformBehavior mine = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[1].Behavior as TransformBehavior;
			//mine.inBulletScript = new CustomBulletScriptSelector(typeof(JammedMinerAttackBababoom));
		}
		public class JammedGoosta : Script
		{
			protected override IEnumerator Top()
			{

				int times = UnityEngine.Random.Range(6, 14);
				for (int i = 0; i < times; i++)
				{
					base.PostWwiseEvent("Play_WPN_uzi_shot_01", null);
					this.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(0f, SpeedType.Absolute), new JammedGoosta.Flames());
					yield return base.Wait((100 / times) * 1.75f);
				}
			}
			public class Flames : Bullet
			{
				protected override IEnumerator Top()
				{
					for (int i = 0; i < 40; i++)
					{
						base.ChangeSpeed(new Speed(base.Speed +.22f, SpeedType.Absolute), 1);
						float aim = this.GetAimDirection(1f, 16f);
						float delta = BraveMathCollege.ClampAngle180(aim - this.Direction);
						if (Mathf.Abs(delta) > 100f)
						{
							yield break;
						}
						this.Direction += Mathf.MoveTowards(0f, delta, 1.75f);
						yield return this.Wait(1);
					}
					yield break;
				}
			}
		}
			
	}
}
/*
namespace BunnyMod
{
	public class JammedgreenLizard : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "6e972cd3b11e4b429b888b488e308551";

		public override void DoOverride()
		{
			//SpinAttackBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as SpinAttackBehavior;
			//ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootBehavior;
			//SequentialAttackBehaviorGroup g = (SequentialAttackBehaviorGroup)behaviorSpec.AttackBehaviors.Find((AttackBehaviorBase behav) => behav is SequentialAttackBehaviorGroup);
			//ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootGunBehavior;
			//ShootBehavior shootBehavior = behaviorSpec.AttackBehaviors[0] as ShootBehavior;
			//shootGunBehavior.WeaponType = WeaponType.BulletScript;
			//shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedJammedCardinalAttack));
			//shootBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedgreenLizardattack));
			
			SequentialAttackBehaviorGroup g = (SequentialAttackBehaviorGroup)behaviorSpec.AttackBehaviors.Find((AttackBehaviorBase behav) => behav is SequentialAttackBehaviorGroup);
			ShootBehavior shootGunBehavior = g.AttackBehaviors[0] as ShootBehavior;
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedgreenLizardattack));

			//ToolsEnemy.DebugInformation(behaviorSpec);

		}
		public class JammedgreenLizardattack : Script
		{
			protected override IEnumerator Top()
			{
				base.Fire(new Direction(0f, DirectionType.Absolute, -1f), new Speed(2f, SpeedType.Absolute), new JammedgreenLizardattack.BubbleBullet());
				return null;
			}

			// Token: 0x04000326 RID: 806
			private const float WaftXPeriod = 3f;

			// Token: 0x04000327 RID: 807
			private const float WaftXMagnitude = 1f;

			// Token: 0x04000328 RID: 808
			private const float WaftYPeriod = 1f;

			// Token: 0x04000329 RID: 809
			private const float WaftYMagnitude = 0.25f;

			// Token: 0x0400032A RID: 810
			private const int BubbleLifeTime = 960;

			// Token: 0x020000C8 RID: 200
			public class BubbleBullet : Bullet
			{
				// Token: 0x0600030B RID: 779 RVA: 0x0000FB68 File Offset: 0x0000DD68
				public BubbleBullet() : base("bubble", false, false, false)
				{
				}

				// Token: 0x0600030C RID: 780 RVA: 0x0000FB78 File Offset: 0x0000DD78
				protected override IEnumerator Top()
				{
					base.ManualControl = true;
					Vector2 truePosition = base.Position;
					this.Projectile.spriteAnimator.Play("bubble_projectile_spawn");
					int animTime = Mathf.RoundToInt(this.Projectile.spriteAnimator.CurrentClip.BaseClipLength * 60f);
					float speed = this.Speed;
					this.Speed = 0f;
					yield return base.Wait(animTime);
					this.Speed = speed;
					this.Direction = base.AimDirection;
					for (int i = 0; i < 900; i++)
					{
						this.Direction = base.AimDirection;
						base.UpdateVelocity();
						truePosition += this.Velocity / 60f;
						float t = (float)i / 60f;
						float waftXOffset = Mathf.Sin(t * 3.14159274f / 3f) * 1f;
						float waftYOffset = Mathf.Sin(t * 3.14159274f / 1f) * 0.25f;
						base.Position = truePosition + new Vector2(waftXOffset, waftYOffset);
						yield return base.Wait(1);
					}
					this.Projectile.spriteAnimator.Play("bubble_projectile_burst");
					animTime = Mathf.RoundToInt(this.Projectile.spriteAnimator.CurrentClip.BaseClipLength * 60f);
					yield return base.Wait(animTime);
					base.Vanish(false);
					yield break;
				}

				// Token: 0x0600030D RID: 781 RVA: 0x0000FB94 File Offset: 0x0000DD94
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (preventSpawningProjectiles)
					{
						return;
					}
					for (int A = -1; A < 1; A++)
					{
						base.Fire(new Direction(0f + (15 * A), DirectionType.Aim, -1f), new Speed(1f, SpeedType.Absolute), null);
					}
				}
				public class Bullespeed : Bullet
				{
					protected override IEnumerator Top()
					{
						base.ChangeSpeed(new Speed(13f, SpeedType.Absolute), 120);
						yield break;
					}
				}
			}
		}
	}
}
*/
namespace BunnyMod
{
	public class JammedBulletShark : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "72d2f44431da43b8a3bae7d8a114a46d";

		public override void DoOverride()
		{

			DashBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as DashBehavior;
			//shootGunBehavior.LeadAmount = 0.70f;
			//shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior.bulletScript = new CustomBulletScriptSelector(typeof(JammedBulletSharkAttack));
			//ToolsEnemy.DebugInformation(behaviorSpec);


		}
		public class JammedBulletSharkAttack : Script
		{
			protected override IEnumerator Top()
			{
				int i = 0;
				for (; ; )
				{
					float startSpeed = UnityEngine.Random.Range(7.5f, 10.5f);
					float endSpeed = UnityEngine.Random.Range(-1.25f, -0.25f);
					int deathTimer = UnityEngine.Random.Range(30, 80);
					for (int A = -1; A < 1; A++)
					{

						base.Fire(new Direction(-60f - (A + 10), DirectionType.Relative, -1f), new Speed(startSpeed, SpeedType.Absolute), new SpeedChangingBullet("tellBullet", 9f, 60, -1, false));
						base.Fire(new Direction(60f + (A + 10), DirectionType.Relative, -1f), new Speed(startSpeed, SpeedType.Absolute), new SpeedChangingBullet("tellBullet", 9f, 60, -1, false));
					}
					{
						for (int A = -1; A < 1; A++)
						{
							base.Fire(new Direction(-90f - (A + 10), DirectionType.Relative, -1f), new Speed(startSpeed, SpeedType.Absolute), new SpeedChangingBullet(endSpeed, 60, deathTimer));
							base.Fire(new Direction(90f + (A + 10), DirectionType.Relative, -1f), new Speed(startSpeed, SpeedType.Absolute), new SpeedChangingBullet(endSpeed, 60, deathTimer));
						}
					}
					if (i % 4 == 1)
					{
						for (int A = -1; A < 1; A++)
						{
							endSpeed = BraveUtility.RandomSign() * UnityEngine.Random.Range(1f, 2f);
							deathTimer = UnityEngine.Random.Range(10, 60);
							base.Fire(new Direction(90f + (A + 10), DirectionType.Relative, -1f), new Speed(0f, SpeedType.Absolute), new SpeedChangingBullet(endSpeed, 60, deathTimer));
						}
					}
					i++;
					yield return base.Wait(3);
				}
			}
			
		}
	}
}

namespace BunnyMod
{
	public class JammedGreatBulletShark : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "b70cbd875fea498aa7fd14b970248920";

		public override void DoOverride()
		{

			ChargeBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ChargeBehavior;
			//shootGunBehavior.LeadAmount = 0.70f;
			//shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior.bulletScript = new CustomBulletScriptSelector(typeof(JammedGreatBulletSharkAttack));
			//ToolsEnemy.DebugInformation(behaviorSpec);


		}
		public class JammedGreatBulletSharkAttack : Script
		{
			protected override IEnumerator Top()
			{

				int i = 0;
				for (; ; )
				{
					float startSpeed = UnityEngine.Random.Range(7.5f, 12.5f);
					float endSpeed = UnityEngine.Random.Range(-1.25f, -0.25f);
					int deathTimer = UnityEngine.Random.Range(10, 60);
					if (UnityEngine.Random.value < 0.5f)
					{
						base.Fire(new Direction(-90f, DirectionType.Relative, -1f), new Speed(startSpeed - 4, SpeedType.Absolute), new SpeedChangingBullet("tellBullet", 9f, 60, -1, false));
						base.Fire(new Direction(90f, DirectionType.Relative, -1f), new Speed(startSpeed - 4, SpeedType.Absolute), new SpeedChangingBullet("tellBullet", 9f, 60, -1, false));
						base.Fire(new Direction(-120f, DirectionType.Relative, -1f), new Speed(startSpeed-2, SpeedType.Absolute), new SpeedChangingBullet("tellBullet", 9f, 60, -1, false));
						base.Fire(new Direction(120f, DirectionType.Relative, -1f), new Speed(startSpeed-2, SpeedType.Absolute), new SpeedChangingBullet("tellBullet", 9f, 60, -1, false));
						base.Fire(new Direction(-150f, DirectionType.Relative, -1f), new Speed(startSpeed - 2, SpeedType.Absolute), new SpeedChangingBullet("tellBullet", 9f, 60, -1, false));
						base.Fire(new Direction(150f, DirectionType.Relative, -1f), new Speed(startSpeed - 2, SpeedType.Absolute), new SpeedChangingBullet("tellBullet", 9f, 60, -1, false));
					}
					
					{
						base.Fire(new Direction(-90f, DirectionType.Relative, -1f), new Speed(startSpeed+4, SpeedType.Absolute), new SpeedChangingBullet("tellBullet", 9f, 60, -1, false));
						base.Fire(new Direction(90f, DirectionType.Relative, -1f), new Speed(startSpeed+4, SpeedType.Absolute), new SpeedChangingBullet("tellBullet", 9f, 60, -1, false));
						base.Fire(new Direction(-90f, DirectionType.Relative, -1f), new Speed(startSpeed, SpeedType.Absolute), new SpeedChangingBullet(endSpeed, 60, deathTimer));
						base.Fire(new Direction(90f, DirectionType.Relative, -1f), new Speed(startSpeed, SpeedType.Absolute), new SpeedChangingBullet(endSpeed, 60, deathTimer));
					}
					if (i % 1 == 1)
					{
						for (int A = -1; A < 1; A++)
                        {

							endSpeed = BraveUtility.RandomSign() * UnityEngine.Random.Range(1f, 2f);
							deathTimer = UnityEngine.Random.Range(10, 60);

							base.Fire(new Direction(90f, DirectionType.Aim, -1f), new Speed(9f+(A*2), SpeedType.Absolute), new SpeedChangingBullet(endSpeed, 60, 999));
						}
						base.Fire(new Direction(90f, DirectionType.Aim, -1f), new Speed(0f, SpeedType.Absolute), new SpeedChangingBullet(endSpeed, 60, 999));
					}
					i++;
					yield return base.Wait(4);
				}
			}

		}
	}
}

namespace BunnyMod
{
	public class JammedGat : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "9b4fb8a2a60a457f90dcf285d34143ac";

		public override void DoOverride()
		{

			TransformBehavior mine = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as TransformBehavior;
			mine.transformedBulletScript = new CustomBulletScriptSelector(typeof(JammedJammedGat));
			//ToolsEnemy.DebugInformation(behaviorSpec);


		}
		public class JammedJammedGat : Script
		{
			protected override IEnumerator Top()
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("41ee1c8538e8474a82a74c4aff99c712").bulletBank.GetBullet("big"));
				}
				AkSoundEngine.PostEvent("Play_BOSS_RatMech_Cannon_01", base.BulletBank.gameObject);
				this.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(1f, SpeedType.Absolute), new JammedJammedGat.Superball());
				yield return base.Wait(20);
				//AkSoundEngine.PostEvent("Play_ENM_ironmaiden_stomp_01", base.BulletBank.gameObject);
				//this.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(5f, SpeedType.Absolute), new JammedJammedGat.Superball());
				//yield return base.Wait(20);
				//AkSoundEngine.PostEvent("Play_ENM_ironmaiden_stomp_01", base.BulletBank.gameObject);
				//this.Fire(new Direction(-60f, DirectionType.Aim, -1f), new Speed(5f, SpeedType.Absolute), new JammedJammedGat.Superball());

				yield break;
			}
			public class Flames : Bullet
			{
				public Flames() : base("spiral", false, false, false)
				{
	
				}
				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(12f, SpeedType.Absolute), 25);
					yield break;
				}
			}
			public class Superball : Bullet
			{
				public Superball() : base("big", false, false, false)
				{
				}
				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(22f, SpeedType.Absolute), 90);
					yield break;
				}
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
					{

					}
					if (!preventSpawningProjectiles)
					{
						float num = base.RandomAngle();
						float Amount = 24;
						float Angle = 360 / Amount;
						for (int i = 0; i < Amount; i++)
						{
							base.Fire(new Direction(num + Angle * (float)i, DirectionType.Absolute, -1f), new Speed(12f, SpeedType.Absolute), null);
						}
					}
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedAmmoconadball : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "f38686671d524feda75261e469f30e0b";

		public override void DoOverride()
		{
			ShootBehavior shootGunBehavior = behaviorSpec.AttackBehaviors[0] as ShootBehavior;
			shootGunBehavior.LeadAmount = 0.70f;
			//shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedAmmoconadballAtatck));
			//ToolsEnemy.DebugInformation(behaviorSpec);


		}
		public class JammedAmmoconadballAtatck : Script
		{
			protected override IEnumerator Top()
			{

				for (int i = -1; i < 2; i++)
				{
					this.Fire(new Direction(0f + (12 * i), DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), new JammedAmmoconadballAtatck.SpikeBullet(90 + (3 - i) * 30));
				};

				yield break;
			}
			private const int NumWaves = 3;

			// Token: 0x0400082B RID: 2091
			private const int NumBullets = 12;

			// Token: 0x0200021F RID: 543
			public class SpikeBullet : Bullet
			{
				// Token: 0x06000826 RID: 2086 RVA: 0x00027920 File Offset: 0x00025B20
				public SpikeBullet(int fireTick) : base(null, false, false, false)
				{
				}

				// Token: 0x06000827 RID: 2087 RVA: 0x00027934 File Offset: 0x00025B34
				protected override IEnumerator Top()
				{
					yield return base.Wait(20);
					for (int i = -1; i < 2; i++)
					{
						//base.PostWwiseEvent("Play_WPN_thompson_shot_01", null);
						this.Fire(new Direction(0f + (40 * i), DirectionType.Relative, -1f), new Speed(3f, SpeedType.Absolute), new JammedAmmoconadballAtatck.SpeedBullet());
					}
					yield break;
				}

				// Token: 0x06000828 RID: 2088 RVA: 0x00027950 File Offset: 0x00025B50

				// Token: 0x06000829 RID: 2089 RVA: 0x000279D0 File Offset: 0x00025BD0
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					this.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(3f, SpeedType.Absolute), new JammedAmmoconadballAtatck.SpeedBullet());
					base.Vanish(false);
				}

			}
			public class SpeedBullet : Bullet
			{
				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(18f, SpeedType.Absolute), 40);
					yield break;
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedBlizzbulon : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "022d7c822bc146b58fe3b0287568aaa2";

		public override void DoOverride()
		{
			ShootBehavior shootGunBehavior1 = behaviorSpec.AttackBehaviors[0] as ShootBehavior;
			//shootGunBehavior.LeadAmount = 0.70f;
			//shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior1.BulletScript = new CustomBulletScriptSelector(typeof(JammedBlizzbulonAtatck1));
			ShootBehavior shootGunBehavior2 = behaviorSpec.AttackBehaviors[1] as ShootBehavior;
			shootGunBehavior2.BulletScript = new CustomBulletScriptSelector(typeof(JammedBlizzbulonAtatck2));

			//ToolsEnemy.DebugInformation(behaviorSpec);


		}
		public class JammedBlizzbulonAtatck1 : Script
		{
			protected override IEnumerator Top()
			{

				for (int i = 0; i < 18; i++)
				{
					this.Fire(new Direction(0f + (20 * i), DirectionType.Absolute, -1f), new Speed(9f, SpeedType.Absolute), null);
				};
				//yield return base.Wait(10);

				for (int i = 0; i < 4; i++)
				{
					for (int a = 0; a < 5; a++)
					{
						this.Fire(new Direction(0f + (90 * i), DirectionType.Aim, -1f), new Speed(9+((a*1.25f)), SpeedType.Absolute), null);
					}
				};
				yield break;
			}
			private const int NumWaves = 3;

			// Token: 0x0400082B RID: 2091
			private const int NumBullets = 12;

			// Token: 0x0200021F RID: 543
			
			public class SpeedBullet : Bullet
			{
				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(18f, SpeedType.Absolute), 40);
					yield break;
				}
			}
		}
		public class JammedBlizzbulonAtatck2 : Script
		{
			protected override IEnumerator Top()
			{
				int speedandspread = UnityEngine.Random.Range(4, 8);
				for (int i = -2; i < 3; i++)
				{
					this.Fire(new Direction(0+(speedandspread * i), DirectionType.Aim, -1f), new Speed(speedandspread - (float)Mathf.Abs(i) * 1f, SpeedType.Absolute), new JammedBlizzbulonAtatck2.SpeedBullet());
				}
				yield return base.Wait(50);
				int speedandspread2 = UnityEngine.Random.Range(12, 18);
				for (int i = -4; i < 5; i++)
				{
					this.Fire(new Direction(0 + ((speedandspread2/2) * i), DirectionType.Aim, -1f), new Speed(speedandspread2 - (float)Mathf.Abs(i) * 0.75f, SpeedType.Absolute), null);
				}
				yield break;
			}
			private const int NumWaves = 3;

			// Token: 0x0400082B RID: 2091
			private const int NumBullets = 12;

			// Token: 0x0200021F RID: 543

			public class SpeedBullet : Bullet
			{
				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(12.5f, SpeedType.Absolute), 20);
					yield break;
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedGunCultist : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "57255ed50ee24794b7aac1ac3cfb8a95"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.62f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedGunCultistAttack)); // Sets the bullet kin's bullet script to our custom bullet script.
			//ToolsEnemy.DebugInformation(behaviorSpec);
		}
		public class JammedGunCultistAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					//base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("37340393f97f41b2822bc02d14654172").bulletBank.GetBullet("quickHoming"));
				}
				int shots = UnityEngine.Random.Range(2, 5);
				for (int a = 0; a < shots; a++)
                {
					base.PostWwiseEvent("Play_WPN_magnum_shot_01", null);
					int angle = UnityEngine.Random.Range(-10, 10);
					for (int i = -1; i < 2; i++)
					{
						this.Fire(new Direction(0 + (8 * i)+ angle, DirectionType.Aim, -1f), new Speed(12 - (float)Mathf.Abs(i) * 0.5f, SpeedType.Absolute), new JammedGunCultistAttack.SpeedBullet());
					}
					yield return this.Wait(60/shots);

				}
				yield break;
			}
			public class SpeedBullet : Bullet
			{
				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(3f, SpeedType.Absolute), 30);
					yield return base.Wait(40);
					base.ChangeSpeed(new Speed(19f, SpeedType.Absolute), 15);
					//yield return base.Wait(40);
				}
			}
		}
	}
}

namespace BunnyMod
{
	public class JammedGummySpent : OverrideBehavior
	{
		public override string OverrideAIActorGUID => "e21ac9492110493baef6df02a2682a0d";

		public override void DoOverride()
		{
			ShootBehavior shootGunBehavior1 = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootBehavior;
			//shootGunBehavior.LeadAmount = 0.70f;
			//shootGunBehavior.WeaponType = WeaponType.BulletScript;
			shootGunBehavior1.BulletScript = new CustomBulletScriptSelector(typeof(JammedGummySpentAttack));
			//ShootBehavior shootGunBehavior2 = behaviorSpec.AttackBehaviors[1] as ShootBehavior;
			//shootGunBehavior2.BulletScript = new CustomBulletScriptSelector(typeof(JammedBlizzbulonAtatck2));
			//ToolsEnemy.DebugInformation(behaviorSpec);
		}
		public class JammedGummySpentAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top()
			{
				float num = base.RandomAngle();
				float num2 = 12f;
				for (int a = 0; a < 3; a++)
                {
					for (int i = 0; i < 20; i++)
					{
						base.Fire(new Direction(0 + (12 * i) +(3*a), DirectionType.Absolute, -1f), new Speed(5.5f, SpeedType.Absolute), new JammedGummySpentAttack.RotatingBullet(base.Position));
						base.Fire(new Direction(num + (float)i * num2 + (3 * a), DirectionType.Absolute, -1f), new Speed(6f, SpeedType.Absolute), new JammedGummySpentAttack.OscillatingBullet());
					}
					yield return this.Wait(7);
				}

				yield return null;
			}

			// Token: 0x04000D63 RID: 3427
			private const int NumBullets = 100;

			// Token: 0x02000336 RID: 822
			private class OscillatingBullet : Bullet
			{
				// Token: 0x06000CBD RID: 3261 RVA: 0x0003D46C File Offset: 0x0003B66C
				public OscillatingBullet() : base(null, false, false, false)
				{
				}

				// Token: 0x06000CBE RID: 3262 RVA: 0x0003D478 File Offset: 0x0003B678
				protected override IEnumerator Top()
				{
					float randomOffset = UnityEngine.Random.value;
					float startSpeed = this.Speed;
					for (int i = 0; i < 300; i++)
					{
						this.Speed = startSpeed + Mathf.SmoothStep(-2f, 2f, Mathf.PingPong((float)base.Tick / 60f + randomOffset, 1f));
						yield return base.Wait(1);
					}
					base.Vanish(false);
					yield break;
				}
			}
			public class RotatingBullet : Bullet
			{
				// Token: 0x06000AD4 RID: 2772 RVA: 0x00034000 File Offset: 0x00032200
				public RotatingBullet(Vector2 origin) : base(null, false, false, false)
				{
					this.m_origin = origin;
				}

				// Token: 0x06000AD5 RID: 2773 RVA: 0x00034014 File Offset: 0x00032214
				protected override IEnumerator Top()
				{
					int numure = UnityEngine.Random.Range(0, 2);
					bool fuckye = numure == 0;
					if (fuckye)
                    {
						Vector2 originToPos = base.Position - this.m_origin;
						float dist = originToPos.magnitude;
						float angle = originToPos.ToAngle();
						base.ManualControl = true;
						for (int i = 0; i < 300; i++)
						{
							angle -= 0.9f;
							dist += this.Speed / 60f;
							base.Position = this.m_origin + BraveMathCollege.DegreesToVector(angle, dist);
							yield return base.Wait(1);
						}
						base.Vanish(false);
						yield break;
					}
					bool a = numure == 1;
					if (a)
					{
						Vector2 originToPos = base.Position - this.m_origin;
						float dist = originToPos.magnitude;
						float angle = originToPos.ToAngle();
						base.ManualControl = true;
						for (int i = 0; i < 300; i++)
						{
							angle -= -0.9f;
							dist += this.Speed / 60f;
							base.Position = this.m_origin + BraveMathCollege.DegreesToVector(angle, dist);
							yield return base.Wait(1);
						}
						base.Vanish(false);
						yield break;
					}
					
				}

				// Token: 0x04000B5E RID: 2910
				private Vector2 m_origin;
			}
			
		}
	}
}

namespace BunnyMod
{
	public class JammedGummy: OverrideBehavior
	{
		public override string OverrideAIActorGUID => "5288e86d20184fa69c91ceb642d31474"; // Replace the GUID with whatever enemy you want to modify. This GUID is for the bullet kin.
																						  // You can find a full list of GUIDs at https://github.com/ModTheGungeon/ETGMod/blob/master/Assembly-CSharp.Base.mm/Content/gungeon_id_map/enemies.txt
		public override void DoOverride()
		{

			ShootGunBehavior shootGunBehavior = behaviorSpec.AttackBehaviorGroup.AttackBehaviors[0].Behavior as ShootGunBehavior; // Get the ShootGunBehavior, at index 0 of the AttackBehaviors list
			shootGunBehavior.LeadAmount = 0.62f;

			shootGunBehavior.WeaponType = WeaponType.BulletScript; // Makes it so the bullet kin will shoot our bullet script instead of his own gun shot.
			shootGunBehavior.BulletScript = new CustomBulletScriptSelector(typeof(JammedGummyAttack)); // Sets the bullet kin's bullet script to our custom bullet script.
																											//ToolsEnemy.DebugInformation(behaviorSpec);
		}
		public class JammedGummyAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					//base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("37340393f97f41b2822bc02d14654172").bulletBank.GetBullet("quickHoming"));
				}
				int shots1 = UnityEngine.Random.Range(3, 7);
				for (int e = 0; e < shots1; e++)
				{
					//PlayerController player = (GameManager.Instance.PrimaryPlayer);
					//float num = 0f;
					//num = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
					int accuracy = UnityEngine.Random.Range(-8, 8);
					base.PostWwiseEvent("Play_WPN_uzi_shot_01", null);
					base.Fire(new Direction(accuracy, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), null);
					yield return base.Wait(10/shots1);
				}
				yield break;
			}
		
		}
	}
}

//LJ Attacks
namespace BunnyMod
{
	public class LOTJScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
	{
		protected override IEnumerator Top()
		{

			for (int i = 0; i < 6; i++)
			{
				base.PostWwiseEvent("Play_ENM_gunknight_shockwave_01", null);
				this.Fire(new Direction(180f, DirectionType.Absolute, -1f), new Speed(6f + i, SpeedType.Relative), new LOTJScript.Break());
				this.Fire(new Direction(0f, DirectionType.Absolute, -1f), new Speed(6f + i, SpeedType.Absolute), new LOTJScript.Break());
				this.Fire(new Direction(90f, DirectionType.Absolute, -1f), new Speed(6f + i, SpeedType.Absolute), new LOTJScript.Break());
				this.Fire(new Direction(270f, DirectionType.Absolute, -1f), new Speed(6f + i, SpeedType.Absolute), new LOTJScript.Break());
			}
			for (int i = 0; i < 30; i++)//i dont need any for scripts
			{
				this.Fire(new Direction(12f * i, DirectionType.Absolute, -1f), new Speed(12f, SpeedType.Relative), new LOTJScript.Break());
			}
			for (int i = 0; i < 4; i++)//i dont need any for scripts
			{
				this.Fire(new Direction(90f * i, DirectionType.Absolute, -1f), new Speed(3f, SpeedType.Relative), new LOTJScript.Break1());
			}
			this.Fire(new Direction(0, DirectionType.Absolute, -1f), new Speed(0f, SpeedType.Relative), new LOTJScript.Break1());
			yield return this.Wait(600);

			yield break;
		}

		// Token: 0x020001E2 RID: 482
		public class Break : Bullet
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				for (int i = 0; i < 70; i++)
				{
					base.ChangeSpeed(new Speed(Speed * 0.97f, SpeedType.Absolute), 0);
					yield return base.Wait(1.25f);
				}
				base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
				yield return base.Wait(40);
				for (int i = 0; i < 20; i++)
				{
					base.ChangeSpeed(new Speed(1f * i, SpeedType.Absolute), 0);
					yield return base.Wait(3);
				}

				yield break;
			}

		}
		public class Break1 : Bullet
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				for (int i = 0; i < 50; i++)
                {
					base.ChangeSpeed(new Speed(Speed * 0.9f, SpeedType.Absolute), 0);
					yield return base.Wait(2);
				}
				base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
				for (int i = 0; i < 3; i++)
				{
					base.PostWwiseEvent("Play_WPN_sniperrifle_shot_01", null);
					for (int A = 0; A < 7; A++)
					{
						this.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(7f + A, SpeedType.Relative), null);
					}
					yield return base.Wait(50);
				}
				base.Vanish(false);
				yield break;
			}

		}
		public class Dagger : Bullet
		{
			public Dagger(bool isBlackPhantom) : base(null, false, false, false)
			{
				base.ForceBlackBullet = true;
				this.m_isBlackPhantom = isBlackPhantom;
			}
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				for (int i = 0; i < 15; i++)
				{
					base.ChangeSpeed(new Speed((1f * i), SpeedType.Absolute), 0);
					yield return base.Wait(4);
				}
				yield break;
			}
			private bool m_isBlackPhantom;
		}
	}
}

namespace BunnyMod
{
	public class LOTJScript1 : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
	{
		protected override IEnumerator Top()
		{
			base.PostWwiseEvent("Play_ENM_kali_shockwave_01", null);
			this.Fire(new Direction(0, DirectionType.Absolute, -1f), new Speed(0f, SpeedType.Relative), new LOTJScript1.Break1());
			yield return this.Wait(600);

			yield break;
		}
		public class Break1 : Bullet
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				float startAngle = base.RandomAngle();
				float delta = 10f;
				yield return base.Wait(10);
				for (int A = 0; A < 36; A++)
				{
					float num = startAngle + (float)A * delta;
					this.Fire(new Direction(num, DirectionType.Absolute, -1f), new Speed(5.5f, SpeedType.Relative), new LOTJScript1.Break(base.Position, num));
					//yield return this.Wait(5);
				}
				yield return this.Wait(20);
				for (int A = 0; A < 36; A++)
				{
					float num = startAngle + (float)A * delta;
					this.Fire(new Direction(num + 9, DirectionType.Absolute, -1f), new Speed(6.5f, SpeedType.Relative), new LOTJScript1.Speen(base.Position, num));
					//yield return this.Wait(5);
				}
				yield return this.Wait(20);
				for (int A = 0; A < 36; A++)
				{
					float num = startAngle + (float)A * delta;
					this.Fire(new Direction(num, DirectionType.Absolute, -1f), new Speed(7.5f, SpeedType.Relative), new LOTJScript1.Break(base.Position, num));
					//yield return this.Wait(5);
				}
				yield return this.Wait(20);
				for (int A = 0; A < 36; A++)
				{
					float num = startAngle + (float)A * delta;
					this.Fire(new Direction(num + 9, DirectionType.Absolute, -1f), new Speed(8.5f, SpeedType.Relative), new LOTJScript1.Speen(base.Position, num));
					//yield return this.Wait(5);
				}
				yield return this.Wait(20);
				base.Vanish(false);
				yield break;
			}

		}
		public class Break : Bullet
		{
			public Break(Vector2 centerPoint, float startAngle)
			{
				this.centerPoint = centerPoint;
				this.startAngle = startAngle;
			}
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				for (int E = 0; E < 100; E++)
                {
					for (int i = 0; i < 100; i++)
					{
						base.ManualControl = true;
						float radius = Vector2.Distance(this.centerPoint, base.Position);
						float speed = this.Speed;
						float spinAngle = this.startAngle;
						float spinSpeed = 0f;
						for (int e = 0; e < 15; e++)
						{
							speed += 0.12f;
							radius += speed / 60f;
							spinSpeed += 8f;
							spinAngle += spinSpeed / 60f;
							base.Position = this.centerPoint + BraveMathCollege.DegreesToVector(spinAngle, radius);
							yield return base.Wait(1);
						}
						base.ChangeSpeed(new Speed(Speed * 0.95f, SpeedType.Absolute), 0);
						yield return base.Wait(1.5f);
					}
				}
				yield break;
			}
			public Vector2 centerPoint;

			public float startAngle;
		}
		public class Speen : Bullet
		{
			public Speen(Vector2 centerPoint, float startAngle)
			{
				this.centerPoint = centerPoint;
				this.startAngle = startAngle;
			}
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				for (int E = 0; E < 100; E++)
				{
					for (int i = 0; i < 100; i++)
					{
						base.ManualControl = true;
						float radius = Vector2.Distance(this.centerPoint, base.Position);
						float speed = this.Speed;
						float spinAngle = this.startAngle;
						float spinSpeed = 0f;
						for (int e = 0; e < 15; e++)
						{
							speed += 0.12f;
							radius += speed / 60f;
							spinSpeed -= 8f;
							spinAngle += spinSpeed / 60f;
							base.Position = this.centerPoint + BraveMathCollege.DegreesToVector(spinAngle, radius);
							yield return base.Wait(1);
						}
						base.ChangeSpeed(new Speed(Speed * 0.95f, SpeedType.Absolute), 0);
						yield return base.Wait(1.5f);
					}
				}
				yield break;
			}
			public Vector2 centerPoint;

			public float startAngle;
		}

	}
}

namespace BunnyMod
{
	public class LOTJScript2 : Script 
	{
		protected override IEnumerator Top()
		{
			for (int A = 0; A < 3; A++)
            {
				base.PostWwiseEvent("Play_ENM_chain_shot_01", null);
				this.Fire(new Direction(60f + (A*4), DirectionType.Aim, -1f), new Speed(16f, SpeedType.Relative), new LOTJScript2.MakeChain());
				this.Fire(new Direction(180f + (A*4), DirectionType.Aim, -1f), new Speed(16f, SpeedType.Relative), new LOTJScript2.MakeChain());
				this.Fire(new Direction(300f + (A*4), DirectionType.Aim, -1f), new Speed(16f, SpeedType.Relative), new LOTJScript2.MakeChain());
				yield return this.Wait(2);
			}
			yield break;
		}
		public class MakeChain : Bullet
		{
			protected override IEnumerator Top()
			{
				for (int A = 0; A < 100; A++)
				{
					this.Fire(new Direction(0f, DirectionType.Relative, -1f), new Speed(0f, SpeedType.Absolute), new LOTJScript2.BreakChain());
					yield return this.Wait(2);
				}
				yield break;
			}

		}
		public class BreakChain : Bullet
		{
			protected override IEnumerator Top() 
			{
				yield return this.Wait(180);
				int numure = UnityEngine.Random.Range(0, 12);
				bool fuckye = numure == 0;
				if (fuckye)
                {
					base.PostWwiseEvent("Play_WPN_smallrocket_impact_01", null);
					this.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), null);
				}
				base.Vanish(false);
				yield break;
			}

		}

	}
}



/*
class CrimsonChamberEyeSpirals1 : Script
{
	protected override IEnumerator Top()
	{
		AkSoundEngine.PostEvent("Play_ENM_cannonball_eyes_01", base.BulletBank.gameObject);
		AkSoundEngine.PostEvent("Play_ENM_highpriest_gatling_01 ", base.BulletBank.gameObject);
		this.PostWwiseEvent("Play_ENM_cannonball_whips_01", null);
		for (int i = 0; i < 210; i++)
		{
			float angleVariance = 15f;
			base.Fire(new Direction(UnityEngine.Random.Range(-angleVariance, angleVariance), DirectionType.Aim, -1f), new SpiralBullet(BraveUtility.RandomBool(), 10f, 15));
			yield return base.Wait(2);
		}
		yield return null;
	}
public class SpiralBullet : Bullet
{
	public SpiralBullet(bool reverse, float speed, int term) : base("spiral", false, false, false)
	{
		this.reverse = reverse;
		this.speed = speed;
		this.term = term;
	}

	protected override IEnumerator Top()
	{
		base.ChangeSpeed(new Speed(reverse ? 0f : this.speed, SpeedType.Absolute), 0);
		for (; ; )
		{
			base.ChangeSpeed(new Speed(reverse ? this.speed : 0f, SpeedType.Absolute), this.term);
			yield return base.Wait(this.term);
			base.ChangeSpeed(new Speed(reverse ? 0f : this.speed, SpeedType.Absolute), this.term);
			yield return base.Wait(this.term);
		}
	}

	private bool reverse;
	private float speed;
	private int term;
}
*/

