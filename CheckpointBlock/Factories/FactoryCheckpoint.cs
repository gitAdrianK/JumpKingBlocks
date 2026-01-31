namespace CheckpointBlock.Factories
{
    using System;
    using Blocks;
    using JumpKing.API;
    using JumpKing.Level;
    using JumpKing.Level.Sampler;
    using JumpKing.Workshop;
    using Microsoft.Xna.Framework;

    public class FactoryCheckpoint : IBlockFactory
    {
        private const int SetCount = ModEntry.SetCount;

        public static ulong[] LastUsedMapIds { get; } = new ulong[SetCount];

        public bool CanMakeBlock(Color blockCode, Level level)
        {
            if (blockCode.G != 238 && blockCode.G != 239)
            {
                return false;
            }

            if (blockCode.B != 124 && blockCode.B != 125)
            {
                return false;
            }

            return blockCode.R >= 1 && blockCode.R <= SetCount;
        }

        public bool IsSolidBlock(Color blockCode) => false;

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, Level level, LevelTexture textureSrc,
            int currentScreen, int x, int y)
        {
            var id = blockCode.R - 1;
            LastUsedMapIds[id] = level.ID;

            switch (blockCode.G)
            {
                case 238 when blockCode.B == 124:
                    return new BlockCheckpoint(blockRect, id);
                case 239 when blockCode.B == 124:
                    return new BlockReset(blockRect, id);
                case 238 when blockCode.B == 125:
                    return new BlockCheckpointSingleUse(blockRect, id);
                default:
                    throw new InvalidOperationException(
                        $"{nameof(FactoryCheckpoint)} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
