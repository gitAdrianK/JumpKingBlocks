namespace AntiBlocks.Patches
{
    using BehaviorTree;
    using Behaviours;
    using HarmonyLib;
    using JumpKing.Player;

    [HarmonyPatch(typeof(FailState), "MyRun")]
    public static class PatchFailState
    {
        // ReSharper disable once InconsistentNaming
        public static bool Prefix(ref BTresult __result)
        {
            if (!BehaviourAntiSplat.IsOnBlock)
            {
                return true;
            }

            __result = BTresult.Failure;
            return false;
        }
    }
}
