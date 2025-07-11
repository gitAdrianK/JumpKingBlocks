namespace MomentumStopBlock
{
    using Behaviours;
    using Blocks;
    using Data;
    using EntityComponent;
    using Factories;
    using JetBrains.Annotations;
    using JumpKing;
    using JumpKing.Level;
    using JumpKing.Mods;
    using JumpKing.Player;
#if DEBUG
    using System.Diagnostics;
#endif

    [JumpKingMod("Zebra.MomentumStopBlock")]
    public static class ModEntry
    {
        public static DataMomentumStop Data { get; private set; }

        [BeforeLevelLoad]
        [UsedImplicitly]
        public static void BeforeLevelLoad()
        {
#if DEBUG
         Debugger.Launch();
#endif
            LevelManager.RegisterBlockFactory(new FactoryMomentumStop());
        }

        [OnLevelStart]
        [UsedImplicitly]
        public static void OnLevelStart()
        {
            var contentManager = Game1.instance.contentManager;
            var level = contentManager.level;
            if (level == null
                || level.ID != FactoryMomentumStop.LastUsedMapId)
            {
                return;
            }

            var entityManager = EntityManager.instance;
            var player = entityManager.Find<PlayerEntity>();

            if (player == null)
            {
                return;
            }

            if (level.ID == FactoryMomentumStop.LastUsedMapIdMomStop)
            {
                _ = player.m_body.RegisterBlockBehaviour(
                    typeof(BlockMomentumStop),
                    new BehaviourMomentumStop());
            }

            if (level.ID != FactoryMomentumStop.LastUsedMapIdMomStopScreen)
            {
                return;
            }

            Data = DataMomentumStop.TryDeserialize();
            _ = player.m_body.RegisterBlockBehaviour(
                typeof(BlockMomentumStopScreen),
                new BehaviourMomentumStopScreen(Data));
        }

        [OnLevelEnd]
        [UsedImplicitly]
        public static void OnLevelEnd()
        {
            var level = Game1.instance.contentManager.level;
            if (level == null
                || level.ID != FactoryMomentumStop.LastUsedMapId
                || level.ID != FactoryMomentumStop.LastUsedMapIdMomStopScreen)
            {
                return;
            }

            Data.SaveToFile();
        }
    }
}
