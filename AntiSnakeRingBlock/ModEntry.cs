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
    using Patches;
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
#if DEBUG
            _ = Debugger.Launch();
#endif
            var harmony = new Harmony(HarmonyIdentifier);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            _ = LevelManager.RegisterBlockFactory(new FactoryAntiSnake());
        }

        [OnLevelStart]
        [UsedImplicitly]
        public static void OnLevelStart()
        {
            var level = Game1.instance.contentManager.level;
            if (level == null || level.ID != FactoryAntiSnake.LastUsedMapId)
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

            PatchGameLoop.ShowAntiSnakeRingIcon = false;
            foreach (var tag in level.Info.Tags)
            {
                if (tag != "ShowAntiSnakeRingIcon")
                {
                    continue;
                }

                PatchGameLoop.ShowAntiSnakeRingIcon = true;
                break;
            }
        }
    }
}
