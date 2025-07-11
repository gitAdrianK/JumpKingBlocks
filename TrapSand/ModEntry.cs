namespace TrapSand
{
    using Behaviours;
    using Blocks;
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

    [JumpKingMod("Zebra.TrapSand")]
    public static class ModEntry
    {
        [BeforeLevelLoad]
        [UsedImplicitly]
        public static void BeforeLevelLoad()
        {
#if DEBUG
         Debugger.Launch();
#endif
            LevelManager.RegisterBlockFactory(new FactoryTrap());
        }

        [OnLevelStart]
        [UsedImplicitly]
        public static void OnLevelStart()
        {
            var level = Game1.instance.contentManager.level;
            if (level == null
                || level.ID != FactoryTrap.LastUsedMapId)
            {
                return;
            }

            var entityManager = EntityManager.instance;
            var player = entityManager.Find<PlayerEntity>();

            if (player == null)
            {
                return;
            }

            var muteSandUp = false;
            var muteSandDown = false;
            foreach (var tag in level.Info.Tags)
            {
                switch (tag)
                {
                    case "MuteTrapSandUp":
                        muteSandUp = true;
                        break;
                    case "MuteTrapSandDown":
                        muteSandDown = true;
                        break;
                }

                if (muteSandDown && muteSandUp)
                {
                    break;
                }
            }

            // Functions as a ICollisionQuery.
            var levelManager = LevelManager.Instance;

            if (level.ID == FactoryTrap.LastUsedMapIdDown)
            {
                _ = player.m_body.RegisterBlockBehaviour(
                    typeof(BlockTrapDown),
                    new BehaviourTrapDown(levelManager, muteSandDown));
            }

            if (level.ID == FactoryTrap.LastUsedMapIdUp)
            {
                _ = player.m_body.RegisterBlockBehaviour(
                    typeof(BlockTrapUp),
                    new BehaviourTrapUp(levelManager, muteSandUp));
            }
        }
    }
}
