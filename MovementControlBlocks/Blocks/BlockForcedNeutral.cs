namespace MovementControl.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockForcedNeutral : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeForcedNeutral = new Color(201, 202, 203);

        public BlockForcedNeutral(Rectangle collider) : base(collider)
        {
        }

        protected override bool canBlockPlayer => false;
        public Color DebugColor => BlockcodeForcedNeutral;
    }
}
