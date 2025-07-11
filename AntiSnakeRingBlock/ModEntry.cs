namespace AntiSnakeRingBlock
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
        private const string Identifier = "Zebra.AntiSnakeRingBlock";
        private const string HarmonyIdentifier = Identifier + ".Harmony";

        /// <summary>
        ///     Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        [UsedImplicitly]
        public static void BeforeLevelLoad()
        {
            var harmony = new Harmony(HarmonyIdentifier);
#if DEBUG
            Debugger.Launch();
#endif
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            _ = LevelManager.RegisterBlockFactory(new FactoryAntiSnake());
        }

        [OnLevelStart]
        [UsedImplicitly]
        public static void OnLevelStart()
        {
            var contentManager = Game1.instance.contentManager;
            if (contentManager.level == null)
            {
                return;
            }

            if (contentManager.level.ID != FactoryAntiSnake.LastUsedMapId)
            {
                return;
            }

            var entityManager = EntityManager.instance;
            var player = entityManager.Find<PlayerEntity>();

            if (player == null)
            {
                return;
            }

            _ = player.m_body.RegisterBlockBehaviour(typeof(BlockAntiSnake), new BehaviourAntiSnake());
        }
    }
}
