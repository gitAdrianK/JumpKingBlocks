namespace ForcedSlopeBlocks
{
    using System.Reflection;
    using Factories;
    using HarmonyLib;
    using JetBrains.Annotations;
    using JumpKing.Level;
    using JumpKing.Mods;
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
    }
}
