namespace AntiSnakeRingBlock
{
    using System.Reflection;
    using AntiSnakeRingBlock.Behaviours;
    using AntiSnakeRingBlock.Blocks;
    using AntiSnakeRingBlock.Factories;
    using EntityComponent;
    using HarmonyLib;
    using JumpKing;
    using JumpKing.Level;
    using JumpKing.Mods;
    using JumpKing.Player;

    [JumpKingMod(IDENTIFIER)]
    public static class ModEntry
    {
        private const string IDENTIFIER = "Zebra.AntiSnakeRingBlock";
        private const string HARMONY_IDENTIFIER = IDENTIFIER + ".Harmony";

        /// <summary>
        /// Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        public static void BeforeLevelLoad()
        {
            var harmony = new Harmony(HARMONY_IDENTIFIER);
#if DEBUG
            Debugger.Launch();
            Harmony.DEBUG = true;
#endif
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            _ = LevelManager.RegisterBlockFactory(new FactoryAntiSnake());
        }

        [OnLevelStart]
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
