// ReSharper disable InconsistentNaming

namespace MovementControl.Patches
{
    using Behaviours;
    using HarmonyLib;
    using JetBrains.Annotations;
    using JumpKing.Player;

    [HarmonyPatch(typeof(JumpState), "DoJump")]
    public static class PatchJumpState
    {
        public static BehaviourForcedNeutral BehaviourForcedNeutral { get; set; }

        [UsedImplicitly]
        public static void Prefix(out float __state, JumpState __instance) => __state = __instance.body.Velocity.X;

        [UsedImplicitly]
        public static void Postfix(float __state, JumpState __instance)
        {
            if (BehaviourForcedNeutral == null || !BehaviourForcedNeutral.IsPlayerOnBlock)
            {
                return;
            }

            __instance.body.Velocity.X = __state;
        }
    }
}
