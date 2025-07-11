namespace AntiSnakeRingBlock.Patches
{
    using System.Diagnostics.CodeAnalysis;
    using Behaviours;
    using HarmonyLib;
    using JetBrains.Annotations;
    using JumpKing.MiscEntities.WorldItems;
    using JumpKing.MiscEntities.WorldItems.Inventory;

    [HarmonyPatch(typeof(InventoryManager), nameof(InventoryManager.HasItemEnabled))]
    public static class PatchInventoryManager
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Harmony naming convention")]
        [UsedImplicitly]
        public static void Postfix(Items pItem, ref bool result)
        {
            if (pItem == Items.SnakeRing && BehaviourAntiSnake.IsOnBlock)
            {
                result = false;
            }
        }
    }
}
