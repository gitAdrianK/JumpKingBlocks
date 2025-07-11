namespace MomentumStopBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockMomentumStop : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeMomStop = new Color(111, 24, 101);

        public BlockMomentumStop(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeMomStop;
    }
}
