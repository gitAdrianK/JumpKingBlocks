namespace MomentumStopBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockMomentumStop : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BLOCKCODE_MOM_STOP = new Color(111, 24, 101);

        public BlockMomentumStop(Rectangle collider) : base(collider) { }

        public Color DebugColor => BLOCKCODE_MOM_STOP;

        protected override bool canBlockPlayer => false;
    }
}

