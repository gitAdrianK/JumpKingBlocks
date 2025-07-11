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

    // Yeah, it's a little copy-paste-ish. I know, I know.

    public static class SetupSet2
    {
        private static EntityFlag EntityFlag { get; set; }

        public static void Setup(
            JKContentManager contentManager,
            Level level,
            PlayerEntity player,
            DataCheckpoint data,
            Point start)
        {
            if (level.ID != FactoryCheckpoint.LastUsedMapIdSet2)
            {
                return;
            }

            var customPath = Path.Combine(level.Root, "checkpoint2");
            var checkpointTexture = contentManager.Load<Texture2D>(File.Exists(customPath + ".xnb")
                ? customPath
                : Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                    throw new InvalidOperationException(), "checkpoint2"));

            EntityFlag = new EntityFlag(checkpointTexture, start) { FlagPosition = data.Set2.Current };
            _ = player.m_body.RegisterBlockBehaviour(
                typeof(BlockReset2), new BehaviourReset2(LevelManager.Instance, data.Set2, start));
            _ = player.m_body.RegisterBlockBehaviour(
                typeof(BlockCheckpoint2), new BehaviourCheckpoint2(data.Set2, EntityFlag));
            _ = player.m_body.RegisterBlockBehaviour(
                typeof(BlockCheckpointSingleUse2), new BehaviourCheckpointSingleUse2(data.Set2, EntityFlag));
        }
    }
}
