namespace MomentumStopBlock.Behaviours
{
    using System;
    using CheckpointBlock.Data;
    using JumpKing;
    using JumpKing.API;
    using JumpKing.BodyCompBehaviours;
    using JumpKing.Level;
    using MomentumStopBlock.Blocks;

    public class BehaviourMomentumStopScreen : IBlockBehaviour
    {
        public float BlockPriority => 2.0f;

        public bool IsPlayerOnBlock { get; set; }

        private DataMomentumStop Data { get; set; }

        public BehaviourMomentumStopScreen(DataMomentumStop data) => this.Data = data;

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
            this.IsPlayerOnBlock = advCollisionInfo.IsCollidingWith<BlockMomentumStopScreen>()
                || advCollisionInfo.IsCollidingWith<BlockMomentumStopScreenSolid>();

            if (this.Data.Screen != Camera.CurrentScreen)
            {
                if (this.IsPlayerOnBlock)
                {
                    var bodyComp = behaviourContext.BodyComp;
                    bodyComp.Velocity.X = 0;
                    bodyComp.Velocity.Y = Math.Max(0, bodyComp.Velocity.Y);
                    this.Data.Screen = Camera.CurrentScreen;
                }
                else
                {
                    this.Data.Screen = -1;
                }
            }

            return true;
        }
    }
}
