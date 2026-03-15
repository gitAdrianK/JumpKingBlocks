namespace ForcedSlopeBlocks.Blocks
{
    using ErikMaths;
    using HarmonyLib;
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class SlopeBottomLeft : SlopeBlock
    {
        /// <summary>FieldRef of the <c>m_box</c> field of <see cref="SlopeBlock" />.</summary>
        private static readonly AccessTools.FieldRef<SlopeBlock, Rectangle> BoxRef =
            AccessTools.FieldRefAccess<SlopeBlock, Rectangle>("m_box");

        /// <summary>FieldRef of the <c>m_lines</c> field of <see cref="SlopeBlock" />.</summary>
        private static readonly AccessTools.FieldRef<SlopeBlock, Line[]> LinesRef =
            AccessTools.FieldRefAccess<SlopeBlock, Line[]>("m_lines");

        public SlopeBottomLeft(Rectangle position) : base(position, SlopeType.BottomLeft)
        {
            BoxRef(this).Deconstruct(out var x, out var y, out var width, out var height);
            var lines = new Line[3];
            var point = new Point(x, y);
            var point2 = new Point(x + width, y);
            var point3 = new Point(x + width, y + height);
            lines[0] = new Line { p0 = point, p1 = point2 };
            lines[1] = new Line { p0 = point2, p1 = point3 };
            lines[2] = new Line { p0 = point3, p1 = point };
            LinesRef(this) = lines;
        }
    }
}
