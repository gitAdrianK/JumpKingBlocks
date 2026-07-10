namespace MovementControl
{
    using System.Reflection;
    using Behaviours;
    using Blocks;
    using Data;
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
        private const string Identifier = "Zebra.MovementControlBlocks";
        private const string HarmonyIdentifier = Identifier + ".Harmony";

        public static DataMomentumStop Data { get; private set; }

        [BeforeLevelLoad]
        [UsedImplicitly]
        public static void BeforeLevelLoad()
        {
#if DEBUG
            _ = Debugger.Launch();
#endif
            LevelManager.RegisterBlockFactory(new FactoryMovementControl());

            var harmony = new Harmony(HarmonyIdentifier);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnLevelStart]
        [UsedImplicitly]
        public static void OnLevelStart()
        {
            var contentManager = Game1.instance.contentManager;
            var level = contentManager.level;
            if (level == null
                || level.ID != FactoryMovementControl.LastUsedMapId)
            {
                return;
            }

            var entityManager = EntityManager.instance;
            var player = entityManager.Find<PlayerEntity>();

            if (player == null)
            {
                return;
            }

            if (level.ID == FactoryMovementControl.LastUsedMapIdMomStop)
            {
                _ = player.m_body.RegisterBlockBehaviour(
                    typeof(BlockMomentumStop),
                    new BehaviourMomentumStop());
            }

            if (level.ID == FactoryMovementControl.LastUsedMapIdMomStopScreen)
            {
                Data = DataMomentumStop.TryDeserialize();
                _ = player.m_body.RegisterBlockBehaviour(
                    typeof(BlockMomentumStopScreen),
                    new BehaviourMomentumStopScreen(Data));
            }

            if (level.ID == FactoryMovementControl.LastUsedMapIdSubpixelRound)
            {
                _ = player.m_body.RegisterBlockBehaviour(typeof(BlockSubpixelRound),
                    new BehaviourSubpixelRound());
            }

            if (level.ID == FactoryMovementControl.LastUsedMapIdInvertInput)
            {
                _ = player.m_body.RegisterBlockBehaviour(typeof(BlockInvertInput),
                    new BehaviourInvertInput());
            }
        }

        [OnLevelEnd]
        [UsedImplicitly]
        public static void OnLevelEnd()
        {
            var level = Game1.instance.contentManager.level;
            if (level == null)
            {
                return;
            }

            if (FactoryMovementControl.LastUsedMapIdMomStopScreen == level.ID)
            {
                Data.SaveToFile();
            }
        }
    }
}
