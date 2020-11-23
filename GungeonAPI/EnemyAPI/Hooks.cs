using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BunnyMod
{
    public static class HooksEnemy
    {
        public static void Init()
        {
            Hook customEnemyChangesHook = new Hook(
                typeof(AIActor).GetMethod("Update", BindingFlags.Instance | BindingFlags.Public),
                typeof(HooksEnemy).GetMethod("HandleCustomEnemyChanges")
            );
        }

        public static void HandleCustomEnemyChanges(Action<AIActor> orig, AIActor self)
        {

            orig(self);
            try
            {
                bool harderlotj = GungeonAPI.JammedSquire.NoHarderLotJ;
                if (harderlotj)
                {
                }
                else
                {
                    if (self.IsBlackPhantom)
                    {
                        var obehaviors = ToolsEnemy.overrideBehaviors.Where(ob => ob.OverrideAIActorGUID == self.EnemyGuid);
                        foreach (var obehavior in obehaviors)
                        {
                            obehavior.SetupOB(self);
                            if (obehavior.ShouldOverride())
                            {
                                obehavior.DoOverride();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ETGModConsole.Log(e.ToString());
            }
        }
    }
}
