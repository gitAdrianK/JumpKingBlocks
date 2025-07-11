namespace MomentumStopBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockMomentumStopScreen : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeMomStopScreen = new Color(111, 24, 102);

        public BlockMomentumStopScreen(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeMomStopScreen;
    }
}
