namespace InvertInputBlock.Factories
{
    using System;
    using Blocks;
    using JumpKing.API;
    using JumpKing.Level;
    using JumpKing.Level.Sampler;
    using JumpKing.Workshop;
    using Microsoft.Xna.Framework;

    public class FactoryInvertInput : IBlockFactory
    {
        public static ulong LastUsedMapId { get; private set; } = ulong.MaxValue;

        public bool CanMakeBlock(Color blockCode, Level level) => blockCode == BlockInvertInput.BlockcodeInvertInput;

        public bool IsSolidBlock(Color blockCode) => false;

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, Level level, LevelTexture textureSrc,
            int currentScreen, int x,
            int y)
        {
            switch (blockCode)
            {
                case var _ when blockCode == BlockInvertInput.BlockcodeInvertInput:
                    LastUsedMapId = level.ID;
                    return new BlockInvertInput(blockRect);
                default:
                    throw new InvalidOperationException(
                        $"{nameof(FactoryInvertInput)} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
