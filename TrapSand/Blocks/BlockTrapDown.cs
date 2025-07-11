namespace TrapSand.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockTrapDown : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeTrapDown = new Color(255, 68, 68);

        public BlockTrapDown(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeTrapDown;
    }
}
