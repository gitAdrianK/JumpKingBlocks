// ReSharper disable InconsistentNaming

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
        public static void Postfix(Items p_item, ref bool __result)
        {
            if (p_item == Items.SnakeRing && BehaviourAntiSnake.IsOnBlock)
            {
                __result = false;
            }
        }
    }
}
