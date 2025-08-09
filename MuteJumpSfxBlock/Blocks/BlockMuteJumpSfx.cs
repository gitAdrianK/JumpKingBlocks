 namespace MuteJumpSfxBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockMuteJumpSfx : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeMuteJumpSfx = new Color(100, 238, 124);

        public BlockMuteJumpSfx(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeMuteJumpSfx;
    }
}
