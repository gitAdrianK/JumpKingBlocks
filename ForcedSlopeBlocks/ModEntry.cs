namespace ForcedSlopeBlocks
{
    using System.Reflection;
    using Factories;
    using HarmonyLib;
    using JetBrains.Annotations;
    using JumpKing;
    using JumpKing.Level;
    using JumpKing.Mods;
    using Patches;
#if DEBUG
    using System.Diagnostics;
#endif

    [JumpKingMod("Zebra.ForcedSlopesBlock")]
    public static class ModEntry
    {
        private const string ModName = "ForcedSlopeBlocks";
        private const string HarmonyName = ModName + ".Harmony";

        /// <summary>
        ///     Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        [UsedImplicitly]
        public static void BeforeLevelLoad()
        {
#if DEBUG
             _ = Debugger.Launch();
#endif
            LevelManager.RegisterBlockFactory(new FactoryForcedSlope());

            var harmony = new Harmony(HarmonyName);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnLevelStart]
        [UsedImplicitly]
        public static void OnLevelStart()
        {
            var tags = Game1.instance.contentManager.level?.Info.Tags;
            if (tags == null)
            {
                return;
            }

            foreach (var tag in tags)
            {
                if (tag != "FixMySlopes")
                {
                    continue;
                }

                PatchSlopeBlock.FixAllSlopes();
                break;
            }

            PatchSlopeBlock.BottomLeftSlopes.Clear();
        }
    }
}
