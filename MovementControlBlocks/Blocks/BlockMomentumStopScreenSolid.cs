namespace MomentumStopBlock.Blocks
{
    using JumpKing;
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockMomentumStopScreenSolid : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BlockcodeMomStopScreenSolid = new Color(111, 25, 102);

        public BlockMomentumStopScreenSolid(Rectangle collider) : base(collider) { }

        protected override bool canBlockPlayer => ModEntry.Data.Screen != Camera.CurrentScreen;

        public Color DebugColor => BlockcodeMomStopScreenSolid;
    }
}
