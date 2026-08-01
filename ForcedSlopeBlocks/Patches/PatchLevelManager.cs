namespace ForcedSlopeBlocks.Patches
{
    using HarmonyLib;
    using JumpKing;
    using JumpKing.Level;

    [HarmonyPatch(typeof(LevelManager), nameof(LevelManager.LoadScreens))]
    public static class PatchLevelManager
    {
        public static void Prefix()
        {
            PatchSlopeBlock.ShouldFixSlopes = false;
            var tags = Game1.instance.contentManager.level?.Info.Tags;
            if (tags == null)
            {
                return;
            }

            foreach (var tag in tags)
            {
                if (tag != "FixMySlopes")
                {
                    continue;
                }

                PatchSlopeBlock.ShouldFixSlopes = true;
                break;
            }
        }
    }
}
