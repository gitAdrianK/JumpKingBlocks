namespace AntiBlocks.Blocks
{
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockAntiSplat : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeAntiSplat = new Color(105, 111, 144);

        public BlockAntiSplat(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => false;

        public Color DebugColor => BlockcodeAntiSplat;
    }
}
