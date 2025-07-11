namespace CheckpointBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockCheckpointSingleUse2 : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeCheckpointSingleUse2 = new Color(6, 238, 124);

        public BlockCheckpointSingleUse2(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeCheckpointSingleUse2;
    }
}
