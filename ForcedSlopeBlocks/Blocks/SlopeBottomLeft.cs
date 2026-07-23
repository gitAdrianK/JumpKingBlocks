namespace ForcedSlopeBlocks.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;
    using Patches;

    public class SlopeBottomLeft : SlopeBlock
    {
        public SlopeBottomLeft(Rectangle position) : base(position, SlopeType.BottomLeft) =>
            PatchSlopeBlock.FixSlopeHitbox(this);
    }
}
