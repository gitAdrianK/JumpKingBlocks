namespace MovementControl.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockSubpixelRound : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeSubpixelRound = new Color(111, 24, 105);

        public BlockSubpixelRound(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeSubpixelRound;
    }
}
