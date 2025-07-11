namespace CheckpointBlock.Behaviours
{
    using System.Linq;
    using Blocks;
    using Data;
    using Entities;
    using JumpKing.API;
    using JumpKing.BodyCompBehaviours;
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BehaviourCheckpoint : IBlockBehaviour
    {
        public BehaviourCheckpoint(CheckpointSet set, EntityFlag entityFlag)
        {
            this.Set = set;
            this.EntityFlag = entityFlag;
        }

        private bool HasSet { get; set; }

        private CheckpointSet Set { get; }
        private EntityFlag EntityFlag { get; }
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
            // I could Harmony Traverse the AdvCollisionInfo typeBlockLookup and clean up the
            // double behaviours, I'll do it when it becomes needed.
            this.IsPlayerOnBlock = advCollisionInfo.IsCollidingWith<BlockCheckpoint>();

            if (!this.IsPlayerOnBlock)
            {
                this.HasSet = false;
                return true;
            }

            if (this.HasSet)
            {
                return true;
            }

            this.HasSet = true;

            var rect = advCollisionInfo.GetCollidedBlocks<BlockCheckpoint>().First().GetRect();
            var point = new Point(rect.Left + (rect.Width / 2), rect.Bottom);
            this.Set.Current = point;
            this.EntityFlag.FlagPosition = point;

            return true;
        }
    }
}
