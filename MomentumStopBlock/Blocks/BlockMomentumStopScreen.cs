namespace MomentumStopBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockMomentumStopScreen : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BLOCKCODE_MOM_STOP_SCREEN = new Color(111, 24, 102);

        public BlockMomentumStopScreen(Rectangle collider) : base(collider) { }

        public Color DebugColor => BLOCKCODE_MOM_STOP_SCREEN;

        protected override bool canBlockPlayer => false;
    }
}

