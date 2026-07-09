namespace MomentumStopBlock.Behaviours
{
    using System;
    using Blocks;
    using JumpKing.API;
    using JumpKing.BodyCompBehaviours;
    using JumpKing.Level;

    public class BehaviourMomentumStop : IBlockBehaviour
    {
        private bool hasStopped;
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
            this.IsPlayerOnBlock = advCollisionInfo.IsCollidingWith<BlockMomentumStop>()
                                   || advCollisionInfo.IsCollidingWith<BlockMomentumStopSolid>();

            if (!this.IsPlayerOnBlock)
            {
                this.hasStopped = false;
            }

            if (!this.IsPlayerOnBlock || this.hasStopped)
            {
                return true;
            }

            var bodyComp = behaviourContext.BodyComp;
            bodyComp.Velocity.X = 0;
            bodyComp.Velocity.Y = Math.Max(0, bodyComp.Velocity.Y);
            this.hasStopped = true;

            return true;
        }
    }
}
