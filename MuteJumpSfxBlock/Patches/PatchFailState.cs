namespace MuteJumpSfxBlock.Patches
{
    using HarmonyLib;
    using JumpKing.Player;
    using Behaviours;
    using JetBrains.Annotations;

    [HarmonyPatch(typeof(FailState), "HandleSounds")]
    public static class PatchFailState
    {
        [UsedImplicitly]
        public static bool Prefix() => !BehaviourMuteJumpSfx.IsOnBlock;
    }
}
