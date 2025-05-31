namespace MomentumStopBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockMomentumStopSolid : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BLOCKCODE_MOM_STOP_SOLID = new Color(111, 25, 101);

        public BlockMomentumStopSolid(Rectangle collider) : base(collider) { }

        public Color DebugColor => BLOCKCODE_MOM_STOP_SOLID;

        protected override bool canBlockPlayer => true;
    }
}


