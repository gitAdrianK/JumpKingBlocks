namespace MomentumStopBlock.Blocks
{
    using JumpKing;
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BlockMomentumStopScreenSolid : BoxBlock, IBlockDebugColor
    {
        public static readonly Color BLOCKCODE_MOM_STOP_SCREEN_SOLID = new Color(111, 25, 102);

        public BlockMomentumStopScreenSolid(Rectangle collider) : base(collider) { }

        public Color DebugColor => BLOCKCODE_MOM_STOP_SCREEN_SOLID;

        protected override bool canBlockPlayer => ModEntry.Data.Screen != Camera.CurrentScreen;
    }
}

