namespace AntiSnakeRingBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockAntiSnake : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BLOCKCODE_ANTI_SNAKE = new Color(105, 111, 143);

        public BlockAntiSnake(Rectangle collider) : base(collider) { }

        public Color DebugColor => BLOCKCODE_ANTI_SNAKE;

        protected override bool canBlockPlayer => false;
    }
}

