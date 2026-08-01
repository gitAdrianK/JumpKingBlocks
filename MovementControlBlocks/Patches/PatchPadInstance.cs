namespace MovementControl.Patches
{
    using Behaviours;
    using HarmonyLib;
    using JumpKing.Controller;

    [HarmonyPatch(typeof(PadInstance), nameof(PadInstance.GetState))]
    public static class PatchPadInstance
    {
        public static BehaviourInvertInput BehaviourInvertInput { get; set; }

        // ReSharper disable once InconsistentNaming
        public static void Postfix(ref PadState __result)
        {
            if (BehaviourInvertInput == null || !BehaviourInvertInput.IsPlayerOnBlock)
            {
                return;
            }

            var temp = __result.left;
            __result.left = __result.right;
            __result.right = temp;
        }
    }
}
