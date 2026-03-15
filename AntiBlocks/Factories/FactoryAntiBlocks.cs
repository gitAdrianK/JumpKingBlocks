namespace AntiBlocks.Factories
{
    using System;
    using System.Collections.Generic;
    using Blocks;
    using JumpKing.API;
    using JumpKing.Level;
    using JumpKing.Level.Sampler;
    using JumpKing.Workshop;
    using Microsoft.Xna.Framework;

    public class FactoryAntiBlocks : IBlockFactory
    {
        private static readonly HashSet<Color> SupportedBlockCodes = new HashSet<Color>
        {
            BlockAntiSnake.BlockcodeAntiSnake,
            BlockAntiSplat.BlockcodeAntiSplat,
        };

        public static ulong LastUsedMapIdSnake { get; private set; } = ulong.MaxValue;

        public static ulong LastUsedMapIdSplat { get; private set; } = ulong.MaxValue;

        public bool CanMakeBlock(Color blockCode, Level level) => SupportedBlockCodes.Contains(blockCode);

        public bool IsSolidBlock(Color blockCode) => false;

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, Level level, LevelTexture textureSrc,
            int currentScreen, int x, int y)
        {
            switch (blockCode)
            {
                case var _ when blockCode == BlockAntiSnake.BlockcodeAntiSnake:
                    LastUsedMapIdSnake = level.ID;
                    return new BlockAntiSnake(blockRect);
                case var _ when blockCode == BlockAntiSplat.BlockcodeAntiSplat:
                    LastUsedMapIdSplat = level.ID;
                    return new BlockAntiSplat(blockRect);
                default:
                    throw new InvalidOperationException(
                        $"{nameof(FactoryAntiBlocks)} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
