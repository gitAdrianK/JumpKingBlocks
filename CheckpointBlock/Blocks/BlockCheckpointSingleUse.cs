namespace CheckpointBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockCheckpointSingleUse : BoxBlock, IBlockDebugColor
    {
        public static readonly Color ColorCheckpointSingleUse = new Color(5, 238, 124);

        public BlockCheckpointSingleUse(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => ColorCheckpointSingleUse;
    }
}
