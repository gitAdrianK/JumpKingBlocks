namespace MomentumStopBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockMomentumStopSolid : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeMomStopSolid = new Color(111, 25, 101);

        public BlockMomentumStopSolid(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => true;

        public Color DebugColor => BlockcodeMomStopSolid;
    }
}
