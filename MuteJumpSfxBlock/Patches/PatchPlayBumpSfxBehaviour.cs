namespace MuteJumpSfxBlock.Patches
{
    using Behaviours;
    using HarmonyLib;
    using JetBrains.Annotations;
    using JumpKing.BodyCompBehaviours;

    [HarmonyPatch(typeof(PlayBumpSFXBehaviour), nameof(PlayBumpSFXBehaviour.ExecuteBehaviour))]
    public static class PatchPlayBumpSfxBehaviour
    {
        [UsedImplicitly]
        public static bool Prefix() => !BehaviourMuteJumpSfx.IsOnBlock;
    }
}
