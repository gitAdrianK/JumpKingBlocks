namespace MuteJumpSfxBlock.Patches
{
    using HarmonyLib;
    using JumpKing.Player;
    using Behaviours;
    using JetBrains.Annotations;

    [HarmonyPatch(typeof(IsOnGround), "HandleSounds")]
    public static class PatchIsOnGround
    {
        [UsedImplicitly]
        public static bool Prefix() => !BehaviourMuteJumpSfx.IsOnBlock;
    }
}
