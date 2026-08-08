namespace MovementControl.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockNoSlowdown : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeNoSlowdown = new Color(201, 202, 204);

        public BlockNoSlowdown(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeNoSlowdown;
    }
}
