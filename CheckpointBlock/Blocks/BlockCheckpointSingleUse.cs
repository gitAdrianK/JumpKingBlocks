namespace CheckpointBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockCheckpointSingleUse : BoxBlock, IBlockDebugColor
    {
        public BlockCheckpointSingleUse(Rectangle collider, int id) : base(collider) => this.Id = id;

        public int Id { get; }

        protected override bool canBlockPlayer => false;

        // R value will be from 1 to SetCount, check in the factory.
        public Color DebugColor => new Color(this.Id, 238, 125);
    }
}
