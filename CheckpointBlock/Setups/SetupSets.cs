namespace CheckpointBlock.Setups
{
    using System;
    using System.IO;
    using System.Reflection;
    using Behaviours;
    using Blocks;
    using Data;
    using Entities;
    using Factories;
    using JumpKing;
    using JumpKing.Level;
    using JumpKing.Player;
    using JumpKing.SaveThread;
    using JumpKing.Workshop;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class SetupSets
    {
        private static Texture2D DefaultTexture { get; set; }
        private static EntityFlag[] EntityFlags { get; set; }
        private static DataCheckpoints Data { get; set; }

        public static void Setup(Level level, PlayerEntity player)
        {
            var contentManager = Game1.instance.contentManager;

            // This is the default/vanilla start position.
            var start = new Point(231, 302);
            var startData = level.Info.About.StartData?.Position;
            if (startData.HasValue)
            {
                start = startData.Value.ToPoint();
            }

            Data = SaveManager.instance.IsNewGame ? new DataCheckpoints() : DataCheckpoints.TryDeserialize();

            var defaultTexturePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new InvalidOperationException(),
                "checkpointDefault");
            DefaultTexture = contentManager.Load<Texture2D>(defaultTexturePath);
            EntityFlags = new EntityFlag[ModEntry.SetCount];

            _ = player.m_body.RegisterBlockBehaviour<BlockReset>(new BehaviourReset(LevelManager.Instance, Data.Sets,
                start));
            _ = player.m_body.RegisterBlockBehaviour<BlockCheckpoint>(new BehaviourCheckpoint(Data.Sets, EntityFlags));
            _ = player.m_body.RegisterBlockBehaviour<BlockCheckpointSingleUse>(
                new BehaviourCheckpointSingleUse(Data.Sets, EntityFlags));

            for (var i = 0; i < ModEntry.SetCount; i++)
            {
                if (FactoryCheckpoint.LastUsedMapIds[i] != level.ID)
                {
                    continue;
                }

                SetupSet(contentManager, level, start, i);
            }
        }

        public static void Cleanup()
        {
            Data.SaveToFile();
            Data = null;
        }

        private static void SetupSet(JKContentManager contentManager, Level level, Point start,
            int id)
        {
            if (Data.Sets[id] == null)
            {
                Data.Sets[id] = new CheckpointSet(start);
            }

            var customPath = Path.Combine(level.Root, $"checkpoint{(id == 0 ? "" : (id + 1).ToString())}");
            var checkpointTexture = File.Exists(customPath + ".xnb")
                ? contentManager.Load<Texture2D>(customPath)
                : DefaultTexture;
            EntityFlags[id] = new EntityFlag(checkpointTexture, start) { FlagPosition = Data.Sets[id].Current };
        }
    }
}
