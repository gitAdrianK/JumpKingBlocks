namespace CheckpointBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockCheckpoint2 : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeCheckpoint2 = new Color(3, 238, 124);

        public BlockCheckpoint2(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeCheckpoint2;
    }
}
