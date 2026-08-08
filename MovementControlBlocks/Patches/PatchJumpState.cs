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

        public static BehaviourNoSlowdown BehaviourNoSlowdown { get; set; }

        [UsedImplicitly]
        public static void Prefix(out float __state, JumpState __instance) => __state = __instance.body.Velocity.X;

        [UsedImplicitly]
        public static void Postfix(float __state, JumpState __instance)
        {
            if (BehaviourForcedNeutral != null && BehaviourForcedNeutral.IsPlayerOnBlock)
            {
                __instance.body.Velocity.X = __state;
            }

            // ReSharper disable once InvertIf
            if (BehaviourNoSlowdown != null && BehaviourNoSlowdown.IsPlayerOnBlock)
            {
                if (__state < 0.0f && __instance.body.Velocity.X > __state)
                {
                    __instance.body.Velocity.X = __state;
                    return;
                }

                if (__state > 0.0f && __instance.body.Velocity.X < __state)
                {
                    __instance.body.Velocity.X = __state;
                }
            }
        }
    }
}
