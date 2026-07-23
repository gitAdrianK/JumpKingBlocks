namespace AntiBlocks.Patches
{
    using Behaviours;
    using HarmonyLib;
    using JetBrains.Annotations;
    using JumpKing.MiscEntities.WorldItems;
    using JumpKing.MiscEntities.WorldItems.Inventory;

    [HarmonyPatch(typeof(InventoryManager), nameof(InventoryManager.HasItemEnabled))]
    public static class PatchInventoryManager
    {
        public static bool OriginalResult { get; private set; }

        [UsedImplicitly]
        // ReSharper disable InconsistentNaming
        public static void Postfix(Items p_item, ref bool __result)
        {
            OriginalResult = __result;
            if (p_item != Items.SnakeRing || !BehaviourAntiSnake.IsOnBlock)
            {
                return;
            }

            __result = false;
        }
    }
}
