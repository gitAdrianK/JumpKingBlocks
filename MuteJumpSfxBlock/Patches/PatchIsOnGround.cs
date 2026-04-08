namespace MuteJumpSfxBlock.Patches
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using Behaviours;
    using HarmonyLib;
    using JetBrains.Annotations;
    using JumpKing.Player;

    [HarmonyPatch(typeof(IsOnGround), "HandleSounds")]
    public static class PatchIsOnGround
    {
        [UsedImplicitly]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            var code = new List<CodeInstruction>(instructions);

            var insertionIndex = -1;
            var continueFound = false;
            var continueLabel =  il.DefineLabel();
            var isWearingSkin = AccessTools.Method(
                AccessTools.TypeByName("JumpKing.Player.Skins.SkinManager"),
                "IsWearingSkin");

            for (var i = 0; i < code.Count - 2; i++)
            {
                if (code[i].opcode != OpCodes.Ldc_I4_6
                    || code[i + 1].opcode != OpCodes.Call
                    || code[i + 1].operand as MethodInfo != isWearingSkin
                    || code[i + 2].opcode != OpCodes.Brfalse_S)
                {
                    continue;
                }

                insertionIndex = i;

                continueFound = true;
                code[i].labels.Add(continueLabel);

                break;
            }

            if (insertionIndex == -1 || !continueFound)
            {
                return code.AsEnumerable();
            }

            var insert = new List<CodeInstruction>
            {
                new CodeInstruction(
                    OpCodes.Call,
                    AccessTools.PropertyGetter(typeof(BehaviourMuteJumpSfx), nameof(BehaviourMuteJumpSfx.IsOnBlock))),
                new CodeInstruction(OpCodes.Brfalse_S, continueLabel),
                new CodeInstruction(OpCodes.Ret),
            };
            code.InsertRange(insertionIndex, insert);

            return code.AsEnumerable();
        }
    }
}
