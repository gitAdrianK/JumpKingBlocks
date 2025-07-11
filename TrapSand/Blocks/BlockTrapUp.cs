namespace TrapSand.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockTrapUp : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeTrapUp = new Color(255, 69, 69);

        public BlockTrapUp(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeTrapUp;
    }
}
