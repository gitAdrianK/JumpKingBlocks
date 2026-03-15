namespace AntiBlocks.Patches
{
    using System;
    using System.IO;
    using System.Reflection;
    using Behaviours;
    using HarmonyLib;
    using JumpKing;
    using JumpKing.GameManager;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    [HarmonyPatch(typeof(GameLoop), nameof(GameLoop.Draw))]
    public static class PatchGameLoop
    {
        private static Texture2D Texture { get; }

        static PatchGameLoop()
        {
            var texturePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new InvalidOperationException(),
                "AntiSnakeRingIcon");
            Texture = Game1.instance.contentManager.Load<Texture2D>(texturePath);
        }

        public static bool ShowAntiSnakeRingIcon { get; set; }

        public static void Postfix()
        {
            if (ShowAntiSnakeRingIcon && PatchInventoryManager.OriginalResult && BehaviourAntiSnake.IsOnBlock)
            {
                Game1.spriteBatch.Draw(
                    Texture,
                    new Vector2(Game1.WIDTH - Texture.Width - 4, Game1.HEIGHT - Texture.Height - 4),
                    new Rectangle(0, 0, Texture.Width, Texture.Height),
                    color: Color.White);
            }
        }
    }
}
