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
    using Patches;
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
            var lastUsedMapIds = FactoryMovementControl.LastUsedMapIds;
            if (level == null
                || level.ID != lastUsedMapIds[(int)FactoryMovementControl.ModBlocks.GeneralModUsage])
            {
                return;
            }

            var entityManager = EntityManager.instance;
            var player = entityManager.Find<PlayerEntity>();

            if (player == null)
            {
                return;
            }

            var body = player.m_body;
            var id = level.ID;

            if (id == lastUsedMapIds[(int)FactoryMovementControl.ModBlocks.MomentumStop])
            {
                _ = body.RegisterBlockBehaviour(typeof(BlockMomentumStop), new BehaviourMomentumStop());
            }

            if (id == lastUsedMapIds[(int)FactoryMovementControl.ModBlocks.MomentumStopScreen])
            {
                Data = DataMomentumStop.TryDeserialize();
                _ = body.RegisterBlockBehaviour(typeof(BlockMomentumStopScreen), new BehaviourMomentumStopScreen(Data));
            }

            if (id == lastUsedMapIds[(int)FactoryMovementControl.ModBlocks.SubpixelRound])
            {
                _ = body.RegisterBlockBehaviour(typeof(BlockSubpixelRound), new BehaviourSubpixelRound());
            }

            if (id == lastUsedMapIds[(int)FactoryMovementControl.ModBlocks.InvertInput])
            {
                var behaviour = new BehaviourInvertInput();
                PatchPadInstance.BehaviourInvertInput = behaviour;
                _ = body.RegisterBlockBehaviour(typeof(BlockInvertInput), behaviour);
            }

            if (id == lastUsedMapIds[(int)FactoryMovementControl.ModBlocks.ForcedNeutral])
            {
                var behaviour = new BehaviourForcedNeutral();
                PatchJumpState.BehaviourForcedNeutral = behaviour;
                _ = body.RegisterBlockBehaviour(typeof(BlockForcedNeutral), behaviour);
            }

            // ReSharper disable once InvertIf
            if (id == lastUsedMapIds[(int)FactoryMovementControl.ModBlocks.NoSlowdown])
            {
                var behaviour = new BehaviourNoSlowdown();
                PatchJumpState.BehaviourNoBreaking = behaviour;
                _ = body.RegisterBlockBehaviour(typeof(BlockNoSlowdown), behaviour);
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

            PatchPadInstance.BehaviourInvertInput = null;
            PatchJumpState.BehaviourForcedNeutral = null;

            // ReSharper disable once InvertIf
            if (level.ID ==
                FactoryMovementControl.LastUsedMapIds[(int)FactoryMovementControl.ModBlocks.MomentumStopScreen])
            {
                Data.SaveToFile();
                Data = null;
            }
        }
    }
}
