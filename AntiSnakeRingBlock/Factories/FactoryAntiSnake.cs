namespace AntiSnakeRingBlock.Factories
{
    using System;
    using System.Collections.Generic;
    using AntiSnakeRingBlock.Blocks;
    using JumpKing.API;
    using JumpKing.Level;
    using JumpKing.Level.Sampler;
    using JumpKing.Workshop;
    using Microsoft.Xna.Framework;

    public class FactoryAntiSnake : IBlockFactory
    {
        public static ulong LastUsedMapId { get; private set; } = ulong.MaxValue;

        private static readonly HashSet<Color> SupportedBlockCodes = new HashSet<Color> {
            BlockAntiSnake.BLOCKCODE_ANTI_SNAKE,
        };

        public bool CanMakeBlock(Color blockCode, Level level) => SupportedBlockCodes.Contains(blockCode);

        public bool IsSolidBlock(Color blockCode) => false;

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, Level level, LevelTexture textureSrc, int currentScreen, int x, int y)
        {
            if (LastUsedMapId != level.ID && SupportedBlockCodes.Contains(blockCode))
            {
                LastUsedMapId = level.ID;
            }
            switch (blockCode)
            {
                case var _ when blockCode == BlockAntiSnake.BLOCKCODE_ANTI_SNAKE:
                    return new BlockAntiSnake(blockRect);
                default:
                    throw new InvalidOperationException($"{nameof(FactoryAntiSnake)} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
