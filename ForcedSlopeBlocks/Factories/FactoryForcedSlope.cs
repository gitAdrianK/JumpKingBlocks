namespace ForcedSlopeBlocks.Factories
{
    using System;
    using System.Collections.Generic;
    using JumpKing.API;
    using JumpKing.Level;
    using JumpKing.Level.Sampler;
    using JumpKing.Workshop;
    using Microsoft.Xna.Framework;

    public class FactoryForcedSlope : IBlockFactory
    {
        private static readonly Color BlockcodeTopLeft = new Color(255, 1, 0);
        private static readonly Color BlockcodeTopRight = new Color(255, 0, 1);
        private static readonly Color BlockcodeBottomLeft = new Color(255, 2, 0);
        private static readonly Color BlockcodeBottomRight = new Color(255, 0, 2);

        private static readonly HashSet<Color> SupportedBlockCodes = new HashSet<Color>
        {
            BlockcodeTopLeft, BlockcodeTopRight, BlockcodeBottomLeft, BlockcodeBottomRight
        };

        bool IBlockFactory.CanMakeBlock(Color blockCode, Level level)
            => SupportedBlockCodes.Contains(blockCode);

        bool IBlockFactory.IsSolidBlock(Color blockCode)
        {
            switch (blockCode)
            {
                case var _ when blockCode == BlockcodeTopLeft:
                case var _ when blockCode == BlockcodeTopRight:
                case var _ when blockCode == BlockcodeBottomLeft:
                case var _ when blockCode == BlockcodeBottomRight:
                    return true;
            }

            return false;
        }

        IBlock IBlockFactory.GetBlock(Color blockCode, Rectangle blockRect, Level level, LevelTexture textureSrc,
            int currentScreen, int x, int y)
        {
            switch (blockCode)
            {
                case var _ when blockCode == BlockcodeTopLeft:
                    return new SlopeBlock(blockRect, SlopeType.TopLeft);
                case var _ when blockCode == BlockcodeTopRight:
                    return new SlopeBlock(blockRect, SlopeType.TopRight);
                case var _ when blockCode == BlockcodeBottomLeft:
                    return new SlopeBlock(blockRect, SlopeType.BottomLeft);
                case var _ when blockCode == BlockcodeBottomRight:
                    return new SlopeBlock(blockRect, SlopeType.BottomRight);
                default:
                    throw new InvalidOperationException(
                        $"{nameof(FactoryForcedSlope)} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
