namespace TrapSand.Behaviours
{
    using System.Linq;
    using Blocks;
    using JumpKing;
    using JumpKing.API;
    using JumpKing.BodyCompBehaviours;
    using JumpKing.Level;

    public class BehaviourTrapDown : IBlockBehaviour
    {
        public BehaviourTrapDown(ICollisionQuery collisionQuery, bool isMuted)
        {
            this.CollisionQuery = collisionQuery;
            this.IsMuted = isMuted;
        }

        private ICollisionQuery CollisionQuery { get; }
        private bool IsMuted { get; }
        private bool HasPlayed { get; set; }

        public float BlockPriority => 0.5f;

        public bool IsPlayerOnBlock { get; set; }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext) => inputXVelocity;

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext) => inputYVelocity;

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext) => inputGravity;

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext) => false;

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (!info.IsCollidingWith<BlockTrapDown>() || this.IsPlayerOnBlock)
            {
                return false;
            }

            var bodyComp = behaviourContext.BodyComp;
            var playerPosition = bodyComp.GetHitbox();
            if (info.GetCollidedBlocks<BlockTrapDown>().Select(block => block.GetRect())
                .Any(blockRect => playerPosition.Top - blockRect.Bottom < -3))
            {
                return false;
            }

            return bodyComp.Velocity.Y < 0;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            var bodyComp = behaviourContext.BodyComp;
            var hitbox = bodyComp.GetHitbox();
            _ = this.CollisionQuery.CheckCollision(hitbox, out _, out AdvCollisionInfo info);
            this.IsPlayerOnBlock = info.IsCollidingWith<BlockTrapDown>();
            if (!this.IsPlayerOnBlock)
            {
                this.HasPlayed = false;
                return true;
            }

            if (!this.IsMuted && !this.HasPlayed)
            {
                Game1.instance?.contentManager?.audio?.player?.SandLand?.Play();
                this.HasPlayed = true;
            }

            bodyComp.Velocity.X *= 0.25f;
            bodyComp.Velocity.Y = 3.5f;

            Camera.UpdateCamera(hitbox.Location);

            return true;
        }
    }
}
