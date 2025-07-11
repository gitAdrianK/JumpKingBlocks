namespace CheckpointBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockCheckpoint : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeCheckpoint = new Color(1, 238, 124);

        public BlockCheckpoint(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeCheckpoint;
    }
}
