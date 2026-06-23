namespace InvertInputBlock.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockInvertInput : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeInvertInput = new Color(123, 32, 1);

        public BlockInvertInput(Rectangle collider) : base(collider)
        {
        }

        protected override bool canBlockPlayer => false;
        public Color DebugColor => BlockcodeInvertInput;
    }
}
