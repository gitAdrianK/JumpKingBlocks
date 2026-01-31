namespace CheckpointBlock
{
    using System.Linq;
    using Entities;
    using EntityComponent;
    using Factories;
    using JetBrains.Annotations;
    using JumpKing;
    using JumpKing.Level;
    using JumpKing.Mods;
    using JumpKing.Player;
    using Setups;
#if DEBUG
    using System.Diagnostics;
#endif

    [JumpKingMod(Identifier)]
    public static class ModEntry
    {
        public const int SetCount = 10;
        private const string Identifier = "Zebra.CheckpointBlock";
        public static bool IgnoreStart { get; private set; }

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
            LevelManager.RegisterBlockFactory(new FactoryCheckpoint());
        }

        /// <summary>
        ///     Called by Jump King when the Level Starts
        /// </summary>
        [OnLevelStart]
        [UsedImplicitly]
        public static void OnLevelStart()
        {
            var level = Game1.instance.contentManager.level;
            if (level == null || !FactoryCheckpoint.LastUsedMapIds.Contains(level.ID))
            {
                return;
            }

            var entityManager = EntityManager.instance;
            var player = entityManager.Find<PlayerEntity>();
            if (player == null)
            {
                return;
            }

            IgnoreStart = false;
            foreach (var tag in level.Info.Tags)
            {
                if (tag != "CheckpointsIgnoreStart")
                {
                    continue;
                }

                IgnoreStart = true;
                break;
            }

            SetupSets.Setup(level, player);

            var entities = entityManager.Entities
                .SkipWhile(entity => entity != player)
                .ToList();
            foreach (var entity in entities.Where(entity => !(entity is EntityFlag)))
            {
                entity.GoToFront();
            }
        }

        [OnLevelEnd]
        [UsedImplicitly]
        public static void OnLevelEnd()
        {
            var level = Game1.instance.contentManager.level;
            if (level == null || !FactoryCheckpoint.LastUsedMapIds.Contains(level.ID))
            {
                return;
            }

            SetupSets.Cleanup();
        }
    }
}
