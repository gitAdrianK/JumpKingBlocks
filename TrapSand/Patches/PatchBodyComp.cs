namespace TrapSand.Patches
{
    using HarmonyLib;
    using JumpKing.Player;

    public static class PatchBodyComp
    {
        /// <summary>FieldRef of the <c>_knocked</c> field of <see cref="BodyComp" />.</summary>
        private static readonly AccessTools.FieldRef<BodyComp, bool> KnockedRef =
            AccessTools.FieldRefAccess<BodyComp, bool>("_knocked");

        /// <summary>
        ///     Sets the <c>_knocked</c> field of the players <see cref="BodyComp" />.
        /// </summary>
        /// <param name="bodyComp">The body comp to change the knocked value of.</param>
        /// <param name="isKnocked">New value to be assigned.</param>
        public static void SetKnocked(BodyComp bodyComp, bool isKnocked) => KnockedRef(bodyComp) = isKnocked;
    }
}
