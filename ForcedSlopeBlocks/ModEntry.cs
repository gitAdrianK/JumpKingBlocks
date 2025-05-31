namespace ForcedSlopeBlocks
{
    using ForcedSlopeBlocks.Factories;
    using JumpKing.Level;
    using JumpKing.Mods;

    [JumpKingMod("Zebra.ForcedSlopesBlock")]
    public static class ModEntry
    {
        /// <summary>
        /// Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        public static void BeforeLevelLoad() => LevelManager.RegisterBlockFactory(new FactoryForcedSlope());
    }
}
