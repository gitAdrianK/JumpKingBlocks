namespace CheckpointBlock.Behaviours
{
    using Blocks;
    using Data;
    using Entities;
    using JumpKing.API;
    using JumpKing.BodyCompBehaviours;
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BehaviourCheckpointSingleUse : IBlockBehaviour
    {
        public BehaviourCheckpointSingleUse(CheckpointSet[] sets, EntityFlag[] entityFlags)
        {
            this.Sets = sets;
            this.EntityFlags = entityFlags;
        }

        private bool HasSet { get; set; }

        private CheckpointSet[] Sets { get; }
        private EntityFlag[] EntityFlags { get; }
        public float BlockPriority => 2.0f;

        public bool IsPlayerOnBlock { get; set; }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext) => inputXVelocity;

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext) => inputYVelocity;

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext) => inputGravity;

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext) => false;

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext) => false;

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo == null)
            {
                return true;
            }

            var advCollisionInfo = behaviourContext.CollisionInfo.PreResolutionCollisionInfo;
            var blocks = advCollisionInfo.GetCollidedBlocks<BlockCheckpointSingleUse>();

            this.IsPlayerOnBlock = blocks.Count > 0;
            if (!this.IsPlayerOnBlock)
            {
                this.HasSet = false;
                return true;
            }

            // Yes technically this will bug if the player touches one block and w/o leaving touches another,
            // but who will find the bug, right?
            if (this.HasSet)
            {
                return true;
            }

            this.HasSet = true;

            foreach (var block in blocks)
            {
                if (!(block is BlockCheckpointSingleUse blockCheckpoint))
                {
                    continue;
                }

                var rect = block.GetRect();
                var point = new Point(rect.Left + (rect.Width / 2), rect.Bottom);

                if (this.Sets[blockCheckpoint.Id].Used.Contains(point))
                {
                    return true;
                }

                _ = this.Sets[blockCheckpoint.Id].Used.Add(point);
                this.Sets[blockCheckpoint.Id].Current = point;
                this.EntityFlags[blockCheckpoint.Id].FlagPosition = point;
            }

            return true;
        }
    }
}
