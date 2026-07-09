namespace MomentumStopBlock.Behaviours
{
    using Blocks;
    using JumpKing.API;
    using JumpKing.BodyCompBehaviours;
    using JumpKing.Level;

    public class BehaviourSubpixelRound : IBlockBehaviour
    {
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
            this.IsPlayerOnBlock = advCollisionInfo.IsCollidingWith<BlockSubpixelRound>();

            var bodyComp = behaviourContext.BodyComp;
            if (!this.IsPlayerOnBlock || bodyComp.Velocity.X != 0)
            {
                return true;
            }

            bodyComp.Position.X = (int)(bodyComp.Position.X + 0.5f);

            return true;
        }
    }
}
