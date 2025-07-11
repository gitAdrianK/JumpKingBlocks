namespace CheckpointBlock
{
    using System.Linq;
    using Data;
    using Entities;
    using EntityComponent;
    using Factories;
    using JetBrains.Annotations;
    using JumpKing;
    using JumpKing.Level;
    using JumpKing.Mods;
    using JumpKing.Player;
    using JumpKing.SaveThread;
    using Microsoft.Xna.Framework;
    using Setups;
#if DEBUG
    using System.Diagnostics;
#endif

    [JumpKingMod(Identifier)]
    public static class ModEntry
    {
        private const string Identifier = "Zebra.CheckpointBlock";

        private static DataCheckpoint Data { get; set; }
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
            var contentManager = Game1.instance.contentManager;
            var level = contentManager.level;
            if (level == null || level.ID != FactoryCheckpoint.LastUsedMapId)
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

            // This is the default start position.
            var start = new Point(231, 302);
            var startData = level.Info.About.StartData?.Position;
            if (startData.HasValue)
            {
                start = startData.Value.ToPoint();
            }

            Data = SaveManager.instance.IsNewGame ? new DataCheckpoint(start) : DataCheckpoint.TryDeserialize(start);

            SetupSet1.Setup(contentManager, level, player, Data, start);
            SetupSet2.Setup(contentManager, level, player, Data, start);

            var entities = entityManager.Entities
                .SkipWhile(entity => entity != player)
                .ToList();
            entities.ForEach(entity =>
            {
                if (!(entity is EntityFlag))
                {
                    entity.GoToFront();
                }
            });
        }

        [OnLevelEnd]
        [UsedImplicitly]
        public static void OnLevelEnd()
        {
            var level = Game1.instance.contentManager.level;
            if (level == null || level.ID != FactoryCheckpoint.LastUsedMapId)
            {
                return;
            }

            Data.SaveToFile();
            Data = null;
        }
    }
}
