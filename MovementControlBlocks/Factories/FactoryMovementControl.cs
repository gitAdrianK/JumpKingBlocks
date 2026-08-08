namespace MovementControl.Factories
{
    using System;
    using System.Collections.Generic;
    using Blocks;
    using JumpKing.API;
    using JumpKing.Level;
    using JumpKing.Level.Sampler;
    using JumpKing.Workshop;
    using Microsoft.Xna.Framework;

    public class FactoryMovementControl : IBlockFactory
    {
        public enum ModBlocks
        {
            GeneralModUsage = 0,
            ForcedNeutral,
            InvertInput,
            MomentumStop,
            MomentumStopScreen,
            SubpixelRound,
            NoSlowdown,
        }

        private static readonly HashSet<Color> SupportedBlockCodes = new HashSet<Color>
        {
            BlockMomentumStop.BlockcodeMomStop,
            BlockMomentumStopSolid.BlockcodeMomStopSolid,
            BlockMomentumStopScreen.BlockcodeMomStopScreen,
            BlockMomentumStopScreenSolid.BlockcodeMomStopScreenSolid,
            BlockSubpixelRound.BlockcodeSubpixelRound,
            BlockInvertInput.BlockcodeInvertInput,
            BlockForcedNeutral.BlockcodeForcedNeutral,
            BlockNoSlowdown.BlockcodeNoSlowdown,
        };

        static FactoryMovementControl()
        {
            LastUsedMapIds = new ulong[Enum.GetValues(typeof(ModBlocks)).Length];
            for (var i = 0; i < LastUsedMapIds.Length; i++)
            {
                LastUsedMapIds[i] = ulong.MaxValue;
            }
        }

        public static ulong[] LastUsedMapIds { get; }

        public bool CanMakeBlock(Color blockCode, Level level) => SupportedBlockCodes.Contains(blockCode);

        public bool IsSolidBlock(Color blockCode)
            => blockCode == BlockMomentumStopSolid.BlockcodeMomStopSolid
               || blockCode == BlockMomentumStopScreenSolid.BlockcodeMomStopScreenSolid;

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, Level level, LevelTexture textureSrc,
            int currentScreen, int x, int y)
        {
            LastUsedMapIds[(int)ModBlocks.GeneralModUsage] = level.ID;

            switch (blockCode)
            {
                case var _ when blockCode == BlockMomentumStop.BlockcodeMomStop:
                    LastUsedMapIds[(int)ModBlocks.MomentumStop] = level.ID;
                    return new BlockMomentumStop(blockRect);
                case var _ when blockCode == BlockMomentumStopSolid.BlockcodeMomStopSolid:
                    LastUsedMapIds[(int)ModBlocks.MomentumStop] = level.ID;
                    return new BlockMomentumStopSolid(blockRect);
                case var _ when blockCode == BlockMomentumStopScreen.BlockcodeMomStopScreen:
                    LastUsedMapIds[(int)ModBlocks.MomentumStopScreen] = level.ID;
                    return new BlockMomentumStopScreen(blockRect);
                case var _ when blockCode == BlockMomentumStopScreenSolid.BlockcodeMomStopScreenSolid:
                    LastUsedMapIds[(int)ModBlocks.MomentumStopScreen] = level.ID;
                    return new BlockMomentumStopScreenSolid(blockRect);
                case var _ when blockCode == BlockSubpixelRound.BlockcodeSubpixelRound:
                    LastUsedMapIds[(int)ModBlocks.SubpixelRound] = level.ID;
                    return new BlockSubpixelRound(blockRect);
                case var _ when blockCode == BlockInvertInput.BlockcodeInvertInput:
                    LastUsedMapIds[(int)ModBlocks.InvertInput] = level.ID;
                    return new BlockInvertInput(blockRect);
                case var _ when blockCode == BlockForcedNeutral.BlockcodeForcedNeutral:
                    LastUsedMapIds[(int)ModBlocks.ForcedNeutral] = level.ID;
                    return new BlockForcedNeutral(blockRect);
                case var _ when blockCode == BlockNoSlowdown.BlockcodeNoSlowdown:
                    LastUsedMapIds[(int)ModBlocks.NoSlowdown] = level.ID;
                    return new BlockNoSlowdown(blockRect);
                default:
                    throw new InvalidOperationException(
                        $"{nameof(FactoryMovementControl)} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
