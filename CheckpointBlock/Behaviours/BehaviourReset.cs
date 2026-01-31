namespace CheckpointBlock.Behaviours
{
    using System.Linq;
    using Blocks;
    using Data;
    using JumpKing;
    using JumpKing.API;
    using JumpKing.BodyCompBehaviours;
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BehaviourReset : IBlockBehaviour
    {
        public BehaviourReset(ICollisionQuery collisionQuery, CheckpointSet[] sets, Point start)
        {
            // Using the given collisionQuery to check collision resulted in inaccuracies.
            this.CollisionQuery = collisionQuery;
            this.Sets = sets;
            this.Start = start;
        }

        private ICollisionQuery CollisionQuery { get; }
        private CheckpointSet[] Sets { get; }
        private Point Start { get; }
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

            var bodyComp = behaviourContext.BodyComp;
            var hitbox = bodyComp.GetHitbox();
            _ = this.CollisionQuery.CheckCollision(hitbox, out _, out AdvCollisionInfo info);
            var blocks = info.GetCollidedBlocks<BlockReset>();

            this.IsPlayerOnBlock = blocks.Count > 0;
            if (!this.IsPlayerOnBlock)
            {
                return true;
            }

            // Resetting to every block touched makes not sense, so we do first only.
            var block = blocks.First();
            if (!(block is BlockReset blockReset) ||
                (ModEntry.IgnoreStart && this.Start == this.Sets[blockReset.Id].Current))
            {
                return true;
            }

            bodyComp.Position.X = this.Sets[blockReset.Id].Current.X - (bodyComp.GetHitbox().Width / 2.0f);
            bodyComp.Position.Y = this.Sets[blockReset.Id].Current.Y - bodyComp.GetHitbox().Height;
            bodyComp.Velocity = Vector2.Zero;
            Camera.UpdateCamera(bodyComp.Position.ToPoint());

            return true;
        }
    }
}
