namespace MomentumStopBlock
{
    using CheckpointBlock.Data;
    using EntityComponent;
    using JumpKing;
    using JumpKing.Level;
    using JumpKing.Mods;
    using JumpKing.Player;
    using MomentumStopBlock.Behaviours;
    using MomentumStopBlock.Blocks;
    using MomentumStopBlock.Factories;

    [JumpKingMod("Zebra.MomentumStopBlock")]
    public static class ModEntry
    {
        public static DataMomentumStop Data { get; private set; }

        [BeforeLevelLoad]
        public static void BeforeLevelLoad() => LevelManager.RegisterBlockFactory(new FactoryMomentumStop());

        [OnLevelStart]
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

            if (level.ID == FactoryMomentumStop.LastUsedMapIdMomStopScreen)
            {
                Data = DataMomentumStop.TryDeserialize();
                _ = player.m_body.RegisterBlockBehaviour(
                    typeof(BlockMomentumStopScreen),
                    new BehaviourMomentumStopScreen(Data));
            }

        }

        [OnLevelEnd]
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
