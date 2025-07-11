namespace CheckpointBlock.Entities
{
    using System.Diagnostics.CodeAnalysis;
    using EntityComponent;
    using JumpKing;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class EntityFlag : Entity
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Field attributes are experimental.")]
        private Point flagPosition;

        public EntityFlag(Texture2D texture, Point start)
        {
            this.Texture = texture;
            this.Start = start;
            this.FlagPosition = start;
        }

        private Texture2D Texture { get; }
        private Point Start { get; }

        public Point FlagPosition
        {
            get => this.flagPosition;
            set
            {
                this.flagPosition = value;
                // Assuming the position is only set by touching the block,
                // and as such the current screen in the active screen.
                this.CurrentScreen = Camera.CurrentScreen;
            }
        }

        private int CurrentScreen { get; set; }

        public override void Draw()
        {
            if (this.CurrentScreen != Camera.CurrentScreen
                || this.Start == this.FlagPosition)
            {
                return;
            }

            var currentPosition = this.FlagPosition;
            currentPosition.Y += Camera.CurrentScreen * 360;
            currentPosition += Camera.Offset.ToPoint();

            Game1.spriteBatch.Draw(
                this.Texture,
                new Vector2(
                    currentPosition.X - (this.Texture.Width / 2.0f),
                    currentPosition.Y - this.Texture.Height),
                new Rectangle(
                    0,
                    0,
                    this.Texture.Width,
                    this.Texture.Height),
                Color.White);
        }
    }
}
