namespace AntiSnakeRingBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockAntiSnake : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeAntiSnake = new Color(105, 111, 143);

        public BlockAntiSnake(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeAntiSnake;
    }
}
