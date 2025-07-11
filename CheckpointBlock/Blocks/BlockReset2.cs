namespace CheckpointBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockReset2 : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeReset2 = new Color(4, 238, 124);

        public BlockReset2(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeReset2;
    }
}
