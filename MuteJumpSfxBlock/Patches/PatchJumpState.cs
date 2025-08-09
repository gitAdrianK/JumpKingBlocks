namespace MuteJumpSfxBlock.Patches
{
    using HarmonyLib;
    using JumpKing.Player;
    using Behaviours;
    using JetBrains.Annotations;

    [HarmonyPatch(typeof(JumpState), "HandleSounds")]
    public static class PatchJumpState
    {
        [UsedImplicitly]
        public static bool Prefix() => !BehaviourMuteJumpSfx.IsOnBlock;
    }
}
