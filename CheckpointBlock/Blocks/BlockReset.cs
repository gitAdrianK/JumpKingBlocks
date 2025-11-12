 namespace CheckpointBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockReset : BoxBlock, IBlockDebugColor
    {
        public static readonly Color ColorReset = new Color(2, 238, 124);

        public BlockReset(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => ColorReset;
    }
}
