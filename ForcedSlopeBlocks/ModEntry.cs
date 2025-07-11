namespace ForcedSlopeBlocks
{
    using Factories;
    using JetBrains.Annotations;
    using JumpKing.Level;
    using JumpKing.Mods;
#if DEBUG
    using System.Diagnostics;
#endif

    [JumpKingMod("Zebra.ForcedSlopesBlock")]
    public static class ModEntry
    {
        /// <summary>
        ///     Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        [UsedImplicitly]
        public static void BeforeLevelLoad()
        {
#if DEBUG
         Debugger.Launch();
#endif
            LevelManager.RegisterBlockFactory(new FactoryForcedSlope());
        }
    }
}
