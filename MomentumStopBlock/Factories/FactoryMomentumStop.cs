namespace MomentumStopBlock.Factories
{
    using System;
    using System.Collections.Generic;
    using Blocks;
    using JumpKing.API;
    using JumpKing.Level;
    using JumpKing.Level.Sampler;
    using JumpKing.Workshop;
    using Microsoft.Xna.Framework;

    public class FactoryMomentumStop : IBlockFactory
    {
        private static readonly HashSet<Color> SupportedBlockCodes = new HashSet<Color>
        {
            BlockMomentumStop.BlockcodeMomStop,
            BlockMomentumStopSolid.BlockcodeMomStopSolid,
            BlockMomentumStopScreen.BlockcodeMomStopScreen,
            BlockMomentumStopScreenSolid.BlockcodeMomStopScreenSolid
        };

        public static ulong LastUsedMapId { get; private set; } = ulong.MaxValue;
        public static ulong LastUsedMapIdMomStop { get; private set; } = ulong.MaxValue;
        public static ulong LastUsedMapIdMomStopScreen { get; private set; } = ulong.MaxValue;

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
                case var _ when blockCode == BlockMomentumStop.BlockcodeMomStop:
                    LastUsedMapIdMomStop = level.ID;
                    return new BlockMomentumStop(blockRect);
                case var _ when blockCode == BlockMomentumStopSolid.BlockcodeMomStopSolid:
                    LastUsedMapIdMomStop = level.ID;
                    return new BlockMomentumStopSolid(blockRect);
                case var _ when blockCode == BlockMomentumStopScreen.BlockcodeMomStopScreen:
                    LastUsedMapIdMomStopScreen = level.ID;
                    return new BlockMomentumStopScreen(blockRect);
                case var _ when blockCode == BlockMomentumStopScreenSolid.BlockcodeMomStopScreenSolid:
                    LastUsedMapIdMomStopScreen = level.ID;
                    return new BlockMomentumStopScreenSolid(blockRect);
                default:
                    throw new InvalidOperationException(
                        $"{nameof(FactoryMomentumStop)} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
