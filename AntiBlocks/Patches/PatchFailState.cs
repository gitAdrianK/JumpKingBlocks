// ReSharper disable InconsistentNaming

namespace AntiBlocks.Patches
{
    using BehaviorTree;
    using Behaviours;
    using HarmonyLib;
    using JumpKing.Player;

    [HarmonyPatch(typeof(FailState), "MyRun")]
    public static class PatchFailState
    {
        public static bool Prefix(FailState __instance, ref BTresult __result)
        {
            if (__instance.last_result == BTresult.Running || !BehaviourAntiSplat.IsOnBlock)
            {
                return true;
            }

            __result = BTresult.Failure;
            return false;
        }
    }
}
