namespace MovementControl.Patches
{
    using Behaviours;
    using HarmonyLib;
    using JumpKing.Controller;

    [HarmonyPatch(typeof(PadInstance), nameof(PadInstance.GetState))]
    public class PatchPadInstance
    {
        // ReSharper disable once InconsistentNaming
        public static void Postfix(ref PadState __result)
        {
            if (!BehaviourInvertInput.IsOnBlock)
            {
                return;
            }

            var temp = __result.left;
            __result.left = __result.right;
            __result.right = temp;
        }
    }
}
