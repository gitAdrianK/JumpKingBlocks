namespace MuteJumpSfxBlock
{
    using System.Reflection;
    using Behaviours;
    using Blocks;
    using EntityComponent;
    using Factories;
    using HarmonyLib;
    using JetBrains.Annotations;
    using JumpKing;
    using JumpKing.Level;
    using JumpKing.Mods;
    using JumpKing.Player;
#if DEBUG
    using System.Diagnostics;
#endif

    [JumpKingMod(Identifier)]
    public static class ModEntry
    {
        private const string Identifier = "Zebra.MuteJumpSfxBlock";
        private const string HarmonyIdentifier = Identifier + ".Harmony";

        [BeforeLevelLoad]
        [UsedImplicitly]
        public static void BeforeLevelLoad()
        {
#if DEBUG
         Debugger.Launch();
#endif
            var harmony = new Harmony(HarmonyIdentifier);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            LevelManager.RegisterBlockFactory(new FactoryMuteJumpSfx());
        }

        [OnLevelStart]
        [UsedImplicitly]
        public static void OnLevelStart()
        {
            var level = Game1.instance.contentManager.level;
            if (level == null
                || level.ID != FactoryMuteJumpSfx.LastUsedMapId)
            {
                return;
            }

            var entityManager = EntityManager.instance;
            var player = entityManager.Find<PlayerEntity>();

            if (player == null)
            {
                return;
            }

            _ = player.m_body.RegisterBlockBehaviour(typeof(BlockMuteJumpSfx), new BehaviourMuteJumpSfx());
        }
    }
}
