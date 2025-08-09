namespace MuteJumpSfxBlock.Factories
{
    using System;
    using System.Collections.Generic;
    using JumpKing.API;
    using JumpKing.Level;
    using JumpKing.Level.Sampler;
    using JumpKing.Workshop;
    using Microsoft.Xna.Framework;
    using Blocks;

    public class FactoryMuteJumpSfx : IBlockFactory
    {
        private static readonly HashSet<Color> SupportedBlockCodes = new HashSet<Color>
        {
            BlockMuteJumpSfx.BlockcodeMuteJumpSfx
        };

        public static ulong LastUsedMapId { get; private set; } = ulong.MaxValue;

        public bool CanMakeBlock(Color blockCode, Level level) => SupportedBlockCodes.Contains(blockCode);

        public bool IsSolidBlock(Color blockCode) => false;

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, Level level, LevelTexture textureSrc,
            int currentScreen, int x, int y)
        {
            if (LastUsedMapId != level.ID)
            {
                LastUsedMapId = level.ID;
            }

            switch (blockCode)
            {
                case var _ when blockCode == BlockMuteJumpSfx.BlockcodeMuteJumpSfx:
                    return new BlockMuteJumpSfx(blockRect);
                default:
                    throw new InvalidOperationException(
                        $"{nameof(FactoryMuteJumpSfx)} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
