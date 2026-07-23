// ReSharper disable InconsistentNaming

namespace ForcedSlopeBlocks.Patches
{
    using System.Collections.Generic;
    using ErikMaths;
    using HarmonyLib;
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    [HarmonyPatch(typeof(SlopeBlock), MethodType.Constructor, typeof(Rectangle), typeof(SlopeType))]
    public static class PatchSlopeBlock
    {
        public static readonly List<SlopeBlock> BottomLeftSlopes = new List<SlopeBlock>();

        /// <summary>FieldRef of the <c>m_box</c> field of <see cref="SlopeBlock" />.</summary>
        private static readonly AccessTools.FieldRef<SlopeBlock, Rectangle> BoxRef =
            AccessTools.FieldRefAccess<SlopeBlock, Rectangle>("m_box");

        /// <summary>FieldRef of the <c>m_lines</c> field of <see cref="SlopeBlock" />.</summary>
        private static readonly AccessTools.FieldRef<SlopeBlock, Line[]> LinesRef =
            AccessTools.FieldRefAccess<SlopeBlock, Line[]>("m_lines");

        public static void Postfix(SlopeBlock __instance, SlopeType p_slope_type)
        {
            if (p_slope_type == SlopeType.BottomLeft)
            {
                BottomLeftSlopes.Add(__instance);
            }
        }

        public static void FixAllSlopes()
        {
            foreach (var bottomLeftSlope in BottomLeftSlopes)
            {
                FixSlopeHitbox(bottomLeftSlope);
            }
        }

        public static void FixSlopeHitbox(SlopeBlock slopeBlock)
        {
            BoxRef(slopeBlock).Deconstruct(out var x, out var y, out var width, out var height);
            var lines = new Line[3];
            var point = new Point(x, y);
            var point2 = new Point(x + width, y);
            var point3 = new Point(x + width, y + height);
            lines[0] = new Line { p0 = point, p1 = point2 };
            lines[1] = new Line { p0 = point2, p1 = point3 };
            lines[2] = new Line { p0 = point3, p1 = point };
            LinesRef(slopeBlock) = lines;
        }
    }
}
