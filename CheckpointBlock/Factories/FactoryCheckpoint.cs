namespace CheckpointBlock.Factories
{
    using System;
    using System.Collections.Generic;
    using Blocks;
    using JumpKing.API;
    using JumpKing.Level;
    using JumpKing.Level.Sampler;
    using JumpKing.Workshop;
    using Microsoft.Xna.Framework;

    public class FactoryCheckpoint : IBlockFactory
    {
        private static readonly HashSet<Color> SupportedBlockCodes = new HashSet<Color>
        {
            BlockCheckpoint.ColorCheckpoint,
            BlockCheckpoint2.ColorCheckpoint2,
            BlockReset.ColorReset,
            BlockReset2.ColorReset2,
            BlockCheckpointSingleUse.ColorCheckpointSingleUse,
            BlockCheckpointSingleUse2.ColorCheckpointSingleUse2
        };

        public static ulong LastUsedMapId { get; private set; } = ulong.MaxValue;
        public static ulong LastUsedMapIdSet1 { get; private set; } = ulong.MaxValue;
        public static ulong LastUsedMapIdSet2 { get; private set; } = ulong.MaxValue;

        public bool CanMakeBlock(Color blockCode, Level level) => SupportedBlockCodes.Contains(blockCode);

        public bool IsSolidBlock(Color blockCode) => false;

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, Level level, LevelTexture textureSrc,
            int currentScreen, int x, int y)
        {
            if (LastUsedMapId != level.ID && SupportedBlockCodes.Contains(blockCode))
            {
                LastUsedMapId = level.ID;
            }

            switch (blockCode)
            {
                case var _ when blockCode == BlockCheckpoint.ColorCheckpoint:
                    LastUsedMapIdSet1 = level.ID;
                    return new BlockCheckpoint(blockRect);
                case var _ when blockCode == BlockCheckpoint2.ColorCheckpoint2:
                    LastUsedMapIdSet2 = level.ID;
                    return new BlockCheckpoint2(blockRect);
                case var _ when blockCode == BlockReset.ColorReset:
                    LastUsedMapIdSet1 = level.ID;
                    return new BlockReset(blockRect);
                case var _ when blockCode == BlockReset2.ColorReset2:
                    LastUsedMapIdSet2 = level.ID;
                    return new BlockReset2(blockRect);
                case var _ when blockCode == BlockCheckpointSingleUse.ColorCheckpointSingleUse:
                    LastUsedMapIdSet1 = level.ID;
                    return new BlockCheckpointSingleUse(blockRect);
                case var _ when blockCode == BlockCheckpointSingleUse2.ColorCheckpointSingleUse2:
                    LastUsedMapIdSet2 = level.ID;
                    return new BlockCheckpointSingleUse2(blockRect);
                default:
                    throw new InvalidOperationException(
                        $"{nameof(FactoryCheckpoint)} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
