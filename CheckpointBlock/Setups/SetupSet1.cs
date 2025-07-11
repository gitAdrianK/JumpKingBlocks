namespace CheckpointBlock.Setups
{
    using System;
    using System.IO;
    using System.Reflection;
    using Behaviours;
    using Blocks;
    using Data;
    using Entities;
    using Factories;
    using JumpKing;
    using JumpKing.Level;
    using JumpKing.Player;
    using JumpKing.Workshop;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class SetupSet1
    {
        private static EntityFlag EntityFlag { get; set; }

        public static void Setup(
            JKContentManager contentManager,
            Level level,
            PlayerEntity player,
            DataCheckpoint data,
            Point start)
        {
            if (level.ID != FactoryCheckpoint.LastUsedMapIdSet1)
            {
                return;
            }

            var customPath = Path.Combine(level.Root, "checkpoint");
            var checkpointTexture = contentManager.Load<Texture2D>(File.Exists(customPath + ".xnb")
                ? customPath
                : Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                    throw new InvalidOperationException(), "checkpoint"));

            EntityFlag = new EntityFlag(checkpointTexture, start) { FlagPosition = data.Set1.Current };
            _ = player.m_body.RegisterBlockBehaviour(
                typeof(BlockReset), new BehaviourReset(LevelManager.Instance, data.Set1, start));
            _ = player.m_body.RegisterBlockBehaviour(
                typeof(BlockCheckpoint), new BehaviourCheckpoint(data.Set1, EntityFlag));
            _ = player.m_body.RegisterBlockBehaviour(
                typeof(BlockCheckpointSingleUse), new BehaviourCheckpointSingleUse(data.Set1, EntityFlag));
        }
    }
}
