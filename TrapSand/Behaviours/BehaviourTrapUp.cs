namespace TrapSand.Behaviours
{
    using System;
    using System.Linq;
    using Blocks;
    using JumpKing;
    using JumpKing.API;
    using JumpKing.BodyCompBehaviours;
    using JumpKing.Level;
    using Microsoft.Xna.Framework;

    public class BehaviourTrapUp : IBlockBehaviour
    {
        public BehaviourTrapUp(ICollisionQuery collisionQuery, bool isMuted)
        {
            this.CollisionQuery = collisionQuery;
            this.IsMuted = isMuted;
        }

        private ICollisionQuery CollisionQuery { get; }
        private bool IsMuted { get; }
        private bool HasPlayed { get; set; }
        private Rectangle PrevPosition { get; set; }

        public float BlockPriority => 2.0f;

        public bool IsPlayerOnBlock { get; set; }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext) => inputXVelocity;

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext) => inputYVelocity;

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext) => inputGravity;

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext) => false;

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            var bodyComp = behaviourContext.BodyComp;
            var playerPosition = bodyComp.GetHitbox();

            if (!info.IsCollidingWith<BlockTrapUp>() ||
                this.IsPlayerOnBlock ||
                info.GetCollidedBlocks<BlockTrapUp>()
                    .Select(block => block.GetRect())
                    .Any(blockRect => (blockRect.Top - playerPosition.Bottom < -5) &&
                                      (blockRect.Top - this.PrevPosition.Bottom < 0)))
            {
                this.PrevPosition = playerPosition;
                return false;
            }

            this.PrevPosition = playerPosition;
            return bodyComp.Velocity.Y >= 0.0f;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            var bodyComp = behaviourContext.BodyComp;
            var hitbox = bodyComp.GetHitbox();
            _ = this.CollisionQuery.CheckCollision(hitbox, out _, out AdvCollisionInfo info);
            this.IsPlayerOnBlock = info.IsCollidingWith<BlockTrapUp>();
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
            bodyComp.Velocity.Y = -2.0f;
            bodyComp.Velocity.Y = Math.Min(0.75f, bodyComp.Velocity.Y);
            bodyComp.Position.Y -= 2.5f;

            Camera.UpdateCamera(hitbox.Location);

            return true;
        }
    }
}
