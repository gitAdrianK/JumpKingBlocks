namespace MuteJumpSfxBlock.Patches
{
    using HarmonyLib;
    using JumpKing.BodyCompBehaviours;
    using Behaviours;
    using JetBrains.Annotations;

    [HarmonyPatch(typeof(PlayBumpSFXBehaviour), nameof(PlayBumpSFXBehaviour.ExecuteBehaviour))]
    public static class PatchPlayBumpSfxBehaviour
    {
        [UsedImplicitly]
        public static bool Prefix() => !BehaviourMuteJumpSfx.IsOnBlock;
    }
}
